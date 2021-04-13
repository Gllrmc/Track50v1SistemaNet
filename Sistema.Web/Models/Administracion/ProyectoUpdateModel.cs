using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ProyectoUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public int empresaid { get; set; }
        public int? clienteid { get; set; }
        [Required]
        public Boolean facturable { get; set; }
        [Required]
        public Boolean liquidable { get; set; }
        [Required]
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
        public int? iduserarch { get; set; }
        public DateTime? fecarch { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
