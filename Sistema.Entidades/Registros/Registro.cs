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
        [Required]
        public DateTime fecregistracion { get; set; }
        [Required]
        public int secuencia { get; set; }
        [Required]
        public bool factutable { get; set; }
        [Required]
        public bool liquidable { get; set; }
        public DateTime? fhdesde { get; set; }
        public DateTime? fhhasta { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2")]
        public decimal horas { get; set; }
        public Point geodesde { get; set; }
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
        public Proyecto proyecto { get; set; }
        public Usuario usuario { get; set; }
        public ICollection<Tarearegistro> tarearegistros { get; set; }
        public ICollection<Etiquetaregistro> etiquetaregistros { get; set; }

    }
}
