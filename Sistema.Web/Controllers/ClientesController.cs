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
    public class ClientesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ClientesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Clientes/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ClienteViewModel>> Listar()
        {
            var cliente = await _context
                .Clientes.ToListAsync();

            return cliente.Select(a => new ClienteViewModel
            {

                Id = a.Id,
                nombre = a.nombre,
                iduseralta = a.iduseralta,
                tarifadefault = a.tarifadefault,
                logo = a.logo,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Clientes/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ClienteSelectModel>> Select()
        {
            var cliente = await _context.Clientes
                .Where(r => r.activo == true)
                .OrderBy(r => r.nombre)
                .ToListAsync();

            return cliente.Select(r => new ClienteSelectModel
            {
                Id = r.Id,
                nombre = r.nombre,
                tarifadefault = r.tarifadefault
            });
        }

        // GET: api/Clientes/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(new ClienteViewModel
            {
                Id = cliente.Id,
                nombre = cliente.nombre,
                logo = cliente.logo,
                tarifadefault = cliente.tarifadefault,
                iduseralta = cliente.iduseralta,
                fecalta = cliente.fecalta,
                iduserumod = cliente.iduserumod,
                fecumod = cliente.fecumod,
                activo = cliente.activo
            });
        }

        // PUT: api/Clientes/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ClienteUpdateModel model)
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
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.nombre = model.nombre;
            cliente.logo = model.logo;
            cliente.tarifadefault = model.tarifadefault;
            cliente.iduserumod = model.iduserumod;
            cliente.fecumod = fechaHora;

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

        // POST: api/Clientes/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ClienteCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Cliente cliente = new Cliente
            {
                nombre = model.nombre,
                iduseralta = model.iduseralta,
                logo = model.logo,
                tarifadefault = model.tarifadefault,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(cliente);
        }

        // DELETE: api/Clientes/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes
                .FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.activo = false;

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

        // PUT: api/Clientes/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.activo = true;

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

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
