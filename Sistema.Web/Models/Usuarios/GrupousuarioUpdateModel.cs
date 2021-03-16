using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Usuarios
{
    public class GrupousuarioUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int grupoid { get; set; }
        [Required]
        public int usuarioid { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
    }
}
