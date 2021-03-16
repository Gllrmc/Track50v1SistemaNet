using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Usuarios
{
    public class GrupousuarioMap : IEntityTypeConfiguration<Grupousuario>
    {
        public void Configure(EntityTypeBuilder<Grupousuario> builder)
        {
            builder.ToTable("grupousuarios")
               .HasKey(u => u.Id);
            builder.HasOne(a => a.usuario)
               .WithMany(d => d.grupousuarios)
               .HasForeignKey(a => a.usuarioid);
            builder.HasOne(a => a.grupo)
                .WithMany(d => d.grupousuarios)
                .HasForeignKey(a => a.grupoid);
        }
    }
}
