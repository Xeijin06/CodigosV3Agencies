using CopaAirlines.RequestsService.ViewModel;
using CopaAirlines.RequestsService.ViewModel.ApiViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.RequestsService.Interface.ApiInterface
{
    public interface IAgencyApi
    {
        Task<OperationResult> ValidateUser(AgencyUserViewModel agencyUser);
        Task<OperationResult> GetAgencyFromUserID(int id);
        Task<OperationResult> GetAgencyFromUserEmail(string email);
    }
}
