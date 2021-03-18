using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Usuarios;
using Sistema.Web.Models.Usuarios;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupousuariosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public GrupousuariosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Grupousuarios/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GrupousuarioViewModel>> Listar()
        {
            var grupousuario = await _context
                .Grupousuarios.ToListAsync();

            return grupousuario.Select(a => new GrupousuarioViewModel
            {

                Id = a.Id,
                usuarioid = a.usuarioid,
                grupoid = a.grupoid,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Grupousuarios/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var grupousuario = await _context.Grupousuarios.FindAsync(id);

            if (grupousuario == null)
            {
                return NotFound();
            }

            return Ok(new GrupousuarioViewModel
            {
                Id = grupousuario.Id,
                grupoid = grupousuario.grupoid,
                usuarioid = grupousuario.usuarioid,
                iduseralta = grupousuario.iduseralta,
                fecalta = grupousuario.fecalta,
                iduserumod = grupousuario.iduserumod,
                fecumod = grupousuario.fecumod,
                activo = grupousuario.activo
            });
        }

        // PUT: api/Grupousuarios/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] GrupousuarioUpdateModel model)
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
            var grupousuario = await _context.Grupousuarios
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (grupousuario == null)
            {
                return NotFound();
            }

            grupousuario.grupoid = model.grupoid;
            grupousuario.usuarioid = model.usuarioid;
            grupousuario.iduserumod = model.iduserumod;
            grupousuario.fecumod = fechaHora;

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

        // POST: api/Grupousuarios/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] GrupousuarioCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Grupousuario grupousuario = new Grupousuario
            {
                grupoid = model.grupoid,
                usuarioid = model.usuarioid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Grupousuarios.Add(grupousuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(grupousuario);
        }

        // DELETE: api/Grupousuarios/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grupousuario = await _context.Grupousuarios
                .FindAsync(id);

            if (grupousuario == null)
            {
                return NotFound();
            }

            _context.Grupousuarios.Remove(grupousuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(grupousuario);
        }

        // PUT: api/Grupousuarios/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var grupousuario = await _context.Grupousuarios
                .FirstOrDefaultAsync(c => c.Id == id);

            if (grupousuario == null)
            {
                return NotFound();
            }

            grupousuario.activo = false;

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

        // PUT: api/Grupousuarios/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var grupousuario = await _context.Grupousuarios
                .FirstOrDefaultAsync(c => c.Id == id);

            if (grupousuario == null)
            {
                return NotFound();
            }

            grupousuario.activo = true;

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

        private bool GrupousuarioExists(int id)
        {
            return _context.Grupousuarios.Any(e => e.Id == id);
        }
    }
}
