using CopaAirlines.AgenciesService.Common;
using CopaAirlines.AgenciesService.DA;
using CopaAirlines.AgenciesService.Interface;
using CopaAirlines.AgenciesService.ViewModel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopaAirlines.AgenciesService.Logic
{
    public class AgencyUserLogic : IUser
    {
        private readonly DbContextAgenciesServices _dbContext;
        private readonly ILogger logger;

        public AgencyUserLogic(DbContextAgenciesServices dbContext,
                                    ILoggerFactory loggerFactory)
        {
            this._dbContext = dbContext;
            this.logger = loggerFactory.CreateLogger("CopaAirlines.AgenciesService.Logic.AgenciesUser");
        }


        public OperationResult ValidateUser(AgencyUserViewModel user)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (user == null)
                    throw new LogicValidationException("No se recibió un datos del usuario");

                if(string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                    throw new LogicValidationException("No se enviaron los datos correctamente");

                var userDb = _dbContext.AgencyUsers.FirstOrDefault(a => a.Email == user.Email && a.Password == user.Password);

                if (userDb != null)
                {
                    operationResult.Result = true;
                    operationResult.Data = new { userId = userDb.UserID };
                }
                else
                    operationResult.Message = "Los datos son incorrectos o el usuario no existe";

            }
            catch(LogicValidationException lv)
            {
                operationResult.Message = lv.Message;
                logger.LogWarning(lv, lv.Message);
            }
            catch(Exception e)
            {
                operationResult.Message = "Ocurrió un error en el sistema. Porfavor informar el administrador.";
                logger.LogError(e, e.Message);
            }

            return operationResult;
        }
    }
}
