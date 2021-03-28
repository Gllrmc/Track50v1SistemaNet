using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Administracion
{
    public class Proyectotarea
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("proyecto")]
        public int proyectoid { get; set; }
        [Required]
        [ForeignKey("tarea")]
        public int tareaid { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal estimadohoras { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal estimadomonto { get; set; }
        public string notas { get; set; }
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
        public Proyecto proyecto { get; set; }
        public Tarea tarea { get; set; }
    }
}
