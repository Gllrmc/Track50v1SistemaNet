using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Registros
{
    public class GastoSelectModel
    {
        public int Id { get; set; }
        public int usuarioid { get; set; }
        public string email { get; set; }
        public string userid { get; set; }
        public int? registroid { get; set; }
        public string actividad { get; set; }
        public int? proyectoid { get; set; }
        public string proyecto { get; set; }
        public int? tareaid { get; set; }
        public string tarea { get; set; }
        public int conceptoid { get; set; }
        public string concepto { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecgasto { get; set; }
        public decimal impneto { get; set; }
    }
}
