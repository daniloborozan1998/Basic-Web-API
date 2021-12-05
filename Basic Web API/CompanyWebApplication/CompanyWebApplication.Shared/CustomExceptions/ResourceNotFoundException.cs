using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyWebApplication.Shared.CustomExceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {

        }
    }
}
