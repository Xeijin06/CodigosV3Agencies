using CopaAirlines.PortalAgencia.DA;
using CopaAirlines.PortalAgencia.DA.ViewModels;
using Portaldeagencias.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Portaldeagencias.Manager
{
    public class AgencyManager
    {

        /// <summary>
        /// Prepara la información para enviar el correo electrónico de notificación
        /// </summary>
        /// <param name="registryID">ID del registro ingresado</param>
        public void EnviarEmail(int registryID, string fileRoute)
        {
            using (var db = new ModelDB_PortalAgencia())
            {
                var nameCorrectionRegistry = db.NameCorrectionRequest.Where(x => x.Id == registryID).FirstOrDefault();
                int agencyID = Convert.ToInt32(nameCorrectionRegistry.AgenciaID);
                var agencyRegistry = db.Agencies.Where(x => x.AgencyID == agencyID).FirstOrDefault();

                EmailSender.SendMessageNotification(fileRoute, nameCorrectionRegistry.Id.ToString(), agencyRegistry.Name, agencyRegistry.IATACode, agencyRegistry.PhoneNumber, agencyRegistry.Email, nameCorrectionRegistry.Pais, nameCorrectionRegistry.IdiomaAtencion, nameCorrectionRegistry.DescripcionCorreccion,nameCorrectionRegistry.ContactPhoneNumber,nameCorrectionRegistry.PNR);
            }
        }

        /// <summary>
        /// Método para validar que el RQ contenga la información completa y además con la cantidad de campos y datos validos.
        /// </summary>
        /// <param name="agencyRQ"></param>
        /// <returns></returns>
        public string ValidateRequest(WaiverNameCorrectionViewModel agencyRQ, HttpPostedFile fileAttachment)
        {
            try
            {
                if (string.IsNullOrEmpty(agencyRQ.UserEmail))
                    return "Request invalido. El campo Correo Electrónico debe contener un valor.";
                else if (agencyRQ.UserEmail.Length > 100)
                    return "Request invalido. El campo Correo Electrónico excedió la cantidad de caracteres permitido.";

                //if (string.IsNullOrEmpty(agencyRQ.PIN))
                //    return "Request invalido. El campo PIN debe contener un valor.";
                //else if (agencyRQ.PIN .Length > 20)
                //    return "Request invalido. El campo PIN excedió la cantidad de caracteres permitido.";

                if (string.IsNullOrEmpty(agencyRQ.Description))
                    return "Request invalido. El campo Descripción debe contener un valor.";
                else if (agencyRQ.Description.Length > 1000)
                    return "Request invalido. El campo Descripción excedió la cantidad de caracteres permitido.";

                if (string.IsNullOrEmpty(agencyRQ.CountryCode))
                    return "Request invalido. El campo País debe contener un valor.";
                else if (agencyRQ.CountryCode.Length > 2)
                    return "Request invalido. El campo País excedió la cantidad de caracteres permitido.";

                if (string.IsNullOrEmpty(agencyRQ.PNR))
                    return "Request invalido. El campo PNR debe contener un valor.";
                else if (agencyRQ.PNR.Length > 6)
                    return "Request invalido. El campo PNR excedió la cantidad de caracteres permitido.";
                if (string.IsNullOrEmpty(agencyRQ.ContactPhoneNumber))
                    return "Request invalido. El campo Teléfono de Contacto debe contener un valor.";
                else if (agencyRQ.ContactPhoneNumber.Length > 20)
                    return "Request invalido. El campo Teléfono de Contacto excedió la cantidad de caracteres permitido.";
                if (string.IsNullOrEmpty(agencyRQ.ServiceLanguage))
                    return "Request invalido. El campo Idioma de Atención debe contener un valor.";
                else if (agencyRQ.ServiceLanguage.Length > 2)
                    return "Request invalido. El campo Idioma de Atención excedió la cantidad de caracteres permitido. Ej. ES, PT, EN";

                ///Validación de archivo

                int fileSize = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("AttachmentFileSize"))? 5000000 : Convert.ToInt32(ConfigurationManager.AppSettings.Get("AttachmentFileSize"));
                List<string> fileContentFormat = ConfigurationManager.AppSettings.Get("AttachmentFileSupportedFormat").Split(';').ToList().Select(x => x.Trim()).ToList();


                if (fileAttachment.ContentLength > fileSize)
                    return "Request invalido. El archivo adjunto es más grande que el tamaño permitido";


                //if(!fileContentFormat.Contains (fileAttachment.ContentType.ToLower()))
                //    return "Request invalido. El archivo adjunto debe ser una imagen en formato .png, .jpeg o .jpg";
                if (!(fileAttachment.FileName.ToLower().EndsWith(".png") || fileAttachment.FileName.ToLower().EndsWith(".jpeg") || fileAttachment.FileName.ToLower().EndsWith(".jpg")))
                {
                    return "Request invalido. El archivo adjunto debe ser una imagen en formato .png, .jpeg o .jpg";
                }

                return null;
            }
            catch(Exception ex)
            {
                return "Request invalido. Por favor valide su request e intente de nuevo";
            }            
        }
    }
}