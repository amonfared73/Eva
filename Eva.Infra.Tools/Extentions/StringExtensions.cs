﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Infra.Tools.Extentions
{
    public static class StringExtensions
    {
        public static int ToInt(this string text)
        {
            return int.Parse(text);
        }
    }
}
