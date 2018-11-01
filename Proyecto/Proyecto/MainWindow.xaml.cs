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
                datos.Respaldo_Tabla(txb_conexion_tabla.Text, txb_contrasena_tabla.Text, txb_nombretabla.Text);
                MessageBox.Show("Respaldo de tabla exitoso", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            datos.Respaldo_Schema(txb_conexion_Schema.Text, txb_contrasena_schema.Text);
            MessageBox.Show("Respaldo de schema exitoso", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            datos.Respaldo_Full();
            MessageBox.Show("Respaldo full exitoso", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            datos.Recuperar_Tabla(txb_conexion_tabla_recuperar.Text, txb_contrasena_tabla_recuperar.Text, txb_recuperartabla.Text);
            MessageBox.Show("Tabla recuperada exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            datos.Recuperar_Schema(txb_conexion_Schema_recuperar.Text, txb_contrasena_schema_recuperar.Text);
            MessageBox.Show("Schema recuperada exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Process proceso = new Process();
            proceso.StartInfo.FileName = @"C:\Backup";
            proceso.Start();
        }

        private void verTableSpace_Click(object sender, RoutedEventArgs e)
        {
            Mostar_TableSpace();
            Process proceso = new Process();
            proceso.StartInfo.FileName = @"C:\oraclexe\app\oracle\oradata\XE";
            proceso.Start();
        }
    }
}
