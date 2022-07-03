using BpNegocio.Prueba.Entidades.Clientes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Clientes
{
    public class ClienteBaseDatos
    {
        public static void MapeoDatosCliente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().Property(p => p.Id).HasColumnName("id").IsRequired();
            modelBuilder.Entity<Cliente>().Property(p => p.PersonaId).HasColumnName("personaId").IsRequired();
            modelBuilder.Entity<Cliente>().Property(p => p.Fecha).HasColumnName("fecha").IsRequired();
            modelBuilder.Entity<Cliente>().Property(p => p.Estado).HasColumnName("estado").IsRequired();
            modelBuilder.Entity<Cliente>().HasKey(p => p.Id);
            modelBuilder.Entity<Cliente>().HasOne(p => p.Persona).WithMany().HasForeignKey(o => o.PersonaId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cliente>().ToTable("cliente", "cliente");
        }
    }
}
