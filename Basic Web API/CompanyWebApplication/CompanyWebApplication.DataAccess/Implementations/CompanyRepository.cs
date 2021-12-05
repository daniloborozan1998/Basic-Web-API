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
    public class CompanyRepository : IRepository<Company>
    {
        private WebAppDbContext _webAppDbContext;

        public CompanyRepository(WebAppDbContext webAppDbContext)
        {
            _webAppDbContext = webAppDbContext;
        }
        public List<Company> Get()
        {
            return _webAppDbContext.Companies.ToList();
        }

        public Company GetById(int id)
        {
            return _webAppDbContext
                .Companies
                .FirstOrDefault(x => x.Id == id);
        }

        public int Create(Company entity)
        {
            _webAppDbContext.Companies.Add(entity);
            return _webAppDbContext.SaveChanges();
        }

        public void Update(Company entity)
        {
            _webAppDbContext.Companies.Update(entity);//no call db
            _webAppDbContext.SaveChanges();//call db
        }

        public void Delete(int id)
        {
            Company companyDb = _webAppDbContext.Companies.FirstOrDefault(x => x.Id == id);
            if(companyDb == null)
            {
                throw new ResourceNotFoundException($"The company with id {id} was not found!");
            }

            _webAppDbContext.Companies.Remove(companyDb);//no call db
            _webAppDbContext.SaveChanges();//call db
        }
    }
}
