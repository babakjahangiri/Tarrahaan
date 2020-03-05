using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.Mappers;

namespace Tarrahaan.CoreLib.Strings
{
    public class StringConvert
    {
        public static long ToMoney(string input)
        {
            input =  input.Replace(",", "");

            return Convert.ToInt64(input);

        }

    }
}