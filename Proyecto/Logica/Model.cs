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

        public void Respaldo_Tabla(string conexion, string contrasena, string nombre_tabla,string direc)
        {
            data.Respaldo_Tabla(conexion,contrasena,nombre_tabla,direc);
        }
        public void Respaldo_Schema(string conexion, string contrasena,string d)
        {
            data.Respaldo_Schema(conexion,contrasena,d);
        }
        public void Respaldo_Full()
        {
            data.Respaldo_Full();
        }
        public void Recuperar_Tabla(string conexion, string contrasena, string nombre_tabla,string direc)
        {
            data.Recuperar_Tabla(conexion,contrasena,nombre_tabla,direc);
        }
        public void Recuperar_Schema(string conexion, string contrasena,string direc)
        {
            data.Recuperar_Schema(conexion,contrasena,direc);
        }
        public void Recuperar_Full()
        {
            data.Recuperar_Full();
        }
        public void Incremetar(string nombre,string size)
        {
            data.Incrementar(nombre,size);
        }

        public void CrearTablespace(string nombre, int tam)
        {
            data.CrearTablespace(nombre, tam);
        }

        public void CrearTablespaceTmp(string nombre, int tam)
        {
            data.CrearTablespaceTmp(nombre, tam);
        }
        public DataTable Mostrar_TableSpace()
        {
            return data.Mostrar_TableSpace();
        }
        public void DataFile(string schema, string nombre, string size)
        {
            data.DataFile(schema,nombre,size);
        }
        public DataTable Schemas()
        {
            return data.Schemas();
        }
        public DataTable Tablas(string nombre)
        {
            return data.Tablas(nombre);
        }
        public int Conexion(string conexion, string contrasena)
        {
            return data.Conexion(conexion, contrasena);
        }
        public void CrearUsuario(string nombre, string contrasena)
        {
            data.CrearUsuario(nombre,contrasena);
        }
        public DataTable MostrarUsuarios()
        {
            return data.MostrarUsuarios();
        }
        public DataTable MostrarDirectorios()
        {
            return data.MostrarDirectorios();
        }
        public DataTable MostrarPermisosUsuarios(string usuario,string con)
        {
            return data.MostrarPermisosUsuarios(usuario,con);
        }
        public void PermisoCreate(string user)
        {
            data.PermisoCreate(user);
        }
        public void PermisoConnect(string user)
        {
            data.PermisoConnect(user);
        }
        public void PermisoAll(string user)
        {
            data.PermisoAll(user);
        }
        public void CrearRolContrasena(string user, string contrasena)
        {
            data.CrearRolContrasena(user,contrasena);
        }

        public void CrearRolSinContrasena(string user)
        {
            data.CrearRolSinContrasena(user);
        }
        public DataTable MostrarRoles()
        {
            return data.MostrarRoles();
        }
        public DataTable MostrarRoles(string usuario, string contrasena)
        {
            return data.MostrarRoles(usuario,contrasena);
        }
        public void RolInsert(string schema, string table, string user)
        {
            data.RolInsert(schema,table,user);
        }
        public void RolUpdate(string schema, string table, string user)
        {
            data.RolUpdate(schema, table, user);
        }
        public void RolEliminar(string schema, string table, string user)
        {
            data.RolEliminar(schema, table, user);
        }
        public void RolSelect(string schema, string table, string user)
        {
            data.RolSelect(schema, table, user);
        }
        public void AsignarRol(string rol, string user)
        {
            data.AsignarRol(rol, user);
        }
        public DataTable RutaDirectorio(string directorio)
        {
            return data.RutaDirectorio(directorio);
        }
        public DataTable ComboDirectorios()
        {
            return data.ComboDirectorios();
        }
        public void CrearDirectorio(string nombre, string ruta)
        {
            data.CrearDirectorio(nombre, ruta);
        }
        public void EliminarDirectorio(string nombre)
        {
            data.EliminarDirectorio(nombre);
        }
        public DataTable MostrarAuditoria(string usuario, string contrasena)
        {
            return data.MostrarAuditoria(usuario,contrasena);
        }

        public int iniciarAuditoria()
        {
            return data.iniciarAuditoria();
        }

        public int auditoriaTotal(string esquema, string tabla)
        {
            return data.auditoriaTotal(esquema,tabla);
        }

        public int auditoriaInsert(string esquema, string tabla)
        {
            return data.auditoriaInsert(esquema, tabla);
        }

        public int AuditoriaUpdate(string esquema, string tabla)
        {
            return data.AuditoriaUpdate(esquema, tabla);
        }

        public int auditoriaDelete(string esquema, string tabla)
        {
            return data.auditoriaDelete(esquema, tabla);
        }

        public int auditoriaSelect(string esquema, string tabla)
        {
            return data.auditoriaSelect(esquema, tabla);
        }
        public void auditoriaTotalBorrar(string esquema, string tabla)
        {
            data.auditoriaTotalBorrar(esquema, tabla);
        }

        public void auditoriaInsertBorrar(string esquema, string tabla)
        {
            data.auditoriaInsertBorrar(esquema, tabla);
        }

        public void AuditoriaUpdateBorrar(string esquema, string tabla)
        {
            data.AuditoriaUpdateBorrar(esquema, tabla);
        }

        public void auditoriaDeleteBorrar(string esquema, string tabla)
        {
            data.auditoriaDeleteBorrar(esquema, tabla);
        }

        public void auditoriaSelectBorrar(string esquema, string tabla)
        {
            data.auditoriaSelectBorrar(esquema,tabla);
        }
        public DataTable MostrarTunning()
        {
            return data.MostrarTunning();
        }
        public DataTable MostrarEstadisticas(string usuario, string contrasena, string ntabla)
        {
            return data.MostrarEstadisticas(usuario,contrasena,ntabla);
        }
        public void EstadisticaSchema(string schema)
        {
            data.EstadisticaSchema(schema);
        }
        public void EstadisticaTabla(string schema, string tabla)
        {
            data.EstadisticaTabla(schema,tabla);
        }

        public void EstadisticaAnalizar(string schema, string tabla)
        {
            data.EstadisticaAnalizar(schema, tabla);
        }
        public DataTable ComboColumnas(string ntabla)
        {
            return data.ComboColumnas(ntabla);
        }
        public void EliminarPlan()
        {
            data.EliminarPlan();
        }

        public void EjecutarPlan(string plan)
        {
            data.EjecutarPlan(plan);
        }
        public void CrearIndex(string tabla, string columna)
        {
            data.CrearIndex(tabla,columna);
        }
    }
}
