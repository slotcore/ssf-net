using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;


namespace SIAC_Negocio.Produccion
{
    public class CN_pro_producciontareas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtListar = new DataTable();
        public DataTable dtListar2 = new DataTable();
        public List<BE_PRO_PRODUCCIONTAREASDET> lstTareaDet = new List<BE_PRO_PRODUCCIONTAREASDET>();

        public bool Listar(int n_IdProduccion)
        {
            bool b_result = false;
            DataTable dtResult = new DataTable();
            int n_row = 0;

            CD_pro_producciontareas miFun = new CD_pro_producciontareas();
            miFun.mysConec = mysConec;

            b_result = miFun.Listar(n_IdProduccion);

            if (b_result == true)
            {
                dtListar = miFun.dtListar;
                dtResult = miFun.dtListarPersonal;

                if (dtResult.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtResult.Rows.Count-1; n_row++)
                    {
                        BE_PRO_PRODUCCIONTAREASDET entTareaDet = new BE_PRO_PRODUCCIONTAREASDET();

                        entTareaDet.n_idpro = Convert.ToInt32(dtResult.Rows[n_row]["n_idpro"]);
                        entTareaDet.n_idtar = Convert.ToInt32(dtResult.Rows[n_row]["n_idtar"]);
                        entTareaDet.n_idper = Convert.ToInt32(dtResult.Rows[n_row]["n_idper"]);
                        entTareaDet.n_id = Convert.ToInt32(dtResult.Rows[n_row]["n_id"]);
                        entTareaDet.n_can = Convert.ToDouble(dtResult.Rows[n_row]["n_can"]);
                        entTareaDet.c_obs = dtResult.Rows[n_row]["c_obs"].ToString();
                        entTareaDet.c_horini = dtResult.Rows[n_row]["c_horini"].ToString();
                        entTareaDet.c_horter = dtResult.Rows[n_row]["c_horter"].ToString();
                        entTareaDet.n_pretar = Convert.ToDouble(dtResult.Rows[n_row]["n_pretar"]);
                        entTareaDet.n_imptot = Convert.ToDouble(dtResult.Rows[n_row]["n_imptot"]);

                        lstTareaDet.Add(entTareaDet);
                    }
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_pro_producciontareas miFun = new CD_pro_producciontareas();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Actualizar(List<BE_PRO_PRODUCCIONTAREAS> lstTar, List<BE_PRO_PRODUCCIONTAREASDET> lstTarDet)
        {
            CD_pro_producciontareas miFun = new CD_pro_producciontareas();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(lstTar, lstTarDet);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool ProcesarPagos(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino)
        {
            bool b_result = false;
            DataTable dtResult = new DataTable();
            int n_row = 0;

            CD_pro_producciontareas miFun = new CD_pro_producciontareas();
            miFun.mysConec = mysConec;

            b_result = miFun.ProcesarPagos(n_IdEmpresa, c_FechaInicio, c_FechaTermino);

            if (b_result == true)
            {
                dtListar = miFun.dtListar;
                dtListar2 = miFun.dtListar2;
                b_result = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_result;
        }
    }
}
