using BpNegocio.Prueba.Entidades.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Prueba.Entidades.Ahorro
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string CodigoTipoCuenta { get; set; }
        public string Numero { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public Cliente Cliente { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
    }
}
