using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class IdiomaViewModel
    {
        public string Nombre { get; set; }
        public string Iniciales { get; set; }


        public static List<IdiomaViewModel> ListFakeIdioma()
        {
            var list = new List<IdiomaViewModel>()
            {
                new IdiomaViewModel()
                {
                    Nombre = "Español",
                    Iniciales = "ES"
                },
                new IdiomaViewModel()
                {
                    Nombre = "Inglés",
                    Iniciales = "EN"
                },
                new IdiomaViewModel()
                {
                    Nombre = "Portugués",
                    Iniciales = "PT"
                }
            };
            return list;
        }
    }
}
