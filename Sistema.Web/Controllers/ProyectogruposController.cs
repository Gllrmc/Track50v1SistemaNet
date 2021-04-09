using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectogruposController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProyectogruposController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Proyectogrupos/Listar
        //[Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProyectogrupoViewModel>> Listar()
        {
            var proyectogrupo = await _context
                .Proyectogrupos.ToListAsync();

            return proyectogrupo.Select(a => new ProyectogrupoViewModel
            {
                Id = a.Id,
                proyectoid = a.proyectoid,
                grupoid = a.grupoid,
                tarifaproyectogrupo = a.tarifaproyectogrupo,
                costoproyectogrupo = a.costoproyectogrupo,
                notas = a.notas,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Proyectogrupos/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var proyectogrupo = await _context.Proyectogrupos.FindAsync(id);

            if (proyectogrupo == null)
            {
                return NotFound();
            }

            return Ok(new ProyectogrupoViewModel
            {
                Id = proyectogrupo.Id,
                proyectoid = proyectogrupo.proyectoid,
                grupoid = proyectogrupo.grupoid,
                tarifaproyectogrupo = proyectogrupo.tarifaproyectogrupo,
                costoproyectogrupo = proyectogrupo.costoproyectogrupo,
                notas = proyectogrupo.notas,
                iduseralta = proyectogrupo.iduseralta,
                fecalta = proyectogrupo.fecalta,
                iduserumod = proyectogrupo.iduserumod,
                fecumod = proyectogrupo.fecumod,
                activo = proyectogrupo.activo
            });
        }

        // PUT: api/Proyectogrupos/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ProyectogrupoUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var fechaHora = DateTime.Now;
            var proyectogrupo = await _context.Proyectogrupos
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (proyectogrupo == null)
            {
                return NotFound();
            }
            proyectogrupo.proyectoid = model.proyectoid;
            proyectogrupo.grupoid = model.grupoid;
            proyectogrupo.tarifaproyectogrupo = model.tarifaproyectogrupo;
            proyectogrupo.costoproyectogrupo = model.costoproyectogrupo;
            proyectogrupo.notas = model.notas;
            proyectogrupo.iduserumod = model.iduserumod;
            proyectogrupo.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Proyectogrupos/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ProyectogrupoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Proyectogrupo proyectogrupo = new Proyectogrupo
            {
                proyectoid = model.proyectoid,
                grupoid = model.grupoid,
                tarifaproyectogrupo = model.tarifaproyectogrupo,
                costoproyectogrupo = model.costoproyectogrupo,
                notas = model.notas,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Proyectogrupos.Add(proyectogrupo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(proyectogrupo);
        }

        // DELETE: api/Proyectogrupos/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proyectogrupo = await _context.Proyectogrupos
                .FindAsync(id);

            if (proyectogrupo == null)
            {
                return NotFound();
            }

            _context.Proyectogrupos.Remove(proyectogrupo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(proyectogrupo);
        }

        // PUT: api/Proyectogrupos/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyectogrupo = await _context.Proyectogrupos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyectogrupo == null)
            {
                return NotFound();
            }

            proyectogrupo.activo = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Proyectogrupos/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyectogrupo = await _context.Proyectogrupos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyectogrupo == null)
            {
                return NotFound();
            }

            proyectogrupo.activo = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        private bool ProyectogrupoExists(int id)
        {
            return _context.Proyectogrupos.Any(e => e.Id == id);
        }
    }
}
