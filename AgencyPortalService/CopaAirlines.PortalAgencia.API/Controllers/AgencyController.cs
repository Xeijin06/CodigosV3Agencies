using CopaAirlines.PortalAgencia.DA.ViewModels;
using CopaAirlines.PortalAgencia.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Portaldeagencias.Controllers
{
    public class AgencyController : ApiController
    {
        private readonly IAgentRequest _agentRequest;

        public AgencyController(IAgentRequest _agentRequest)
        {
            this._agentRequest = _agentRequest;
        }

        [HttpGet]
        [Route("api/Agency/ValidateEmail")]
        [ActionName("ValidateEmail")]
        public ResponseOperation ValidateEmail(string email,string pin)
        {
            /*TODO, CREAR TABLA DE AGENCIAS Y VALIDAR QUE EL EMAIL DE LA AGENCIA Y EL PIN COINCIDAN.*/
            ResponseOperation oResponse = new ResponseOperation();
            oResponse.Result = true;
            oResponse.Message = "Credenciales Válidas.";
            return oResponse;
        }
    }
}
