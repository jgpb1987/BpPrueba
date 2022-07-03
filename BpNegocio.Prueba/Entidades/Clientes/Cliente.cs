using BpNegocio.Prueba.Entidades.Sujeto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Prueba.Entidades.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public Persona Persona { get; set; }
    }
}
