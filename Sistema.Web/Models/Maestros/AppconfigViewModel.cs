using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros
{
    public class AppconfigViewModel
    {
        public int id { get; set; }
        public string parametro { get; set; }
        public string vstring { get; set; }
        public int? vint { get; set; }
        public decimal? vdecimal { get; set; }
        public DateTime? vdatetime { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
