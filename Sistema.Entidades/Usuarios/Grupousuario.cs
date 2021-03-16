using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Usuarios
{
    public class Grupousuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("grupos")]
        public int grupoid { get; set; }
        [Required]
        [ForeignKey("usuarios")]
        public int usuarioid { get; set; }
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
        public Usuario usuario { get; set; }
        public Grupo grupo { get; set; }
    }
}
