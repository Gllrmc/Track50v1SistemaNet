using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Usuarios
{
    public class UsuarioCreateModel
    {
        [Required]
        public int rolId { get; set; }
        [Required]
        public string userid { get; set; }
        [Required]
        public string iniciales { get; set; }
        public string telefono { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string colfondo { get; set; }
        public string coltexto { get; set; }
        public string imgusuario { get; set; }
        public bool pxch { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
