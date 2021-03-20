using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Administracion;
using Sistema.Datos.Mapping.Maestros;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Entidades.Administracion;
using Sistema.Entidades.Maestros;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Grupousuario> Grupousuarios { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Appconfig> Appconfigs { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Provincia> Provincias { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new GrupoMap());
            modelBuilder.ApplyConfiguration(new GrupousuarioMap());
            modelBuilder.ApplyConfiguration(new EtiquetaMap());
            modelBuilder.ApplyConfiguration(new ProyectoMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new TareaMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new AppconfigMap());
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new ProvinciaMap());

        }
    }
}
