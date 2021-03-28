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
    class ProyectogrupoMap : IEntityTypeConfiguration<Proyectogrupo>
    {
        public void Configure(EntityTypeBuilder<Proyectogrupo> builder)
        {
            builder.ToTable("proyectogrupos")
               .HasKey(u => u.Id);
            builder.HasOne(a => a.proyecto)
               .WithMany(d => d.proyectogrupos)
               .HasForeignKey(a => a.proyectoid);
            builder.HasOne(a => a.grupo)
                .WithMany(d => d.proyectogrupos)
                .HasForeignKey(a => a.grupoid);
        }
    }
}
