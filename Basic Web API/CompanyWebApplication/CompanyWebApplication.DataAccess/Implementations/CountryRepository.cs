using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyWebApplication.DataAccess.Interfaces;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Shared.CustomExceptions;

namespace CompanyWebApplication.DataAccess.Implementations
{
    public class CountryRepository : IRepository<Country>
    {
        private WebAppDbContext _webAppDbContext;

        public CountryRepository(WebAppDbContext webAppDbContext)
        {
            _webAppDbContext = webAppDbContext;
        }
        public List<Country> Get()
        {
            return _webAppDbContext.Countries.ToList();
        }

        public Country GetById(int id)
        {
            return _webAppDbContext
                .Countries
                .FirstOrDefault(x => x.Id == id);
        }

        public int Create(Country entity)
        {
            _webAppDbContext.Countries.Add(entity);
            return _webAppDbContext.SaveChanges();
        }

        public void Update(Country entity)
        {
            _webAppDbContext.Countries.Update(entity);//no call db
            _webAppDbContext.SaveChanges();//call db
        }

        public void Delete(int id)
        {
            Country countryDb = _webAppDbContext.Countries.FirstOrDefault(x => x.Id == id);
            if (countryDb == null)
            {
                throw new ResourceNotFoundException($"The country with id {id} was not found!");
            }

            _webAppDbContext.Countries.Remove(countryDb);//no call db
            _webAppDbContext.SaveChanges();//call db
        }
    }
}
