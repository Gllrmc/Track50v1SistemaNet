using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ProyectousuarioCreateModel
    {
        [Required]
        public int proyectoid { get; set; }
        [Required]
        public int usuarioid { get; set; }
        [Required]
        public decimal tarifaproyectousuario { get; set; }
        [Required]
        public decimal costoproyectousuario { get; set; }
        public string notas { get; set; }
        public int iduseralta { get; set; }
    }
}
