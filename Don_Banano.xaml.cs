using Google.Protobuf.Collections;
using Proyecto_H2.Clases;
using Proyecto_H2.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Proyecto_H2
{
  
    public partial class Don_Banano : Window
    {

        static string datos = "Server=localhost; database=don_banano; user=root", msg, resul;
        Conexion.ConexionProducto conexionproducto = new Conexion.ConexionProducto(datos);
        Conexion.ConexionFactura conexionfactura = new Conexion.ConexionFactura(datos);
        Conexion.ConexionPersona conexionpersona = new Conexion.ConexionPersona(datos);
        static List<Producto> List = new List<Producto>();
        static Producto producto = new Producto();
        static List<Producto> lista = new List<Producto>();
        static Factura factura = new Factura();
        static Cliente cliente = new Cliente();
        static List<Cliente> listas = new List<Cliente>();
        static Trabajador user = new Trabajador();
        static List<Trabajador> lista_user = new List<Trabajador>();

        public Don_Banano()
        {
            InitializeComponent();
            lista_user.Clear();
            Lista_Usuario.Items.Clear();
            lista_user = conexionpersona.leer_usuarios();
            foreach(Trabajador t in lista_user)
            {
                Lista_Usuario.Items.Add( new Trabajador { Id = t.Id, Nombre = t.Nombre, Apellido = t.Apellido, Usuario = t.Usuario, Id_rol = t.Id_rol });
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow Login = new MainWindow();
            Login.Show();
            limpiar_cliente();
            limpiar_inventario();
        }
        private void limpiar_inventario()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            Venta.Items.Remove(producto);
            Inventario.Items.Clear();
            lista.Clear();
            List.Clear();
            listas.Clear();
            Lista_clientes.Items.Clear();
            Venta.Items.Clear(); 
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Inventario.Items.Clear();
                msg = txtCodigo.Text;
                producto = conexionproducto.leer(msg);

                if (msg != "")
                {
                    if (producto.Codigo != null)
                    {
                        txtCodigo.Text = producto.Codigo;
                        txtDescripcion.Text = producto.Descripcion;
                        txtPrecio.Text = producto.Precio.ToString();
                        txtCantidad.Text = producto.Cantidad.ToString();
                    }
                    else
                    {
                        MessageBox.Show("El codigo no existe");
                    }
                }
                
                else
                {
                    MessageBox.Show("La caja de texto esta vacia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List.Clear();
                limpiar_inventario();
                Inventario.Items.Clear();
                List = conexionproducto.Ver();
                StreamWriter sw = new StreamWriter("C:\\Archivo_plano_H2\\productos.txt");
               
                foreach (Producto i in List)
                {
                   
                    Inventario.Items.Add(new Producto { Codigo = i.Codigo, Descripcion = i.Descripcion, Precio = i.Precio, Cantidad = i.Cantidad });
                    
                    sw.WriteLine($"{i.Codigo};{i.Descripcion};{i.Precio};{i.Cantidad}");
                }
                sw.Close();
                List.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }     
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar_inventario();
        }

        private void btnRespaldo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List.Clear();
                Inventario.Items.Clear();
                StreamReader sr = new StreamReader("C:\\Archivo_plano_H2\\productos.txt");
                string linea = sr.ReadLine();

                while (linea != null)
                {
                    string[] vector = linea.Split(';');

                    
                    List.Add(new Producto { Codigo = vector[0], Descripcion = vector[1], Precio = Convert.ToDecimal(vector[2]), Cantidad = Convert.ToInt32(vector[3]) });
                    linea = sr.ReadLine();

                }
                sr.Close();

                foreach (Producto i in List)
                {
                    Inventario.Items.Add(new Producto { Codigo = i.Codigo, Descripcion = i.Descripcion, Precio = i.Precio, Cantidad = i.Cantidad });
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            
            producto.Codigo = txtCodigo.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precio = Convert.ToDecimal(txtPrecio.Text);
            producto.Cantidad = Convert.ToInt32(txtCantidad.Text);

            msg = conexionproducto.actualizar(producto);
            MessageBox.Show(msg);
            
            Inventario.Items.Remove(producto);
            Inventario.Items.Clear();

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            producto.Codigo = txtCodigo.Text;
            msg = conexionproducto.eliminar(producto);
            MessageBox.Show(msg);
           
        }

        private void btnbuscarprod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                msg = txtcodigo.Text;
                producto = conexionproducto.leer(msg);

                if (msg != "")
                {
                    if (producto.Codigo != null)
                    {
                        txtcodigo.Text = producto.Codigo;
                        txtnombre.Text = producto.Descripcion;
                        txtprecio.Text = producto.Precio.ToString();
                        txtcantidad.Text = producto.Cantidad.ToString();
                    }
                    else
                    {
                        MessageBox.Show("El codigo no existe");
                    }
                }
                else
                {
                    MessageBox.Show("La caja de texto esta vacia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {  if(txtcedula.Text == "")
                {
                    MessageBox.Show("Ingrese el Documento del cliente");
                }
                else
                {
                    if (txtcodigo != null)
                    {

                        Venta.Items.Add(new Producto { Codigo = txtcodigo.Text, Descripcion = txtnombre.Text, Precio = Convert.ToDecimal(txtprecio.Text), Cantidad = Convert.ToInt32(txtcantidad.Text) });
                        lista.Add(new Producto { Codigo = txtcodigo.Text, Descripcion = txtnombre.Text, Precio = Convert.ToDecimal(txtprecio.Text), Cantidad = Convert.ToInt32(txtcantidad.Text) });
                        txtcodigo.Clear();
                        txtnombre.Clear();
                        txtprecio.Clear();
                        txtcantidad.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Caja de texto vacia");
                    }
                }
                
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnLiquidar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int suma = 0, total = 0;
                int numero = 0;

                factura.Id_cliente = txtcedula.Text;
                msg = conexionfactura.Vender(factura);
                numero = conexionfactura.numerofactura(factura);
                txtfactura.Text = numero.ToString();
                foreach (Producto l in lista)
                {
                    suma = Convert.ToInt32(l.Precio) * l.Cantidad;
                    Factura fac = new Factura { Id= numero, Id_producto = l.Codigo, Cantidad = l.Cantidad, Precio=l.Precio };
                    msg = conexionfactura.Venta(fac);
                    total = suma + total;

                    producto = conexionproducto.leer(l.Codigo);
                    producto.Cantidad = producto.Cantidad - l.Cantidad;
                    msg = conexionproducto.actualizar(producto);


                }
                MessageBox.Show("Total a Pagar: " + total);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void bntBuscar_doc_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string msg = txtcedula.Text;
                cliente = conexionpersona.verificar(msg);

                if (msg != "")
                {
                    if (cliente.Id is null)
                    {
                        MessageBox.Show("El cliente no existe");
                        txtcedula.Clear();
                    }
                    else
                    {
                        txtcedula.Text = cliente.Id;
                        txtnombre_cliente.Text = cliente.Apellido + " " + cliente.Nombre;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un numero de documento");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSalir1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow Login = new MainWindow();
            Login.Show();
            limpiar_cliente();
            limpiar_inventario();
        }

        private void btnLista_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listas.Clear();
                Lista_clientes.Items.Clear();
                listas = conexionpersona.lista();

                foreach (Cliente c in listas)
                {
                    Lista_clientes.Items.Add(new Cliente { Id = c.Id, Nombre = c.Nombre, Apellido = c.Apellido, Telefono = c.Telefono, Correo = c.Correo, Direccion = c.Direccion });
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void limpiar_cliente()
        {
            txtdocumento_cliente.Clear();
            txtnombre_client.Clear();
            txtApellido_cliente.Clear();
            txtTelefono_cliente.Clear();
            txtCorreo_cliente.Clear();
            txtDireccion_cliente.Clear();
            Lista_clientes.Items.Clear();
        }

        private void bntLimpiar_todo_Click(object sender, RoutedEventArgs e)
        {
            limpiar_cliente();
        }

        private void btnBuscar_cliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string msg = txtdocumento_cliente.Text;

                if (msg != "")
                {
                    cliente = conexionpersona.verificar(msg);
                    if (cliente is null)
                    {
                        MessageBox.Show("El cliente no existe");
                        txtcedula.Clear();
                    }
                    else
                    {
                        txtdocumento_cliente.Text = cliente.Id;
                        txtnombre_client.Text = cliente.Nombre;
                        txtApellido_cliente.Text = cliente.Apellido;
                        txtTelefono_cliente.Text = cliente.Telefono;
                        txtCorreo_cliente.Text = cliente.Correo;
                        txtDireccion_cliente.Text = cliente.Direccion;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un numero de documento");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow Login = new MainWindow();
            Login.Show();
            limpiar_cliente();
            limpiar_inventario();

        }

        private void btnCrearCliente_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtdocumento_cliente.Text == "" || txtnombre_client.Text == "" || txtApellido_cliente.Text == "" || txtTelefono_cliente.Text == "" || txtCorreo_cliente.Text == "" || txtDireccion_cliente.Text == "")
            {
                MessageBox.Show("llene todos los datos");
            }
            else
            {
                msg = txtdocumento_cliente.Text;
                cliente = conexionpersona.leer(msg);

                if (cliente.Id is null)
                {
                    cliente.Id = txtdocumento_cliente.Text;
                    cliente.Nombre = txtnombre_client.Text;
                    cliente.Apellido = txtApellido_cliente.Text;
                    cliente.Telefono = txtTelefono_cliente.Text;
                    cliente.Correo = txtCorreo_cliente.Text;
                    cliente.Direccion = txtDireccion_cliente.Text;

                    msg = conexionpersona.crear_cliente(cliente);
                }
                else
                {
                    msg = "El documento ingresado ya existe en la base de datos";
                }
                MessageBox.Show(msg);
            }
            limpiar_cliente();
        }

        private void btnEliminar_cliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                msg = txtdocumento_cliente.Text;
                resul = conexionpersona.eliminar(msg);
                MessageBox.Show(resul);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            limpiar_cliente();
        }

        private void txtCrear_usuario_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txtIDusuario.Text == "" || txtNombreusuario.Text == "" || txtApellidousuario.Text == "" || txtusuario.Text == "" || txtContrasenausuario.Password.ToString() == "" || txtconfirmarcontrasena.Password.ToString() == "")
                {
                    MessageBox.Show("Ingrese todos los campos requeridos");
                }
                else
                {
                    if (txtContrasenausuario.Password.ToString() != txtconfirmarcontrasena.Password.ToString())
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                    }
                    else
                    {
                        ComboBoxItem seleccion = (ComboBoxItem)permisos.SelectedItem;
                        TextBlock select = (TextBlock)seleccion.Content;
                        if (select.Text == "Administrador")
                        {
                            user.Id = txtIDusuario.Text;
                            user.Nombre = txtNombreusuario.Text;
                            user.Apellido = txtApellidousuario.Text;
                            user.Id_rol = 1;
                            user.Usuario = txtusuario.Text;
                            user.Contrasena = txtContrasenausuario.Password.ToString();
                            msg = conexionpersona.crear_usuario(user);
                            MessageBox.Show(msg);

                        }else if (select.Text == "Vendedor")
                        {
                            user.Id = txtIDusuario.Text;
                            user.Nombre = txtNombreusuario.Text;
                            user.Apellido = txtApellidousuario.Text;
                            user.Id_rol = 2;
                            user.Usuario = txtusuario.Text;
                            user.Contrasena = txtContrasenausuario.Password.ToString();
                            msg = conexionpersona.crear_usuario(user);
                            MessageBox.Show(msg);
                        }
                        else
                        {
                            MessageBox.Show("Ingrese el tipo de permiso");
                        }
                        Lista_Usuario.Items.Clear();
                        limpiar_usuario();
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }
        public void limpiar_usuario()
        {
            txtIDusuario.Clear();
            txtNombreusuario.Clear();
            txtApellidousuario.Clear();
            txtusuario.Clear();
            lista_user.Clear();
            Lista_Usuario.Items.Clear();
        }


        private void bntLimpiarUsuario_Click(object sender, RoutedEventArgs e)
        {
            limpiar_usuario();
        }

        private void bntBuscarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                msg = txtIDusuario.Text;
                if (txtIDusuario.Text == "")
                {
                    MessageBox.Show("Ingrese el Id a buscar");
                }
                else
                {
                  user = conexionpersona.buscar_usuario(msg);
                  txtIDusuario.Text = user.Id;
                  txtNombreusuario.Text = user.Nombre;
                  txtApellidousuario.Text = user.Apellido;
                  txtusuario.Text = user.Usuario;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void txtActualizar_usuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtIDusuario.Text == "" || txtNombreusuario.Text == "" || txtApellidousuario.Text == "" || txtusuario.Text == "" || txtContrasenausuario.Password.ToString() == "")
                {
                    MessageBox.Show("Ingrese todos los datos");
                }
                else
                {
                    if (txtconfirmarcontrasena.Password.ToString() == txtconfirmarcontrasena.Password.ToString())
                    {
                        user.Id = txtIDusuario.Text;
                        user.Nombre = txtNombreusuario.Text;
                        user.Apellido = txtApellidousuario.Text;
                        user.Usuario = txtusuario.Text;
                        user.Contrasena = txtContrasenausuario.Password.ToString();
                        msg = conexionpersona.actualizar_usuario(user);
                    }
                    else
                    {
                        MessageBox.Show("Contraseñas no coinciden");
                    }
                    limpiar_usuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtIDusuario.Text != "")
                {
                    msg = txtIDusuario.Text;
                    resul = conexionpersona.eliminar_usuario(msg);
                }
                else
                {
                   resul = "Ingrese el id del usuario a eliminar";
                }
                MessageBox.Show(resul);
                limpiar_usuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            Lista_Usuario.Items.Clear();
            lista_user = conexionpersona.leer_usuarios();
            foreach (Trabajador t in lista_user)
            {
                Lista_Usuario.Items.Add(new Trabajador { Id = t.Id, Nombre = t.Nombre, Apellido = t.Apellido, Usuario = t.Usuario, Id_rol = t.Id_rol });
            }
        }

        private void btnSalir_l_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow Login = new MainWindow();
            Login.Show();
            limpiar_usuario();
        }

        private void btnRefrescar_Click_1(object sender, RoutedEventArgs e)
        {
            limpiar_usuario();
            lista_user.Clear();
            Lista_Usuario.Items.Clear();
            lista_user = conexionpersona.leer_usuarios();
            foreach (Trabajador t in lista_user)
            {
                Lista_Usuario.Items.Add(new Trabajador { Id = t.Id, Nombre = t.Nombre, Apellido = t.Apellido, Usuario = t.Usuario, Id_rol = t.Id_rol });
            }
        }

        private void btnModificar_cliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.Id = txtdocumento_cliente.Text;
                cliente.Nombre = txtnombre_client.Text;
                cliente.Apellido = txtApellido_cliente.Text;
                cliente.Telefono = txtTelefono_cliente.Text;
                cliente.Correo = txtCorreo_cliente.Text;
                cliente.Direccion = txtDireccion_cliente.Text;
                resul = conexionpersona.modificar(cliente);
                MessageBox.Show(resul);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            limpiar_cliente();
        }
        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                producto.Codigo = txtCodigo.Text;
                producto = conexionproducto.leer(producto.Codigo);

                if(producto.Codigo is null)
                {
                    producto.Codigo = txtCodigo.Text;
                    producto.Descripcion = txtDescripcion.Text;
                    producto.Precio = Convert.ToDecimal(txtPrecio.Text);
                    producto.Cantidad = Convert.ToInt32(txtCantidad.Text);

                    msg = conexionproducto.crear(producto);
                }
                else
                {
                    msg = "El codigo ya a sido asignado a otro producto";
                }
                MessageBox.Show(msg);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
