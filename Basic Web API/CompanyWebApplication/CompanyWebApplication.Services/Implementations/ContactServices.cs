using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyWebApplication.DataAccess.Interfaces;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Dto.Contact;
using CompanyWebApplication.Mappers.Contact;
using CompanyWebApplication.Services.Interfaces;
using CompanyWebApplication.Shared.CustomExceptions;

namespace CompanyWebApplication.Services.Implementations
{
    public class ContactServices : IContactServices
    {
        private IContactRepository _contactRepository;
        private IRepository<Company> _companyRepository;
        private IRepository<Country> _countryRepository;

        public ContactServices(IContactRepository contactRepository, IRepository<Company> companyRepository, IRepository<Country> countryRepository)
        {
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
        }
        public List<ContactDto> GetAllContact()
        {
            List<Contact> contactDb = _contactRepository.Get();
            return contactDb.Select(x => x.ToContactDto()).ToList();
        }

        public ContactDto GetContactById(int id)
        {
            Contact contactDb = _contactRepository.GetById(id);
            if (contactDb == null)
            {
                //log
                throw new ResourceNotFoundException($"Contact with id {id} was not found");
            }
            return contactDb.ToContactDto();
        }

        public void AddContact(AddUpdateContactDto addContactDto)
        {
            ValidateInput(addContactDto);

            Contact newContact = addContactDto.ToContact();
            _contactRepository.Create(newContact);
        }

        public void UpdateContact(AddUpdateContactDto updateContactDto)
        {
            Contact contactDb = _contactRepository.GetById(updateContactDto.ContactId);
            if (contactDb == null)
            {
                throw new ResourceNotFoundException($"Contact with id {updateContactDto.ContactId} was not found");
            }
            ValidateInput(updateContactDto);
            contactDb.ContactName = updateContactDto.ContactName;

            _contactRepository.Update(contactDb);
        }

        public void DeleteContact(int id)
        {
            Contact contactDb = _contactRepository.GetById(id);
            if (contactDb == null)
            {
                throw new ResourceNotFoundException($"Contact with {id} was not found!");
            }
            _contactRepository.Delete(contactDb.Id);
        }

        public List<FilterDto> FilterByCompanyIdAndCountryId(int? countryId, int?  companyId)
        {
            if (countryId == null)
            {
                List<Contact> contactDb1 = _contactRepository.FilterContact(countryId, companyId);
                return contactDb1.Select(x => x.ToContactFilterCompany()).ToList();
            }

            if (companyId == null)
            {
                List<Contact> contactDb2 = _contactRepository.FilterContact(countryId, companyId);
                return contactDb2.Select(x => x.ToContactFilterCountry()).ToList();
            }

            List<Contact> contactDb = _contactRepository.FilterContact(countryId, companyId);
            return contactDb.Select(x => x.ToContactFilter()).ToList();



        }

        public List<GetContactsWithCompanyAndCountryDto> GetContactsWithCompanyAndCountry()
        {
            List<Contact> contactsDb = _contactRepository.GetContactsWithCompanyAndCountry();
            if (contactsDb == null)
            {
                throw new ResourceNotFoundException($"Contact was not found!");
            }

            return contactsDb.Select(x => x.ToGetContact()).ToList();
        }
        private void ValidateInput(AddUpdateContactDto AddUpdateContact)
        {
            if (string.IsNullOrEmpty(AddUpdateContact.ContactName))
            {
                throw new WebInputException("The contact name must not be empty!");
            }
            if (AddUpdateContact.ContactName.Length > 50)
            {
                throw new WebInputException("The contact name must not contain more than 50 characters!");
            }
        }
    }
}
