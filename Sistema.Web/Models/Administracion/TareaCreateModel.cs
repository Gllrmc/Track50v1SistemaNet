using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class TareaCreateModel
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
