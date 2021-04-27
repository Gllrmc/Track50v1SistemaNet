using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Registros
{
    public class GastoCreateModel
    {
        [Required]
        public int usuarioid { get; set; }
        public int? registroid { get; set; }
        [Required]
        public int? proyectoid { get; set; }
        public int? tareaid { get; set; }
        [Required]
        public int conceptoid { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime fecgasto { get; set; }
        [Required]
        public string referencia { get; set; }
        [Required]
        public decimal impneto { get; set; }
        [Required]
        public decimal impiva { get; set; }
        [Required]
        public decimal impivaper { get; set; }
        [Required]
        public decimal impiibb { get; set; }
        [Required]
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
        [Required]
        public int iduseralta { get; set; }
        public double latalta { get; set; }
        public double longalta { get; set; }
    }
}
