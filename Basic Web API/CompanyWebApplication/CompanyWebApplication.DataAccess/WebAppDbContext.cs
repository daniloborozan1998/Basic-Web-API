using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CompanyWebApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebApplication.DataAccess
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public  DbSet<Company> Companies { get; set; }
        public  DbSet<Contact> Contacts { get; set; }
        public  DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Company
            modelBuilder.Entity<Company>()
                .Property(x => x.CompanyName)
                .HasMaxLength(50) //nvarchar(50)
                .IsRequired(); //not null

            //Contact
            modelBuilder.Entity<Contact>()
                .Property(x => x.ContactName)
                .HasMaxLength(50)
                .IsRequired();
            //Country
            modelBuilder.Entity<Country>()
                .Property(x => x.CountryName)
                .HasMaxLength(50) //nvarchar(50)
                .IsRequired(); //not null

            //Relation

            //Company to Contact
            modelBuilder.Entity<Company>() //table
                .HasMany(x => x.CompanyContacts)//m side of 1 -m relation
                .WithOne(x => x.Company)//1 side of 1- m relation
                .HasForeignKey(x => x.CompanyId);
            //Country to Contact
            modelBuilder.Entity<Country>() //table
                .HasMany(x => x.CountryContact)//m side of 1 -m relation
                .WithOne(x => x.Country)//1 side of 1- m relation
                .HasForeignKey(x => x.CountryId); //FK

            modelBuilder.Entity<Contact>() //table
                .HasOne(x => x.Company)//m side of 1 -m relation
                .WithMany(x => x.CompanyContacts)//1 side of 1- m relation
                .HasForeignKey(x => x.CompanyId); //FK
            modelBuilder.Entity<Contact>() //table
                .HasOne(x => x.Country)//m side of 1 -m relation
                .WithMany(x => x.CountryContact)//1 side of 1- m relation
                .HasForeignKey(x => x.CountryId); //FK


            //Seed

            modelBuilder.Entity<Company>()
                .HasData(new Company
                {
                    Id = 1,
                    CompanyName = "Aspekt"
                });
            modelBuilder.Entity<Country>()
                .HasData(new Country
                {
                    Id = 1,
                    CountryName = "Macedonia"
                });
            modelBuilder.Entity<Contact>()
                .HasData(new Contact
                {
                    Id = 1,
                    ContactName = "Danilo",
                    CompanyId = 1,
                    CountryId = 1
                }, new Contact()
                {
                    Id = 2,
                    ContactName = "Danilo Borozan",
                    CompanyId = 2,
                    CountryId = 2
                });
        }
    }
}
