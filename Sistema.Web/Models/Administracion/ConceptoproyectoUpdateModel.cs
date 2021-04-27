using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ConceptoproyectoUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int proyectoid { get; set; }
        [Required]
        public int conceptoid { get; set; }
        [Required]
        public int sec { get; set; }
        public string referencia { get; set; }
        public decimal impreferencia { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
