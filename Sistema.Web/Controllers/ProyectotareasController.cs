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
    public class ProyectotareasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProyectotareasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Proyectotareas/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProyectotareaViewModel>> Listar()
        {
            var proyectotarea = await _context
                .Proyectotareas.ToListAsync();

            return proyectotarea.Select(a => new ProyectotareaViewModel
            {
                Id = a.Id,
                proyectoid = a.proyectoid,
                tareaid = a.tareaid,
                estimadohoras = a.estimadohoras,
                estimadomonto = a.estimadomonto,
                notas = a.notas,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Proyectotareas/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var proyectotarea = await _context.Proyectotareas.FindAsync(id);

            if (proyectotarea == null)
            {
                return NotFound();
            }

            return Ok(new ProyectotareaViewModel
            {
                Id = proyectotarea.Id,
                proyectoid = proyectotarea.proyectoid,
                tareaid = proyectotarea.tareaid,
                estimadohoras = proyectotarea.estimadohoras,
                estimadomonto = proyectotarea.estimadomonto,
                notas = proyectotarea.notas,
                iduseralta = proyectotarea.iduseralta,
                fecalta = proyectotarea.fecalta,
                iduserumod = proyectotarea.iduserumod,
                fecumod = proyectotarea.fecumod,
                activo = proyectotarea.activo
            });
        }

        // PUT: api/Proyectotareas/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ProyectotareaUpdateModel model)
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
            var proyectotarea = await _context.Proyectotareas
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (proyectotarea == null)
            {
                return NotFound();
            }
            proyectotarea.proyectoid = model.proyectoid;
            proyectotarea.tareaid = model.tareaid;
            proyectotarea.estimadomonto = model.estimadomonto;
            proyectotarea.estimadohoras = model.estimadohoras;
            proyectotarea.notas = model.notas;
            proyectotarea.iduserumod = model.iduserumod;
            proyectotarea.fecumod = fechaHora;

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

        // POST: api/Proyectotareas/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ProyectotareaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Proyectotarea proyectotarea = new Proyectotarea
            {
                proyectoid = model.proyectoid,
                tareaid = model.tareaid,
                estimadohoras = model.estimadohoras,
                estimadomonto = model.estimadomonto,
                notas = model.notas,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Proyectotareas.Add(proyectotarea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(proyectotarea);
        }

        // DELETE: api/Proyectotareas/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proyectotarea = await _context.Proyectotareas
                .FindAsync(id);

            if (proyectotarea == null)
            {
                return NotFound();
            }

            _context.Proyectotareas.Remove(proyectotarea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(proyectotarea);
        }

        // PUT: api/Proyectotareas/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyectotarea = await _context.Proyectotareas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyectotarea == null)
            {
                return NotFound();
            }

            proyectotarea.activo = false;

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

        // PUT: api/Proyectotareas/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyectotarea = await _context.Proyectotareas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyectotarea == null)
            {
                return NotFound();
            }

            proyectotarea.activo = true;

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

        private bool ProyectotareaExists(int id)
        {
            return _context.Proyectotareas.Any(e => e.Id == id);
        }
    }
}
