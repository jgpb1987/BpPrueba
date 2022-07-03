using BpNegocio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BpNegocio.Prueba.Dto.Financiero
{
    public class DtoParamCrearMovimiento
    {
        [Required]
        [StringLength(15, ErrorMessage = "El campo {0} debe tener entre {1} caracteres y {2}", MinimumLength = 6)]
        public string Numero { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", ErrorMessage = "La fecha no cumple con el formato dd/MM/yyyy")]
        public string Fecha { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
    public class DtoResultMovimiento: ResultadoBase
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public int JournalMovimiento { get; set; }
        public string Numero { get; set; }
        public string FechaProceso { get; set; }
        public string Fecha { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public bool EsDebito { get; set; }

    }

    public class DtoResultMovimientoReporte 
    {
        public string FechaProceso { get; set; }
        public string Fecha { get; set; }
        public int JournalMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public bool EsDebito { get; set; }

    }
}
