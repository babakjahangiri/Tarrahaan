using System;
using System.Configuration;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Itanc.AspNetIdentity;
using Itanc.AspNetIdentity.AdoNet;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Tarrahaan.Models;
using System.Net.Mail;
 

namespace Tarrahaan
{
   

    public class EmailService : IIdentityMessageService
    {
      
        public async Task SendAsync(IdentityMessage message)
        {

           var credential = new System.Net.Configuration.SmtpSection().Network;
            var email =
                new MailMessage(new MailAddress(credential.UserName, "(do not reply)"),
                    new MailAddress(message.Destination))
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };
       
            using (var smtp = new SmtpClient())
            {
                // var smtpnetworksettings = new System.Net.Configuration.SmtpSection().Network;
                //var credential = new NetworkCredential

                await smtp.SendMailAsync(email);

                //try
                //{
                  
                //}
                //catch (Exception ex)
                //{
                    
                //    throw new Exception(ex.Message);
                //}
          

            }
            
          //  return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            //context.Get<ApplicationDbContext>()
            var connectionString = ConfigurationManager.ConnectionStrings["IdentityConnectionString"].ConnectionString;
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new SqlServerDatabase(connectionString)));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false,
               
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 10;
            
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Tarahan"));
            }
            return manager;
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["IdentityConnectionString"].ConnectionString;
             return new ApplicationRoleManager(new RoleStore<IdentityRole>(new SqlServerDatabase(connectionString)));
        }
       
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
