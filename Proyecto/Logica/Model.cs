using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data;

namespace Logica
{
    public class Model
    {
        Data data = new Data();

        public void Respaldo_Tabla(string conexion, string contrasena, string nombre_tabla)
        {
            data.Respaldo_Tabla(conexion,contrasena,nombre_tabla);
        }
        public void Respaldo_Schema(string conexion, string contrasena)
        {
            data.Respaldo_Schema(conexion,contrasena);
        }
        public void Respaldo_Full()
        {
            data.Respaldo_Full();
        }
        public void Recuperar_Tabla(string conexion, string contrasena, string nombre_tabla)
        {
            data.Recuperar_Tabla(conexion,contrasena,nombre_tabla);
        }
        public void Recuperar_Schema(string conexion, string contrasena)
        {
            data.Recuperar_Schema(conexion,contrasena);
        }
        public void Recuperar_Full()
        {
            data.Recuperar_Full();
        }
        public void Incremetar()
        {
            data.Incrementar();
        }

        public void CrearTablespace(string nombre, int tam)
        {
            data.CrearTablespace(nombre, tam);
        }

        public void CrearTablespaceTmp(string nombre, int tam)
        {
            data.CrearTablespaceTmp(nombre, tam);
        }
    }
}
