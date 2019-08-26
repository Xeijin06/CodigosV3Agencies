using CopaAirlines.AgenciesService.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.AgenciesService.Interface
{
    public interface IAgency
    {
        OperationResult GetAgencyByUserId(int userId);
        OperationResult GetAgencyByUserEmail(string email);
    }
}
