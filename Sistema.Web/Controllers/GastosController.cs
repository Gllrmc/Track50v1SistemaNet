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
    public class GastosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public GastosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Gastos/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GastoViewModel>> Listar()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            //var miubicacion = geometryFactory.CreatePoint(new Coordinate(-69.938972, 18.481208));
            var gasto = await _context
                .Gastos
                .Include(p => p.proyecto)
                .Include(p => p.usuario)
                .Include(p => p.registro)
                .Include(p => p.tarea)
                .Include(p => p.concepto)
                .ToListAsync();

            return gasto.Select(a => new GastoViewModel
            {
                Id = a.Id,
                usuarioid = a.usuarioid,
                email = a.usuario.email,
                userid = a.usuario.userid,
                registroid = a.registroid,
                actividad = a.registroid.HasValue ? a.registro.actividad : "",
                proyectoid = a.proyectoid,
                proyecto = a.proyecto.nombre,
                coltexto = a.proyecto.coltexto,
                colfondo = a.proyecto.colfondo,
                tareaid = a.tareaid,
                tarea = a.tareaid.HasValue ? a.tarea.nombre : "",
                conceptoid = a.conceptoid,
                concepto = a.concepto.nombre,
                fecgasto = a.fecgasto,
                impneto = a.impneto,
                impiva = a.impiva,
                impivaper = a.impivaper,
                impiibb = a.impiibb,
                impotros = a.impotros,
                facturable = a.facturable,
                liquidable = a.liquidable,
                referencia = a.referencia,
                notas = a.notas,
                autorizado = a.autorizado,
                iduserauto = a.iduserauto,
                fhauto = a.fhauto,
                facturado = a.facturado,
                iduserfact = a.iduserfact,
                fhfact = a.fhfact,
                liquidado = a.liquidado,
                iduserliqui = a.iduserliqui,
                fhliqui = a.fhliqui,
                pdfid = a.pdfid,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                latalta = a.geoalta.X,
                longalta = a.geoalta.Y,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                latumod = a.geoumod.X,
                longumod = a.geoumod.Y,
                activo = a.activo
            });
        }


        // GET: api/Gastos/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GastoSelectModel>> Select()
        {
            var gasto = await _context.Gastos
                .Include(p => p.proyecto)
                .Include(p => p.usuario)
                .Include(p => p.registro)
                .Include(p => p.concepto)
                .Where(r => r.activo == true)
                .OrderBy(r => r.Id)
                .ToListAsync();

            return gasto.Select(a => new GastoSelectModel
            {
                Id = a.Id,
                usuarioid = a.usuarioid,
                email = a.usuario.email,
                userid = a.usuario.userid,
                registroid = a.registroid,
                actividad = a.registroid.HasValue ? a.registro.actividad : "",
                proyectoid = a.proyectoid,
                proyecto = a.proyecto.nombre,
                tareaid = a.tareaid,
                tarea = a.tareaid.HasValue ? a.tarea.nombre : "",
                conceptoid = a.conceptoid,
                concepto = a.concepto.nombre,
                fecgasto = a.fecgasto,
                impneto = a.impneto
            });
        }

        // GET: api/Gastos/Mostrar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var gasto = await _context.Gastos.FindAsync(id);

            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(new GastoViewModel
            {
                Id = gasto.Id,
                usuarioid = gasto.usuarioid,
                email = gasto.usuario.email,
                userid = gasto.usuario.userid,
                registroid = gasto.registroid,
                actividad = gasto.registroid.HasValue ? gasto.registro.actividad : "",
                proyectoid = gasto.proyectoid,
                proyecto = gasto.proyecto.nombre,
                tareaid = gasto.tareaid,
                tarea = gasto.tareaid.HasValue ? gasto.tarea.nombre : "",
                conceptoid = gasto.conceptoid,
                concepto = gasto.concepto.nombre,
                fecgasto = gasto.fecgasto,
                impneto = gasto.impneto,
                impiva = gasto.impiva,
                impivaper = gasto.impivaper,
                impiibb = gasto.impiibb,
                impotros = gasto.impotros,
                facturable = gasto.facturable,
                liquidable = gasto.liquidable,
                referencia = gasto.referencia,
                notas = gasto.notas,
                autorizado = gasto.autorizado,
                iduserauto = gasto.iduserauto,
                fhauto = gasto.fhauto,
                facturado = gasto.facturado,
                iduserfact = gasto.iduserfact,
                fhfact = gasto.fhfact,
                liquidado = gasto.liquidado,
                iduserliqui = gasto.iduserliqui,
                fhliqui = gasto.fhliqui,
                pdfid = gasto.pdfid,
                iduseralta = gasto.iduseralta,
                fecalta = gasto.fecalta,
                latalta = gasto.geoalta.X,
                longalta = gasto.geoalta.Y,
                iduserumod = gasto.iduserumod,
                fecumod = gasto.fecumod,
                latumod = gasto.geoumod.X,
                longumod = gasto.geoumod.Y,
                activo = gasto.activo
            });
        }

        // PUT: api/Gastos/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] GastoUpdateModel model)
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
            var gasto = await _context.Gastos
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (gasto == null)
            {
                return NotFound();
            }

            gasto.usuarioid = model.usuarioid;
            gasto.registroid = model.registroid;
            gasto.proyectoid = model.proyectoid;
            gasto.tareaid = model.tareaid;
            gasto.conceptoid = model.conceptoid;
            gasto.fecgasto = model.fecgasto;
            gasto.referencia = model.referencia;
            gasto.impneto = model.impneto;
            gasto.impiva = model.impiva;
            gasto.impivaper = model.impivaper;
            gasto.impiibb = model.impiibb;
            gasto.impotros = model.impotros;
            gasto.facturable = model.facturable;
            gasto.liquidable = model.liquidable;
            gasto.notas = model.notas;
            gasto.autorizado = model.autorizado;
            gasto.iduserauto = model.iduserauto;
            gasto.fhauto = model.fhauto;
            gasto.facturado = model.facturado;
            gasto.iduserfact = model.iduserfact;
            gasto.fhfact = model.fhfact;
            gasto.liquidado = model.liquidado;
            gasto.iduserliqui = model.iduserliqui;
            gasto.fhliqui = model.fhliqui;
            gasto.pdfid = model.pdfid;
            gasto.iduserumod = model.iduserumod;
            gasto.fecumod = fechaHora;
            gasto.geoumod = geometryFactory.CreatePoint(new Coordinate(model.latumod, model.longumod));

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

        // POST: api/Gastos/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] GastoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var fechaHora = DateTime.Now;
            Gasto gasto = new Gasto
            {
                usuarioid = model.usuarioid,
                registroid = model.registroid,
                proyectoid = model.proyectoid,
                tareaid = model.tareaid,
                conceptoid = model.conceptoid,
                fecgasto = model.fecgasto,
                impneto = model.impneto,
                impiva = model.impiva,
                impivaper = model.impivaper,
                impiibb = model.impiibb,
                impotros = model.impotros,
                facturable = model.facturable,
                liquidable = model.liquidable,
                referencia = model.referencia,
                notas = model.notas,
                autorizado = model.autorizado,
                iduserauto = model.iduserauto,
                fhauto = model.fhauto,
                facturado = model.facturado,
                iduserfact = model.iduserfact,
                fhfact = model.fhfact,
                liquidado = model.liquidado,
                iduserliqui = model.iduserliqui,
                fhliqui = model.fhliqui,
                pdfid = model.pdfid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                geoalta = geometryFactory.CreatePoint(new Coordinate(model.latalta, model.longalta)),
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                geoumod = geometryFactory.CreatePoint(new Coordinate(model.latalta, model.longalta)),
                activo = true
            };

            _context.Gastos.Add(gasto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(gasto);
        }

        // DELETE: api/Gastos/Eliminar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gasto = await _context.Gastos
                .FindAsync(id);

            if (gasto == null)
            {
                return NotFound();
            }

            _context.Gastos.Remove(gasto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(gasto);
        }


        // PUT: api/Gastos/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var gasto = await _context.Gastos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (gasto == null)
            {
                return NotFound();
            }

            gasto.activo = false;

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

        // PUT: api/Gastos/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var gasto = await _context.Gastos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (gasto == null)
            {
                return NotFound();
            }

            gasto.activo = true;

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

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }
    }
}
