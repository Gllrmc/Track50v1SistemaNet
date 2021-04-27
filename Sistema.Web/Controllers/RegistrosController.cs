using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Sistema.Datos;
using Sistema.Entidades.Registros;
using Sistema.Web.Models.Registros;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RegistrosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Registros/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<RegistroViewModel>> Listar()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            //var miubicacion = geometryFactory.CreatePoint(new Coordinate(-69.938972, 18.481208));
            var registro = await _context
                .Registros
                .Include(p => p.proyecto)
                .Include(p => p.tarea)
                .Include(p => p.usuario)
                .ToListAsync();

            return registro.Select(a => new RegistroViewModel
            {
                Id = a.Id,
                actividad = a.actividad,
                usuarioid = a.usuarioid,
                email = a.usuario.email,
                userid = a.usuario.userid,
                proyectoid = a.proyectoid,
                proyecto = a.proyectoid.HasValue?a.proyecto.nombre:"",
                colfondo = a.proyecto.colfondo,
                coltexto = a.proyecto.coltexto,
                tareaid = a.tareaid,
                tarea = a.tareaid.HasValue?a.tarea.nombre:"",
                fecregistracion = a.fecregistracion,
                facturable = a.facturable,
                liquidable = a.liquidable,
                fhdesde = a.fhdesde,
                fhhasta = a.fhhasta,
                minutos = a.minutos,
                latdesde = a.geodesde.X,
                longdesde = a.geodesde.Y,
                lathasta = a.geohasta.X,
                longhasta = a.geohasta.Y,
                tarifa = a.tarifa,
                costo = a.costo,
                facturado = a.facturado,
                iduserfact = a.iduserfact,
                fhfact = a.fhfact,
                liquidado = a.liquidado,
                iduserliqui = a.iduserliqui,
                fhliqui = a.fhliqui,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });
        }

        // GET: api/Registros/Listaractivos
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<RegistroViewModel>> Listaractivos()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            //var miubicacion = geometryFactory.CreatePoint(new Coordinate(-69.938972, 18.481208));
            var registro = await _context
                .Registros
                .Include(p => p.proyecto)
                .Include(p => p.tarea)
                .Where(p => p.activo == true && p.facturado == false && p.liquidado == false)
                .OrderBy(p => p.usuarioid)
                .ThenBy(p => p.proyectoid)
                .ThenBy(p => p.tareaid)
                .ThenBy(p => p.fecregistracion)
                .ToListAsync();

            return registro.Select(a => new RegistroViewModel
            {
                Id = a.Id,
                actividad = a.actividad,
                usuarioid = a.usuarioid,
                proyectoid = a.proyectoid,
                proyecto = a.proyectoid.HasValue ? a.proyecto.nombre : "",
                colfondo = a.proyecto.colfondo,
                coltexto = a.proyecto.coltexto,
                tareaid = a.tareaid,
                tarea = a.tareaid.HasValue ? a.tarea.nombre : "",
                fecregistracion = a.fecregistracion,
                facturable = a.facturable,
                liquidable = a.liquidable,
                fhdesde = a.fhdesde,
                fhhasta = a.fhhasta,
                minutos = a.minutos,
                latdesde = a.geodesde.X,
                longdesde = a.geodesde.Y,
                lathasta = a.geohasta.X,
                longhasta = a.geohasta.Y,
                tarifa = a.tarifa,
                costo = a.costo,
                facturado = a.facturado,
                iduserfact = a.iduserfact,
                fhfact = a.fhfact,
                liquidado = a.liquidado,
                iduserliqui = a.iduserliqui,
                fhliqui = a.fhliqui,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });
        }


        // GET: api/Registros/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<RegistroSelectModel>> Select()
        {
            var registro = await _context.Registros
                .Include(p => p.proyecto)
                .Include(p => p.tarea)
                .Where(r => r.activo == true)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return registro.Select(a => new RegistroSelectModel
            {
                Id = a.Id,
                actividad = a.actividad,
                usuarioid = a.usuarioid,
                proyectoid = a.proyectoid,
                proyecto = a.proyecto.nombre,
                tareaid = a.tareaid,
                tarea = a.tareaid.HasValue ? a.tarea.nombre : "",
                fecregistracion = a.fecregistracion,
                facturable = a.facturable,
                liquidable = a.liquidable,
                fhdesde = a.fhdesde,
                fhhasta = a.fhhasta,
                minutos = a.minutos,
            });
        }

        // GET: api/Registros/Selectusuario/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<RegistroSelectModel>> Selectusuario([FromRoute] int id)
        {
            var registro = await _context.Registros
                .Include(p => p.proyecto)
                .Include(p => p.tarea)
                .Where(r => r.activo == true && r.usuarioid == id )
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return registro.Select(a => new RegistroSelectModel
            {
                Id = a.Id,
                actividad = a.actividad,
                usuarioid = a.usuarioid,
                proyectoid = a.proyectoid,
                proyecto = a.proyecto.nombre,
                tareaid = a.tareaid,
                tarea = a.tareaid.HasValue ? a.tarea.nombre : "",
                fecregistracion = a.fecregistracion,
                facturable = a.facturable,
                liquidable = a.liquidable,
                fhdesde = a.fhdesde,
                fhhasta = a.fhhasta,
                minutos = a.minutos,
            });
        }

        // GET: api/Registros/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var registro = await _context.Registros.FindAsync(id);

            if (registro == null)
            {
                return NotFound();
            }

            return Ok(new RegistroViewModel
            {
                Id = registro.Id,
                actividad = registro.actividad,
                usuarioid = registro.usuarioid,
                proyectoid = registro.proyectoid,
                tareaid = registro.tareaid,
                fecregistracion = registro.fecregistracion,
                facturable = registro.facturable,
                liquidable = registro.liquidable,
                fhdesde = registro.fhdesde,
                fhhasta = registro.fhhasta,
                minutos = registro.minutos,
                latdesde = registro.geodesde.X,
                longdesde = registro.geodesde.Y,
                lathasta = registro.geohasta.X,
                longhasta = registro.geohasta.Y,
                tarifa = registro.tarifa,
                costo = registro.costo,
                facturado = registro.facturado,
                iduserfact = registro.iduserfact,
                fhfact = registro.fhfact,
                liquidado = registro.liquidado,
                iduserliqui = registro.iduserliqui,
                fhliqui = registro.fhliqui,
                iduseralta = registro.iduseralta,
                fecalta = registro.fecalta,
                iduserumod = registro.iduserumod,
                fecumod = registro.fecumod,
                activo = registro.activo
            });
        }

        // PUT: api/Registros/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] RegistroUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var fechaHora = DateTime.Now;
            var registro = await _context.Registros
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (registro == null)
            {
                return NotFound();
            }

            registro.actividad = model.actividad;
            registro.usuarioid = model.usuarioid;
            registro.proyectoid = model.proyectoid;
            registro.tareaid = model.tareaid;
            registro.fecregistracion = model.fecregistracion;
            registro.facturable = model.facturable;
            registro.liquidable = model.liquidable;
            registro.fhdesde = model.fhdesde;
            registro.fhhasta = model.fhhasta;
            registro.minutos = model.minutos;
            registro.geodesde = geometryFactory.CreatePoint(new Coordinate(model.latdesde, model.longdesde));
            registro.geohasta = geometryFactory.CreatePoint(new Coordinate(model.lathasta, model.longhasta));
            registro.tarifa = model.tarifa;
            registro.costo = model.costo;
            registro.facturado = model.facturado;
            registro.iduserfact = model.iduserfact;
            registro.fhfact = model.fhfact;
            registro.liquidado = model.liquidado;
            registro.iduserliqui = model.iduserliqui;
            registro.fhliqui = model.fhliqui;
            registro.iduserumod = model.iduserumod;
            registro.fecumod = fechaHora;

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

        // POST: api/Registros/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] RegistroCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var fechaHora = DateTime.Now;
            Registro registro = new Registro
            {
                actividad = model.actividad,
                usuarioid = model.usuarioid,
                proyectoid = model.proyectoid,
                tareaid = model.tareaid,
                fecregistracion = model.fecregistracion,
                facturable = model.facturable,
                liquidable = model.liquidable,
                fhdesde = model.fhdesde,
                fhhasta = model.fhhasta,
                minutos = model.minutos,
                geodesde = geometryFactory.CreatePoint(new Coordinate(model.latdesde, model.longdesde)),
                geohasta = geometryFactory.CreatePoint(new Coordinate(model.lathasta, model.longhasta)),
                tarifa = model.tarifa,
                costo = model.costo,
                facturado = model.facturado,
                iduserfact = model.iduserfact,
                fhfact = model.fhfact,
                liquidado = model.liquidado,
                iduserliqui = model.iduserliqui,
                fhliqui = model.fhliqui,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Registros.Add(registro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(registro);
        }

        // DELETE: api/Registros/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registro = await _context.Registros
                .FindAsync(id);

            if (registro == null)
            {
                return NotFound();
            }

            _context.Registros.Remove(registro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(registro);
        }

        // PUT: api/Registros/Actualizarhoras
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarhoras([FromBody] RegistroHorasUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var fechaHora = DateTime.Now;
            var registro = await _context.Registros
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (registro == null)
            {
                return NotFound();
            }
            registro.fhdesde = model.fhdesde;
            registro.fhhasta = model.fhhasta;
            registro.minutos = model.minutos;
            registro.geodesde = geometryFactory.CreatePoint(new Coordinate(model.latdesde, model.longdesde));
            registro.geohasta = geometryFactory.CreatePoint(new Coordinate(model.lathasta, model.longhasta));
            registro.iduserumod = model.iduserumod;
            registro.fecumod = fechaHora;

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

        // PUT: api/Registros/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var registro = await _context.Registros
                .FirstOrDefaultAsync(c => c.Id == id);

            if (registro == null)
            {
                return NotFound();
            }

            registro.activo = false;

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

        // PUT: api/Registros/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var registro = await _context.Registros
                .FirstOrDefaultAsync(c => c.Id == id);

            if (registro == null)
            {
                return NotFound();
            }

            registro.activo = true;

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

        private bool RegistroExists(int id)
        {
            return _context.Registros.Any(e => e.Id == id);
        }
    }
}
