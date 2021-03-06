using NetTopologySuite.Geometries;
using Sistema.Entidades.Administracion;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Registros
{
    public class Registro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string actividad { get; set; }
        [Required]
        [ForeignKey("usuario")]
        public int usuarioid { get; set; }
        [ForeignKey("proyecto")]
        public int? proyectoid { get; set; }
        [ForeignKey("tarea")]
        public int? tareaid { get; set; }
        [Required]
        public DateTime fecregistracion { get; set; }
        [Required]
        public bool facturable { get; set; }
        [Required]
        public bool liquidable { get; set; }
        public DateTime? fhdesde { get; set; }
        public DateTime? fhhasta { get; set; }
        [Required]
        public int minutos { get; set; }
        [Column(TypeName = "geography (point)")]
        public Point geodesde { get; set; }
        [Column(TypeName = "geography (point)")]
        public Point geohasta { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal tarifa { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal costo { get; set; }
        [Required]
        public bool facturado { get; set; }
        public int? iduserfact { get; set; }
        public DateTime? fhfact { get; set; }
        [Required]
        public bool liquidado { get; set; }
        public int? iduserliqui { get; set; }
        public DateTime? fhliqui { get; set; }
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
        public Usuario usuario { get; set; }
        public Proyecto proyecto { get; set; }
        public Tarea tarea { get; set; }
        public ICollection<Etiquetaregistro> etiquetaregistros { get; set; }
        public ICollection<Gasto> gastos { get; set; }

    }
}
