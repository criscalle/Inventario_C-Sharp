using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_H2.Clases
{
    public class Factura
    {
        private int id;
        private string id_cliente;
        private int numero_detalle;
        private string id_producto;
        private string nombre_producto;
        private decimal precio;
        private int cantidad;

        public string Id_cliente { get => id_cliente; set => id_cliente = value; }
        public int Numero_detalle { get => numero_detalle; set => numero_detalle = value; }
        public string Id_producto { get => id_producto; set => id_producto = value; }
        public string Nombre_producto { get => nombre_producto; set => nombre_producto = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public int Id { get => id; set => id = value; }

        public Factura()
        {

        }
    }
}
