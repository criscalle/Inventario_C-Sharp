using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_H2
{
    public class Persona
    {
        private string id;
        private string nombre;
        private string apellido;
        private int id_rol;

        public string Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public int Id_rol { get => id_rol; set => id_rol = value; }

        public Persona()
        {

        }
    }
}
