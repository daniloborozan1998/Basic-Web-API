using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Dto.Contact;
using CompanyWebApplication.Services.Interfaces;
using CompanyWebApplication.Shared.CustomExceptions;

namespace CompanyWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private IContactServices _contactServices;

        public ContactController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        [HttpGet]
        public ActionResult<List<ContactDto>> Get()
        {
            try
            {
                return _contactServices.GetAllContact();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<ContactDto> GetById(int id)
        {
            try
            {
                return _contactServices.GetContactById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Contact with id {id} was not found"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] AddUpdateContactDto addContact)
        {
            try
            {
                _contactServices.AddContact(addContact);
                return StatusCode(StatusCodes.Status201Created, "Contact created successfully");
            }
            catch (WebInputException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Wrong data for new contact was sent!"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpPut]
        public IActionResult UpdateContact([FromBody] AddUpdateContactDto updateContact)
        {
            try
            {
                _contactServices.UpdateContact(updateContact);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WebInputException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Wrong data for new contact was sent!"));
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
                _contactServices.DeleteContact(id);
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
        [HttpGet("getContacts")]
        public ActionResult<List<GetContactsWithCompanyAndCountryDto>> GetContactsWithCompanyAndCountry()
        {
            try
            {
                return _contactServices.GetContactsWithCompanyAndCountry();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpGet("filter")] // api/contact/filter?countryId=2  or /api/contact/filter?companyId=2 or /api/contact/filter?countryId=2&company=2
        public ActionResult<List<FilterDto>> GetByCompanyIdAndContactId(int? countryId , int? companyId)
        {
            try
            {
                return _contactServices.FilterByCompanyIdAndCountryId(countryId, companyId);
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, ($"Contact with companyId {companyId} and {countryId} was not found"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
