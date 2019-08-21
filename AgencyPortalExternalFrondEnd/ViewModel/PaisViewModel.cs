using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PaisViewModel
    {
        public string Nombre { get; set; }
        public string Iniciales { get; set; }

        public static List<PaisViewModel> ListFakePaises()
        {
            var list = new List<PaisViewModel>()
            {
                new PaisViewModel() { Nombre = "Brasil", Iniciales = "BR" },

                new PaisViewModel() { Nombre = "Canadá", Iniciales = "CA" },

                new PaisViewModel() { Nombre = "Colombia", Iniciales = "CO" },

                new PaisViewModel() { Nombre = "Estados Unidos", Iniciales = "US" },

                new PaisViewModel() { Nombre = "Mexico", Iniciales = "MX" },

                new PaisViewModel() { Nombre = "Belice", Iniciales = "BZ" },

                new PaisViewModel() { Nombre = "Costa Rica", Iniciales = "CR" },

                new PaisViewModel() { Nombre = "El Salvador", Iniciales = "SV" },

                new PaisViewModel() { Nombre = "Guatemala", Iniciales = "GT" },

                new PaisViewModel() { Nombre = "Honduras", Iniciales = "HN" },

                new PaisViewModel() { Nombre = "Nicaragua", Iniciales = "NI" },

                new PaisViewModel() { Nombre = "Panamá", Iniciales = "PA" },

                new PaisViewModel() { Nombre = "Aruba", Iniciales = "AW" },

                new PaisViewModel() { Nombre = "Bahamas", Iniciales = "BS" },

                new PaisViewModel() { Nombre = "Barbados", Iniciales = "BB" },

                new PaisViewModel() { Nombre = "Cuba", Iniciales = "CU" },

                new PaisViewModel() { Nombre = "Curazao", Iniciales = "CW" },

                new PaisViewModel() { Nombre = "Haití", Iniciales = "HT" },

                new PaisViewModel() { Nombre = "Jamaica", Iniciales = "JM" },

                new PaisViewModel() { Nombre = "Puerto Rico", Iniciales = "PR" },

                new PaisViewModel() { Nombre = "República Dominicana", Iniciales = "DO" },

                new PaisViewModel() { Nombre = "St Maarten", Iniciales = "SX" },

                new PaisViewModel() { Nombre = "Trinidad y Tobago", Iniciales = "TT" },

                new PaisViewModel() { Nombre = "Argentina", Iniciales = "AR" },

                new PaisViewModel() { Nombre = "Bolivia", Iniciales = "BO" },

                new PaisViewModel() { Nombre = "Chile", Iniciales = "CL" },

                new PaisViewModel() { Nombre = "Ecuador", Iniciales = "EC" },

                new PaisViewModel() { Nombre = "Guyana", Iniciales = "GY" },

                new PaisViewModel() { Nombre = "Paraguay", Iniciales = "PY" },

                new PaisViewModel() { Nombre = "Perú", Iniciales = "PE" },

                new PaisViewModel() { Nombre = "Surinam", Iniciales = "SR" },

                new PaisViewModel() { Nombre = "Uruguay", Iniciales = "UY" },

                new PaisViewModel() { Nombre = "Venezuela", Iniciales = "VE" }

            };
            return list;
        }
    }
}
