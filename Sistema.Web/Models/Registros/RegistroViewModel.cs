using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Registros
{
    public class RegistroViewModel
    {
        public int Id { get; set; }
        public string actividad { get; set; }
        public int usuarioid { get; set; }
        public int? proyectoid { get; set; }
        public string proyecto { get; set; }
        public int? tareaid { get; set; }
        public string tarea { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecregistracion { get; set; }
        public bool facturable { get; set; }
        public bool liquidable { get; set; }
        public DateTime? fhdesde { get; set; }
        public DateTime? fhhasta { get; set; }
        public int minutos { get; set; }
        public double latdesde { get; set; }
        public double longdesde { get; set; }
        public double lathasta { get; set; }
        public double longhasta { get; set; }
        public decimal tarifa { get; set; }
        public decimal costo { get; set; }
        public bool facturado { get; set; }
        public int? iduserfact { get; set; }
        public DateTime? fhfact { get; set; }
        public bool liquidado { get; set; }
        public int? iduserliqui { get; set; }
        public DateTime? fhliqui { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
