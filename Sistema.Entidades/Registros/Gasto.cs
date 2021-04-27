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
    public class Gasto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("usuario")]
        public int usuarioid { get; set; }
        [ForeignKey("registro")]
        public int? registroid { get; set; }
        [Required]
        [ForeignKey("proyecto")]
        public int? proyectoid { get; set; }
        [ForeignKey("tarea")]
        public int? tareaid { get; set; }
        [Required]
        [ForeignKey("concepto")]
        public int conceptoid { get; set; }
        [Required]
        public DateTime fecgasto { get; set; }
        [Required]
        public string referencia { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal impneto { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal impiva { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal impivaper { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal impiibb { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal impotros { get; set; }
        [Required]
        public bool facturable { get; set; }
        [Required]
        public bool liquidable { get; set; }
        public string notas { get; set; }
        [Required]
        public bool autorizado { get; set; }
        public int? iduserauto { get; set; }
        public DateTime? fhauto { get; set; }
        [Required]
        public bool facturado { get; set; }
        public int? iduserfact { get; set; }
        public DateTime? fhfact { get; set; }
        [Required]
        public bool liquidado { get; set; }
        public int? iduserliqui { get; set; }
        public DateTime? fhliqui { get; set; }
        public string pdfid { get; set; }
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Column(TypeName = "geography (point)")]
        public Point geoalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
        [Required]
        [Column(TypeName = "geography (point)")]
        public Point geoumod { get; set; }
        public bool activo { get; set; }
        public Usuario usuario { get; set; }
        public Proyecto proyecto { get; set; }
        public Registro registro { get; set; }
        public Tarea tarea { get; set; }
        public Concepto concepto { get; set; }
    }
}
