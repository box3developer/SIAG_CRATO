﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.Utils
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException(string? message) : base(message)
        {
        }
    }
}
}
