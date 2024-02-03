using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Exceptions.Common
{
    public class ObjectNullException : Exception
    {
        public string ParamName { get; set; }
        public ObjectNullException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}