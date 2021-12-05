using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyWebApplication.DataAccess.Interfaces;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Shared.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebApplication.DataAccess.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private WebAppDbContext _webAppDbContext;

        public ContactRepository(WebAppDbContext webAppDbContext)
        {
            _webAppDbContext = webAppDbContext;
        }
        public List<Contact> Get()
        {
            return _webAppDbContext.Contacts.ToList();
        }

        public Contact GetById(int id)
        {
            return _webAppDbContext
                .Contacts
                .FirstOrDefault(x => x.Id == id);
        }

        public int Create(Contact entity)
        {

            _webAppDbContext.Contacts.Add(entity); //no db call
            return _webAppDbContext.SaveChanges(); //db call
        }

        public void Update(Contact entity)
        {
            _webAppDbContext.Contacts.Update(entity); //no db call
            _webAppDbContext.SaveChanges();// db call
        }

        public void Delete(int id)
        {
            Contact orderDb = _webAppDbContext.Contacts.FirstOrDefault(x => x.Id == id);
            if (orderDb == null)
            {
                throw new ResourceNotFoundException($"The contact with id {id} was not found!");
            }

            _webAppDbContext.Contacts.Remove(orderDb); //no call db
            _webAppDbContext.SaveChanges(); //call db
        }

        public List<Contact> GetContactsWithCompanyAndCountry()
        {
            return _webAppDbContext.Contacts.Include(x => x.Company).Include(x => x.Country).ToList();
        }


        public List<Contact> FilterContact(int? countryId, int? companyId)
        {
            if (countryId == null)
            {
                return _webAppDbContext.Contacts.Include(x => x.Company).Where(x=> x.CompanyId == companyId).ToList();
            }

            if (companyId == null)
            {
                return _webAppDbContext.Contacts.Include(x => x.Country).Where(x => x.CountryId == countryId).ToList();
            }

            return _webAppDbContext.Contacts.Include(x => x.Country).Include(x => x.Company).Where(x=>x.CountryId == countryId && x.CompanyId == companyId).ToList();
        }
    }
}
