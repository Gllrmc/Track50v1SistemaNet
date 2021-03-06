using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ProyectotareaCreateModel
    {
        [Required]
        public int proyectoid { get; set; }
        [Required]
        public int tareaid { get; set; }
        [Required]
        public decimal estimadohoras { get; set; }
        [Required]
        public decimal estimadomonto { get; set; }
        public string notas { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
