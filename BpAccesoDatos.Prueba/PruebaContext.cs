using BpNegocio.Prueba.Entidades.Ahorro;
using BpNegocio.Prueba.Entidades.Clientes;
using BpNegocio.Prueba.Entidades.Financiero;
using BpNegocio.Prueba.Entidades.Sujeto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpAccesoDatos.Prueba
{
    public class PruebaContext : DbContext
    {
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options)
        { }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Persona> Personas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cliente_Contrasenia> Cliente_Contrasenias { get; set; }

        public DbSet<TipoCuenta> TipoCuentas { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<MontoMaximoRetiro_TipoCuenta> MontoMaximoRetiro_TipoCuentas { get; set; }

        public DbSet<Movimiento> Movimientos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Configuracion.Sujeto.GeneroBaseDatos.MapeoDatosGenero(modelBuilder);
            Configuracion.Sujeto.PersonaBaseDatos.MapeoDatosPersona(modelBuilder);

            Configuracion.Clientes.ClienteBaseDatos.MapeoDatosCliente(modelBuilder);
            Configuracion.Clientes.Cliente_ContraseniaBaseDatos.MapeoDatosCliente_Contrasenia(modelBuilder);
        
            Configuracion.Ahorro.TipoCuentaBaseDatos.MapeoDatosTipoCuenta(modelBuilder);
            Configuracion.Ahorro.CuentaBaseDatos.MapeoDatosCuenta(modelBuilder);
            Configuracion.Ahorro.MontoMaximoRetiro_TipoCuentaBaseDatos.MapeoDatosMontoMaximoRetiro_TipoCuenta(modelBuilder);    

            Configuracion.Financiero.MovimientoBaseDatos.MapeoDatosMovimiento(modelBuilder);
        }
    }
}
