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

        private void cmb_Schema_Auditoria_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Schema_Auditoria.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void cmb_Schema_Auditoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_Tabla_Auditoria.Items.Clear();
            DataTable dt = datos.Tablas(cmb_Schema_Auditoria.SelectedItem.ToString());
            foreach (DataRow fila in dt.Rows)
            {
                cmb_Tabla_Auditoria.Items.Add(Convert.ToString(fila["TABLE_NAME"]));
            }
        }

        private void btn_CrearAuditoria_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Insert_Auditoria.IsChecked == true)
            {
                datos.auditoriaInsert(cmb_Schema_Auditoria.Text,cmb_Tabla_Auditoria.Text);
            }
            if (cb_Delete_Auditoria.IsChecked == true)
            {
                datos.auditoriaDelete(cmb_Schema_Auditoria.Text, cmb_Tabla_Auditoria.Text);
            }
            if (cb_Select_Auditoria.IsChecked == true)
            {
                datos.auditoriaSelect(cmb_Schema_Auditoria.Text, cmb_Tabla_Auditoria.Text);
            }
            if (cb_Update_Auditoria.IsChecked == true)
            {
                datos.AuditoriaUpdate(cmb_Schema_Auditoria.Text, cmb_Tabla_Auditoria.Text);
            }
            if (cb_Full_Auditoria.IsChecked == true)
            {
                datos.auditoriaTotal(cmb_Schema_Auditoria.Text, cmb_Tabla_Auditoria.Text);
            }
            MessageBox.Show("AUDITORIA CREADA", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void MostrarAuditoria()
        {
            int valor = datos.Conexion(cmb_Ver_Auditoria.Text, txb_Contrasena_Auditoria.Text);
            if (valor == 1)
            {
                dtg_Auditorias.ItemsSource = datos.MostrarAuditoria(cmb_Ver_Auditoria.Text, txb_Contrasena_Auditoria.Text).DefaultView;
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmb_Ver_Auditoria_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Ver_Auditoria.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void btn_VerAuditoria_Click(object sender, RoutedEventArgs e)
        {
            MostrarAuditoria();
        }

        private void btn_Iniciar_Auditoria_Click(object sender, RoutedEventArgs e)
        {
            datos.iniciarAuditoria();
            MessageBox.Show("AuditoriaIniciada", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_BorrarAuditoria_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Insert_Auditoria_B.IsChecked == true)
            {
                datos.auditoriaInsertBorrar(cmb_Schema_Auditoria_B.Text, cmb_Tabla_Auditoria_B.Text);
            }
            if (cb_Delete_Auditoria_B.IsChecked == true)
            {
                datos.auditoriaDeleteBorrar(cmb_Schema_Auditoria_B.Text, cmb_Tabla_Auditoria_B.Text);
            }
            if (cb_Select_Auditoria_B.IsChecked == true)
            {
                datos.auditoriaSelectBorrar(cmb_Schema_Auditoria_B.Text, cmb_Tabla_Auditoria_B.Text);
            }
            if (cb_Update_Auditoria_B.IsChecked == true)
            {
                datos.AuditoriaUpdateBorrar(cmb_Schema_Auditoria_B.Text, cmb_Tabla_Auditoria_B.Text);
            }
            if (cb_Full_Auditoria_B.IsChecked == true)
            {
                datos.auditoriaTotalBorrar(cmb_Schema_Auditoria_B.Text, cmb_Tabla_Auditoria_B.Text);
            }
            MessageBox.Show("AUDITORIA BORRADA", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void cmb_Schema_Auditoria_B_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Schema_Auditoria_B.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void cmb_Schema_Auditoria_B_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_Tabla_Auditoria_B.Items.Clear();
            DataTable dt = datos.Tablas(cmb_Schema_Auditoria_B.SelectedItem.ToString());
            foreach (DataRow fila in dt.Rows)
            {
                cmb_Tabla_Auditoria_B.Items.Add(Convert.ToString(fila["TABLE_NAME"]));
            }
        }
        public void MostrarTunning()
        {
            dtg_Tunning.ItemsSource = datos.MostrarTunning().DefaultView;
        }

        private void dtg_Tunning_Initialized(object sender, EventArgs e)
        {
            MostrarTunning();
        }

        private void MostrarEstadisticas()
        {
            int valor = datos.Conexion(cmb_Ver_Auditoria.Text, txb_Contrasena_Auditoria.Text);
            if (valor == 1)
            {
                dtg_Auditorias.ItemsSource = datos.MostrarAuditoria(cmb_Ver_Auditoria.Text, txb_Contrasena_Auditoria.Text).DefaultView;
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmb_Schema_Est_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Schema_Est.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void cmb_Schema_Est_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_tabla_Est.Items.Clear();
            DataTable dt = datos.Tablas(cmb_Schema_Est.SelectedItem.ToString());
            foreach (DataRow fila in dt.Rows)
            {
                cmb_tabla_Est.Items.Add(Convert.ToString(fila["TABLE_NAME"]));
            }
        }

        private void btn_VerEst_Click(object sender, RoutedEventArgs e)
        {
            int valor = datos.Conexion(cmb_Schema_Est.Text, txb_Contrasena_Est.Text);
            if (valor == 1)
            {
                dtg_Estadisticas.ItemsSource = datos.MostrarEstadisticas(cmb_Schema_Est.Text, txb_Contrasena_Est.Text,cmb_tabla_Est.Text).DefaultView;
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_EstSchema_Click(object sender, RoutedEventArgs e)
        {
            datos.EstadisticaSchema(cmb_Schema_Est.Text);
            MessageBox.Show("Estadistica Schema Creada Correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_EstTabla_Click(object sender, RoutedEventArgs e)
        {
            datos.EstadisticaTabla(cmb_Schema_Est.Text,cmb_tabla_Est.Text);
            MessageBox.Show("Estadistica Tabla Creada Correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_Analizar_Click(object sender, RoutedEventArgs e)
        {
            datos.EstadisticaAnalizar(cmb_Schema_Est.Text, cmb_tabla_Est.Text);
            MessageBox.Show("Estadistica Analizada", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void cmb_Schema_Plan_Initialized(object sender, EventArgs e)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                cmb_Schema_Plan.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }

        private void cmb_Schema_Plan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_Tabla_Plan.Items.Clear();
            DataTable dt = datos.Tablas(cmb_Schema_Plan.SelectedItem.ToString());
            foreach (DataRow fila in dt.Rows)
            {
                cmb_Tabla_Plan.Items.Add(Convert.ToString(fila["TABLE_NAME"]));
            }
        }

        private void btn_CrearIndex_Click(object sender, RoutedEventArgs e)
        {
            datos.CrearIndex(cmb_Tabla_Plan.Text,cmb_columna_Plan.Text);
            MessageBox.Show("INDICE CREADO", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            MostrarTunning();
        }

        private void btn_CrearPlan_Click(object sender, RoutedEventArgs e)
        {
            datos.EjecutarPlan(txb_Plan.Text);
            MessageBox.Show("PLAN CREADO CREADO", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            MostrarTunning();
        }

        private void btn_BorrarPlan_Click(object sender, RoutedEventArgs e)
        {
            datos.EliminarPlan();
            MessageBox.Show("PLAN CREADO CREADO", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            MostrarTunning();
        }

        private void cmb_Tabla_Plan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_columna_Plan.Items.Clear();
            DataTable dt = datos.ComboColumnas(cmb_Tabla_Plan.SelectedItem.ToString());
            foreach (DataRow fila in dt.Rows)
            {
                cmb_columna_Plan.Items.Add(Convert.ToString(fila["column_name"]));
            }
        }
    }
}
