using CopaAirlines.RequestsService.Common;
using CopaAirlines.RequestsService.Interface;
using CopaAirlines.RequestsService.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CopaAirlines.RequestsService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NameCorrectionController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INameCorrection _nameCorrectionLogic;
        private readonly IEmailSender _emailSender;
        private readonly ILogger logger;

        public NameCorrectionController(IConfiguration configuration,
                                        INameCorrection nameCorrectionLogic,
                                        IEmailSender emailSender,
                                        ILoggerFactory loggerFactory)
        {
            this._configuration = configuration;
            this._nameCorrectionLogic = nameCorrectionLogic;
            this._emailSender = emailSender;
            this.logger = loggerFactory.CreateLogger("CopaAirlines.RequestService.Api.NameCorrection");
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<OperationResult> PostAsync()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                string serverFolderPath = _configuration["NameCorrectionUploadedImages"];
                var nameRequestJson = Request.Form.FirstOrDefault(fod => fod.Key == "NameCorrectionRequestData");
            

                var httpFile = Request.Form.Files.Count > 0 ? Request.Form.Files.FirstOrDefault
                                                                        (k => k.Name == "PassportImage") : null;

                var nameCorrectionModel = JsonConvert.DeserializeObject<NameCorrectionViewModel>(nameRequestJson.Value);

                operationResult = await _nameCorrectionLogic.RegisterNameCorrection(nameCorrectionModel);

                if (!operationResult.Result)
                    return operationResult;

                int regId = Convert.ToInt32(operationResult.Data);
                
                operationResult.Message = string.Format("Se registró la solicitud de forma correcta, su número de solicitud es la {0}.", regId);

                string fileRoute = string.Empty;
                if (httpFile != null)
                {
                     fileRoute = String.Format("{0}{1}{2}", serverFolderPath, regId, System.IO.Path.GetExtension(httpFile.FileName));

                    var imageResult = await _nameCorrectionLogic.SaveImageAsync(httpFile, fileRoute);
                    if (!imageResult.Result)
                        operationResult.Message = string.Format("{0} {1}", operationResult.Message, imageResult.Message);
                }

                string templatePath = Path.Combine(_configuration["EmailFormatFolder"], "emailTemplate.txt");

                var emailResult = await _nameCorrectionLogic.SendNameCorrectionMailByIdRequest(regId, templatePath, fileRoute);

                if(!emailResult)
                    operationResult.Message = string.Format("{0} No se logró enviar correo de notificación. Notifique manualmente al encargado del proceso en Copa Airlines.", operationResult.Message);


                operationResult.Data = null;
                
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
            }

            return operationResult;
        }
    }
}
