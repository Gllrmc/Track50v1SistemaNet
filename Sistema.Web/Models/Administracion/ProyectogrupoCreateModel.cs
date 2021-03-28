using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ProyectogrupoCreateModel
    {
        [Required]
        public int proyectoid { get; set; }
        [Required]
        public int grupoid { get; set; }
        [Required]
        public decimal tarifaproyectogrupo { get; set; }
        [Required]
        public decimal costoproyectogrupo { get; set; }
        public string notas { get; set; }
        public int iduseralta { get; set; }
    }
}
