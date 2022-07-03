using BpNegocio.Prueba.Entidades.Ahorro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Ahorro
{
    public class MontoMaximoRetiro_TipoCuentaBaseDatos
    {
        public static void MapeoDatosMontoMaximoRetiro_TipoCuenta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MontoMaximoRetiro_TipoCuenta>().Property(p => p.Id).HasColumnName("id").IsRequired();
            modelBuilder.Entity<MontoMaximoRetiro_TipoCuenta>().Property(p => p.CodigoTipoCuenta).HasColumnName("codigoTipoCuenta").IsRequired().HasMaxLength(5);
            modelBuilder.Entity<MontoMaximoRetiro_TipoCuenta>().Property(p => p.Monto).HasColumnName("monto").IsRequired();
            modelBuilder.Entity<MontoMaximoRetiro_TipoCuenta>().Property(p => p.Estado).HasColumnName("estado").IsRequired();
            modelBuilder.Entity<MontoMaximoRetiro_TipoCuenta>().HasKey(p => p.Id);
            modelBuilder.Entity<MontoMaximoRetiro_TipoCuenta>().HasOne(p => p.TipoCuenta).WithMany().HasForeignKey(o => o.CodigoTipoCuenta).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MontoMaximoRetiro_TipoCuenta>().ToTable("montoMaximoRetiro_tipoCuenta", "ahorro");
        }
    }
}
