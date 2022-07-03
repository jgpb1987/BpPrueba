using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Base
{
    public class ResultadoMensaje
    {
        public CodigosMensaje CodigoMensaje { get; set; } = CodigosMensaje.OK;
        public string Descripcion { get; set; } = MensajesExcepcion.OK;
    }
}
