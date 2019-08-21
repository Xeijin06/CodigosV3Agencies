using CopaAirlines.PortalAgencia.DA.ViewModels;
using CopaAirlines.PortalAgencia.Interface;
using Newtonsoft.Json;
using Portaldeagencias.Manager;
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
    public class NameCorrectionController : ApiController
    {
        #region Private Variable Members
        private readonly IAgentRequest _agentRequest;
        private AgencyManager _agencyManager;
        private int _attemptID;
        #endregion

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Constructor - Destructor
        public NameCorrectionController(IAgentRequest _agentRequest)
        {
            this._agentRequest = _agentRequest;
        }

        #endregion

        #region Properties
        public AgencyManager AgencyManager
        {
            get
            {
                if (_agencyManager == null)
                {
                    _agencyManager = new AgencyManager();
                }
                return _agencyManager;
            }
            set
            {
                _agencyManager = value;
            }
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("NameCorrection")]
        [ActionName("Register")]
        public HttpResponseMessage Register()
        {
            try
            {
                string serverFolderPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AgenciaImages"]);
                var agenciaJson = HttpContext.Current.Request.Params["NameCorrectionRequestData"];
                var httpFile = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files["PassportImage"] : null;

                //Valida que el request no sea null y el archivo tampoco
                if (string.IsNullOrEmpty(agenciaJson) || httpFile == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ResponseOperation() { Message = "Request no detectado o incompleto, favor valide e intente de nuevo." });

                WaiverNameCorrectionViewModel agenciaVM = JsonConvert.DeserializeObject<WaiverNameCorrectionViewModel>(agenciaJson);
                //_attemptID = this._agentRequest.CreateAttemptRegistry(agenciaVM.UserEmail, agenciaVM.PIN);

                _attemptID = this._agentRequest.CreateAttemptRegistry(agenciaVM.User, "test");

                /*if (this._agentRequest.ValidateBlockedCredentials(agenciaVM.UserEmail))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ResponseOperation() { Message = "Ha excedido la cantidad de intentos permitidos. Debe esperar 1 hora para poder intentar nuevamente" });
                }*/


                //Validar que el RQ no contenga campos vacios
                string validationResult = AgencyManager.ValidateRequest(agenciaVM, httpFile);
                if (!string.IsNullOrEmpty(validationResult))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ResponseOperation() { Message = validationResult });

                //Validamos si el RQ contiene una agencia valida antes de generar el cambio

                var ValidacionAgencia = this._agentRequest.ValidarAgencia(agenciaVM);

                if (!ValidacionAgencia.Result)
                {
                    if (ValidacionAgencia.Message.StartsWith("Información de credenciales inválido"))
                    {
                        int maxAttempts = Convert.ToInt32(ConfigurationManager.AppSettings["MaxLoginAttempts"]);
                        this._agentRequest.UpdateFailAttempt(_attemptID, maxAttempts, agenciaVM.User);
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, ValidacionAgencia);
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ValidacionAgencia);
                }
                else
                {

                    // Si tiene agencia valida se procede con la insercción del registro
                    var Response = this._agentRequest.Registrar(agenciaVM);

                    if (Response.Result)
                    {
                        int registryID = Convert.ToInt32(Response.Object);
                        string fileRoute = String.Format("{0}{1}.{2}", serverFolderPath, registryID, httpFile.FileName.Split('.').Last());
                        httpFile.SaveAs(fileRoute);
                        
                        Response.Message = string.Format("Se registró la solicitud de forma correcta, su número de solicitud es la {0}.", Convert.ToInt32(Response.Object));
                        Response.Object = null;
                        httpFile.InputStream.Close();
                        //Enviamos correo antes de finalizar el proceso
                        this.AgencyManager.EnviarEmail(registryID, fileRoute);
                        //eliminar el archivo después de enviar el correo
                        httpFile.InputStream.Close();
                        httpFile.InputStream.Dispose();

                        File.Delete(fileRoute);
                        this._agentRequest.UpdateSuccessAttempt(_attemptID);
                        return Request.CreateResponse(HttpStatusCode.OK, Response);
                    }
                    else
                    {
                        Response.Object = null;
                        Response.Message = "No se pudo completar la solicitud, favor intente de nuevo";
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, Response);
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ResponseOperation() { Message = "Ocurrió un error al procesar su solicitud, favor intente de nuevo." });
            }
        }

        #endregion
    }
}
