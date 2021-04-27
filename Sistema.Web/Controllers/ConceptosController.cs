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
    public class ConceptosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ConceptosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Conceptos/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConceptoViewModel>> Listar()
        {
            var concepto = await _context
                .Conceptos.ToListAsync();

            return concepto.Select(a => new ConceptoViewModel
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

        // GET: api/Conceptos/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConceptoSelectModel>> Select()
        {
            var concepto = await _context.Conceptos
                .Where(r => r.activo == true)
                .OrderBy(r => r.nombre)
                .ToListAsync();

            return concepto.Select(r => new ConceptoSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }

        // GET: api/Conceptos/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var concepto = await _context.Conceptos.FindAsync(id);

            if (concepto == null)
            {
                return NotFound();
            }

            return Ok(new ConceptoViewModel
            {
                Id = concepto.Id,
                nombre = concepto.nombre,
                iduseralta = concepto.iduseralta,
                fecalta = concepto.fecalta,
                iduserumod = concepto.iduserumod,
                fecumod = concepto.fecumod,
                activo = concepto.activo
            });
        }

        // PUT: api/Conceptos/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ConceptoUpdateModel model)
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
            var concepto = await _context.Conceptos
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (concepto == null)
            {
                return NotFound();
            }

            concepto.nombre = model.nombre;
            concepto.iduserumod = model.iduserumod;
            concepto.fecumod = fechaHora;

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

        // POST: api/Conceptos/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ConceptoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Concepto concepto = new Concepto
            {
                nombre = model.nombre,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Conceptos.Add(concepto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(concepto);
        }

        // DELETE: api/Conceptos/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concepto = await _context.Conceptos
                .FindAsync(id);

            if (concepto == null)
            {
                return NotFound();
            }

            _context.Conceptos.Remove(concepto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(concepto);
        }

        // PUT: api/Conceptos/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var concepto = await _context.Conceptos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (concepto == null)
            {
                return NotFound();
            }

            concepto.activo = false;

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

        // PUT: api/Conceptos/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var concepto = await _context.Conceptos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (concepto == null)
            {
                return NotFound();
            }

            concepto.activo = true;

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

        private bool ConceptogralExists(int id)
        {
            return _context.Conceptos.Any(e => e.Id == id);
        }
    }
}
