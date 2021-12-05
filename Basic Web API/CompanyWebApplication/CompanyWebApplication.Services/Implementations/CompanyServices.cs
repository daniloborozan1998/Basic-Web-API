using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyWebApplication.DataAccess.Interfaces;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Dto.Company;
using CompanyWebApplication.Mappers.Company;
using CompanyWebApplication.Services.Interfaces;
using CompanyWebApplication.Shared.CustomExceptions;

namespace CompanyWebApplication.Services.Implementations
{
    public class CompanyServices : ICompanyServices
    {
        private IRepository<Company> _companyRepository;

        public CompanyServices(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public List<CompanyDto> GetAllCompany()
        {
            List<Company> companyDb = _companyRepository.Get();
            return companyDb.Select(x => x.ToCompanyDto()).ToList();
        }

        public CompanyDto GetCompanyById(int id)
        {
            Company companyDb = _companyRepository.GetById(id);
            if (companyDb == null)
            {
                //log
                throw new ResourceNotFoundException($"Company with id {id} was not found");
            }
            return companyDb.ToCompanyDto();
        }

        public void AddCompany(AddUpdateCompanyDto addCompanyDto)
        {
            ValidateInput(addCompanyDto);

            Company newCompany = addCompanyDto.ToCompany();
            _companyRepository.Create(newCompany);
            
        }

        public void UpdateCompany(AddUpdateCompanyDto updateCompanyDto)
        {
            Company companyDb = _companyRepository.GetById(updateCompanyDto.CompanyId);
            if (companyDb == null)
            {
                throw new ResourceNotFoundException($"Company with id {updateCompanyDto.CompanyId} was not found");
            }
            ValidateInput(updateCompanyDto);
            companyDb.CompanyName = updateCompanyDto.CompanyName;
            
            _companyRepository.Update(companyDb);
            
            
        }

        public void DeleteCompany(int id)
        {
            Company companyDb = _companyRepository.GetById(id);
            if (companyDb == null)
            {
                throw new ResourceNotFoundException($"Company with {id} was not found!");
            }
            _companyRepository.Delete(companyDb.Id);
        }

        private void ValidateInput(AddUpdateCompanyDto addCompanyDto)
        {
            if (string.IsNullOrEmpty(addCompanyDto.CompanyName))
            {
                throw new WebInputException("The company name must not be empty!");
            }
            if (addCompanyDto.CompanyName.Length > 50)
            {
                throw new WebInputException("The company name must not contain more than 50 characters!");
            }
        }
    }
}
