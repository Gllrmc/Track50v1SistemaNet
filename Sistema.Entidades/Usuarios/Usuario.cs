using Sistema.Entidades.Administracion;
using Sistema.Entidades.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Usuarios
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("rol")]
        public int rolId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El userid no debe de tener más de 100, ni menos de 3 caracteres.")]
        public string userid { get; set; }
        [Required]
        public string iniciales { get; set; }
        public string telefono { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public byte[] password_hash { get; set; }
        [Required]
        public byte[] password_salt { get; set; }
        [Required]
        public bool reservado { get; set; }
        public int primerahora { get; set; }
        public int lineaspag { get; set; }
        public string colfondo { get; set; }
        public string coltexto { get; set; }
        public string imgusuario { get; set; }
        [Required]
        public bool pxch { get; set; }
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
        public Rol rol { get; set; }
        public ICollection<Grupousuario> grupousuarios { get; set; }
        public ICollection<Proyectousuario> proyectousuarios { get; set; }
        public ICollection<Registro> registros { get; set; }
        public ICollection<Gasto> gastos { get; set; }
    }
}
