using BpNegocio.Prueba.Entidades.Sujeto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Sujeto
{
    public class GeneroBaseDatos
    {
        public static void MapeoDatosGenero(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>().Property(p => p.Codigo).HasColumnName("codigo").IsRequired().HasMaxLength(1);
            modelBuilder.Entity<Genero>().Property(p => p.Descripcion).HasColumnName("descripcion").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Genero>().Property(p => p.Estado).HasColumnName("estado").IsRequired();
            modelBuilder.Entity<Genero>().HasKey(p => p.Codigo);
            modelBuilder.Entity<Genero>().ToTable("genero", "sujeto");
        }
    }
}
