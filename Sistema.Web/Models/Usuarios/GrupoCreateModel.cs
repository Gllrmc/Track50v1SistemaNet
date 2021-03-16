using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Usuarios
{
    public class GrupoCreateModel
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
