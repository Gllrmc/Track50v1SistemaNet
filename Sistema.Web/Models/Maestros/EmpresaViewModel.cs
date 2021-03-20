using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string logo { get; set; }
        public Boolean aceptacargadiaria { get; set; }
        public Boolean aceptacargasemanal { get; set; }
        public Boolean facturabledefault { get; set; }
        public Boolean reservadodefault { get; set; }
        public decimal tarifadefault { get; set; }
        public string monedadefault { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
