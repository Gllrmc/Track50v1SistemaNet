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
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public GruposController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Grupos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<GrupoViewModel>> Listar()
        {
            var grupo = await _context
                .Grupos.ToListAsync();

            return grupo.Select(a => new GrupoViewModel
            {

                Id = a.Id,
                nombre = a.nombre,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Grupos/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<GrupoSelectModel>> Select()
        {
            var grupo = await _context.Grupos
                .Where(r => r.activo == true)
                .OrderBy(r => r.nombre)
                .ToListAsync();

            return grupo.Select(r => new GrupoSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }

        // GET: api/Grupos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var grupo = await _context.Grupos.FindAsync(id);

            if (grupo == null)
            {
                return NotFound();
            }

            return Ok(new GrupoViewModel
            {
                Id = grupo.Id,
                nombre = grupo.nombre,
                iduseralta = grupo.iduseralta,
                fecalta = grupo.fecalta,
                iduserumod = grupo.iduserumod,
                fecumod = grupo.fecumod,
                activo = grupo.activo
            });
        }

        // PUT: api/Grupos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] GrupoUpdateModel model)
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
            var grupo = await _context.Grupos
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (grupo == null)
            {
                return NotFound();
            }

            grupo.nombre = model.nombre;
            grupo.iduserumod = model.iduserumod;
            grupo.fecumod = fechaHora;

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

        // POST: api/Grupos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] GrupoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Grupo grupo = new Grupo
            {
                nombre = model.nombre,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Grupos.Add(grupo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Grupos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grupo = await _context.Grupos
                .FindAsync(id);

            if (grupo == null)
            {
                return NotFound();
            }

            _context.Grupos.Remove(grupo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(grupo);
        }

        // PUT: api/Grupos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var grupo = await _context.Grupos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (grupo == null)
            {
                return NotFound();
            }

            grupo.activo = false;

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

        // PUT: api/Grupos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var grupo = await _context.Grupos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (grupo == null)
            {
                return NotFound();
            }

            grupo.activo = true;

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

        private bool GrupoExists(int id)
        {
            return _context.Grupos.Any(e => e.Id == id);
        }
    }
}
