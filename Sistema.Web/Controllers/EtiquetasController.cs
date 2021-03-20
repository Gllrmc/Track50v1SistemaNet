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
    public class EtiquetasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EtiquetasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Etiquetas/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<EtiquetaViewModel>> Listar()
        {
            var etiqueta = await _context
                .Etiquetas.ToListAsync();

            return etiqueta.Select(a => new EtiquetaViewModel
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

        // GET: api/Etiquetas/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<EtiquetaSelectModel>> Select()
        {
            var etiqueta = await _context.Etiquetas
                .Where(r => r.activo == true)
                .OrderBy(r => r.nombre)
                .ToListAsync();

            return etiqueta.Select(r => new EtiquetaSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }

        // GET: api/Etiquetas/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var etiqueta = await _context.Etiquetas.FindAsync(id);

            if (etiqueta == null)
            {
                return NotFound();
            }

            return Ok(new EtiquetaViewModel
            {
                Id = etiqueta.Id,
                nombre = etiqueta.nombre,
                iduseralta = etiqueta.iduseralta,
                fecalta = etiqueta.fecalta,
                iduserumod = etiqueta.iduserumod,
                fecumod = etiqueta.fecumod,
                activo = etiqueta.activo
            });
        }

        // PUT: api/Etiquetas/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] EtiquetaUpdateModel model)
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
            var etiqueta = await _context.Etiquetas
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (etiqueta == null)
            {
                return NotFound();
            }

            etiqueta.nombre = model.nombre;
            etiqueta.iduserumod = model.iduserumod;
            etiqueta.fecumod = fechaHora;

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

        // POST: api/Etiquetas/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] EtiquetaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Etiqueta etiqueta = new Etiqueta
            {
                nombre = model.nombre,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Etiquetas.Add(etiqueta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(etiqueta);
        }

        // DELETE: api/Etiquetas/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etiqueta = await _context.Etiquetas
                .FindAsync(id);

            if (etiqueta == null)
            {
                return NotFound();
            }

            _context.Etiquetas.Remove(etiqueta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(etiqueta);
        }

        // PUT: api/Etiquetas/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var etiqueta = await _context.Etiquetas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (etiqueta == null)
            {
                return NotFound();
            }

            etiqueta.activo = false;

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

        // PUT: api/Etiquetas/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var etiqueta = await _context.Etiquetas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (etiqueta == null)
            {
                return NotFound();
            }

            etiqueta.activo = true;

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

        private bool EtiquetaExists(int id)
        {
            return _context.Etiquetas.Any(e => e.Id == id);
        }
    }
}
