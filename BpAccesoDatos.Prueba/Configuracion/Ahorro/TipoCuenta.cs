using BpNegocio.Prueba.Entidades.Ahorro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba.Configuracion.Ahorro
{
    public class TipoCuentaBaseDatos
    {
        public static void MapeoDatosTipoCuenta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoCuenta>().Property(p => p.Codigo).HasColumnName("codigo").IsRequired().HasMaxLength(5);
            modelBuilder.Entity<TipoCuenta>().Property(p => p.Descripcion).HasColumnName("descripcion").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoCuenta>().Property(p => p.Estado).HasColumnName("estado").IsRequired();
            modelBuilder.Entity<TipoCuenta>().HasKey(p => p.Codigo);
            modelBuilder.Entity<TipoCuenta>().ToTable("tipoCuenta", "ahorro");
        }
    }
}
