using AutoMapper;
using BpAccesoDatos.Prueba;
using BpNegocio.Base;
using BpNegocio.Prueba.Dto.Ahorro;
using BpNegocio.Prueba.Dto.Financiero;
using BpNegocio.Prueba.Entidades.Financiero;
using Microsoft.EntityFrameworkCore;

namespace BpWebApi.Prueba.Services.Financiero
{
    public class MovimientoServices
    {
        private readonly ILogger<MovimientoServices> logger;
        private readonly IMapper mapper;

        public MovimientoServices(ILogger<MovimientoServices> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public DtoResultMovimiento CrearMovimiento(PruebaContext contexto, DtoParamCrearMovimiento parametro)
        {
            DtoResultMovimiento resultado = new();
            Movimiento nuevoMovimiento;
            decimal calculo = 0;
            using var transaction = contexto.Database.BeginTransaction();
            try
            {
                if(parametro.Valor==0)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No es posible ejecutar la transacción. El valor debe ser diferente de Cero, cuenta: {1}", "CMOV001", parametro.Numero);

                var cuenta = contexto.Cuentas.Where(p => p.Numero == parametro.Numero).FirstOrDefault();
                if (cuenta == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La cuenta ingresada no existe: {1}", "CMOV001", parametro.Numero);

                var montoMaximo = contexto.MontoMaximoRetiro_TipoCuentas.Where(p=> p.CodigoTipoCuenta==cuenta.CodigoTipoCuenta && p.Estado).FirstOrDefault();
                if(montoMaximo==null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No se encuentra parametrizado o esta desactivado el monto máximo para el tipo de cuenta : {1}", "CMOV002", cuenta.CodigoTipoCuenta);

                var movimiento = contexto.Movimientos.Include(o => o.Cuenta).Where(p => p.Cuenta.Numero == parametro.Numero).OrderByDescending(p => p.Id).Take(1).FirstOrDefault();
                if (movimiento == null)
                {                   
                     calculo = cuenta.SaldoInicial - (parametro.Valor * -1);
                       if (calculo < 0)
                            throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No es posible ejecutar la transacción. Saldo no disponible, cuenta: {1}", "CMOV003", parametro.Numero);
                }
                else
                {
                    calculo = movimiento.Saldo - (parametro.Valor * -1);     
                    if(calculo<0)
                        throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No es posible ejecutar la transacción. Saldo no disponible, cuenta: {1}", "CMOV004", parametro.Numero);

                }

                if (parametro.Valor < 0)
                {
                    var movimientoDia = contexto.Movimientos.Where(p => p.CuentaId == cuenta.Id && DateTime.Compare(p.Fecha, Convert.ToDateTime(parametro.Fecha))==0 && p.EsDebito).Sum(p => p.Valor);
                    var movDiaMasTrans=(movimientoDia * -1) + (parametro.Valor * -1);
                    if (movDiaMasTrans >= montoMaximo.Monto)
                        throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No es posible ejecutar la transacción. el cupo diario Excedido, cuenta: {1}", "CMOV005", parametro.Numero);

                }
                nuevoMovimiento = new Movimiento()
                {
                    CuentaId = cuenta.Id,
                    FechaProceso = DateTime.Now,
                    Fecha = Convert.ToDateTime(parametro.Fecha),
                    Valor = parametro.Valor,
                    Saldo = calculo,
                    EsDebito = parametro.Valor < 0 ? true : false
                };
                contexto.Movimientos.Add(nuevoMovimiento);
                contexto.SaveChanges();
                transaction.Commit();
                resultado = BuscarMovimiento(contexto, nuevoMovimiento.Id);
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
        public DtoResultMovimiento BuscarMovimiento(PruebaContext contexto, int idMovimiento)
        {
            DtoResultMovimiento resultado = new();
            try
            {
              var movimiento = contexto.Movimientos.Include(o => o.Cuenta.Cliente.Persona).Where(p => p.Id == idMovimiento).FirstOrDefault();
                resultado = new DtoResultMovimiento() { 
                  Identificacion=movimiento.Cuenta.Cliente.Persona.Identificacion,
                  Nombre=movimiento.Cuenta.Cliente.Persona.Nombre,
                  JournalMovimiento=movimiento.Id,
                  Numero=movimiento.Cuenta.Numero,
                  FechaProceso=movimiento.FechaProceso.ToString("dd/MM/yyyy HH:mm:ss"),
                  Fecha=movimiento.Fecha.ToString("dd/MM/yyyy"),
                  Valor=movimiento.Valor,
                  Saldo=movimiento.Saldo,
                  EsDebito=movimiento.EsDebito
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

        public DtoEstadoCuentaResult GenerarEstadoCuenta(PruebaContext contexto, string identificacion, string fechaInicio, string fechaFin)
        {
            DtoEstadoCuentaResult results = new();
            List<DtoReporteCuentaResult> listaEstadoCuentas = new ();
            DtoResultCuenta cuenta;
            List<DtoResultMovimientoReporte> listaMovimientos= new();
            DtoReporteCuentaResult reporteCuenta=new();
            try
            {
                var cuentas = contexto.Cuentas.Include(o => o.Cliente.Persona).Where(p => p.Cliente.Persona.Identificacion == identificacion).ToList();
                foreach (var item in cuentas)
                {
                    cuenta = new DtoResultCuenta()
                    {
                        Id=item.Id,
                        Identificacion=item.Cliente.Persona.Identificacion,
                        Nombre=item.Cliente.Persona.Nombre,
                        CodigoTipoCuenta=item.CodigoTipoCuenta,
                        Numero=item.Numero,
                        SaldoInicial=item.SaldoInicial,
                        Estado=item.Estado
                    };
                    var movimientos = contexto.Movimientos.Where(p => p.CuentaId == item.Id && p.Fecha >= Convert.ToDateTime(fechaInicio) && p.Fecha <= Convert.ToDateTime(fechaFin)).OrderBy(p => p.FechaProceso).ToList();
                    listaMovimientos = mapper.Map<List<DtoResultMovimientoReporte>>(movimientos);
                    reporteCuenta = new DtoReporteCuentaResult() { 
                         Cuenta=cuenta,
                         ListaMovimientos=listaMovimientos
                    };
                    listaEstadoCuentas.Add(reporteCuenta);
                }
                results.ListaEstadoCuentas = listaEstadoCuentas;
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
            return results;
        }
    }
}
