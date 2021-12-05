using CompanyWebApplication.Dto.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyWebApplication.Mappers.Company
{
    public static class CompanyMapper
    {
        public static CompanyDto ToCompanyDto(this Domain.Models.Company company)
        {
            return new CompanyDto()
            {
                CompanyId = company.Id,
                CompanyName = company.CompanyName
            };
        }

        public static Domain.Models.Company ToCompany(this AddUpdateCompanyDto addCompanyDto)
        {
            return new Domain.Models.Company()
            {
                CompanyName = addCompanyDto.CompanyName
            };
        }
    }
}
