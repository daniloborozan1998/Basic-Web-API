using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.Dto.Contact;
using CompanyWebApplication.Dto.Country;


namespace CompanyWebApplication.Mappers.Contact
{
    public static class ContactMapper
    {
        public static ContactDto ToContactDto(this Domain.Models.Contact contact)
        {
            return new ContactDto()
            {
                ContactId = contact.Id,
                CompanyId = contact.CompanyId,
                CountryId = contact.CountryId,
                ContactName = contact.ContactName
            };
        }

        public static Domain.Models.Contact ToContact(this AddUpdateContactDto addContactDto)
        {
            return new Domain.Models.Contact()
            {
                Id = addContactDto.ContactId,
                CompanyId = addContactDto.CompanyId,
                CountryId = addContactDto.CountryId,
                ContactName = addContactDto.ContactName
            };
        }
        public static GetContactsWithCompanyAndCountryDto ToGetContact(this Domain.Models.Contact getContactWithCompanyAndCountry)
        {
            return new GetContactsWithCompanyAndCountryDto()
            {
                ContactName = getContactWithCompanyAndCountry.ContactName,
                CompanyId = getContactWithCompanyAndCountry.CompanyId,
                CountryId = getContactWithCompanyAndCountry.CountryId,
                CompanyName = $"{getContactWithCompanyAndCountry.Company.CompanyName}",
                CountryName = $"{getContactWithCompanyAndCountry.Country.CountryName}",
                ContactId = getContactWithCompanyAndCountry.Id
            };
        }
        public static FilterDto ToContactFilter(this Domain.Models.Contact filter)
        {
            return new FilterDto()
            {
                ContactName = filter.ContactName,
                CompanyId = filter.CompanyId,
                CountryId = filter.CountryId,
                ContactId = filter.Id,
                CompanyName = $"{filter.Company.CompanyName}",
                CountryName = $"{filter.Country.CountryName}"
            };
        }
        public static FilterDto ToContactFilterCompany(this Domain.Models.Contact filter)
        {
            return new FilterDto()
            {
                ContactName = filter.ContactName,
                CompanyId = filter.CompanyId,
                ContactId = filter.Id,
                CompanyName = $"{filter.Company.CompanyName}"
                
            };
        }
        public static FilterDto ToContactFilterCountry(this Domain.Models.Contact filter)
        {
            return new FilterDto()
            {
                ContactName = filter.ContactName,
                CountryId = filter.CountryId,
                ContactId = filter.Id,
                CountryName = $"{filter.Country.CountryName}"
            };
        }

    }
}
