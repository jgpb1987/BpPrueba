using BpNegocio.Prueba.Entidades.Ahorro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Ahorro
{
    public class CuentaBaseDatos
    {
        public static void MapeoDatosCuenta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>().Property(p => p.Id).HasColumnName("id").IsRequired();
            modelBuilder.Entity<Cuenta>().Property(p => p.ClienteId).HasColumnName("clienteId").IsRequired();
            modelBuilder.Entity<Cuenta>().Property(p => p.CodigoTipoCuenta).HasColumnName("codigoTipoCuenta").IsRequired().HasMaxLength(5);
            modelBuilder.Entity<Cuenta>().Property(p => p.Numero).HasColumnName("numero").IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Cuenta>().Property(p => p.SaldoInicial).HasColumnName("saldoInicial").IsRequired();
            modelBuilder.Entity<Cuenta>().Property(p => p.Estado).HasColumnName("estado").IsRequired();
            modelBuilder.Entity<Cuenta>().HasKey(p => p.Id);
            modelBuilder.Entity<Cuenta>().HasOne(p => p.Cliente).WithMany().HasForeignKey(o => o.ClienteId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cuenta>().HasOne(p => p.TipoCuenta).WithMany().HasForeignKey(o => o.CodigoTipoCuenta).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cuenta>().ToTable("cuenta", "ahorro");
        }
    }
}
