using CopaAirlines.RequestsService.Common;
using CopaAirlines.RequestsService.DA;
using CopaAirlines.RequestsService.DA.DbModel;
using CopaAirlines.RequestsService.Interface;
using CopaAirlines.RequestsService.Interface.ApiInterface;
using CopaAirlines.RequestsService.ViewModel;
using CopaAirlines.RequestsService.ViewModel.ApiViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CopaAirlines.RequestsService.Logic
{
    public class NameCorrectionLogic : INameCorrection
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext_RequestService _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly IAgencyApi _agencyApi;
        private readonly ILogger logger;

        public NameCorrectionLogic(IConfiguration configuration,
                                    DBContext_RequestService dbContext,
                                    IEmailSender emailSender,
                                    IAgencyApi agencyApi,
                                    ILoggerFactory loggerFactory)
        {
            
            this._configuration = configuration;
            this._dbContext = dbContext;
            this._emailSender = emailSender;
            this._agencyApi = agencyApi;

            this.logger = loggerFactory.CreateLogger("CopaAirlines.RequestService.Logic.NameCorrection");
        }

        public async Task<OperationResult> RegisterNameCorrection(NameCorrectionViewModel requestNameCorrection)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var agencyRequest = await _agencyApi.GetAgencyFromUserEmail(requestNameCorrection.UserEmail);
                var agency = JsonConvert.DeserializeObject<AgencyViewModel>(agencyRequest.Data.ToString());
                
                var correctionName = new NameCorrectionRequest()
                {
                    AgencyID = agency.Id,
                    PNR = requestNameCorrection.PNR,
                    ContactPhoneNumber = requestNameCorrection.ContactPhoneNumber,
                    CountryCode = requestNameCorrection.CountryCode,
                    ServiceLanguage = requestNameCorrection.ServiceLanguage,
                    Description = requestNameCorrection.Description,
                    RegisteredDate = DateTime.Now
                };

                _dbContext.NameCorrectionRequests.Add(correctionName);
                
                operationResult.Result = _dbContext.SaveChanges() > 0;

                operationResult.Data = correctionName.NameCorrectionRequestID;

            }
            catch(Exception e)
            {
                operationResult.Message = "Ocurrió un error en el registro de la corrección de nombre, intente nuevamente";
                logger.LogError(e, e.Message);
            }

            return operationResult;
        }


        public async Task<OperationResult> SaveImageAsync(IFormFile httpFile, string fileRoute)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (httpFile.Length > 0)
                {
                    using (var fileStream = new FileStream(fileRoute, FileMode.Create))
                        await httpFile.CopyToAsync(fileStream);

                    operationResult.Result = true;
                }
                else
                    operationResult.Message = "No se logró subir imagen.";
            }
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
                operationResult.Message = "No se logró subir la imagen.";
                
            }
            
            return operationResult;
        }

        public async Task<bool> SendNameCorrectionMailByIdRequest(int id, string fileTemplatePath, string filePathToAttach)
        {
            try
            {
                var request = _dbContext.NameCorrectionRequests.FirstOrDefault(fod => fod.NameCorrectionRequestID == id);

                if (request == null)
                    return false;

                string templateText = File.ReadAllText(fileTemplatePath);
                
                var agencyRequest = await _agencyApi.GetAgencyFromUserEmail("lugonzalezr@copaair.com");
                var agencyData = JsonConvert.DeserializeObject<AgencyViewModel>(agencyRequest.Data.ToString());

                Dictionary<string, string> replacements = new Dictionary<string, string>(){
                    {"{caseID}", request.NameCorrectionRequestID.ToString()},
                    { "{agencyName}", request.AgencyID.ToString()},
                    { "{iATACode}", agencyData.IATA},
                    { "{agencyEmail}", agencyData.Email},
                    { "{agencyPhoneNumber}", request.ContactPhoneNumber},
                    { "{country}", request.CountryCode},
                    { "{language}", request.ServiceLanguage},
                    { "{description}", request.Description}
                };


                var emailModel = new EmailViewModel()
                {
                    From = HttpUtility.HtmlEncode(_configuration["NameCorrectionEmailData:EmailFrom"]),
                    To = HttpUtility.HtmlEncode(_configuration["NameCorrectionEmailData:EmailTo"]),
                    Subject = HttpUtility.HtmlEncode(string.Format(_configuration["NameCorrectionEmailData:EmailSubject"], request.NameCorrectionRequestID, request.ServiceLanguage)),
                    SMTPServer = HttpUtility.HtmlEncode(_configuration["NameCorrectionEmailData:SMTPServer"]),
                    Body = HttpUtility.HtmlEncode(replacements.Aggregate(templateText, (result, s) => result.Replace(s.Key, s.Value)))
                };

                return _emailSender.Send(emailModel, filePathToAttach); ;
            }
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
                return false;
            }
        }

        
    }
}
