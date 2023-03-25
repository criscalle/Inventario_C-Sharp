using MySql.Data.MySqlClient;
using Proyecto_H2.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_H2.Conexion
{
    public class ConexionFactura
    {
        private MySqlConnection conexionfactura= new MySqlConnection();

        public ConexionFactura(string _datos)
        {
            conexionfactura.ConnectionString = _datos;
        }

        public int numerofactura(Factura factura)
        {
            int resultado = 0;
            try
            {
                conexionfactura.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT max(id) from factura", conexionfactura);
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {
                    resultado = info.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conexionfactura.Close();
            return resultado;
        }

        public string Vender(Factura factura)
        {
            string respuesta = null;
            try
            {
                conexionfactura.Open();
                MySqlCommand cmd = new MySqlCommand("insert into factura (id_cliente)  values (@id_cliente)", conexionfactura);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_cliente", factura.Id_cliente);
                cmd.ExecuteNonQuery();

                respuesta = "Factura ingresada con exito :)";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conexionfactura.Close();

            return respuesta;
        }

        public string Venta(Factura fac)
        {

            string respuesta = null;

            try
            {
                conexionfactura.Open();
                string consulta = "Insert into detalle_factura (id_factura, id_producto, precio, cantidad)  values (@id_factura, @id_producto, @precio, @cantidad)";

                MySqlCommand cmd = new MySqlCommand(consulta, conexionfactura);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_factura", fac.Id);
                cmd.Parameters.AddWithValue("@id_producto", fac.Id_producto);
                cmd.Parameters.AddWithValue("@precio", fac.Precio);
                cmd.Parameters.AddWithValue("@cantidad", fac.Cantidad);
                cmd.ExecuteNonQuery();
                respuesta = "\nFactura generada con exito :)\n";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            conexionfactura.Close();
            return respuesta;

        }

    }
}
