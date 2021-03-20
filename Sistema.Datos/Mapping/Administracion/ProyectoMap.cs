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
    public class ProyectoMap : IEntityTypeConfiguration<Proyecto>
    {
        public void Configure(EntityTypeBuilder<Proyecto> builder)
        {
            builder.ToTable("proyectos")
               .HasKey(r => r.Id);
            builder.HasIndex(a => new { a.nombre })
                .IsUnique(true);
            builder.HasOne(a => a.empresa)
                .WithMany(d => d.proyectos)
                .HasForeignKey(a => a.empresaid);
            builder.HasOne(a => a.cliente)
                .WithMany(d => d.proyectos)
                .HasForeignKey(a => a.clienteid);
        }
    }
}
