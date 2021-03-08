﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros
{
    public class EmpresaCreateModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La empresa no debe tener mas de 50 caracteres, ni menos de 2 caracteres")]
        public string nombre { get; set; }
        [Required]
        public string cuit { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string localidad { get; set; }
        public string cpostal { get; set; }
        [Required]
        public int provinciaId { get; set; }
        [Required]
        public int paisId { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string webpage { get; set; }
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
    }
}
