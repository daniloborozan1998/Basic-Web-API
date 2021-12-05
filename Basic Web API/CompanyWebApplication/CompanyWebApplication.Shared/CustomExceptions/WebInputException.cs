using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyWebApplication.Shared.CustomExceptions
{
    public class WebInputException : Exception
    {
        public WebInputException(string message) : base(message)
        {

        }
    }
}
