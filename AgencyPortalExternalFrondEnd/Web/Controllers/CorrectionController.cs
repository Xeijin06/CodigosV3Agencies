using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ViewModel;


namespace Web.Controllers
{
    public class CorrectionController : Controller
    {
        public static HttpClient client = new HttpClient();
        // GET: Correction
        public ActionResult Index()
        {

            ViewBag.Lenguages = IdiomaViewModel.ListFakeIdioma();
            ViewBag.Paises = PaisViewModel.ListFakePaises();

            return View();
        }
        
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> SendRequest()
        {
            try
            {
                var nameRequestJson = Request.Form["model"];

                var httpFile = Request.Files["image"];

                if (httpFile.ContentLength > 2097152)
                {
                    var operationResult = new OperationResult();
                    operationResult.Message = "La imagen debe ser menor a 2MB";
                    return Json(operationResult);
                }

                var model = JsonConvert.DeserializeObject<NameCorrectionViewModel>(nameRequestJson);

                model.User = User.Identity.Name;
                return await SendDataToAPI(httpFile, model);

            }
            catch (Exception e)
            {
                var operationResult = new OperationResult();
                operationResult.Message = "Hay un error de datos. Verifique si hay campos vacios o si no corresponde al tipo de dato.";
                return Json(operationResult);
            }
        }

        private async System.Threading.Tasks.Task<JsonResult> SendDataToAPI(HttpPostedFileBase httpFile, NameCorrectionViewModel model)
        {

            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);

            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            OperationResult operationResult = new OperationResult();

            try
            {
                byte[] fileData = null;

                if (httpFile != null)
                {
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                    }
                }


                client.DefaultRequestHeaders.Add("AccessTokenCOPAValidator", "12345");

                var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));

                content.Add(new StringContent(JsonConvert.SerializeObject(model)), "NameCorrectionRequestData");

                if (httpFile != null)
                    content.Add(new StreamContent(new MemoryStream(fileData)), "PassportImage", httpFile.FileName);

                var message = await client.PostAsync(String.Format("{0}/NameCorrection", WebConfigurationManager.AppSettings["RequestServicesURL"]), content).Result.Content.ReadAsStringAsync();

                //var data = await message.Content.ReadAsStringAsync();

                operationResult = JsonConvert.DeserializeObject<OperationResult>(message);
                return Json(operationResult);


            }
            catch (Exception e)
            {
                operationResult.Message = "Ocurrió un error al conectarse con los servicios de cambio de nombre. Notifique el error a un administrador.";
                return Json(operationResult);
            }
        }
    }
}