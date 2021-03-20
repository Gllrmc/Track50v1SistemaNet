using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Maestros
{
    public class Pais
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        public string cuit { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }

        public ICollection<Provincia> provincias { get; set; }
    }
}
