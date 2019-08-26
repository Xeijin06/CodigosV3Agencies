using CopaAirlines.AgenciesService.DA.DBModels;
using CopaAirlines.AgenciesService.ViewModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.AgenciesService.Test.MockData
{
    public static class MockAgencies
    {
        /// <summary>
        /// Get empty Agency mocked
        /// </summary>
        /// <returns></returns>
        public static Mock<DbSet<Agencies>> MockAgencyEmpty()
        {
            var mockSet = new Mock<DbSet<Agencies>>();

            return mockSet;
        }


        public static List<Agencies> GetAgenciesDBList()
        {
            var list = new List<Agencies>()
            {
                new Agencies()
                {
                    AgencyID = 1,
                    IATACode = "89876543",
                    Name = "Agencia Atlantis",
                    Email = "contacto@hotmail.com",
                    PhoneNumber = "965497854",
                    CreationDate = DateTime.Now
                },
                new Agencies()
                {
                    AgencyID = 2,
                    IATACode = "5498121354",
                    Name = "Agencia Pacífico",
                    Email = "pacifico@hotmail.com",
                    PhoneNumber = "13235612",
                    CreationDate = DateTime.Now
                },
                new Agencies()
                {
                    AgencyID = 3,
                    IATACode = "12314123",
                    Name = "Agencia Portobello",
                    Email = "portobello@hotmail.com",
                    PhoneNumber = "123123",
                    CreationDate = DateTime.Now
                }
            };

            return list;
        }
    }
}
