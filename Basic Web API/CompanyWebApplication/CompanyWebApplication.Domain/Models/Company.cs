using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompanyWebApplication.Domain.Models
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public List<Contact> CompanyContacts { get; set; }
    }
}
