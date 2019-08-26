using CopaAirlines.AgenciesService.Common;
using CopaAirlines.AgenciesService.DA;
using CopaAirlines.AgenciesService.Interface;
using CopaAirlines.AgenciesService.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopaAirlines.AgenciesService.Logic
{
    public class AgenciesLogic : IAgency
    {
        private readonly DbContextAgenciesServices _dbContext;
        private readonly ILogger logger;

        public AgenciesLogic(DbContextAgenciesServices dbContext,
                                    ILoggerFactory loggerFactory)
        {
            this._dbContext = dbContext;
            this.logger = loggerFactory.CreateLogger("CopaAirlines.AgenciesService.Logic.Agencies");
        }

        
        /// <summary>
        /// Get agency information by user agency id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public OperationResult GetAgencyByUserId(int userId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (userId <= 0)
                    throw new LogicValidationException("No se recibió un id de usuario");

                var user = _dbContext.AgencyUsers.Include(ag => ag.Agency).
                                            FirstOrDefault(fod => fod.UserID == userId);

                if (user == null)
                    throw new LogicValidationException("El registro no fue encontrado");

                operationResult.Result = true;
                operationResult.Data = new AgencyViewModel()
                {
                    Id = user.Agency.AgencyID,
                    Name = user.Agency.Name,
                    IATA = user.Agency.IATACode,
                    Email = user.Agency.Email,
                    PhoneNumber = user.Agency.PhoneNumber,
                    CreationDate = user.Agency.CreationDate
                };
                    
            }
            catch(LogicValidationException lv)
            {
                logger.LogWarning(lv, lv.Message);
                operationResult.Message = lv.Message;
            }
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
                operationResult.Message = "Ocurrió un error en el sistema. Porfavor informar el administrador.";
            }
            
            return operationResult;
        }

        /// <summary>
        /// Get agency information by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public OperationResult GetAgencyByUserEmail(string email)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (string.IsNullOrEmpty(email))
                    throw new LogicValidationException("No se recibió un email de usuario");

                var user = _dbContext.AgencyUsers.Include(ag => ag.Agency).
                                            FirstOrDefault(fod => fod.Email == email);

                if (user == null)
                    throw new LogicValidationException("El registro no fue encontrado");

                operationResult.Result = true;
                operationResult.Data = new AgencyViewModel()
                {
                    Id = user.Agency.AgencyID,
                    Name = user.Agency.Name,
                    IATA = user.Agency.IATACode,
                    Email = user.Agency.Email,
                    PhoneNumber = user.Agency.PhoneNumber,
                    CreationDate = user.Agency.CreationDate
                };

            }
            catch (LogicValidationException lv)
            {
                logger.LogWarning(lv, lv.Message);
                operationResult.Message = lv.Message;
            }
            catch (Exception e)
            {
                operationResult.Message = "Ocurrió un error en el sistema. Porfavor informar el administrador.";
                logger.LogError(e, e.Message);
            }

            return operationResult;
        }
    }
}
