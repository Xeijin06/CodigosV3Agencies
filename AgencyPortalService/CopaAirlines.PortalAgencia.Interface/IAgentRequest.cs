using CopaAirlines.PortalAgencia.DA.ViewModels;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CopaAirlines.PortalAgencia.Interface
{
    public interface IAgentRequest
    {        
        ResponseOperation Registrar(WaiverNameCorrectionViewModel agencia);
        ResponseOperation ValidarAgencia(WaiverNameCorrectionViewModel agencia);
        int CreateAttemptRegistry(string email, string pin);
        void UpdateFailAttempt(int attemptID, int maxAttemptAllowed, string email);
        bool ValidateBlockedCredentials(string email);
        void UpdateSuccessAttempt(int attemptID);
    }
}
