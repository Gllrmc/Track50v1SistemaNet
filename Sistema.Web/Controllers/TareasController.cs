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
    public class TareasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public TareasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Tareas/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<TareaViewModel>> Listar()
        {
            var tarea = await _context
                .Tareas.ToListAsync();

            return tarea.Select(a => new TareaViewModel
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

        // GET: api/Tareas/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<TareaSelectModel>> Select()
        {
            var tarea = await _context.Tareas
                .Where(r => r.activo == true)
                .OrderBy(r => r.nombre)
                .ToListAsync();

            return tarea.Select(r => new TareaSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }

        // GET: api/Tareas/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return Ok(new TareaViewModel
            {
                Id = tarea.Id,
                nombre = tarea.nombre,
                iduseralta = tarea.iduseralta,
                fecalta = tarea.fecalta,
                iduserumod = tarea.iduserumod,
                fecumod = tarea.fecumod,
                activo = tarea.activo
            });
        }

        // PUT: api/Tareas/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] TareaUpdateModel model)
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
            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (tarea == null)
            {
                return NotFound();
            }

            tarea.nombre = model.nombre;
            tarea.iduserumod = model.iduserumod;
            tarea.fecumod = fechaHora;

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

        // POST: api/Tareas/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] TareaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Tarea tarea = new Tarea
            {
                nombre = model.nombre,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Tareas.Add(tarea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(tarea);
        }

        // DELETE: api/Tareas/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tarea = await _context.Tareas
                .FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(tarea);
        }

        // PUT: api/Tareas/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (tarea == null)
            {
                return NotFound();
            }

            tarea.activo = false;

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

        // PUT: api/Tareas/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (tarea == null)
            {
                return NotFound();
            }

            tarea.activo = true;

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

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }
    }
}
