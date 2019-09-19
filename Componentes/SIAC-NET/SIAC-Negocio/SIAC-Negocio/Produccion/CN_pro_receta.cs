using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;
using System.Data.OleDb;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_receta
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public OleDbConnection AccConec = new OleDbConnection();

        public List<BE_PRO_RECETA> lstReceta = new List<BE_PRO_RECETA>();
        public List<BE_PRO_RECETAINSUMO> lstRecetaInsumo = new List<BE_PRO_RECETAINSUMO>();
        public List<BE_PRO_RECETATAREA> lstRecetaTarea = new List<BE_PRO_RECETATAREA>();

        public DataTable Listar(int n_idempresa)
        {
            DataTable dtResul = new DataTable();

            CD_pro_receta miFun = new CD_pro_receta();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_idempresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable ListarProductosConReceta(int n_idempresa)
        {
            DataTable dtResul = new DataTable();

            CD_pro_receta miFun = new CD_pro_receta();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarProductosConReceta(n_idempresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        

        public DataTable ListarRecetaLineas(int n_idempresa)
        {
            DataTable dtResul = new DataTable();

            CD_pro_receta miFun = new CD_pro_receta();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarRecetaLineas(n_idempresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public void TraerRecetaProducto(Int64 n_IdProducto)
        {
            BE_PRO_RECETA EntCabecera = new BE_PRO_RECETA();
            bool booResult;
            DataTable DtRecIns = new DataTable();
            DataTable DtRecTar = new DataTable();
            DataTable DtRec = new DataTable();
            CD_pro_receta miFun = new CD_pro_receta();
            int n_row = 0;

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRecetaProducto(n_IdProducto);
            DtRec = miFun.dtRecCab;
            DtRecIns = miFun.dtRecIns;
            DtRecTar = miFun.dtRecTar;

            lstReceta.Clear();
            lstRecetaInsumo.Clear();
            lstRecetaTarea.Clear();

            if (DtRec.Rows.Count != 0)
            {
                // CARGAMOS LAS RECETAS
                for (n_row = 0; n_row <= DtRec.Rows.Count - 1; n_row++)
                {
                    BE_PRO_RECETA entReceta = new BE_PRO_RECETA();
                    entReceta.n_idemp = Convert.ToInt16(DtRec.Rows[n_row]["n_idemp"].ToString());
                    entReceta.n_id = Convert.ToInt16(DtRec.Rows[n_row]["n_id"].ToString());
                    entReceta.c_codrec = DtRec.Rows[n_row]["c_codrec"].ToString();
                    entReceta.n_idite = Convert.ToInt16(DtRec.Rows[n_row]["n_idite"].ToString());
                    entReceta.c_des = DtRec.Rows[n_row]["c_des"].ToString();
                    entReceta.n_idunimed = Convert.ToInt16(DtRec.Rows[n_row]["n_idunimed"].ToString());
                    entReceta.n_can = Convert.ToDouble(DtRec.Rows[n_row]["n_can"].ToString());
                    entReceta.n_prirec = Convert.ToInt16(DtRec.Rows[n_row]["n_prirec"].ToString());
                    entReceta.c_obs = DtRec.Rows[n_row]["c_obs"].ToString();
                    entReceta.n_act = Convert.ToInt16(DtRec.Rows[n_row]["n_act"].ToString());
                    lstReceta.Add(entReceta);
                }
            }

            if (DtRecIns.Rows.Count != 0)
            {
                // CARGAMOS LOS INSUMOS
                lstRecetaInsumo.Clear();
                for (n_row = 0; n_row <= DtRecIns.Rows.Count - 1; n_row++)
                {
                    BE_PRO_RECETAINSUMO entRecetaInsumo = new BE_PRO_RECETAINSUMO();
                    entRecetaInsumo.n_idrec = Convert.ToInt16(DtRecIns.Rows[n_row]["n_idrec"].ToString());
                    entRecetaInsumo.n_idite = Convert.ToInt16(DtRecIns.Rows[n_row]["n_idite"].ToString());
                    entRecetaInsumo.n_idunimed = Convert.ToInt16(DtRecIns.Rows[n_row]["n_idunimed"].ToString());
                    entRecetaInsumo.n_can = Convert.ToDouble(DtRecIns.Rows[n_row]["n_can"].ToString());
                    lstRecetaInsumo.Add(entRecetaInsumo);
                }
            }

            if (DtRecTar.Rows.Count != 0)
            {
                // CARGAMOS LAS TAREAS
                n_row = 0;
                lstRecetaTarea.Clear();
                for (n_row = 0; n_row <= DtRecTar.Rows.Count - 1; n_row++)
                {
                    BE_PRO_RECETATAREA entRecetaTarea = new BE_PRO_RECETATAREA();

                    entRecetaTarea.n_idrec = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idrec"].ToString());
                    entRecetaTarea.n_idtar = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idtar"].ToString());
                    entRecetaTarea.n_idunimed = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idunimed"].ToString());
                    entRecetaTarea.n_can = Convert.ToInt16(DtRecTar.Rows[n_row]["n_can"].ToString());
                    entRecetaTarea.n_ord = Convert.ToInt16(DtRecTar.Rows[n_row]["n_ord"].ToString());
                    entRecetaTarea.n_factor = Convert.ToDouble(DtRecTar.Rows[n_row]["n_factor"].ToString());
                    entRecetaTarea.n_cosklg = Convert.ToDouble(DtRecTar.Rows[n_row]["n_cosklg"].ToString());
                    entRecetaTarea.n_coshor = Convert.ToDouble(DtRecTar.Rows[n_row]["n_coshor"].ToString());
                    entRecetaTarea.n_jorklg = Convert.ToDouble(DtRecTar.Rows[n_row]["n_jorklg"].ToString());
                    entRecetaTarea.c_despro = DtRecTar.Rows[n_row]["c_despro"].ToString();
                    entRecetaTarea.n_numper = Convert.ToInt16(DtRecTar.Rows[n_row]["n_numper"].ToString());
                    entRecetaTarea.h_horarr = DtRecTar.Rows[n_row]["h_horarr"].ToString();
                    entRecetaTarea.n_aplpor = Convert.ToInt16(DtRecTar.Rows[n_row]["n_aplpor"].ToString());
                    entRecetaTarea.n_idare = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idare"].ToString());
                    entRecetaTarea.n_idtiptra = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idtiptra"].ToString());
                    entRecetaTarea.n_idforpag = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idforpag"].ToString());
                    lstRecetaTarea.Add(entRecetaTarea);
                }
            }
            return;
        }
        public void TraerRegistro(Int64 n_IdRegistro)
        {
            BE_PRO_RECETA EntCabecera = new BE_PRO_RECETA();
            bool booResult;
            DataTable DtRecIns = new DataTable();
            DataTable DtRecTar = new DataTable();
            DataTable DtRec = new DataTable();
            CD_pro_receta miFun = new CD_pro_receta();
            int n_row =0;

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRecetaProducto(n_IdRegistro);
            DtRec = miFun.dtRecCab;
            DtRecIns = miFun.dtRecIns;
            DtRecTar = miFun.dtRecTar;

            lstReceta.Clear();
            lstRecetaInsumo.Clear();
            lstRecetaTarea.Clear();

            if (DtRec.Rows.Count != 0)
            {
                // CARGAMOS LAS RECETAS
                for (n_row = 0; n_row < DtRec.Rows.Count; n_row++)
                {
                    BE_PRO_RECETA entReceta = new BE_PRO_RECETA();
                    entReceta.n_idemp = Convert.ToInt16(DtRec.Rows[n_row]["n_idemp"].ToString());
                    entReceta.n_id = Convert.ToInt16(DtRec.Rows[n_row]["n_id"].ToString());
                    entReceta.c_codrec = DtRec.Rows[n_row]["c_codrec"].ToString();
                    entReceta.n_idite = Convert.ToInt16(DtRec.Rows[n_row]["n_idite"].ToString());
                    entReceta.c_des = DtRec.Rows[n_row]["c_des"].ToString();
                    entReceta.n_idunimed = Convert.ToInt16(DtRec.Rows[n_row]["n_idunimed"].ToString());
                    entReceta.n_can = Convert.ToDouble(DtRec.Rows[n_row]["n_can"].ToString());
                    entReceta.n_prirec = Convert.ToInt16(DtRec.Rows[n_row]["n_prirec"].ToString());
                    entReceta.c_obs = DtRec.Rows[n_row]["c_obs"].ToString();
                    lstReceta.Add(entReceta);
                }
            }

            if (DtRecIns.Rows.Count != 0)
            { 
                // CARGAMOS LOS INSUMOS
                lstRecetaInsumo.Clear();
                for(n_row = 0; n_row < DtRecIns.Rows.Count; n_row++)
                {
                    BE_PRO_RECETAINSUMO entRecetaInsumo = new BE_PRO_RECETAINSUMO();
                    entRecetaInsumo.n_idrec = Convert.ToInt16(DtRecIns.Rows[n_row]["n_idrec"].ToString());
                    entRecetaInsumo.n_idite = Convert.ToInt16(DtRecIns.Rows[n_row]["n_idite"].ToString());
                    entRecetaInsumo.n_idunimed = Convert.ToInt16(DtRecIns.Rows[n_row]["n_idunimed"].ToString());
                    entRecetaInsumo.n_can = Convert.ToDouble(DtRecIns.Rows[n_row]["n_can"].ToString());
                    lstRecetaInsumo.Add(entRecetaInsumo);
                }
            }

            if (DtRecTar.Rows.Count != 0)
            {
                // CARGAMOS LAS TAREAS
                n_row = 0;
                lstRecetaTarea.Clear();
                for (n_row = 0; n_row < DtRecTar.Rows.Count; n_row++)
                {
                    BE_PRO_RECETATAREA entRecetaTarea = new BE_PRO_RECETATAREA();

                    entRecetaTarea.n_idrec = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idrec"].ToString());
                    entRecetaTarea.n_idtar = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idtar"].ToString());
                    entRecetaTarea.n_idunimed = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idunimed"].ToString());
                    entRecetaTarea.n_can = Convert.ToInt16(DtRecTar.Rows[n_row]["n_can"].ToString());
                    entRecetaTarea.n_ord = Convert.ToInt16(DtRecTar.Rows[n_row]["n_ord"].ToString());
                    entRecetaTarea.n_factor = Convert.ToDouble(DtRecTar.Rows[n_row]["n_factor"].ToString());
                    entRecetaTarea.n_cosklg = Convert.ToDouble(DtRecTar.Rows[n_row]["n_cosklg"].ToString());
                    entRecetaTarea.n_coshor = Convert.ToDouble(DtRecTar.Rows[n_row]["n_coshor"].ToString());
                    entRecetaTarea.n_jorklg = Convert.ToDouble(DtRecTar.Rows[n_row]["n_jorklg"].ToString());
                    entRecetaTarea.c_despro = DtRecTar.Rows[n_row]["c_despro"].ToString();
                    entRecetaTarea.n_numper = Convert.ToInt16(DtRecTar.Rows[n_row]["n_numper"].ToString());
                    entRecetaTarea.h_horarr = DtRecTar.Rows[n_row]["h_horarr"].ToString();
                    entRecetaTarea.n_aplpor = Convert.ToInt16(DtRecTar.Rows[n_row]["n_aplpor"].ToString());
                    entRecetaTarea.n_idare = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idare"].ToString());
                    entRecetaTarea.n_idtiptra = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idtiptra"].ToString());
                    entRecetaTarea.n_idforpag = Convert.ToInt16(DtRecTar.Rows[n_row]["n_idforpag"].ToString());
                    lstRecetaTarea.Add(entRecetaTarea);
                }
            }
            return;
        }
        public bool Eliminar(BE_PRO_RECETA entReceta)
        {
            bool booResult = false;
            CD_pro_receta miFun = new CD_pro_receta();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(entReceta.n_id);
            if (booResult == false)
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(List<BE_PRO_RECETA> lstReceta, List<BE_PRO_RECETAINSUMO> lstRecetaInsumo, List<BE_PRO_RECETATAREA> lstRecetaTarea)
        {
            bool booOk = false;
            CD_pro_receta miFun = new CD_pro_receta();
            miFun.mysConec = mysConec;

            booOk = miFun.Insertar(lstReceta, lstRecetaInsumo, lstRecetaTarea);
            if (booOk == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar(List<BE_PRO_RECETA> lstReceta, List<BE_PRO_RECETAINSUMO> lstRecetaInsumo, List<BE_PRO_RECETATAREA> lstRecetaTarea)
        {
            bool booOk = false;
            CD_pro_receta miFun = new CD_pro_receta();
            miFun.mysConec = mysConec;

            booOk = miFun.Actualizar(lstReceta, lstRecetaInsumo, lstRecetaTarea);

            if (booOk == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public void ReporteRecetas( int n_IdProducto)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[9, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idite";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_IdProducto.ToString();

            arrPara[2, 0] = "c_titulo1";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = "INGRESO DE INVETARIO PERMANENTE EN UNIDADES FISICAS";

            arrPara[3, 0] = "c_titulo2";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = ".";

            arrPara[4, 0] = "c_nomemp";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[5, 0] = "c_numruc";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = STU_SISTEMA.EMPRESARUC;

            c_NomArchivo = "RptRecetas.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Produccion\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PRODUCCION - REPORTE DE RECETAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
