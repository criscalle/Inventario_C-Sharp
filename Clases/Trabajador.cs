using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_H2.Clases
{
    public class Trabajador : Persona
    {
        private string usuario;
        private string contrasena;

        public string Usuario { get => usuario; set => usuario = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }

        public Trabajador()
        {

        }
    }
}
