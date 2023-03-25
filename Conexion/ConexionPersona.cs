using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Proyecto_H2.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;

namespace Proyecto_H2.Conexion
{
    public class ConexionPersona
    {
        private MySqlConnection conexionpersona = new MySqlConnection();

        public ConexionPersona(string _datos)
        {
            conexionpersona.ConnectionString = _datos;
        }

        static List<Cliente> list = new List<Cliente>();
        static Cliente cliente = new Cliente();
        static List<Trabajador> lista_user = new List<Trabajador>();
        static Trabajador user = new Trabajador();
        public Trabajador validar(string[] usuario)
        {
            Trabajador user = new Trabajador();

            try
            {
                conexionpersona.Open();
                MySqlCommand cmd = new MySqlCommand("select id, nombre, apellido, id_rol ,usuario, contrasena from Persona where usuario= @usuario and contrasena = @contrasena", conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usuario", usuario[0]);
                cmd.Parameters.AddWithValue("@contrasena", usuario[1]);
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {
                    user.Id = info.GetString(0);
                    user.Nombre = info.GetString(1);
                    user.Apellido = info.GetString(2);
                    user.Id_rol = info.GetInt16(3);
                    user.Usuario = info.GetString(4);
                    user.Contrasena = info.GetString(5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            conexionpersona.Close();
            return user;
        }
        public Cliente verificar(string msg)
        {
            Cliente cliente = new Cliente();

            conexionpersona.Open();
            MySqlCommand cmd = new MySqlCommand("select id, nombre, apellido, id_rol, telefono, correo , direccion from Persona where id= @id and id_rol = 3", conexionpersona);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", msg);
            MySqlDataReader info = cmd.ExecuteReader();

            while (info.Read())
            {
                cliente.Id = info.GetString(0);
                cliente.Nombre = info.GetString(1);
                cliente.Apellido = info.GetString(2);
                cliente.Id_rol = info.GetInt16(3);
                cliente.Telefono = info.GetString(4);
                cliente.Correo = info.GetString(5);
                cliente.Direccion = info.GetString(6);
            }
            conexionpersona.Close();
            return cliente;

        }

        public List<Cliente> lista()
        {

            try
            {
                conexionpersona.Open();
                string consulta = "Select id, nombre, apellido, telefono, correo, direccion from persona where id_rol = 3";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionpersona);
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {
                    list.Add(new Cliente { Id = info.GetString(0), Nombre = info.GetString(1), Apellido = info.GetString(2), Telefono = info.GetString(3), Correo = info.GetString(4), Direccion = info.GetString(5) });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            conexionpersona.Close();
            return list;
        }

        public string crear_cliente(Cliente cliente)
        {
            string respuesta = null;
            try
            {
                conexionpersona.Open();
                string consulta = "Insert into persona (id, nombre, apellido, id_rol, telefono, correo, direccion) values (@id, @nombre, @apellido, 3, @telefono, @correo, @direccion)";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionpersona);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", cliente.Id);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.ExecuteNonQuery();
                respuesta = "Cliente ingresado con exito :)";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            conexionpersona.Close();
            return respuesta;
        }

        public Cliente leer(string msg)
        {
            try
            {
                conexionpersona.Open();
                MySqlCommand cmd = new MySqlCommand("select id, nombre, apellido, id_rol, telefono, correo, direccion from Persona where id = @id and id_rol = 3", conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", msg);
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {
                    cliente.Id = info.GetString(0);
                    cliente.Nombre = info.GetString(1);
                    cliente.Apellido = info.GetString(2);
                    cliente.Id_rol = info.GetInt32(3);
                    cliente.Telefono = info.GetString(4);
                    cliente.Correo = info.GetString(5);
                    cliente.Direccion = info.GetString(6);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexionpersona.Close();
            return cliente;
        }

        public string modificar(Cliente cliente)
        {
            string respuesta = null;
            try
            {
                conexionpersona.Open();
                string consulta = "Update persona set nombre =@nombre, apellido = @apellido, telefono = @telefono, correo = @correo, direccion = @direccion where id=@id";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", cliente.Id);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.ExecuteNonQuery();
                respuesta = "Cliente actualizado con exito :)";

            }
            catch (Exception ex)
            {
                respuesta = "\n" + ex.Message;
            }
            conexionpersona.Close();
            return respuesta;
        }

        public string eliminar(string msg)
        {
            string respuesta = null;
            try
            {
                conexionpersona.Open();
                string consulta = "delete from persona where persona.id=@id";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", msg);
                cmd.ExecuteNonQuery();
                respuesta = "Cliente eliminado con exito :)";

            }
            catch (Exception ex)
            {
                respuesta = "\n" + ex.Message;
            }
            conexionpersona.Close();
            return respuesta;

        }
        public string crear_usuario(Trabajador user)
        {
            string respuesta = null;
            try
            {
                conexionpersona.Open();
                string consulta = "Insert into persona (id, nombre, apellido, id_rol, usuario, contrasena) values (@id, @nombre, @apellido, @id_rol, @usuario, @contrasena)";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionpersona);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@nombre", user.Nombre);
                cmd.Parameters.AddWithValue("@apellido", user.Apellido);
                cmd.Parameters.AddWithValue("@id_rol", user.Id_rol);
                cmd.Parameters.AddWithValue("@usuario", user.Usuario);
                cmd.Parameters.AddWithValue("@contrasena", user.Contrasena);
                cmd.ExecuteNonQuery();
                respuesta = "Usuario ingresado con exito :)";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            conexionpersona.Close();
            return respuesta;
        }
        public List<Trabajador> leer_usuarios()
        {
            try
            {
                conexionpersona.Open();
                MySqlCommand cmd = new MySqlCommand("select id, nombre, apellido, id_rol, usuario from Persona where id_rol = 1 or id_rol = 2", conexionpersona);
                cmd.Parameters.Clear();
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {

                    lista_user.Add(new Trabajador { Id = info.GetString(0), Nombre = info.GetString(1), Apellido = info.GetString(2), Id_rol = info.GetInt16(3), Usuario = info.GetString(4) });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexionpersona.Close();
            return lista_user;
        }
        public Trabajador buscar_usuario(string msg)
        {
            try
            {
                conexionpersona.Open();
                MySqlCommand cmd = new MySqlCommand("select id, nombre, apellido, usuario, contrasena from Persona where id = @id", conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", msg);
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {
                    user.Id = info.GetString(0);
                    user.Nombre = info.GetString(1);
                    user.Apellido = info.GetString(2);
                    user.Usuario = info.GetString(3);
                    user.Contrasena = info.GetString(4);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexionpersona.Close();
            return user;
        }
        public Trabajador leer_user(string msg)
        {
            try
            {
                conexionpersona.Open();
                MySqlCommand cmd = new MySqlCommand("select id, nombre, apellido, id_rol, telefono, correo, direccion from Persona where id = @id and (id_rol = 1 or id_rol = 2)", conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", msg);
                MySqlDataReader info = cmd.ExecuteReader();

                while (info.Read())
                {
                    cliente.Id = info.GetString(0);
                    cliente.Nombre = info.GetString(1);
                    cliente.Apellido = info.GetString(2);
                    cliente.Id_rol = info.GetInt32(3);
                    cliente.Telefono = info.GetString(4);
                    cliente.Correo = info.GetString(5);
                    cliente.Direccion = info.GetString(6);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexionpersona.Close();
            return user;
        }
        public string actualizar_usuario(Trabajador user)
        {
            string respuesta = null;
            try
            {
                conexionpersona.Open();
                string consulta = "Update persona set nombre =@nombre, apellido = @apellido, usuario = @usuario, contrasena = @contrasena, id_rol = @id_rol where id=@id";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@nombre", user.Nombre);
                cmd.Parameters.AddWithValue("@apellido", user.Apellido);
                cmd.Parameters.AddWithValue("@usuario", user.Usuario);
                cmd.Parameters.AddWithValue("@contrasena", user.Contrasena);
                cmd.Parameters.AddWithValue("@id_rol", user.Id_rol);
                cmd.ExecuteNonQuery();
                respuesta = "Usuario actualizado con exito :)";

            }
            catch (Exception ex)
            {
                respuesta = "\n" + ex.Message;
            }
            conexionpersona.Close();
            return respuesta;
        }
        public string eliminar_usuario(string msg)
        {
            string respuesta = null;
            try
            {
                conexionpersona.Open();
                string consulta = "delete from persona where persona.id=@id";
                MySqlCommand cmd = new MySqlCommand(consulta, conexionpersona);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", msg);
                cmd.ExecuteNonQuery();
                respuesta = "usuario eliminado con exito :)";

            }
            catch (Exception ex)
            {
                respuesta =  ex.Message;
            }
            conexionpersona.Close();
            return respuesta;

        }

    }
}
