using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyWebApplication.DataAccess.Interfaces;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Dto.Country;
using CompanyWebApplication.Mappers.Country;
using CompanyWebApplication.Services.Interfaces;
using CompanyWebApplication.Shared.CustomExceptions;

namespace CompanyWebApplication.Services.Implementations
{
    public class CountryServices : ICountryServices
    {
        private IRepository<Country> _countryRepository;

        public CountryServices(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public List<CountryDto> GetAllCountry()
        {
            List<Country> countryDb = _countryRepository.Get();
            return countryDb.Select(x => x.ToCountryDto()).ToList();
        }

        public CountryDto GetCountryById(int id)
        {
            Country countryDb = _countryRepository.GetById(id);
            if (countryDb == null)
            {
                //log
                throw new ResourceNotFoundException($"Country with id {id} was not found");
            }
            return countryDb.ToCountryDto();
        }

        public void AddCompany(AddUpdateCountry addCountryDto)
        {
            ValidateInput(addCountryDto);

            Country newCountry = addCountryDto.ToCountry();
            _countryRepository.Create(newCountry);
        }

        public void UpdateCountry(AddUpdateCountry updateCountryDto)
        {
            Country countryDb = _countryRepository.GetById(updateCountryDto.CountryId);
            if (countryDb == null)
            {
                throw new ResourceNotFoundException($"Country with id {updateCountryDto.CountryId} was not found");
            }
            ValidateInput(updateCountryDto);
            countryDb.CountryName = updateCountryDto.CountryName;

            _countryRepository.Update(countryDb);
        }

        public void DeleteCountry(int id)
        {
            Country countryDB = _countryRepository.GetById(id);
            if (countryDB == null)
            {
                throw new ResourceNotFoundException($"Country with {id} was not found!");
            }
            _countryRepository.Delete(countryDB.Id);
        }


        private void ValidateInput(AddUpdateCountry AddUpdateCountry)
        {
            if (string.IsNullOrEmpty(AddUpdateCountry.CountryName))
            {
                throw new WebInputException("The country name must not be empty!");
            }
            if (AddUpdateCountry.CountryName.Length > 50)
            {
                throw new WebInputException("The country name must not contain more than 50 characters!");
            }
        }
    }
}
