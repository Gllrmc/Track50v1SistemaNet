using Sistema.Entidades.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Administracion
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
        [Required]
        public bool activo { get; set; }
        public ICollection<Proyectotarea> proyectotareas { get; set; }
        public ICollection<Registro> registros { get; set; }
        public ICollection<Gasto> gastos { get; set; }
    }
}
