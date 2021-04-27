using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class EtiquetaregistroUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int etiquetaid { get; set; }
        [Required]
        public int registroid { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
