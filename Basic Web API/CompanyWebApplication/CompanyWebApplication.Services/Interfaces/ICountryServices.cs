using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.Dto.Country;

namespace CompanyWebApplication.Services.Interfaces
{
    public interface ICountryServices
    {
        List<CountryDto> GetAllCountry();
        CountryDto GetCountryById(int id);
        void AddCompany(AddUpdateCountry addCountryDto);
        void UpdateCountry(AddUpdateCountry updateCountryDto);
        void DeleteCountry(int id);
    }
}
