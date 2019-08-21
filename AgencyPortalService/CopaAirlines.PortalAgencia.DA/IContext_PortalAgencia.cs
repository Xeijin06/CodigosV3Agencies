using CopaAirlines.PortalAgencia.DA.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaAirlines.PortalAgencia.DA
{
    public interface IContext_PortalAgencia
    {
        DbSet<NameCorrectionRequest> NameCorrectionRequest { get; set; }

        void Dispose();
        int SaveChanges();
    }
}
