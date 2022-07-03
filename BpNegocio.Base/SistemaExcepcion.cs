using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Base
{
    public class SistemaExcepcion : Exception
    {
        public CodigosMensaje CodigoMensaje { get; private set; }
        public string Descripcion { get; private set; }

        public SistemaExcepcion(CodigosMensaje codigoMensaje, string message)
        {
            CodigoMensaje = codigoMensaje;
            Descripcion = message;
        }
        public static SistemaExcepcion CrearException(CodigosMensaje codigoError, string mensaje, string codigoErrorTexto)
        {
            return new SistemaExcepcion(codigoError, String.Format(mensaje, codigoErrorTexto));
        }
        public static SistemaExcepcion CrearException(CodigosMensaje codigoError, string mensaje, string codigoErrorTexto, string para1)
        {
            return new SistemaExcepcion(codigoError, String.Format(mensaje, codigoErrorTexto, para1));
        }
        public static SistemaExcepcion CrearException(CodigosMensaje codigoError, string mensaje, string codigoErrorTexto, string para1, string para2)
        {
            return new SistemaExcepcion(codigoError, String.Format(mensaje, codigoErrorTexto, para1, para2));
        }
        public static SistemaExcepcion CrearException(CodigosMensaje codigoError, string mensaje, string codigoErrorTexto, string para1, string para2, string para3)
        {
            return new SistemaExcepcion(codigoError, String.Format(mensaje, codigoErrorTexto, para1, para2, para3));
        }
        public static ResultadoMensaje GetError(Exception ex)
        {
            return ex switch
            {
                SistemaExcepcion exc => new ResultadoMensaje { CodigoMensaje = exc.CodigoMensaje, Descripcion = exc.Descripcion },
                _ => new ResultadoMensaje { CodigoMensaje = CodigosMensaje.SERVER_ERROR, Descripcion = CodigosMensaje.SERVER_ERROR.ToString() + " | " + MensajesExcepcion.SERVER_ERROR }
            };
        }
    }
}
