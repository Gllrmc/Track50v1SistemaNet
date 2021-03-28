using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ProyectogrupoViewModel
    {
        public int Id { get; set; }
        public int proyectoid { get; set; }
        public int grupoid { get; set; }
        public decimal tarifaproyectogrupo { get; set; }
        public decimal costoproyectogrupo { get; set; }
        public string notas { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
