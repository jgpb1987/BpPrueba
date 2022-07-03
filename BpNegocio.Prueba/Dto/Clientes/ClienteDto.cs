using BpNegocio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BpNegocio.Prueba.Dto.Clientes
{
    public class DtoParamCrearCliente
    {
        [Required]
        [StringLength(1, ErrorMessage = "El campo {0} debe tener entre {1} caracteres y {2}", MinimumLength = 1)]
        public string CodigoGenero { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 5)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 10)]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", ErrorMessage = "La fecha de nacimiento no cumple con el formato dd/MM/yyyy")]
        public string FechaNacimiento { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 10)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 10)]
        [RegularExpression(@"^[+-]?\d+(\.\d+)?$", ErrorMessage = "No se permiten letras y caracteres especiales en el número de teléfono")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 4)]
        public string Clave { get; set; }
    }
    public class DtoParamDesactivarCliente
    {
        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} caracteres y {1}", MinimumLength = 5)]
        public string Identificacion { get; set; }

    }
    public class DtoResultCliente : ResultadoBase
    {
        public int Id { get; set; }
        public string CodigoGenero { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

    }
}
