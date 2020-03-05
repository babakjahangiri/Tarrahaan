using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Tarrahaan.Models;
using Itanc.AspNetIdentity;
using Itanc.AspNetIdentity.AdoNet;

namespace Tarrahaan
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            //var cookieProvider = new CookieAuthenticationProvider();
            //var originalHandler = cookieProvider.OnApplyRedirect;

            //  RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            //Our logic to dynamically modify the path
            //cookieProvider.OnApplyRedirect = context =>
            //{
            //    var mvcContext = new HttpContextWrapper();

            //    var routeData = RouteTable.Routes.GetRouteData(mvcContext);

            //    //Get the current language  
            //    var routeValues = new RouteValueDictionary();
            //    if (routeData != null) routeValues.Add("lang", routeData.Values["lang"]);

            //    //Reuse the RetrunUrl
            //    var uri = new Uri(context.RedirectUri);
            //    var returnUrl = HttpUtility.ParseQueryString(uri.Query)[context.Options.ReturnUrlParameter];
            //    routeValues.Add(context.Options.ReturnUrlParameter, returnUrl);

            //    //Overwrite the redirection uri
            //    context.RedirectUri = url.Action("login", "account", routeValues);
            //    originalHandler.Invoke(context);

            //};


            ////   Enables the application to validate the security stamp when the user logs in.
            //// This is a security feature which is used when you change a password or add an external login to your account.  
            //cookieProvider.OnValidateIdentity = SecurityStampValidator
            //    .OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //        validateInterval: TimeSpan.FromMinutes(30),
            //        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager));


            // Configure the db context, user manager and signin manager to use a single instance per request
            var connectionString =  System.Configuration.ConfigurationManager.ConnectionStrings["IdentityConnectionString"].ConnectionString;
            app.CreatePerOwinContext(() => new SqlServerDatabase(connectionString));
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
 
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                // LoginPath = new PathString("/en/Account/Login"),
                LoginPath = new PathString(url.RouteUrl(new {controller="Account",action="Login", lang ="en"})),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))

                        

                }

            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}