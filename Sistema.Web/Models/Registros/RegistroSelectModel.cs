using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Registros
{
    public class RegistroSelectModel
    {
        public int Id { get; set; }
        public string actividad { get; set; }
        public int usuarioid { get; set; }
        public int? proyectoid { get; set; }
        public int? tareaid { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecregistracion { get; set; }
        public bool facturable { get; set; }
        public bool liquidable { get; set; }
        public DateTime? fhdesde { get; set; }
        public DateTime? fhhasta { get; set; }
        public int minutos { get; set; }
    }
}
