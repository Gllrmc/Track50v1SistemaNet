using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros
{
    public class CalendarioViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecha { get; set; }
        public int ynum { get; set; }
        public int mnum { get; set; }
        public string mtext { get; set; }
        public int dnum { get; set; }
        public string dtext { get; set; }
        public int snum { get; set; }
        [DataType(DataType.Date)]
        public DateTime sini { get; set; }
        [DataType(DataType.Date)]
        public DateTime sfin { get; set; }
        public string stext { get; set; }
        public string stxt { get; set; }
        public bool laborweek { get; set; }
        public bool holydays { get; set; }
        public int laborable { get; set; }
    }
}
