using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using Logica;

namespace ProyectoBases
{
    /// <summary>
    /// Interaction logic for Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Window
    {
        public Usuarios()
        {
            InitializeComponent();
        }
        Model datos = new Model();
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            this.Close();
            ventana.Show();
        }

        private void btn_VerPermisosUsuario_Click(object sender, RoutedEventArgs e)
        {
            MostarPermisosUsuario();
        }
        private void MostarPermisosUsuario()
        {
            int valor = datos.Conexion(cmb_Usuarios.Text, txb_Contrasna_Permisos.Text);
            if (valor == 1)
            {
                dtg_PermisosUsuario.ItemsSource = datos.MostrarPermisosUsuarios(cmb_Usuarios.Text, txb_Contrasna_Permisos.Text).DefaultView;
                dtg_PermisosUsuario.Columns[0].Header = "Usuario";
                dtg_PermisosUsuario.Columns[1].Header = "Permisos";
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmb_Usuarios_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.MostrarUsuarios();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Usuarios.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void btn_CrearUsuario_Click(object sender, RoutedEventArgs e)
        {
            datos.CrearUsuario(txb_Crear_Nombre_Usuario.Text,txb_Crear_Contrasena_Usuario.Text);
            MessageBox.Show("Usuario creado exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void cmb_Usuarios_Permisos_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.MostrarUsuarios();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Usuarios_Permisos.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void btn_PermisosUsuario_Click(object sender, RoutedEventArgs e)
        {
            if(cb_Resource.IsChecked == true)
            {
                datos.PermisoCreate(cmb_Usuarios_Permisos.Text);
            }
            if (cb_Connect.IsChecked == true)
            {
                datos.PermisoConnect(cmb_Usuarios_Permisos.Text);
            }
            if (cb_All.IsChecked == true)
            {
                datos.PermisoAll(cmb_Usuarios_Permisos.Text);
            }
            MessageBox.Show("Permisos otorgados correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_CrearRol_Click(object sender, RoutedEventArgs e)
        {
            if(txb_Crear_Contrasena_Rol.Text == "")
            {
                datos.CrearRolSinContrasena(txb_Crear_Nombre_Rol.Text);
                MessageBox.Show("ROL CREADO EXITOSAMENTE", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }else
            {
                datos.CrearRolContrasena(txb_Crear_Nombre_Rol.Text,txb_Crear_Contrasena_Rol.Text);
                MessageBox.Show("ROL CREADO EXITOSAMENTE", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void cmb_Roles_Permisos_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.MostrarRoles();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Roles_Permisos.Items.Add(Convert.ToString(fila["ROLE"]));
            }
        }

        private void cmb_Schema_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Schema.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void cmb_Schema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_Tabla.Items.Clear();
            DataTable dt = datos.Tablas(cmb_Schema.SelectedItem.ToString());
            foreach (DataRow fila in dt.Rows)
            {
                cmb_Tabla.Items.Add(Convert.ToString(fila["TABLE_NAME"]));
            }
        }

        private void cmb_Usuario_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Usuario.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void cmb_Usuario_Permisos_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Usuarios_Permisos.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void btn_AsignarRol_Click(object sender, RoutedEventArgs e)
        {
            datos.AsignarRol(cmb_Roles_Permisos.Text,cmb_Usuario.Text);
            MessageBox.Show("ROL ASIGNADO CORRECTAMENTE", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_PermisosRoles_Click(object sender, RoutedEventArgs e)
        {
            if(cb_Insert.IsChecked == true)
            {
                datos.RolInsert(cmb_Schema.Text,cmb_Tabla.Text,cmb_Usuario_Permiso.Text);
            }
            if (cb_Delete.IsChecked == true)
            {
                datos.RolEliminar(cmb_Schema.Text, cmb_Tabla.Text, cmb_Usuario_Permiso.Text);
            }
            if (cb_Select.IsChecked == true)
            {
                datos.RolSelect(cmb_Schema.Text, cmb_Tabla.Text, cmb_Usuario_Permiso.Text);
            }
            if (cb_Update.IsChecked == true)
            {
                datos.RolUpdate(cmb_Schema.Text, cmb_Tabla.Text, cmb_Usuario_Permiso.Text);
            }
            MessageBox.Show("PERMISOS OTORGADOS", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_VerRoles_Click(object sender, RoutedEventArgs e)
        {
            int valor = datos.Conexion(cmb_Usuario_Rol.Text, txb_contrasenaRol.Text);
            if (valor == 1)
            {
                dtg_Roles.ItemsSource = datos.MostrarRoles(cmb_Usuario_Rol.Text, txb_contrasenaRol.Text).DefaultView;
                dtg_Roles.Columns[0].Header = "ROL";
                dtg_Roles.Columns[1].Header = "ESTADO";
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void cmb_Usuario_Rol_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Usuario_Rol.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void cmb_Usuario_Permiso_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Usuario_Permiso.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }
    }
}
