using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Maestros;
using Sistema.Web.Models.Maestros;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendariosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CalendariosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Calendarios/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]" )]
        public async Task<IEnumerable<CalendarioViewModel>> Listar()
        {
            var calendario = await _context
                .Calendarios.ToListAsync();

            return calendario.Select(a => new CalendarioViewModel
            {

                Id = a.Id,
                fecha = a.fecha,
                ynum = a.ynum,
                mnum = a.mnum,
                mtext = a.mtext,
                dnum = a.dnum,
                dtext = a.dtext,
                snum = a.snum,
                sini = a.sini,
                sfin = a.sfin,
                stext = a.stext,
                stxt = a.stxt,
                laborweek = a.laborweek,
                holydays = a.holydays,
                laborable = a.laborable
            });

        }


        // GET: api/Calendarios/Listarfechas/2021-01-01/2021-02-01
        //[Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]/{fecdesde}/{fechasta}")]
        public async Task<IEnumerable<CalendarioViewModel>> Listarfechas([FromRoute] DateTime fecdesde, [FromRoute] DateTime fechasta)
        {
            var calendario = await _context
                .Calendarios
                .Where(f => f.fecha >= fecdesde && f.fecha < fechasta)
                .OrderBy(f => f.fecha)
                .ToListAsync();

            return calendario.Select(a => new CalendarioViewModel
            {

                Id = a.Id,
                fecha = a.fecha,
                ynum = a.ynum,
                mnum = a.mnum,
                mtext = a.mtext,
                dnum = a.dnum,
                dtext = a.dtext,
                snum = a.snum,
                sini = a.sini,
                sfin = a.sfin,
                stext = a.stext,
                stxt = a.stxt,
                laborweek = a.laborweek,
                holydays = a.holydays,
                laborable = a.laborable
            });

        }

        private bool CalendarioExists(int id)
        {
            return _context.Calendarios.Any(e => e.Id == id);
        }
    }
}
