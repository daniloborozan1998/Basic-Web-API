using System;
using System.Collections.Generic;
using System.Text;
using CompanyWebApplication.DataAccess;
using CompanyWebApplication.DataAccess.Implementations;
using CompanyWebApplication.DataAccess.Interfaces;
using CompanyWebApplication.Domain.Models;
using CompanyWebApplication.Services.Implementations;
using CompanyWebApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyWebApplication.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebAppDbContext>(x =>
                x.UseSqlServer(connectionString));
        }
        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Company>, CompanyRepository>(); //DI
            services.AddTransient<IRepository<Country>, CountryRepository>(); //DI
            services.AddTransient<IContactRepository, ContactRepository>(); //DI
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<ICompanyServices, CompanyServices>(); //DI
            services.AddTransient<ICountryServices, CountryServices>(); //DI
            services.AddTransient<IContactServices, ContactServices>(); //DI
            
        }
    }
}
