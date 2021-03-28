using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Registros
{
    class RegistroMap : IEntityTypeConfiguration<Registro>
    {
        public void Configure(EntityTypeBuilder<Registro> builder)
        {
            builder.ToTable("registros")
               .HasKey(r => r.Id);
            builder.HasOne(a => a.proyecto)
               .WithMany(d => d.registros)
               .HasForeignKey(a => a.proyectoid);
            builder.HasOne(a => a.usuario)
                .WithMany(d => d.registros)
                .HasForeignKey(a => a.usuarioid);
        }
    }
}
