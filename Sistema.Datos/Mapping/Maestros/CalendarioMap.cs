using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Maestros
{
    public class CalendarioMap : IEntityTypeConfiguration<Calendario>
    {
        void IEntityTypeConfiguration<Calendario>.Configure(EntityTypeBuilder<Calendario> builder)
        {
            builder.ToTable("calendarios")
            .HasKey(u => u.Id);
        }
    }
}
