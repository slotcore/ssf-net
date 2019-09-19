using System;
using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using SIAC_Entidades.Sistema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SIAC_Objetos;
using Helper;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_usuarios
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        CD_sys_usuarios miFun = new CD_sys_usuarios();

        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        public DataTable dtLista = new DataTable();
        public BE_SYS_USUARIOS e_usuarios = new BE_SYS_USUARIOS();

        public DataTable TraerUsuario(string c_Usuario, string c_Password, int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResult = new DataTable();
          
            CD_sys_usuarios miFun = new CD_sys_usuarios();
            miFun.mysConec = mysConec;

            dtResult = miFun.TraerUsuario(c_Usuario, c_Password, n_IdEmpresa, n_EsUnificado);

            if (miFun.n_ErrorNumber != 0)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResult;
        }
        public void Consulta1(int n_IdEmpresa)
        {
            CD_sys_usuarios miFun = new CD_sys_usuarios();
            miFun.mysConec = mysConec;

            miFun.Consulta1(n_IdEmpresa);

            if (miFun.n_ErrorNumber != 0)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                dtLista = null;
                return;
            }
            dtLista = miFun.dtLista;
            return;
        }
        public void BuscarUsuario(int n_IdEmpresa)
        {
            CD_sys_usuarios o_usu = new CD_sys_usuarios();
            DataTable dtres = new DataTable();

            o_usu.mysConec = mysConec;
            o_usu.Consulta1(n_IdEmpresa);
            dtres = o_usu.dtLista;

            DataTable dtResult = new DataTable();
            string[,] arrCabeceraFlexFil = new string[3, 4];

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Apellidos y Nombres";
            arrCabeceraFlexFil[0, 1] = "300";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "c_apenom";

            arrCabeceraFlexFil[1, 0] = "Usuario";
            arrCabeceraFlexFil[1, 1] = "80";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "c_usuario";

            arrCabeceraFlexFil[2, 0] = "ID";
            arrCabeceraFlexFil[2, 1] = "0";
            arrCabeceraFlexFil[2, 2] = "N";
            arrCabeceraFlexFil[2, 3] = "n_id";

            funDatos.Buscar_CampoBusqueda = "n_id";
            funDatos.Buscar_CadFiltro = "";
            dtResult = funDatos.Buscar(arrCabeceraFlexFil, dtres);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    dtLista = dtResult;
                }
            }
        }
        public void Listar()
        {
            miFun.mysConec = mysConec;

            miFun.Listar();
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_usuarios.n_idemp = Convert.ToInt32(dtLista.Rows[0]["n_idemp"]);
                e_usuarios.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_usuarios.c_nom = dtLista.Rows[0]["c_nom"].ToString();
                e_usuarios.c_apepat = dtLista.Rows[0]["c_apepat"].ToString();
                e_usuarios.c_apemat = dtLista.Rows[0]["c_apemat"].ToString();
                e_usuarios.c_usuario = dtLista.Rows[0]["c_usuario"].ToString();
                e_usuarios.c_pass = dtLista.Rows[0]["c_pass"].ToString();
                e_usuarios.n_activo = Convert.ToInt32(dtLista.Rows[0]["n_activo"]);
                e_usuarios.n_idperfil = Convert.ToInt32(dtLista.Rows[0]["n_idperfil"]);
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_SYS_USUARIOS e_usuarios)
        {
            bool b_result = false;

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_usuarios);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_SYS_USUARIOS e_usuarios)
        {
            bool b_result = false;

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_usuarios);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
    }
}
