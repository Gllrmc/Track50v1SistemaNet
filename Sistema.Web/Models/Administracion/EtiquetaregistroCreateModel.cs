using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class EtiquetaregistroCreateModel
    {
        [Required]
        public int etiquetaid { get; set; }
        [Required]
        public int registroid { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
