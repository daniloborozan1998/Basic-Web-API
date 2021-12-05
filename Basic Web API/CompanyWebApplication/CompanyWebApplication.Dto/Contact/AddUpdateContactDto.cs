using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyWebApplication.Dto.Contact
{
    public class AddUpdateContactDto
    {
        public int ContactId { get; set; }
        public int CountryId { get; set; }
        public int CompanyId { get; set; }
        public string ContactName { get; set; }
    }
}
