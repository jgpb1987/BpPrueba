using BpNegocio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BpNegocio.Prueba.Dto.Ahorro
{
    public class DtoParamCrearCuenta
    {

        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 5)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "El campo {0} debe tener entre {1} caracteres y {2}", MinimumLength = 3)]
        public string CodigoTipoCuenta { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 6)]
        public string Numero { get; set; }

        [Required]
        public decimal SaldoInicial { get; set; }

        [Required]
        public bool Estado { get; set; }
    }

    public class DtoParamActualizarCuenta
    {
        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 5)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "El campo {0} debe tener entre {1} caracteres y {2}", MinimumLength = 3)]
        public string CodigoTipoCuenta { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 6)]
        public string Numero { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
    public class DtoParamEliminarCuenta
    {
        [Required]
        [StringLength(15, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 6)]
        public string Numero { get; set; }

    }
    public class DtoResultCuenta : ResultadoBase
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string CodigoTipoCuenta { get; set; }
        public string Numero { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
    }
}
