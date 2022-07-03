using BpAccesoDatos.Prueba;
using BpNegocio.Base;
using BpNegocio.Prueba.Dto.Ahorro;
using BpWebApi.Prueba.Services.Ahorro;
using Microsoft.AspNetCore.Mvc;

namespace BpWebApi.Prueba.Controllers.Ahorro
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentaController
    {
        private readonly CuentaServices cuentaServices;
        private readonly PruebaContext contexto;
        public CuentaController(CuentaServices clienteServices, PruebaContext contexto)
        {
            this.cuentaServices = clienteServices;
            this.contexto = contexto;
        }

        [HttpPost("postCrearCuenta")]
        public ActionResult<DtoResultCuenta> PostCrearCuenta([FromBody] DtoParamCrearCuenta parametro)
        {
            DtoResultCuenta result = new();
            try
            {
                result = cuentaServices.CrearCuenta(contexto, parametro);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpGet("getBuscarCuenta")]
        public ActionResult<DtoResultCuenta> GetBuscarCuenta(string numero)
        {
            DtoResultCuenta result = new();
            try
            {
                result = cuentaServices.BuscarCuenta(contexto, numero);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpPut("putActualizarCuenta")]
        public ActionResult<DtoResultCuenta> PutBuscarCliente(int id, [FromBody] DtoParamActualizarCuenta parametro)
        {
            DtoResultCuenta result = new();
            try
            {
                result = cuentaServices.ActualizarCuenta(contexto, id, parametro);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpDelete("deleteCuenta")]
        public ActionResult<ResultadoBase> DeleteCuenta([FromBody] DtoParamEliminarCuenta parametro)
        {
            ResultadoBase result = new();
            try
            {
                result = cuentaServices.EliminarCuenta(contexto, parametro);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
    }
}
