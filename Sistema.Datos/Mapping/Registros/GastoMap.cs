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
    public class GastoMap : IEntityTypeConfiguration<Gasto>
    {
        public void Configure(EntityTypeBuilder<Gasto> builder)
        {
            builder.ToTable("gastos")
               .HasKey(r => r.Id);
            builder.HasOne(a => a.proyecto)
               .WithMany(d => d.gastos)
               .HasForeignKey(a => a.proyectoid);
            builder.HasOne(a => a.usuario)
                .WithMany(d => d.gastos)
                .HasForeignKey(a => a.usuarioid);
            builder.HasOne(a => a.registro)
                .WithMany(d => d.gastos)
                .HasForeignKey(a => a.registroid);
            builder.HasOne(a => a.tarea)
                .WithMany(d => d.gastos)
                .HasForeignKey(a => a.tareaid);
            builder.HasOne(a => a.concepto)
                .WithMany(d => d.gastos)
                .HasForeignKey(a => a.conceptoid);
        }
    }
}
