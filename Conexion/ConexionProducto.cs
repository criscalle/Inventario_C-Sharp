using MySql.Data.MySqlClient;
using Proyecto_H2.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_H2.Conexion
{
    public class ConexionProducto
    {

        private MySqlConnection conexionproducto = new MySqlConnection();

        public ConexionProducto(string _datos)
        {
            conexionproducto.ConnectionString = _datos;
        }

        static List<Producto> list = new List<Producto>();
        static Producto prod = new Producto();
        static string respuesta = null;
    

        public Producto leer(string msg)
        {

            try
            {
                conexionproducto.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Producto where codigo = @codigo", conexionproducto);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@codigo", msg);
                MySqlDataReader info = cmd.ExecuteReader();

                while(info.Read())
                {
                    prod.Codigo = info.GetString(0);
                    prod.Descripcion = info.GetString(1);
                    prod.Precio = info.GetDecimal(2);
                    prod.Cantidad = info.GetInt32(3) ;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexionproducto.Close();
            return prod;

        }
        public List<Producto> Ver()
        {
            list.Clear();

            try
            {
                conexionproducto.Open();
                string consulta = "Select * from producto";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionproducto);
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {

                    list.Add(new Producto{ Codigo = info.GetString(0), Descripcion = info.GetString(1), Precio = info.GetDecimal(2), Cantidad = info.GetInt32(3) });
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            conexionproducto.Close();
            return list;
        }

        public string crear(Producto producto)
        {
            try
            {
                conexionproducto.Open();
                string consulta = "Insert into producto (codigo, descripcion, precio, cantidad) values (@codigo, @descripcion, @precio, @cantidad)";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionproducto);

                cmd.Parameters.Clear(); 
                cmd.Parameters.AddWithValue("@codigo", producto.Codigo);  
                cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion );
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                cmd.ExecuteNonQuery();  
                respuesta = "Producto ingresado con exito :)";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            conexionproducto.Close();
            return respuesta;
        }

        public string actualizar(Producto producto)
        {
            string respuesta = null;
            try
            {
                conexionproducto.Open();
                string consulta = "Update producto set descripcion =@descripcion, precio = @precio, cantidad = @cantidad where codigo=@codigo";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionproducto);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                cmd.ExecuteNonQuery();
                respuesta = "Producto actualizado con exito :)";

            }
            catch (Exception ex)
            {
                respuesta = "\n" + ex.Message;
            }
            conexionproducto.Close();
            return respuesta;
        }

        public string eliminar(Producto producto)
        {
            string respuesta = null;
            try
            {
                conexionproducto.Open();
                string consulta = "delete from producto where producto.codigo=@codigo";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionproducto);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                cmd.ExecuteNonQuery();
                respuesta = "\nProducto eliminado con exito :)";

            }
            catch (Exception ex)
            {
                respuesta = "\n" + ex.Message;
            }
            conexionproducto.Close();
            return respuesta;

        }


    }
}
