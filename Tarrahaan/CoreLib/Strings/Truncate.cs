using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.CoreLib.Strings
{
    public class Truncate
    {
        ///<summary>
        ///shorten the text without cut the word from middle
        ///</summary>
        ///<remarks>
        ///Lorem ipsum dolor sit ame (14 limit) => Lorem ipsum
        ///</remarks>
        public static string TextTruncate(string input, int characterLimit ,string continueSign="")
        {
            //Check if the string is longer than the allowed amount otherwise do nothing
            string output = input;

            if (output.Length > characterLimit && characterLimit > 0)
            {
                // cut the string down to the maximum number of characters
                output = output.Substring(0, characterLimit);

                // Check if the character right after the truncate point was a space
                // if not, we are in the middle of a word and need to remove the rest of it

                if (input.Substring(output.Length, 1) != " ")
                {
                    var lastspace = output.LastIndexOf(" ", StringComparison.Ordinal);
                    // if we found a space then, cut back to that space
                    if (lastspace != -1)
                        output = output.Substring(0, lastspace);
                }
            }


            if (input.Length > characterLimit)
                return output + " " + continueSign;
                return output;
                
        }

        ///<summary>
        ///shorten the text from anywhere of the word.
        ///</summary>
        ///Lorem ipsum dolor sit ame (14 limit) => Lorem ipsum do
        public static string StrictTruncate(string input, int characterLimit)
        {
            var output = input;

            if (output.Length > characterLimit && characterLimit > 0)
                output = output.Substring(0, characterLimit);

            return output;

        }

    }
}