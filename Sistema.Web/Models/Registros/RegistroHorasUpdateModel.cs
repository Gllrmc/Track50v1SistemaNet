using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Registros
{
    public class RegistroHorasUpdateModel
    {
        [Required]
        public int Id { get; set; }
        public DateTime? fhdesde { get; set; }
        public DateTime? fhhasta { get; set; }
        [Required]
        public int minutos { get; set; }
        public double latdesde { get; set; }
        public double longdesde { get; set; }
        public double lathasta { get; set; }
        public double longhasta { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
