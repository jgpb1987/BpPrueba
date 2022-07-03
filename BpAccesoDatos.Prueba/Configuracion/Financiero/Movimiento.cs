using BpNegocio.Prueba.Entidades.Financiero;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Financiero
{
    public class MovimientoBaseDatos
    {
        public static void MapeoDatosMovimiento(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movimiento>().Property(p => p.Id).HasColumnName("id").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(p => p.CuentaId).HasColumnName("cuentaId").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(p => p.FechaProceso).HasColumnName("fechaProceso").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(p => p.Fecha).HasColumnName("fecha").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(p => p.Valor).HasColumnName("valor").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(p => p.Saldo).HasColumnName("saldo").IsRequired();
            modelBuilder.Entity<Movimiento>().Property(p => p.EsDebito).HasColumnName("esDebito").IsRequired();
            modelBuilder.Entity<Movimiento>().HasKey(p => p.Id);
            modelBuilder.Entity<Movimiento>().HasOne(p => p.Cuenta).WithMany().HasForeignKey(o => o.CuentaId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimiento>().ToTable("movimiento", "financiero");
        }
    }
}
