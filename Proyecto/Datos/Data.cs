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

        public int Conexion(string conexion,string contrasena)
        {
            string v_Conn = "DATA SOURCE=localhost:1521/XE;PERSIST SECURITY INFO=True;USER ID=" + conexion + ";PASSWORD = " + contrasena + "";
            try
            {
                OracleConnection conn = new OracleConnection(v_Conn);
                conn.Open();
                conn.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public void Respaldo_Tabla(string conexion, string contrasena, string nombre_tabla,string directorio)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("EXPDP " + conexion + "/" + contrasena + "@XE TABLES="+conexion+"." + nombre_tabla + " DIRECTORY="+directorio+" DUMPFILE=" + nombre_tabla + ".DMP LOGFILE=" + nombre_tabla + ".LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
        public void Respaldo_Schema(string conexion, string contrasena,string direc)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("EXPDP " + conexion + "/" + contrasena + "@XE SCHEMAS=" + conexion + " DIRECTORY="+ direc + " DUMPFILE=" + conexion + ".DMP LOGFILE=" + conexion + ".LOG;");
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

        public void Recuperar_Tabla(string conexion, string contrasena, string nombre_tabla,string direc)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("IMPDP " + conexion + "/" + contrasena + "@XE TABLES="+conexion+"." + nombre_tabla + " DIRECTORY="+direc+" DUMPFILE=" + nombre_tabla + ".DMP LOGFILE=" + nombre_tabla + ".LOG;");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        public void Recuperar_Schema(string conexion, string contrasena,string direc)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("IMPDP " + conexion + "/" + contrasena + "@XE SCHEMAS=" + conexion + " DIRECTORY="+direc+" DUMPFILE=" + conexion + ".DMP LOGFILE=" + conexion + ".LOG;");
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
        public void Incrementar(string nombre,string size)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "ALTER DATABASE DATAFILE 'C:/oraclexe/app/oracle/oradata/XE/"+nombre+".DBF' RESIZE "+size+"M";
            OracleDataReader dr = comando.ExecuteReader();
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
            conn.Close();
        }
        public DataTable Mostrar_TableSpace()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT t.tablespace_name ,t.status ,ROUND (MAX (d.bytes) / 1024 / 1024, 2),ROUND ((MAX (d.bytes) / 1024 / 1024) - (SUM (DECODE (f.bytes, NULL, 0, f.bytes)) / 1024 / 1024), 2), ROUND (SUM (DECODE (f.bytes, NULL, 0, f.bytes) ) / 1024 / 1024, 2) , t.pct_increase , SUBSTR (d.file_name, 1, 80)  FROM DBA_FREE_SPACE f, DBA_DATA_FILES d, DBA_TABLESPACES t WHERE t.tablespace_name = d.tablespace_name AND f.tablespace_name(+) = d.tablespace_name AND f.file_id(+) = d.file_id GROUP BY t.tablespace_name, d.file_name, t.pct_increase, t.status ORDER BY 1, 3 DESC";
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public void DataFile(string schema, string nombre,string size)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "ALTER TABLESPACE "+schema+" ADD DATAFILE 'C:/oraclexe/app/oracle/oradata/XE/"+nombre+".DBF' SIZE "+size+"M";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public DataTable Schemas()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT USERNAME FROM DBA_USERS";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        public DataTable Tablas(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT TABLE_NAME FROM DBA_TABLES WHERE OWNER = '" + nombre + "'";
            Console.WriteLine(comando.CommandText);
            comando.Parameters.Add(new OracleParameter("OWNER", nombre));
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public void CrearUsuario(string nombre, string contrasena)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "CREATE USER "+nombre+" IDENTIFIED BY "+contrasena+"";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public DataTable MostrarUsuarios()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "SELECT USERNAME FROM DBA_USERS";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable MostrarDirectorios()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select * from all_directories";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable MostrarPermisosUsuarios(string usuario,string contrasena)
        {
            string v_Conn = "DATA SOURCE=localhost:1521/XE;PERSIST SECURITY INFO=True;USER ID=" + usuario + ";PASSWORD = " + contrasena + "";
            OracleConnection conn = new OracleConnection(v_Conn);
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select USERNAME,GRANTED_ROLE from user_role_privs";
            Console.WriteLine(comando.CommandText);
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        public void PermisoCreate (string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "GRANT RESOURCE, CREATE SESSION TO "+user+"";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void PermisoConnect(string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "GRANT CONNECT TO " + user + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void PermisoAll(string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "GRANT ALL PRIVILEGES TO " + user + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

        public void CrearRolContrasena(string user,string contrasena)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "CREATE ROLE "+user+" IDENTIFIED BY "+contrasena+"";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

        public void CrearRolSinContrasena(string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "CREATE ROLE "+user+" NOT IDENTIFIED";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public DataTable MostrarRoles()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select ROLE from DBA_ROLES";

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable MostrarRoles(string usuario, string contrasena)
        {
            string v_Conn = "DATA SOURCE=localhost:1521/XE;PERSIST SECURITY INFO=True;USER ID=" + usuario + ";PASSWORD = " + contrasena + "";
            OracleConnection conn = new OracleConnection(v_Conn);
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select role, granted_role from role_role_privs";
            Console.WriteLine(comando.CommandText);
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public void RolInsert(string schema,string table,string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "GRANT INSERT ON "+schema+"."+table+" TO "+user+"";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void RolUpdate(string schema, string table, string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "GRANT UPDATE ON " + schema + "." + table + " TO " + user + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void RolEliminar(string schema, string table, string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "GRANT DELETE ON " + schema + "." + table + " TO " + user + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void RolSelect(string schema, string table, string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "GRANT SELECT ON " + schema + "." + table + " TO " + user + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void AsignarRol(string rol,string user)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            OracleCommand comando2 = new OracleCommand();
            comando2.Connection = conn;
            comando.CommandText = "GRANT CONNECT TO "+rol+"";
            comando2.CommandText = "GRANT "+rol+" TO "+user+"";
            OracleDataReader dr = comando.ExecuteReader();
            OracleDataReader dr2 = comando2.ExecuteReader();
            conn.Close();
        }
        public DataTable RutaDirectorio(string directorio)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select DIRECTORY_PATH from all_directories where DIRECTORY_NAME = '"+directorio+"'";
            Console.WriteLine(comando.CommandText);
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public DataTable ComboDirectorios()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "select DIRECTORY_NAME from all_directories";
            Console.WriteLine(comando.CommandText);
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }
        public void CrearDirectorio(string nombre,string ruta)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "CREATE OR REPLACE DIRECTORY "+nombre+" AS '"+ruta+"'";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }
        public void EliminarDirectorio(string nombre)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "DROP DIRECTORY "+nombre+"";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

        public DataTable MostrarAuditoria(string usuario, string contrasena)
        {
            string v_Conn = "DATA SOURCE=localhost:1521/XE;PERSIST SECURITY INFO=True;USER ID=" + usuario + ";PASSWORD = " + contrasena + "";
            OracleConnection conn = new OracleConnection(v_Conn);
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = " Select object_name, object_type, aud, del, ins, sel, upd,alt,aud,com,gra,ind,loc,ren,fbk from user_obj_audit_opts";
            Console.WriteLine(comando.CommandText);
            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            conn.Close();
            return tabla;
        }

        public int iniciarAuditoria()
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("alter system set audit_trail=DB scope=spfile", conn as OracleConnection);
            comando.CommandType = CommandType.Text;
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        public int auditoriaTotal(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("audit all ON " + esquema + "." + tabla + " by access", conn as OracleConnection);
            comando.CommandType = CommandType.Text;
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        public int auditoriaInsert(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("audit insert ON " + esquema + "." + tabla + " by access", conn as OracleConnection);
            comando.CommandType = CommandType.Text;
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;
        }

        public int AuditoriaUpdate(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("audit update ON " + esquema + "." + tabla + " by access", conn as OracleConnection);
            comando.CommandType = CommandType.Text;
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;

        }

        public int auditoriaDelete(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("audit delete ON " + esquema + "." + tabla + " by access", conn as OracleConnection);
            comando.CommandType = CommandType.Text;
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;

        }

        public int auditoriaSelect(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand("audit select ON " + esquema + "." + tabla + " by access", conn as OracleConnection);
            comando.CommandType = CommandType.Text;
            int v_Resultado = comando.ExecuteNonQuery();
            conn.Close();
            return v_Resultado;

        }

        public void auditoriaTotalBorrar(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "NOAUDIT all ON "+esquema+"."+tabla+"";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

        public void auditoriaInsertBorrar(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "NOAUDIT INSERT ON " + esquema + "." + tabla + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

        public void AuditoriaUpdateBorrar(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "NOAUDIT UPDATE ON " + esquema + "." + tabla + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

        public void auditoriaDeleteBorrar(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "NOAUDIT DELETE ON " + esquema + "." + tabla + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

        public void auditoriaSelectBorrar(string esquema, string tabla)
        {
            OracleConnection conn = DataBase.Conexion();
            conn.Open();
            OracleCommand comando = new OracleCommand();
            comando.Connection = conn;
            comando.CommandText = "NOAUDIT SELECT ON " + esquema + "." + tabla + "";
            OracleDataReader dr = comando.ExecuteReader();
            conn.Close();
        }

    }
}
