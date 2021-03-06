using Sistema.Entidades.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Administracion
{
    public class Etiquetaregistro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("etiqueta")]
        public int etiquetaid { get; set; }
        [Required]
        [ForeignKey("registro")]
        public int registroid { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
        [Required]
        public bool activo { get; set; }
        public Etiqueta etiqueta { get; set; }
        public Registro registro { get; set; }
    }
}
