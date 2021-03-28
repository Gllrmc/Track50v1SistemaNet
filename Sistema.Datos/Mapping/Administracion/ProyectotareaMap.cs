using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Administracion;

namespace Sistema.Datos.Mapping.Administracion
{
    class ProyectotareaMap : IEntityTypeConfiguration<Proyectotarea>
    {
        public void Configure(EntityTypeBuilder<Proyectotarea> builder)
        {
            builder.ToTable("proyectotareas")
               .HasKey(u => u.Id);
            builder.HasOne(a => a.proyecto)
               .WithMany(d => d.proyectotareas)
               .HasForeignKey(a => a.proyectoid);
            builder.HasOne(a => a.tarea)
                .WithMany(d => d.proyectotareas)
                .HasForeignKey(a => a.tareaid);
        }
    }
}
