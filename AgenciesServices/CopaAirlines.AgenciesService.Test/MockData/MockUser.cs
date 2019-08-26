using CopaAirlines.AgenciesService.DA.DBModels;
using CopaAirlines.AgenciesService.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.AgenciesService.Test.MockData
{
    public static class MockUser
    {

        public static List<AgencieUsers> GetListAgencyUserDBModel()
        {
            return new List<AgencieUsers>()
            {
                new AgencieUsers()
                {
                    UserID = 1,
                    Agency = new Agencies() { AgencyID = 1 },
                    Email = "lugonzalezr@copaair.com",
                    Password = "123456"
                },
                new AgencieUsers()
                {
                    UserID = 2,
                    Agency = new Agencies() { AgencyID = 1 },
                    Email = "mmendozar@copaair.com",
                    Password = "567352"
                },
                new AgencieUsers()
                {
                    UserID = 3,
                    Agency = new Agencies() { AgencyID = 2 },
                    Email = "etoribio@copaair.com",
                    Password = "89237612"
                },
                new AgencieUsers()
                {
                    UserID = 4,
                    Agency = new Agencies() { AgencyID = 2 },
                    Email = "arodriguez@copaair.com",
                    Password = "854420"
                },
                new AgencieUsers()
                {
                    UserID = 5,
                    Agency = new Agencies() { AgencyID = 1 },
                    Email = "valvarado@copaair.com",
                    Password = "7345289"
                }
            };
        }


        public static List<AgencyUserViewModel> ValidUsers()
        {
            return new List<AgencyUserViewModel>()
            {
                new AgencyUserViewModel()
                {
                    Email = "lugonzalezr@copaair.com",
                    Password = "123456"
                },
                new AgencyUserViewModel()
                {
                    Email = "mmendozar@copaair.com",
                    Password = "567352"
                },
                new AgencyUserViewModel()
                {
                    Email = "etoribio@copaair.com",
                    Password = "89237612"
                }
            };
        }


        public static List<AgencyUserViewModel> InvalidUsers()
        {
            return new List<AgencyUserViewModel>()
            {
                new AgencyUserViewModel()
                {
                    Email = "lugonzalezr@copaair.com",
                    Password = "1234567"
                },
                new AgencyUserViewModel()
                {
                    Email = "mmendozaar@copaair.com",
                    Password = "567352"
                },
                new AgencyUserViewModel()
                {
                    Email = "",
                    Password = "89237612"
                },
                new AgencyUserViewModel()
                {
                    Email = "mmendozaar@copaair.com",
                    Password = ""
                },
                new AgencyUserViewModel()
                {
                    Password = "89237612"
                },
                new AgencyUserViewModel()
                {
                    Email = "mmendozaar@copaair.com"
                },
            };
        }
    }
}
