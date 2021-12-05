using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.Domain.Models;

namespace CompanyWebApplication.DataAccess.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        List<Contact> GetContactsWithCompanyAndCountry();
        List<Contact> FilterContact(int? countryId, int? companyId);
    }
}
