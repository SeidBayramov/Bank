using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Exceptions.Account
{
    public class UsedEmailException : Exception
    {
        public string ParamName { get; set; }
        public UsedEmailException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}