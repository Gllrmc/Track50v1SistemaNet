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
    public class ProyectosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProyectosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Proyectos/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProyectoViewModel>> Listar()
        {
            var proyecto = await _context.Proyectos
                .Include(a => a.empresa)
                .Include(a => a.cliente)
                .Where(a => a.archivado == false)
                .ToListAsync();

            return proyecto.Select(a => new ProyectoViewModel
            {
                Id = a.Id,
                nombre = a.nombre,
                empresaid = a.empresaid,
                empresa = a.empresa.nombre,
                clienteid = a.clienteid,
                cliente = a.clienteid.HasValue ? a.cliente.nombre : "sin cliente",
                facturable = a.facturable,
                liquidable = a.liquidable,
                tarifadefault = a.tarifadefault,
                notas = a.notas,
                reservado = a.reservado,
                coltexto = a.coltexto,
                colfondo = a.colfondo,
                fecregdesde = a.fecregdesde,
                estimadohoras = a.estimadohoras,
                estimadomonto = a.estimadomonto,
                fecultfact = a.fecultfact,
                fecultliqui = a.fecultliqui,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Proyectos/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProyectoSelectModel>> Select()
        {
            var proyecto = await _context.Proyectos
                .Include(a => a.empresa)
                .Include(a => a.cliente)
                .Where(a => a.archivado == false && a.activo == true)
                .OrderBy(a => a.nombre)
                .AsNoTracking()
                .ToListAsync();

            return proyecto.Select(a => new ProyectoSelectModel
            {
                Id = a.Id,
                nombre = a.nombre,
                empresaid = a.empresaid,
                empresa = a.empresa.nombre, 
                clienteid = a.clienteid,
                cliente = a.clienteid.HasValue ? a.cliente.nombre : "<Sin Cliente>",
                facturable = a.facturable,
                liquidable = a.liquidable,
                tarifadefault = a.tarifadefault,
                notas = a.notas,
                reservado = a.reservado,
                coltexto = a.coltexto,
                colfondo = a.colfondo,
                fecregdesde = a.fecregdesde,
                estimadohoras = a.estimadohoras,
                estimadomonto = a.estimadomonto,
                fecultfact = a.fecultfact,
                fecultliqui = a.fecultliqui,
            });
        }

        // GET: api/Proyectos/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var proyecto = await _context.Proyectos
                .SingleOrDefaultAsync(a => a.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return Ok(new ProyectoViewModel
            {
                Id = proyecto.Id,
                nombre = proyecto.nombre,
                empresaid = proyecto.empresaid,
                clienteid = proyecto.clienteid,
                facturable = proyecto.facturable,
                liquidable = proyecto.liquidable,
                tarifadefault = proyecto.tarifadefault,
                notas = proyecto.notas,
                reservado = proyecto.reservado,
                coltexto = proyecto.coltexto,
                colfondo = proyecto.colfondo,
                fecregdesde = proyecto.fecregdesde,
                estimadohoras = proyecto.estimadohoras,
                estimadomonto = proyecto.estimadomonto,
                fecultfact = proyecto.fecultfact,
                fecultliqui = proyecto.fecultliqui,
                iduseralta = proyecto.iduseralta,
                fecalta = proyecto.fecalta,
                iduserumod = proyecto.iduserumod,
                fecumod = proyecto.fecumod,
                activo = proyecto.activo
            });
        }

        // PUT: api/Proyectos/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ProyectoUpdateModel model)
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
            var proyecto = await _context.Proyectos
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.nombre = model.nombre;
            proyecto.empresaid = model.empresaid;
            proyecto.clienteid = model.clienteid;
            proyecto.facturable = model.facturable;
            proyecto.liquidable = model.liquidable;
            proyecto.tarifadefault = model.tarifadefault;
            proyecto.notas = model.notas;
            proyecto.reservado = model.reservado;
            proyecto.coltexto = model.coltexto;
            proyecto.colfondo = model.colfondo;
            proyecto.fecregdesde = model.fecregdesde;
            proyecto.estimadohoras = model.estimadohoras;
            proyecto.estimadomonto = model.estimadomonto;
            proyecto.iduserumod = model.iduserumod;
            proyecto.fecumod = fechaHora;
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

        // POST: api/Proyectos/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ProyectoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Proyecto proyecto = new Proyecto
            {
                nombre = model.nombre,
                empresaid = model.empresaid,
                clienteid = model.clienteid,
                facturable = model.facturable,
                liquidable = model.liquidable,
                tarifadefault = model.tarifadefault,
                notas = model.notas,
                reservado = model.reservado,
                coltexto = model.coltexto,
                colfondo = model.colfondo,
                fecregdesde = model.fecregdesde,
                estimadohoras = model.estimadohoras,
                estimadomonto = model.estimadomonto,
                archivado = false,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Proyectos.Add(proyecto);
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

        // DELETE: api/Proyectos/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proyecto = await _context.Proyectos
                .FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(proyecto);
        }

        // PUT: api/Proyectos/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyecto = await _context
                .Proyectos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.activo = false;

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

        // PUT: api/Proyectos/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var proyecto = await _context
                .Proyectos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.activo = true;

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

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
