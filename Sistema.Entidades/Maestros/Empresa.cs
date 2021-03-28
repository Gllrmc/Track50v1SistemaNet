using Sistema.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Maestros
{
    public class Empresa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        public string logo { get; set; }
        public Boolean aceptacargalapsos { get; set; }
        public Boolean aceptacargatiempos { get; set; }
        public Boolean facturabledefault { get; set; }
        public Boolean reservadodefault { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal tarifadefault { get; set; }
        [Required]
        public string monedadefault { get; set; }
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
        public ICollection<Proyecto> proyectos { get; set; }
    }
}
