using BpNegocio.Prueba.Entidades.Ahorro;
using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Prueba.Entidades.Financiero
{
    public class Movimiento
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaProceso { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public bool EsDebito { get; set; }
        public Cuenta Cuenta { get; set; }
    }
}
