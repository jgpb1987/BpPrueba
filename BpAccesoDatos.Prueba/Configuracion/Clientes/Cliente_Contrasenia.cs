using BpNegocio.Prueba.Entidades.Clientes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Clientes
{
    public class Cliente_ContraseniaBaseDatos
    {
        public static void MapeoDatosCliente_Contrasenia(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente_Contrasenia>().Property(p => p.Id).HasColumnName("id").IsRequired();
            modelBuilder.Entity<Cliente_Contrasenia>().Property(p => p.ClienteId).HasColumnName("clienteId").IsRequired();
            modelBuilder.Entity<Cliente_Contrasenia>().Property(p => p.Contrasenia).HasColumnName("contrasenia").IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Cliente_Contrasenia>().Property(p => p.Estado).HasColumnName("estado").IsRequired();
            modelBuilder.Entity<Cliente_Contrasenia>().HasKey(p => p.Id);
            modelBuilder.Entity<Cliente_Contrasenia>().HasOne(p => p.Cliente).WithMany().HasForeignKey(o => o.ClienteId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cliente_Contrasenia>().ToTable("cliente_contrasenia", "cliente");
        }
    }
}
