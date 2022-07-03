using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Prueba.Entidades.Ahorro
{
    public class MontoMaximoRetiro_TipoCuenta
    {
        public int Id { get; set; }
        public string CodigoTipoCuenta { get; set; }
        public decimal Monto { get; set; }
        public bool Estado { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
    }
}
