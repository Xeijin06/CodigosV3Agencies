using CopaAirlines.AgenciesService.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.AgenciesService.Interface
{
    public interface IUser
    {
        OperationResult ValidateUser(AgencyUserViewModel user);
    }
}
