using System;
using Oracle.DataAccess.Client;
namespace Datos
{
    public static class DataBase
    {
        private static string v_Conn = "DATA SOURCE=localhost:1521/XE;PERSIST SECURITY INFO=True;USER ID=ELECTORAL;PASSWORD = root123";//Establecemos la conexion con la base de datos.


        public static OracleConnection Conexion()
        {
            return new OracleConnection(v_Conn);
        }

        public static OracleCommand ObtenerComando(string v_Comando, OracleConnection v_Conn)
        {
            return new OracleCommand(v_Comando, v_Conn as OracleConnection);
        }
    }
}
