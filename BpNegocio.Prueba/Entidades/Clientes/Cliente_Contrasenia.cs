using System;
using System.Collections.Generic;
using System.Text;

namespace BpNegocio.Prueba.Entidades.Clientes
{
    public class Cliente_Contrasenia
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; }
        public Cliente Cliente { get; set; }
    }
}
