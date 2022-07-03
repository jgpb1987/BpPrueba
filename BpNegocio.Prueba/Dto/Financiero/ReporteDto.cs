using BpNegocio.Base;
using BpNegocio.Prueba.Dto.Ahorro;
using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Prueba.Dto.Financiero
{
    public class DtoReporteCuentaResult
    {
        public DtoResultCuenta Cuenta { get; set; }
        public List<DtoResultMovimientoReporte> ListaMovimientos { get; set; }
    }
    public class DtoEstadoCuentaResult : ResultadoBase
    {
        public List<DtoReporteCuentaResult> ListaEstadoCuentas { get; set; }
    }
}
