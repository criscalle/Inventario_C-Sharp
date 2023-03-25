using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_H2.Clases
{
    public class Cliente : Persona
    {
        private string telefono;
        private string correo;
        private string direccion;

        public string Telefono { get => telefono; set => telefono = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        public Cliente()
        {

        }
    }
}
