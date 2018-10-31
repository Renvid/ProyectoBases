using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using System.Diagnostics;

namespace Datos
{
    public class Data
    {

        public void Respaldo_Tabla(string conexion, string contrasena, string nombre_tabla)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("EXPDP " + conexion + "/" + contrasena + "@XE TABLES="+conexion+"." + nombre_tabla + " DIRECTORY=RESPALDO DUMPFILE=" + nombre_tabla + ".DMP LOGFILE=" + nombre_tabla + ".LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
        public void Respaldo_Schema(string conexion, string contrasena)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("EXPDP " + conexion + "/" + contrasena + "@XE SCHEMAS=" + conexion + " DIRECTORY=RESPALDO DUMPFILE=" + conexion + ".DMP LOGFILE=" + conexion + ".LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
        public void Respaldo_Full()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("EXPDP SYSTEM/root123@XE FULL=Y DIRECTORY=RESPALDO DUMPFILE=XE.DMP LOGFILE=XE.LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        public void Recuperar_Tabla(string conexion, string contrasena, string nombre_tabla)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("IMPDP " + conexion + "/" + contrasena + "@XE TABLES="+conexion+"." + nombre_tabla + " DIRECTORY=RESPALDO DUMPFILE=" + nombre_tabla + ".DMP LOGFILE=" + nombre_tabla + ".LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        public void Recuperar_Schema(string conexion, string contrasena)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("IMPDP " + conexion + "/" + contrasena + "@XE SCHEMAS=" + conexion + " DIRECTORY=RESPALDO DUMPFILE=" + conexion + ".DMP LOGFILE=" + conexion + ".LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        public void Recuperar_Full()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("IMPDP SYSTEM/root123@XE FULL=Y DIRECTORY=RESPALDO DUMPFILE=XE.DMP LOGFILE=XE.LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
        public void Incrementar()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "ALTER DATABASE DATAFILE 'C:/oraclexe/app/oracle/oradata/XE/ELECTORAL1.DBF' RESIZE 600M";
            OracleDataReader dr = comando.ExecuteReader();
            //int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
        }

        public void CrearTablespace(string nombre, int tam)
        {
             
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "CREATE TABLESPACE " + nombre + " DATAFILE 'C:/oraclexe/app/oracle/oradata/XE/" + nombre + ".dbf' SIZE " + tam + "M ONLINE ";
            OracleDataReader dr = comando.ExecuteReader();
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
           
        }

        public void CrearTablespaceTmp(string nombre, int tam)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "CREATE TEMPORARY TABLESPACE " + nombre + " TEMPFILE 'C:/oraclexe/app/oracle/oradata/XE/" + nombre + ".dbf' SIZE " + tam + "M AUTOEXTEND ON ";
            OracleDataReader dr = comando.ExecuteReader();
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
        }
    }
}
