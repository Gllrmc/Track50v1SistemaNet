using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ProyectotareaViewModel
    {
        public int Id { get; set; }
        public int proyectoid { get; set; }
        public int tareaid { get; set; }
        public decimal estimadohoras { get; set; }
        public decimal estimadomonto { get; set; }
        public string notas { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
