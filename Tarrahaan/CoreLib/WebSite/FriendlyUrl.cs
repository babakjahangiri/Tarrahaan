using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Tarrahaan.CoreLib.WebSite
{
    public static class FriendlyUrl
    {
        public static string Make(string input)
        {
            var outputString = input.Trim();
            outputString = outputString.Replace(" ", "_");
            outputString = outputString.Replace("-", "_");

            return outputString;
        }
    }
}