using BpAccesoDatos.Prueba;
using BpNegocio.Base;
using BpNegocio.Prueba.Dto.Financiero;
using BpWebApi.Prueba.Services.Financiero;
using Microsoft.AspNetCore.Mvc;

namespace BpWebApi.Prueba.Controllers.Financiero
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovimientoController
    {
        private readonly MovimientoServices services;
        private readonly PruebaContext contexto;
        public MovimientoController(MovimientoServices services, PruebaContext contexto)
        {
            this.services = services;
            this.contexto = contexto;
        }

        [HttpPost("postCrearMovimiento")]
        public ActionResult<DtoResultMovimiento> PostCrearMovimiento([FromBody] DtoParamCrearMovimiento parametro)
        {
            DtoResultMovimiento result = new();
            try
            {
                result = services.CrearMovimiento(contexto, parametro);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpGet("getBuscarMovimiento")]
        public ActionResult<DtoResultMovimiento> GetBuscarMovimiento(int journal)
        {
            DtoResultMovimiento result = new();
            try
            {
                result = services.BuscarMovimiento(contexto, journal);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpGet("getEstadoCuenta")]
        public ActionResult<DtoEstadoCuentaResult> GetEstadoCuenta(string identificacion, string fechaInicio, string fechaFin)
        {
            DtoEstadoCuentaResult result = new();
            try
            {
                result = services.GenerarEstadoCuenta(contexto, identificacion, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
    }
}
