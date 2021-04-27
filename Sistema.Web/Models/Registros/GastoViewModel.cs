using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Registros
{
    public class GastoViewModel
    {
        public int Id { get; set; }
        public int usuarioid { get; set; }
        public string email { get; set; }
        public string userid { get; set; }
        public int? registroid { get; set; }
        public string actividad { get; set; }
        public int? proyectoid { get; set; }
        public string proyecto { get; set; }
        public string colfondo { get; set; }
        public string coltexto { get; set; }
        public int? tareaid { get; set; }
        public string tarea { get; set; }
        public int conceptoid { get; set; }
        public string concepto { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecgasto { get; set; }
        public string referencia { get; set; }
        public decimal impneto { get; set; }
        public decimal impiva { get; set; }
        public decimal impivaper { get; set; }
        public decimal impiibb { get; set; }
        public decimal impotros { get; set; }
        public bool facturable { get; set; }
        public bool liquidable { get; set; }
        public string notas { get; set; }
        public bool autorizado { get; set; }
        public int? iduserauto { get; set; }
        public DateTime? fhauto { get; set; }
        public bool facturado { get; set; }
        public int? iduserfact { get; set; }
        public DateTime? fhfact { get; set; }
        public bool liquidado { get; set; }
        public int? iduserliqui { get; set; }
        public DateTime? fhliqui { get; set; }
        public string pdfid { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public double latalta { get; set; }
        public double longalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public double latumod { get; set; }
        public double longumod { get; set; }
        public bool activo { get; set; }
    }
}
