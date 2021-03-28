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
    public class TarearegistroMap : IEntityTypeConfiguration<Tarearegistro>
    {
        public void Configure(EntityTypeBuilder<Tarearegistro> builder)
        {
            builder.ToTable("tarearegistros")
               .HasKey(u => u.Id);
            builder.HasOne(a => a.tarea)
               .WithMany(d => d.tarearegistros)
               .HasForeignKey(a => a.tareaid);
            builder.HasOne(a => a.registro)
                .WithMany(d => d.tarearegistros)
                .HasForeignKey(a => a.registroid);
        }
    }
}
