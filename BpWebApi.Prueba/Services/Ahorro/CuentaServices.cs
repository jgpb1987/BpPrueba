using BpAccesoDatos.Prueba;
using BpNegocio.Base;
using BpNegocio.Prueba.Dto.Ahorro;
using BpNegocio.Prueba.Entidades.Ahorro;
using BpWebApi.Prueba.Services.Clientes;
using Microsoft.EntityFrameworkCore;

namespace BpWebApi.Prueba.Services.Ahorro
{
    public class CuentaServices
    {
        private readonly ILogger<CuentaServices> logger;
        private readonly ClienteServices clientesServices;

        public CuentaServices(ILogger<CuentaServices> logger, ClienteServices clientesServices)
        {
            this.logger = logger;
            this.clientesServices = clientesServices;
        }

        public DtoResultCuenta CrearCuenta(PruebaContext contexto, DtoParamCrearCuenta parametro)
        {
            DtoResultCuenta resultado = new();
            Cuenta nuevaCuena;
            using var transaction = contexto.Database.BeginTransaction();
            try
            {
                var tipoCuenta = contexto.TipoCuentas.Where(p => p.Codigo == parametro.CodigoTipoCuenta && p.Estado).FirstOrDefault();
                if (tipoCuenta == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No existe o esta deshabilitado el código de tipo de cuenta enviado: {1}", "CCUE001", parametro.CodigoTipoCuenta);

                var persona = clientesServices.BuscarCliente(contexto, parametro.Identificacion, true);
                var cliente = contexto.Clientes.Where(p => p.PersonaId == persona.Id).FirstOrDefault();

                var cuenta = contexto.Cuentas.Where(p => p.Numero == parametro.Numero).FirstOrDefault();
                if(cuenta!= null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La cuenta ingresada ya se encuentra creada: {1}", "CCUE001", parametro.Numero);

                nuevaCuena = new Cuenta()
                { 
                    ClienteId=cliente.Id,
                    CodigoTipoCuenta=parametro.CodigoTipoCuenta,
                    Numero=parametro.Numero,
                    SaldoInicial=parametro.SaldoInicial,
                    Estado=parametro.Estado
                };
                contexto.Cuentas.Add(nuevaCuena);
                contexto.SaveChanges();
                transaction.Commit();
                resultado = BuscarCuenta(contexto, nuevaCuena.Numero);
            }
            catch (SistemaExcepcion sex)
            {
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return resultado;
        }
        public DtoResultCuenta BuscarCuenta(PruebaContext contexto, string numero)
        {
            DtoResultCuenta resultado = new();
            try
            {
                var cuenta = contexto.Cuentas.Include(o => o.Cliente.Persona).Where(p => p.Numero == numero).FirstOrDefault();
                if (cuenta == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La cuenta ingresada no existe: {1}", "BCUE001", numero);

                resultado = new DtoResultCuenta()
                {
                    Id = cuenta.Id,
                    Identificacion=cuenta.Cliente.Persona.Identificacion,
                    Nombre=cuenta.Cliente.Persona.Nombre,
                    CodigoTipoCuenta=cuenta.CodigoTipoCuenta,
                    Numero=cuenta.Numero,
                    SaldoInicial=cuenta.SaldoInicial,
                    Estado=cuenta.Estado
                };
            }
            catch (SistemaExcepcion sex)
            {
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return resultado;
        }

        public DtoResultCuenta ActualizarCuenta(PruebaContext contexto, int id, DtoParamActualizarCuenta parametro)
        {
            DtoResultCuenta resultado = new();
            using var transaction = contexto.Database.BeginTransaction();
            try
            {
                var cuenta = contexto.Cuentas.Where(p => p.Id == id).FirstOrDefault();
                if (cuenta == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La cuenta ingresada no existe: {1}", "ACUE001", id.ToString());

                var cliente = contexto.Clientes.Include(o => o.Persona).Where(p => p.Persona.Identificacion == parametro.Identificacion && p.Estado).FirstOrDefault();
                if (cliente == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La identificación ingresada no existe o esta desactivada: {1}", "ACUE002", parametro.Identificacion);

                var cuentaActualizar = contexto.Cuentas.Where(p => p.Numero == parametro.Numero).FirstOrDefault();
                if (cuentaActualizar == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La cuenta que desea actualizar ya se encuentra asignada a un cliente: {1}", "ACUE003", parametro.Numero);

                cuenta.ClienteId = cliente.Id;
                cuenta.CodigoTipoCuenta = parametro.CodigoTipoCuenta;
                cuenta.Numero=parametro.Numero;
                cuenta.Estado= parametro.Estado;

                contexto.SaveChanges();
                transaction.Commit();
                resultado = BuscarCuenta(contexto, parametro.Numero);

            }
            catch (SistemaExcepcion sex)
            {
                transaction.Rollback();
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return resultado;
        }
        public ResultadoBase EliminarCuenta(PruebaContext contexto, DtoParamEliminarCuenta parametro)
        {
            ResultadoBase resultado = new();
            using var transaction = contexto.Database.BeginTransaction();
            try
            {
                var cuenta= contexto.Cuentas.Where(p => p.Numero == parametro.Numero).FirstOrDefault();
                if (cuenta == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | El numero de cuenta que desea eliminar no existe: {1}", "ECUE001", parametro.Numero);

                var movimientos = contexto.Movimientos.Where(p => p.CuentaId == cuenta.Id).ToList();
                if (movimientos != null)
                {
                    contexto.RemoveRange(movimientos);
                    contexto.SaveChanges();
                }

                contexto.Remove(cuenta);
                contexto.SaveChanges();
                transaction.Commit();
                resultado = new ResultadoBase() { Mensaje=new ResultadoMensaje() };

            }
            catch (SistemaExcepcion sex)
            {
                transaction.Rollback();
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return resultado;
        }
    }
}
