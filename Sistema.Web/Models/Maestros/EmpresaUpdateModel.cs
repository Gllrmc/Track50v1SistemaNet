using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros
{
    public class EmpresaUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        public string logo { get; set; }
        public Boolean aceptacargalapsos { get; set; }
        public Boolean aceptacargatiempos { get; set; }
        public Boolean facturabledefault { get; set; }
        public Boolean reservadodefault { get; set; }
        public decimal tarifadefault { get; set; }
        public decimal costodefault { get; set; }
        [Required]
        public string monedadefault { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
