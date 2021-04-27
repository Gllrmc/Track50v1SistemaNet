using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ConceptoproyectoViewModel
    {
        public int Id { get; set; }
        public int proyectoid { get; set; }
        public int conceptoid { get; set; }
        public int sec { get; set; }
        public string referencia { get; set; }
        public decimal impreferencia { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
