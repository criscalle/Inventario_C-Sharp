using Proyecto_H2.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_H2
{
  

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Trabajador user = new Trabajador();
                string[] usuario = new string[2];
                string datos = "Server=localhost; database=don_banano; user=root";
                Conexion.ConexionPersona conexionpersona = new Conexion.ConexionPersona(datos);
                usuario[0] = txtUsuario.Text;
                usuario[1] = txtContrasena.Password.ToString();

                user = conexionpersona.validar(usuario);

                if (user.Id_rol == 1)
                {
                    MessageBox.Show("Ingreso exitoso");
                    this.Hide();
                    Don_Banano don = new Don_Banano();
                    don.Show();
                    

                }
                else if (user.Id_rol == 2)
                {
                    MessageBox.Show("Ingreso exitoso");
                    this.Hide();
                    Don_Banano_Ventas don_ventas = new Don_Banano_Ventas();
                    don_ventas.Show();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña invalidos");
                    txtUsuario.Clear();
                    txtContrasena.Clear();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
    }
}
