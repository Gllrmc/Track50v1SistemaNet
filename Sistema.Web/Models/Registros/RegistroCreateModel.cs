using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Registros
{
    public class RegistroCreateModel
    {
        public string actividad { get; set; }
        [Required]
        public int usuarioid { get; set; }
        public int? proyectoid { get; set; }
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
        public double latdesde { get; set; }
        public double longdesde { get; set; }
        public double lathasta { get; set; }
        public double longhasta { get; set; }
        [Required]
        public decimal tarifa { get; set; }
        [Required]
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
    }
}
