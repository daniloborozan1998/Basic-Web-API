using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.DataAccess.Interfaces;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Services.Implementations;
using CompanyWebApplication.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace CompanyWebApplication.Tests
{
    [TestClass]
    public class CompanyServiceTests
    {
        [TestMethod]
        public void GetAllCompany_should_return_valid_num_of_records()
        {
            List<Company> company = new List<Company>()
            {
                new Company()
                {
                    Id = 4,
                    CompanyName = "Apple"
                }
            };

            var companyMockRepository = new Mock<IRepository<Company>>();
            companyMockRepository.Setup(x => x.Get()).Returns(company);

            ICompanyServices companyServices = new CompanyServices(companyMockRepository.Object);
            var result = companyServices.GetAllCompany();
            Assert.AreEqual(1,result.Count);
        }

    }
}
