using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Administracion
{
    public class ProyectousuarioMap : IEntityTypeConfiguration<Proyectousuario>
    {
        public void Configure(EntityTypeBuilder<Proyectousuario> builder)
        {
            builder.ToTable("proyectousuarios")
               .HasKey(u => u.Id);
            builder.HasOne(a => a.proyecto)
               .WithMany(d => d.proyectousuarios)
               .HasForeignKey(a => a.proyectoid);
            builder.HasOne(a => a.usuario)
                .WithMany(d => d.proyectousuarios)
                .HasForeignKey(a => a.usuarioid);
        }
    }
}
