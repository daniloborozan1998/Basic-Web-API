using CompanyWebApplication.Dto.Company;
using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.Domain.Models;

namespace CompanyWebApplication.Services.Interfaces
{
    public interface ICompanyServices
    {
        List<CompanyDto> GetAllCompany();
        CompanyDto GetCompanyById(int id);
        void AddCompany(AddUpdateCompanyDto addCompanyDto);
        void UpdateCompany(AddUpdateCompanyDto updateCompanyDto);
        void DeleteCompany(int id);
    }
}
