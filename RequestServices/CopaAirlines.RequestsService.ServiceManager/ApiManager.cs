using CopaAirlines.RequestsService.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.RequestsService.ServiceManager
{
    public class ApiManager
    {
        public string hostAPI;
        public string _givenToken;
        
        protected void SetUrlHost(IConfiguration configuration, EnumServiceManager enumServiceManager)
        {
            switch(enumServiceManager)
            {
                case EnumServiceManager.Agencies:
                    hostAPI = configuration["ExternalServicesUrl:AgenciesServiceURL"];
                    break;

                default:
                    hostAPI = string.Empty;
                    break;
            }
        }


        protected async Task<T> ExecuteAPICallSimple<T>(string pathService)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                if (!string.IsNullOrEmpty(_givenToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _givenToken);

                // New code:
                HttpResponseMessage response = await client.GetAsync(String.Format("{0}{1}", hostAPI, pathService));
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<T>(responseData);
                    return Items;
                }
                else
                {
                    throw new Exception("Error al Invocar un llamado al API, Info: " + response.ToString());
                }
            }
        }


        protected async Task<T> ExecutePOSTAPICallSimple<T, M>(string pathService, M dataRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(hostAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                if (!string.IsNullOrEmpty(_givenToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _givenToken);

                var json = JsonConvert.SerializeObject(dataRequest);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                try
                {
                    response = await client.PostAsync(pathService, content);
                }
                catch (Exception er)
                {

                    throw er;
                }

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<T>(responseData);
                    return Items;
                }
                else
                {
                    throw new Exception("Error al Invocar un llamado al API, Info: " + response.ToString());
                }
            }
        }
    }
}
