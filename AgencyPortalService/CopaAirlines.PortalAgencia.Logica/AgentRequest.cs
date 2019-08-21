using CopaAirlines.PortalAgencia.DA;
using CopaAirlines.PortalAgencia.DA.DbModels;
using CopaAirlines.PortalAgencia.DA.ViewModels;
using CopaAirlines.PortalAgencia.Interface;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Autofac;

namespace CopaAirlines.PortalAgencia.Logica
{
    public class AgentRequest : IAgentRequest
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Methods    
        public ResponseOperation Registrar(WaiverNameCorrectionViewModel agencia)
        {
            ResponseOperation response = new ResponseOperation();

            try
            {
                using (var db = new ModelDB_PortalAgencia())
                {                    
                    var correctionName = new NameCorrectionRequest()
                    {
                        PIN = "1",
                        Pais = agencia.CountryCode,
                        PNR = agencia.PNR,
                        IdiomaAtencion= agencia.ServiceLanguage,
                        DescripcionCorreccion = agencia.Description,
                        ContactPhoneNumber=agencia.ContactPhoneNumber,
                        AgenciaID = db.Agencies.Where(x => x.User.ToLower().Equals(agencia.User.ToLower())).Select(x => x.AgencyID).FirstOrDefault().ToString (),
                        FechaCreacion = DateTime.Now
                    };

                    db.NameCorrectionRequest.Add(correctionName);

                    response.Result = db.SaveChanges() > 0;
                    response.Object = correctionName.Id;
                    
                }
                
            }
            catch (Exception e)
            {
                log.Error(e);
                response.Message= "No se pudo guardar su solicitud, favor revise su request e intente de nuevo.";
                return response;
            }

            return response;
        }

        /// <summary>
        /// Método que se encarga de validar que los credenciales enviados por la agencia sean correctos y además existan
        /// </summary>
        /// <param name="agencia">Información enviada por la agencia</param>
        /// <returns>Objeto con resultado true si es una agencia valida o false si no es valido</returns>
        public ResponseOperation ValidarAgencia(WaiverNameCorrectionViewModel agencia)
        {
            ResponseOperation response = new ResponseOperation();

            try
            {
                using (var db = new ModelDB_PortalAgencia())
                {
                    var dataInBD = db.Agencies.Where(x => x.User.ToLower().Equals(agencia.User.ToLower()));
                    if(dataInBD != null && dataInBD.Count() > 0 /*&& dataInBD.FirstOrDefault().PIN.Equals(agencia.PIN)*/)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        ///Agencia no encontrada o invalida
                        response.Result = false;
                        response.Message = "Información de credenciales inválido, por favor revise sus credenciales e intente de nuevo.";
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                response.Result = false;
                response.Message = "Ocurrió un error al procesar su solicitud, favor intente de nuevo.";
            }
            return response;
        }

        /// <summary>
        /// Crea un registro de intento de acceso en la base de datos
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pin"></param>
        public int CreateAttemptRegistry(string email, string pin)
        {
            try
            {
                using (var db = new ModelDB_PortalAgencia())
                {
                    var attempt = new SecurityAttempt() { Pin = pin, Email = email, CreatedDate = DateTime.UtcNow};
                    db.SecurityAttempts.Add(attempt);
                    db.SaveChanges();
                    return attempt.Id;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                
                return 0;
            }
        }

        /// <summary>
        /// Actualiza un intento fallido de autenticación al servicio
        /// </summary>
        /// <param name="attemptID">ID del registro/param>
        /// <param name="maxAttemptAllowed">Cantidad máxima de intentos permitidos</param>
        /// <param name="pin">Pin</param>
        public void UpdateFailAttempt(int attemptID, int maxAttemptAllowed, string email)
        {
            try
            {
                using (var db = new ModelDB_PortalAgencia())
                {
                    var attempt = db.SecurityAttempts.Where(x => x.Id == attemptID).FirstOrDefault();
                    attempt.WasAttemptSuccessful = false;

                    var date = DateTime.UtcNow.AddHours(-1);
                    var countAttempts = db.SecurityAttempts.Where(x => x.Email == email && x.CreatedDate > date && x.WasAttemptSuccessful == false ).Count();

                    if (countAttempts >= maxAttemptAllowed-1)
                    {
                        attempt.IsAgencyBlocked = true;
                        attempt.BlockedExpirationDateTime = DateTime.UtcNow.AddHours(1);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// Método para revisar si los credenciales están bloqueados
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateBlockedCredentials(string email)
        {
            try
            {
                using (var db = new ModelDB_PortalAgencia())
                {
                    var attempt = db.SecurityAttempts.Where(x => x.Email == email && x.IsAgencyBlocked == true && x.BlockedExpirationDateTime > DateTime.UtcNow);
                    
                    return attempt.Count() > 0;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return true;
            }
        }

        /// <summary>
        /// Actualiza el registro con un intento exitoso
        /// </summary>
        /// <param name="attemptID"></param>
        public void UpdateSuccessAttempt(int attemptID)
        {
            try
            {
                using (var db = new ModelDB_PortalAgencia())
                {
                    var attempt = db.SecurityAttempts.Where(x => x.Id == attemptID).FirstOrDefault();
                    attempt.WasAttemptSuccessful = true;
                    db.SaveChanges();
                }
            }
            catch( Exception ex)
            {
                log.Error(ex);
            }
        }


        #endregion
    }
}
