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
    public class EtiquetaregistroMap : IEntityTypeConfiguration<Etiquetaregistro>
    {
        public void Configure(EntityTypeBuilder<Etiquetaregistro> builder)
        {
            builder.ToTable("etiquetaregistros")
               .HasKey(u => u.Id);
            builder.HasOne(a => a.etiqueta)
               .WithMany(d => d.etiquetaregistros)
               .HasForeignKey(a => a.etiquetaid);
            builder.HasOne(a => a.registro)
                .WithMany(d => d.etiquetaregistros)
                .HasForeignKey(a => a.registroid);
        }
    }
}
