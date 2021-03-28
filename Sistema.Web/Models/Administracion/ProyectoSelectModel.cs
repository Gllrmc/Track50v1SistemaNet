using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ProyectoSelectModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public int empresaid { get; set; }
        public string empresa { get; set; }
        public int? clienteid { get; set; }
        public string cliente { get; set; }
        public Boolean facturable { get; set; }
        public decimal tarifadefault { get; set; }
        public string notas { get; set; }
        public Boolean reservado { get; set; }
        public string colfondo { get; set; }
        public string coltexto { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecregdesde { get; set; }
        [Column(TypeName = "decimal(10,0")]
        public decimal estimadohoras { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal estimadomonto { get; set; }
        [DataType(DataType.Date)]
        public DateTime? fecultfact { get; set; }
        [DataType(DataType.Date)]
        public DateTime? fecultliqui { get; set; }
        public Boolean archivado { get; set; }
        public int? iduserarch { get; set; }
        public DateTime? fecarch { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
    }
}
