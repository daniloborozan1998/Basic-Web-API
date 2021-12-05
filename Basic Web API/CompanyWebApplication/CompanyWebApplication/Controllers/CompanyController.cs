using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyWebApplication.Dto.Company;
using CompanyWebApplication.Services.Interfaces;
using CompanyWebApplication.Shared.CustomExceptions;

namespace CompanyWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyServices _companyServices;

        public CompanyController(ICompanyServices companyServices)
        {
            _companyServices = companyServices;
        }

        [HttpGet]
        public ActionResult<List<CompanyDto>> Get()
        {
            try
            {
                return _companyServices.GetAllCompany();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<CompanyDto> GetById(int id)
        {
            try
            {
                return _companyServices.GetCompanyById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Company with id {id} was not found"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPost]
        public IActionResult AddCompnay([FromBody] AddUpdateCompanyDto addCompany)
        {
            try
            {
                _companyServices.AddCompany(addCompany);
                return StatusCode(StatusCodes.Status201Created, "Company created successfully");
            }
            catch (WebInputException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Wrong data for new company was sent!"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpPut]
        public IActionResult UpdateCompany([FromBody] AddUpdateCompanyDto updateCompany)
        {
            try
            {
                _companyServices.UpdateCompany(updateCompany);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WebInputException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Wrong data for new company was sent!"));
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
                _companyServices.DeleteCompany(id);
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
