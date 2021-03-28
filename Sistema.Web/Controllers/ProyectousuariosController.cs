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
    public class ProyectousuariosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProyectousuariosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Proyectousuarios/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProyectousuarioViewModel>> Listar()
        {
            var proyectousuario = await _context
                .Proyectousuarios.ToListAsync();

            return proyectousuario.Select(a => new ProyectousuarioViewModel
            {
                Id = a.Id,
                proyectoid = a.proyectoid,
                usuarioid = a.usuarioid,
                tarifaproyectousuario = a.tarifaproyectousuario,
                costoproyectousuario = a.costoproyectousuario,
                notas = a.notas,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Proyectousuarios/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var proyectousuario = await _context.Proyectousuarios.FindAsync(id);

            if (proyectousuario == null)
            {
                return NotFound();
            }

            return Ok(new ProyectousuarioViewModel
            {
                Id = proyectousuario.Id,
                proyectoid = proyectousuario.proyectoid,
                usuarioid = proyectousuario.usuarioid,
                tarifaproyectousuario = proyectousuario.tarifaproyectousuario,
                costoproyectousuario = proyectousuario.costoproyectousuario,
                notas = proyectousuario.notas,
                iduseralta = proyectousuario.iduseralta,
                fecalta = proyectousuario.fecalta,
                iduserumod = proyectousuario.iduserumod,
                fecumod = proyectousuario.fecumod,
                activo = proyectousuario.activo
            });
        }

        // PUT: api/Proyectousuarios/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ProyectousuarioUpdateModel model)
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
            var proyectousuario = await _context.Proyectousuarios
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (proyectousuario == null)
            {
                return NotFound();
            }
            proyectousuario.proyectoid = model.proyectoid;
            proyectousuario.usuarioid = model.usuarioid;
            proyectousuario.tarifaproyectousuario = model.tarifaproyectousuario;
            proyectousuario.costoproyectousuario = model.costoproyectousuario;
            proyectousuario.notas = model.notas;
            proyectousuario.iduserumod = model.iduserumod;
            proyectousuario.fecumod = fechaHora;

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

        // POST: api/Proyectousuarios/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ProyectousuarioCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Proyectousuario proyectousuario = new Proyectousuario
            {
                proyectoid = model.proyectoid,
                usuarioid = model.usuarioid,
                tarifaproyectousuario = model.tarifaproyectousuario,
                costoproyectousuario = model.costoproyectousuario,
                notas = model.notas,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Proyectousuarios.Add(proyectousuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(proyectousuario);
        }

        // DELETE: api/Proyectousuarios/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proyectousuario = await _context.Proyectousuarios
                .FindAsync(id);

            if (proyectousuario == null)
            {
                return NotFound();
            }

            _context.Proyectousuarios.Remove(proyectousuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(proyectousuario);
        }

        // PUT: api/Proyectousuarios/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyectousuario = await _context.Proyectousuarios
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyectousuario == null)
            {
                return NotFound();
            }

            proyectousuario.activo = false;

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

        // PUT: api/Proyectousuarios/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyectousuario = await _context.Proyectousuarios
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyectousuario == null)
            {
                return NotFound();
            }

            proyectousuario.activo = true;

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

        private bool ProyectousuarioExists(int id)
        {
            return _context.Proyectousuarios.Any(e => e.Id == id);
        }
    }
}
