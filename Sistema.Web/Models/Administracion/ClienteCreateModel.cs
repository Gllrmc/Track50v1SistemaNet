using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion
{
    public class ClienteCreateModel
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public decimal tarifadefault { get; set; }
        public string logo { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
