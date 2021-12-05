using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyWebApplication.Dto.Country;
using CompanyWebApplication.Services.Interfaces;
using CompanyWebApplication.Shared.CustomExceptions;

namespace CompanyWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryServices _countryServices;

        public CountryController(ICountryServices countryServices)
        {
            _countryServices = countryServices;
        }

        [HttpGet]
        public ActionResult<List<CountryDto>> Get()
        {
            try
            {
                return _countryServices.GetAllCountry();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<CountryDto> GetById(int id)
        {
            try
            {
                return _countryServices.GetCountryById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Country with id {id} was not found"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPost]
        public IActionResult AddCountry([FromBody] AddUpdateCountry addCountry)
        {
            try
            {
                _countryServices.AddCompany(addCountry);
                return StatusCode(StatusCodes.Status201Created, "Country created successfully");
            }
            catch (WebInputException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Wrong data for new country was sent!"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpPut]
        public IActionResult UpdateCompany([FromBody] AddUpdateCountry updateCountry)
        {
            try
            {
                _countryServices.UpdateCountry(updateCountry);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WebInputException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Wrong data for new country was sent!"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid id value!");
                }
                _countryServices.DeleteCountry(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
