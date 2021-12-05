using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompanyWebApplication.Domain.Models
{
    public class Contact : BaseEntity
    {
        public string ContactName { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
