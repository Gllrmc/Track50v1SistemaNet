using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Maestros
{
    public class Provincia
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [ForeignKey("pais")]
        public int paisId { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }

        public Pais pais { get; set; }
    }
}
