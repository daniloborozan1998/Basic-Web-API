using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.Dto.Company;
using CompanyWebApplication.Dto.Country;

namespace CompanyWebApplication.Mappers.Country
{
    public static class CountryMapper
    {
        public static CountryDto ToCountryDto(this Domain.Models.Country country)
        {
            return new CountryDto()
            {
                CountryId = country.Id,
                CountryName = country.CountryName
            };
        }

        public static Domain.Models.Country ToCountry(this AddUpdateCountry addCountryDto)
        {
            return new Domain.Models.Country()
            {
                CountryName = addCountryDto.CountryName
            };
        }
    }
}
