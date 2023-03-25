using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_H2.Clases
{
    public class Producto
    {
        private string codigo;
        private string descripcion;
        private decimal precio;
        private int cantidad;

        public string Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }


        public Producto()
        {
            
        }

    }
}
