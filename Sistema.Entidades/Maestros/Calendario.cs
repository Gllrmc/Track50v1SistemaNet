using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Maestros
{
    public class Calendario
    {
        [Key]
        public int Id { get; set; }
        public DateTime fecha { get; set; }
        public int ynum { get; set; }
        public int mnum { get; set; }
        public string mtext { get; set; }
        public int dnum { get; set; }
        public string dtext { get; set; }
        public int snum { get; set; }
        public DateTime sini { get; set; }
        public DateTime sfin { get; set; }
        public string stext { get; set; }
        public string stxt { get; set; }
        public bool laborweek { get; set; }
        public bool holydays { get; set; }
        public int laborable { get; set; }
    }
}
