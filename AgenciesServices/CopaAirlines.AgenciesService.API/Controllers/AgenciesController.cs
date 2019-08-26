using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaAirlines.AgenciesService.Interface;
using CopaAirlines.AgenciesService.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CopaAirlines.AgenciesService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgenciesController : Controller
    {
        IAgency _agency;
        IUser _user;

        public AgenciesController(IAgency agency, IUser user)
        {
            this._agency = agency;
            this._user = user;
        }
        
        [HttpGet("GetAgencyFromUserID/{id}")]
        public OperationResult GetAgencyFromUserID(int id)
        {
            OperationResult operationResult;

            operationResult = this._agency.GetAgencyByUserId(id);

            return operationResult;
        }

        [HttpGet("GetAgencyFromUserEmail")]
        public OperationResult GetAgencyFromEmail(string email)
        {
            OperationResult operationResult;

            operationResult = this._agency.GetAgencyByUserEmail(email);

            return operationResult;
        }

        [HttpPost]
        [Route("Users/ValidateUser")]
        public OperationResult ValidateUser(AgencyUserViewModel user)
        {
            OperationResult operationResult;

            operationResult = this._user.ValidateUser(user);

            return operationResult;
        }
    }
}