using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.Dto.Contact;

namespace CompanyWebApplication.Services.Interfaces
{
    public interface IContactServices
    {
        List<ContactDto> GetAllContact();
        ContactDto GetContactById(int id);
        void AddContact(AddUpdateContactDto addContactDto);
        void UpdateContact(AddUpdateContactDto updateContactDto);
        void DeleteContact(int id);
        List<FilterDto> FilterByCompanyIdAndCountryId(int? companyId, int? countryId);
        List<GetContactsWithCompanyAndCountryDto> GetContactsWithCompanyAndCountry();
    }
}
