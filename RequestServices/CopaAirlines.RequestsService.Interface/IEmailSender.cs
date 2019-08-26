using CopaAirlines.RequestsService.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaAirlines.RequestsService.Interface
{
    public interface IEmailSender
    {
        bool Send(EmailViewModel emailModel, string pathFile = null);
    }
}
