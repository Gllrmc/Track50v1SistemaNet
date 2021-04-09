using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;

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

        // GET: api/Etiquetaregistros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etiquetaregistro>>> GetEtiquetaregistros()
        {
            return await _context.Etiquetaregistros.ToListAsync();
        }

        // GET: api/Etiquetaregistros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Etiquetaregistro>> GetEtiquetaregistro(int id)
        {
            var etiquetaregistro = await _context.Etiquetaregistros.FindAsync(id);

            if (etiquetaregistro == null)
            {
                return NotFound();
            }

            return etiquetaregistro;
        }

        // PUT: api/Etiquetaregistros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtiquetaregistro(int id, Etiquetaregistro etiquetaregistro)
        {
            if (id != etiquetaregistro.Id)
            {
                return BadRequest();
            }

            _context.Entry(etiquetaregistro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtiquetaregistroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Etiquetaregistros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Etiquetaregistro>> PostEtiquetaregistro(Etiquetaregistro etiquetaregistro)
        {
            _context.Etiquetaregistros.Add(etiquetaregistro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtiquetaregistro", new { id = etiquetaregistro.Id }, etiquetaregistro);
        }

        // DELETE: api/Etiquetaregistros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtiquetaregistro(int id)
        {
            var etiquetaregistro = await _context.Etiquetaregistros.FindAsync(id);
            if (etiquetaregistro == null)
            {
                return NotFound();
            }

            _context.Etiquetaregistros.Remove(etiquetaregistro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtiquetaregistroExists(int id)
        {
            return _context.Etiquetaregistros.Any(e => e.Id == id);
        }
    }
}
