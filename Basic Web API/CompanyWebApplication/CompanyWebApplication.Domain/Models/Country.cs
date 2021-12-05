using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompanyWebApplication.Domain.Models
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }
        public List<Contact> CountryContact { get; set; }
    }
}
