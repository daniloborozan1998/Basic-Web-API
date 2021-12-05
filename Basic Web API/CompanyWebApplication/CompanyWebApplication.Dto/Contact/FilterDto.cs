using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyWebApplication.Dto.Contact
{
    public class FilterDto
    {
        public int ContactId { get; set; }
        public int CountryId { get; set; }
        public int CompanyId { get; set; }
        public string ContactName { get; set; }
        public string CountryName { get; set; }
        public string CompanyName { get; set; }
    }
}
