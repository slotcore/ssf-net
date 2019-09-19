using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Sistema;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Ventas;
using SIAC_Entidades;
using SIAC_Entidades.Tesoreria;
using SIAC_Objetos.Constantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;


namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_documentos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtLista = new DataTable();

        public BE_TES_DOCUMENTOS e_Documentos = new BE_TES_DOCUMENTOS();
        public void Listar()
        {
            CD_tes_documentos miFun = new CD_tes_documentos();
            miFun.mysConec = mysConec;

            miFun.Listar();
            dtLista = miFun.dtDocumentos;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
        }
        public DataTable BuscarDocumentos(DataTable dtDocumentos)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();

            arrCabeceraDg1[0, 0] = "Descripcion";
            arrCabeceraDg1[0, 1] = "400";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_des";

            arrCabeceraDg1[1, 0] = "Abr";
            arrCabeceraDg1[1, 1] = "40";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abr";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtDocumentos);

            return dtResult;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            bool booResult;
            CD_tes_documentos miFun = new CD_tes_documentos();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtLista;

            if (dtResult.Rows.Count != 0)
            {
                e_Documentos.n_id = Convert.ToInt16(dtResult.Rows[0]["n_id"]);
                e_Documentos.c_des = dtResult.Rows[0]["c_des"].ToString();
                e_Documentos.c_abr = dtResult.Rows[0]["c_abr"].ToString();
                e_Documentos.n_tipo = Convert.ToInt16(dtResult.Rows[0]["n_tipo"]);
                e_Documentos.n_sel = Convert.ToInt16(dtResult.Rows[0]["n_sel"]);
            }
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_tes_documentos miFun = new CD_tes_documentos();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_DOCUMENTOS e_Documentos)
        {
            CD_tes_documentos miFun = new CD_tes_documentos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Documentos);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_DOCUMENTOS e_Documentos)
        {
            CD_tes_documentos miFun = new CD_tes_documentos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Documentos);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
