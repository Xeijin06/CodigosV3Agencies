using CopaAirlines.RequestsService.Common;
using CopaAirlines.RequestsService.Interface.ApiInterface;
using CopaAirlines.RequestsService.ViewModel;
using CopaAirlines.RequestsService.ViewModel.ApiViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CopaAirlines.RequestsService.ServiceManager
{
    public class AgenciesServicesApi : ApiManager, IAgencyApi
    {
        private readonly ILogger logger;
        public AgenciesServicesApi(IConfiguration configuration,
                                    ILoggerFactory loggerFactory)
        {
            this.SetUrlHost(configuration, EnumServiceManager.Agencies);
            this.logger = loggerFactory.CreateLogger("CopaAirlines.RequestService.ServiceManager.AgencyService");
        }

        public async Task<OperationResult> ValidateUser(AgencyUserViewModel agencyUser)
        {
            try
            {
                string pathURL = string.Format("/Users/ValidateUser");
                return await this.ExecutePOSTAPICallSimple<OperationResult, AgencyUserViewModel>(pathURL, agencyUser);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw e;
            }
        }

        public async Task<OperationResult> GetAgencyFromUserID(int id)
        {
            try
            {
                string pathURL = string.Format("/GetAgencyFromUserID/{0}", id);
                return await this.ExecuteAPICallSimple<OperationResult>(pathURL);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw e;
            }
        }

        public async Task<OperationResult> GetAgencyFromUserEmail(string email)
        {
            try
            {
                string pathURL = string.Format("/GetAgencyFromUserEmail?email={0}", email);
                return await this.ExecuteAPICallSimple<OperationResult>(pathURL);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw e;
            }
        }
    }
}
