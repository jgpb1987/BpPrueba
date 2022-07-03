using BpAccesoDatos.Prueba;
using BpNegocio.Base;
using BpNegocio.Prueba.Dto.Clientes;
using BpWebApi.Prueba.Services.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace BpWebApi.Prueba.Controllers.Clientes
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController
    {
        private readonly ClienteServices clienteServices;
        private readonly PruebaContext contexto;

        public ClienteController(ClienteServices clienteServices, PruebaContext contexto)
        { 
            this.clienteServices = clienteServices;
            this.contexto = contexto;
        }

        [HttpPost("postCrearCliente")]
        public ActionResult<DtoResultCliente> PostCrearCliente([FromBody] DtoParamCrearCliente parametro)
        {
            DtoResultCliente result = new();
            try
            {
                result = clienteServices.CrearCliente(contexto, parametro);
            }
            catch (Exception ex) {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpGet("getBuscarCliente")]
        public ActionResult<DtoResultCliente> GetBuscarCliente(string identificacion)
        {
            DtoResultCliente result = new();
            try
            {
                result = clienteServices.BuscarCliente(contexto, identificacion, true);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpPut("putActualizarCliente")]
        public ActionResult<DtoResultCliente> PutBuscarCliente([FromBody] DtoParamCrearCliente parametro)
        {
            DtoResultCliente result = new();
            try
            {
                result = clienteServices.ActualizarCliente(contexto, parametro);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
        [HttpDelete("deleteDesactivarCliente")]
        public ActionResult<DtoResultCliente> DeleteDesactivarCliente([FromBody] DtoParamDesactivarCliente parametro)
        {
            DtoResultCliente result = new();
            try
            {
                result = clienteServices.DesactivarCliente(contexto, parametro.Identificacion);
            }
            catch (Exception ex)
            {
                result.Mensaje = SistemaExcepcion.GetError(ex);
            }
            return result;
        }
    }
}
