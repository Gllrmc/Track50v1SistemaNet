﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Usuarios
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public int rolId { get; set; }
        public string rol { get; set; }
        public string userid { get; set; }
        public string iniciales { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public bool reservado { get; set; }
        public byte[] password_hash { get; set; }
        public string colfondo { get; set; }
        public string coltexto { get; set; }
        public string imgusuario { get; set; }
        public int primerahora { get; set; }
        public int lineaspag { get; set; }
        public bool pxch { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
