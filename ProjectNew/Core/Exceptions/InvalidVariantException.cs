using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNew.Core.Exceptions
{
    public class InvalidVariantException : Exception
    {
        public InvalidVariantException(string message) : base(message) { }
    }
}
