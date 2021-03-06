using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sistema.Datos;
using Sistema.Entidades.Usuarios;
using Sistema.Web.Models.Usuarios;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextSistema _context;
        private readonly IConfiguration _config;

        public UsuariosController(DbContextSistema context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Usuarios/Listar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios
                .Include(u => u.rol)
                .ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                Id = u.Id,
                rolId = u.rolId,
                rol = u.rol.nombre,
                userid = u.userid,
                iniciales = u.iniciales,
                telefono = u.telefono,
                email = u.email,
                password_hash = u.password_hash,
                reservado = u.reservado,
                colfondo = u.colfondo,
                coltexto = u.coltexto,
                imgusuario = u.imgusuario,
                primerahora = u.primerahora,
                lineaspag = u.lineaspag,
                pxch = u.pxch,
                iduseralta = u.iduseralta,
                fecalta = u.fecalta,
                iduserumod = u.iduserumod,
                fecumod = u.fecumod,
                activo = u.activo
            });

        }

        // GET: api/Usuarios/Listaractivos
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listaractivos()
        {
            var usuario = await _context.Usuarios
                .Where(u => u.activo == true)
                .Include(u => u.rol)
                .ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                Id = u.Id,
                rolId = u.rolId,
                rol = u.rol.nombre,
                userid = u.userid,
                iniciales = u.iniciales,
                telefono = u.telefono,
                email = u.email,
                password_hash = u.password_hash,
                reservado = u.reservado,
                colfondo = u.colfondo,
                coltexto = u.coltexto,
                imgusuario = u.imgusuario,
                primerahora = u.primerahora,
                lineaspag = u.lineaspag,
                pxch = u.pxch,
                activo = u.activo
            });

        }

        // GET: api/Usuarios/Selectall
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioSelectModel>> Selectall()
        {
            var usuario = await _context.Usuarios
                .ToListAsync();

            return usuario.Select(u => new UsuarioSelectModel
            {
                Id = u.Id,
                userid = u.userid,
                iniciales = u.iniciales,
                telefono = u.telefono,
                email = u.email,
                reservado = u.reservado,
                colfondo = u.colfondo,
                coltexto = u.coltexto,
                imgusuario = u.imgusuario,
                primerahora = u.primerahora,
                lineaspag = u.lineaspag,
                activo = u.activo
            });
        }

        // GET: api/Usuarios/Select
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioSelectModel>> Select()
        {
            var usuario = await _context.Usuarios
                .Where(u => u.activo == true)
                .ToListAsync();

            return usuario.Select(u => new UsuarioSelectModel
            {
                Id = u.Id,
                userid = u.userid,
                iniciales = u.iniciales,
                telefono = u.telefono,
                email = u.email,
                reservado = u.reservado,
                colfondo = u.colfondo,
                coltexto = u.coltexto,
                imgusuario = u.imgusuario,
                primerahora = u.primerahora,
                lineaspag = u.lineaspag,
                activo = u.activo
            });
        }

        // GET: api/Usuarios/Traer/1
//        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Liderproyecto,Consultor,Dataentry")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Traer([FromRoute] int id)
        {
            var usuario = await _context.Usuarios
                .FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok (new UsuarioSelectModel
            {
                Id = usuario.Id,
                userid = usuario.userid,
                iniciales = usuario.iniciales,
                telefono = usuario.telefono,
                email = usuario.email,
                reservado = usuario.reservado,
                colfondo = usuario.colfondo,
                coltexto = usuario.coltexto,
                imgusuario = usuario.imgusuario,
                primerahora = usuario.primerahora,
                lineaspag = usuario.lineaspag,
                activo = usuario.activo
            });
        }


        // POST: api/Usuarios/Crear
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] UsuarioCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.email == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            var fechaHora = DateTime.Now;
            Usuario usuario = new Usuario
            {
                rolId = model.rolId,
                userid = model.userid,
                iniciales = model.iniciales.ToUpper(),
                telefono = model.telefono,
                email = model.email.ToLower(),
                password_hash = passwordHash,
                password_salt = passwordSalt,
                reservado = model.reservado,
                colfondo = model.colfondo,
                coltexto= model.coltexto,
                imgusuario = model.imgusuario,
                primerahora = model.primerahora,
                lineaspag = model.lineaspag,
                pxch = model.pxch,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Usuarios.Add(usuario);
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


        // PUT: api/Usuarios/Actualizar
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] UsuarioUpdateModel model)
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
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == model.Id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.rolId = model.rolId;
            usuario.userid = model.userid;
            usuario.iniciales = model.iniciales.ToUpper();
            usuario.telefono = model.telefono;
            usuario.email = model.email.ToLower();
            usuario.reservado = model.reservado;
            usuario.colfondo = model.colfondo;
            usuario.coltexto = model.coltexto;
            usuario.imgusuario = model.imgusuario;
            usuario.primerahora = model.primerahora;
            usuario.lineaspag = model.lineaspag;
            usuario.pxch = model.pxch;
            usuario.iduserumod = model.iduserumod;
            usuario.fecumod = fechaHora;


            if (model.act_password == true)
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.password_hash = passwordHash;
                usuario.password_salt = passwordSalt;
            }

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

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        // PUT: api/Usuarios/Desactivar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.activo = false;

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

        // PUT: api/Usuarios/Activar/1
        [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.activo = true;

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

        // PUT: api/Usuarios/Pxch
        [HttpPut("[action]")]
        public async Task<IActionResult> Pxch(PxchUpdateModel model)
        {

            var fechaHora = DateTime.Now;
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == model.Id);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.oldpassword, usuario.password_hash, usuario.password_salt))
            {
                return NotFound();
            }

            usuario.pxch = false;
            usuario.iduserumod = model.Id;
            usuario.fecumod = fechaHora;
            CrearPasswordHash(model.newpassword, out byte[] passwordHash, out byte[] passwordSalt);
            usuario.password_hash = passwordHash;
            usuario.password_salt = passwordSalt;

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


        // PUT: api/Usuarios/Login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var email = model.email.ToLower();
            int rolId = -1;

            var usuario = await _context.Usuarios
                .Include(u => u.rol)
                .FirstOrDefaultAsync(u => u.email == email);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.password, usuario.password_hash, usuario.password_salt))
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.rol.nombre ),
                new Claim("idusuario", usuario.Id.ToString() ),
                new Claim("rol", usuario.rol.nombre ),
                new Claim("nombre", usuario.userid ),
                new Claim("pxch", usuario.pxch?"SI":"NO"),
            };

            rolId = usuario.rol.Id;

            var activerol = await _context.Roles
                .FirstOrDefaultAsync(u => u.Id == rolId);

            if (activerol.activo == false)
            {
                return BadRequest();
            }

            return Ok(
                    new { token = GenerarToken(claims) }
                );

        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }

        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
