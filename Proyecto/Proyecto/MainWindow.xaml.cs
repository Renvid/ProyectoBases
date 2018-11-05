using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.Data;
using System.Data.OracleClient;
using Logica;

namespace ProyectoBases
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        Model datos = new Model();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int valor = datos.Conexion(cmb_Schema.Text, txb_contrasena_tabla.Text);
            if(valor == 1)
            {
                datos.Respaldo_Tabla(cmb_Schema.Text, txb_contrasena_tabla.Text, cmb_Tabla.Text);
                MessageBox.Show("Respaldo de tabla exitoso", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int valor = datos.Conexion(cmb_Schema_Conexion.Text, txb_contrasena_schema.Text);
            if (valor == 1)
            {
                datos.Respaldo_Schema(cmb_Schema_Conexion.Text, txb_contrasena_schema.Text);
                MessageBox.Show("Respaldo de schema exitoso", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            datos.Respaldo_Full();
            MessageBox.Show("Respaldo full exitoso", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int valor = datos.Conexion(cmb_recuperar_nombre.Text, txb_contrasena_tabla_recuperar.Text);
            if (valor == 1)
            {
                datos.Recuperar_Tabla(cmb_recuperar_nombre.Text, txb_contrasena_tabla_recuperar.Text, cmb_recuperar_tabla.Text);
                MessageBox.Show("Tabla recuperada exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int valor = datos.Conexion(cmb_recuperar_schema.Text, txb_contrasena_schema_recuperar.Text);
            if (valor == 1)
            {
                datos.Recuperar_Schema(cmb_recuperar_schema.Text, txb_contrasena_schema_recuperar.Text);
                MessageBox.Show("Schema recuperada exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (valor == 0)
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            datos.Recuperar_Full();
            MessageBox.Show("Los datos fueron recuperados exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Menu ventana = new Menu();
            ventana.Show();
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            datos.Incremetar(txb_NomTbs_ajustar.Text,txb_TamTbs_ajustar.Text);
            Mostar_TableSpace();

        }
        public void Mostar_TableSpace()
        {
            dtg_Mostar_Tbl.ItemsSource = datos.Mostrar_TableSpace().DefaultView;
            dtg_Mostar_Tbl.Columns[0].Header = "TableSpace";
            dtg_Mostar_Tbl.Columns[1].Header = "Estado";
            dtg_Mostar_Tbl.Columns[2].Header = "MBTamaño";
            dtg_Mostar_Tbl.Columns[3].Header = "MBUsados";
            dtg_Mostar_Tbl.Columns[4].Header = "MBLibres";
            dtg_Mostar_Tbl.Columns[5].Header = "%Incremeto";
            dtg_Mostar_Tbl.Columns[6].Header = "Fichero de datos";
        }

        private void btn_CrearTbs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                datos.CrearTablespace(txb_NomTbs.Text, Convert.ToInt32(txb_TamTbs.Text));
                Mostar_TableSpace();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_CrearTbsTmp_Click(object sender, RoutedEventArgs e)
        {
            datos.CrearTablespaceTmp(txb_NomTbsTmp.Text, Convert.ToInt32(txb_TamTbsTmp.Text));
        }

        private void Respaldo_Click(object sender, RoutedEventArgs e)
        {
            MostrarDirectorios();
        }

        private void verTableSpace_Click(object sender, RoutedEventArgs e)
        {
            Mostar_TableSpace();
        }

        private void btn_AgregarDataFile_Click(object sender, RoutedEventArgs e)
        {
            datos.DataFile(txb_Nom_Agregar.Text, txb_NomAgregarDataFile.Text, txb_Tam_Agregar.Text);
            MessageBox.Show("Datafile agregado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            Mostar_TableSpace();
        }

        private void verFicheros_Click(object sender, RoutedEventArgs e)
        {
            Process proceso = new Process();
            proceso.StartInfo.FileName = @"C:\oraclexe\app\oracle\oradata\XE";
            proceso.Start();
        }

        private void cmb_Schema_Initialized(object sender, EventArgs e)
        {
            Iniciar_Nombre_Combo_Schema(cmb_Schema);
        }

        private void cmb_Schema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Combo_Dependiente(cmb_Schema,cmb_Tabla);
        }

        private void cmb_Schema_Conexion_Initialized(object sender, EventArgs e)
        {
            Iniciar_Nombre_Combo_Schema(cmb_Schema_Conexion);
        }
        private void Iniciar_Nombre_Combo_Schema(ComboBox combo)
        {
            DataTable dt = datos.Schemas();

            foreach (DataRow fila in dt.Rows)
            {
                combo.Items.Add(Convert.ToString(fila["USERNAME"]));
            }
        }
        private void Combo_Dependiente(ComboBox padre,ComboBox dependiente)
        {
            dependiente.Items.Clear();
            DataTable dt = datos.Tablas(padre.SelectedItem.ToString());
            foreach (DataRow fila in dt.Rows)
            {
                dependiente.Items.Add(Convert.ToString(fila["TABLE_NAME"]));
            }
        }

        private void cmb_recuperar_schema_Initialized(object sender, EventArgs e)
        {
            Iniciar_Nombre_Combo_Schema(cmb_recuperar_schema);
        }

        private void cmb_recuperar_nombre_Initialized(object sender, EventArgs e)
        {
            Iniciar_Nombre_Combo_Schema(cmb_recuperar_nombre);
        }

        private void cmb_recuperar_nombre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Combo_Dependiente(cmb_recuperar_nombre,cmb_recuperar_tabla);
        }
        private void MostrarDirectorios()
        {
            dtg_MostrarDirectorios.ItemsSource = datos.MostrarDirectorios().DefaultView;
            dtg_MostrarDirectorios.Columns[0].Header = "Owner";
            dtg_MostrarDirectorios.Columns[1].Header = "Nombre Directorio";
            dtg_MostrarDirectorios.Columns[2].Header = "PATH Directorio";
        }

        private void Respaldo_Mostrar_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = datos.RutaDirectorio("RESPALDO");

            Process proceso = new Process();
            foreach (DataRow fila in dt.Rows)
            {
                proceso.StartInfo.FileName = @"" + Convert.ToString(fila["DIRECTORY_PATH"]) + "";
            }
            proceso.Start();
        }
    }
}
