using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros
{
    public class EmpresaSelectModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public decimal tarifadefault { get; set; }
        public decimal costodefault { get; set; }
    }
}
