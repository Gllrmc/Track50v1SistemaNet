using Sistema.Entidades.Maestros;
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
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        [ForeignKey("empresa")]
        public int empresaid { get; set; }
        [ForeignKey("cliente")]
        public int? clienteid { get; set; }
        [Required]
        public Boolean facturable { get; set; }
        [Required]
        public Boolean liquidable { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2")]
        public decimal tarifadefault { get; set; }
        public string notas { get; set; }
        [Required]
        public Boolean reservado { get; set; }
        public string colfondo { get; set; }
        public string coltexto { get; set; }
        [Required]
        public DateTime fecregdesde { get; set; }
        [Column(TypeName = "decimal(10,0")]
        public decimal estimadohoras { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal estimadomonto { get; set; }
        public DateTime? fecultfact { get; set; }
        public DateTime? fecultliqui { get; set; }
        [Required]
        public Boolean archivado { get; set; }
        public int? iduserarch { get; set; }
        public DateTime? fecarch { get; set; }
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
        public Empresa empresa { get; set; }
        public Cliente cliente { get; set; }
        public ICollection<Proyectousuario> proyectousuarios { get; set; }
        public ICollection<Proyectogrupo> proyectogrupos { get; set; }
        public ICollection<Proyectotarea> proyectotareas { get; set; }
        public ICollection<Registro> registros { get; set; }
    }
}
