using CopaAirlines.RequestsService.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.RequestsService.Interface
{
    public interface INameCorrection
    {
        Task<OperationResult> RegisterNameCorrection(NameCorrectionViewModel requestNameCorrection);
        Task<OperationResult> SaveImageAsync(IFormFile httpFile, string fileRoute);
        Task<bool> SendNameCorrectionMailByIdRequest(int id, string fileTemplatePath, string filePathToAttach);
    }
}
