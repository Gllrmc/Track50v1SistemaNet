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
    public class EtiquetaregistrosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EtiquetaregistrosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Etiquetaregistros/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<EtiquetaregistroViewModel>> Listar()
        {
            var etiquetaregistro = await _context
                .Etiquetaregistros.ToListAsync();

            return etiquetaregistro.Select(a => new EtiquetaregistroViewModel
            {
                Id = a.Id,
                etiquetaid = a.etiquetaid,
                registroid = a.registroid,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Etiquetaregistros/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var etiquetaregistro = await _context.Etiquetaregistros.FindAsync(id);

            if (etiquetaregistro == null)
            {
                return NotFound();
            }

            return Ok(new EtiquetaregistroViewModel
            {
                Id = etiquetaregistro.Id,
                etiquetaid = etiquetaregistro.etiquetaid,
                registroid = etiquetaregistro.registroid,
                iduseralta = etiquetaregistro.iduseralta,
                fecalta = etiquetaregistro.fecalta,
                iduserumod = etiquetaregistro.iduserumod,
                fecumod = etiquetaregistro.fecumod,
                activo = etiquetaregistro.activo
            });
        }

        // PUT: api/Etiquetaregistros/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] EtiquetaregistroUpdateModel model)
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
            var etiquetaregistro = await _context.Etiquetaregistros
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (etiquetaregistro == null)
            {
                return NotFound();
            }
            etiquetaregistro.etiquetaid = model.etiquetaid;
            etiquetaregistro.registroid = model.registroid;
            etiquetaregistro.iduserumod = model.iduserumod;
            etiquetaregistro.fecumod = fechaHora;

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

        // POST: api/Etiquetaregistros/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] EtiquetaregistroCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Etiquetaregistro etiquetaregistro = new Etiquetaregistro
            {
                etiquetaid = model.etiquetaid,
                registroid = model.registroid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Etiquetaregistros.Add(etiquetaregistro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(etiquetaregistro);
        }

        // DELETE: api/Etiquetaregistros/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etiquetaregistro = await _context.Etiquetaregistros
                .FindAsync(id);

            if (etiquetaregistro == null)
            {
                return NotFound();
            }

            _context.Etiquetaregistros.Remove(etiquetaregistro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(etiquetaregistro);
        }

        // PUT: api/Etiquetaregistros/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var etiquetaregistro = await _context.Etiquetaregistros
                .FirstOrDefaultAsync(c => c.Id == id);

            if (etiquetaregistro == null)
            {
                return NotFound();
            }

            etiquetaregistro.activo = false;

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

        // PUT: api/Etiquetaregistros/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var etiquetaregistro = await _context.Etiquetaregistros
                .FirstOrDefaultAsync(c => c.Id == id);

            if (etiquetaregistro == null)
            {
                return NotFound();
            }

            etiquetaregistro.activo = true;

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

        private bool EtiquetaregistroExists(int id)
        {
            return _context.Etiquetaregistros.Any(e => e.Id == id);
        }
    }
}
