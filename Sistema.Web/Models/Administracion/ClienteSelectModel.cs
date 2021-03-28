using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ClienteSelectModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public decimal tarifadefault { get; set; }
    }
}
