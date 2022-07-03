using BpNegocio.Prueba.Entidades.Sujeto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Sujeto
{
    public class PersonaBaseDatos
    {
        public static void MapeoDatosPersona(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().Property(p => p.Id).HasColumnName("id").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.CodigoGenero).HasColumnName("codigoGenero").IsRequired().HasMaxLength(1);
            modelBuilder.Entity<Persona>().Property(p => p.Identificacion).HasColumnName("identificacion").IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Persona>().Property(p => p.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Persona>().Property(p => p.FechaNacimiento).HasColumnName("fechaNacimiento").IsRequired();
            modelBuilder.Entity<Persona>().Property(p => p.Direccion).HasColumnName("direccion").IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Persona>().Property(p => p.Telefono).HasColumnName("telefono").IsRequired().HasMaxLength(13);
            modelBuilder.Entity<Persona>().HasKey(p => p.Id);
            modelBuilder.Entity<Persona>().HasOne(p => p.Genero).WithMany().HasForeignKey(o => o.CodigoGenero).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Persona>().ToTable("persona", "sujeto");
        }
    }
}
