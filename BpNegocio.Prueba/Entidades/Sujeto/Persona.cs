using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Prueba.Entidades.Sujeto
{
    public class Persona
    {
        public int Id { get; set; }
        public string CodigoGenero { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Genero Genero { get; set; }
    }
}
