using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;
using SIAC_Objetos;
using Helper.Comunes;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_liquidacion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_EST_LIQUIDACION e_Liquidacion = new BE_EST_LIQUIDACION();
        public List<BE_EST_LIQUIDACIONDET> l_LiquidacionDet = new List<BE_EST_LIQUIDACIONDET>();

        Helper.Comunes.Funciones fungen = new Helper.Comunes.Funciones();
        
        public DataTable dtListar = new DataTable();
        public DataTable dtListarDet = new DataTable();
        public DataTable Listar(int n_IdEmpresa, int AnoTrabajo, int MesTrabajo)
        {
            DataTable dtResul = new DataTable();

            CD_est_liquidacion miFun = new CD_est_liquidacion();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, AnoTrabajo, MesTrabajo);
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResul;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            CD_est_liquidacion miFun = new CD_est_liquidacion();
            DataTable dtliquidadet = new DataTable();
            int n_row = 0;

            miFun.mysConec = mysConec;

            if (miFun.TraerRegistro(n_IdRegistro) == true)
            {
                dtListar = miFun.dtListar;
                dtliquidadet = miFun.dtLiquidaDet;
                dtListarDet = miFun.dtListarDetalle;

                if (dtListar.Rows.Count != 0)
                {
                    e_Liquidacion.n_idemp = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
		            e_Liquidacion.n_id = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
                    e_Liquidacion.n_idpla = Convert.ToInt16(dtListar.Rows[0]["n_idpla"]);
                    e_Liquidacion.n_idcaj = Convert.ToInt16(dtListar.Rows[0]["n_idcaj"]);
                    e_Liquidacion.d_fchemi = Convert.ToDateTime(dtListar.Rows[0]["d_fchemi"]);
                    e_Liquidacion.d_fchliq = Convert.ToDateTime(dtListar.Rows[0]["d_fchliq"]);
                    e_Liquidacion.n_importe = Convert.ToDouble(dtListar.Rows[0]["n_importe"]);
                    e_Liquidacion.c_obs = dtListar.Rows[0]["c_obs"].ToString();
                    e_Liquidacion.n_numdoccob = Convert.ToInt16(dtListar.Rows[0]["n_numdoccob"]);
                    e_Liquidacion.h_horliq = dtListar.Rows[0]["h_horliq"].ToString();
                    e_Liquidacion.n_tipo = Convert.ToInt16(dtListar.Rows[0]["n_tipo"]);
                }

                if (dtliquidadet.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtliquidadet.Rows.Count - 1; n_row++)
                    {
                        BE_EST_LIQUIDACIONDET e_LiquidaDet = new BE_EST_LIQUIDACIONDET();

                        e_LiquidaDet.n_idliq = Convert.ToInt16(dtliquidadet.Rows[n_row]["n_idliq"]);
                        e_LiquidaDet.n_idven = Convert.ToInt16(dtliquidadet.Rows[n_row]["n_idven"]);
                        e_LiquidaDet.n_impcob = Convert.ToDouble(dtliquidadet.Rows[n_row]["n_impcob"]);
                        
                        l_LiquidacionDet.Add(e_LiquidaDet);
                    }
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booOk = false;
            CD_est_liquidacion miFun = new CD_est_liquidacion();

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Insertar(BE_EST_LIQUIDACION e_Liquidacion, List<BE_EST_LIQUIDACIONDET> e_LiquidacionDet)
        {
            CD_est_liquidacion miFun = new CD_est_liquidacion();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Liquidacion, e_LiquidacionDet);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_LIQUIDACION e_Liquidacion, List<BE_EST_LIQUIDACIONDET> e_LiquidacionDet)
        {
            CD_est_liquidacion miFun = new CD_est_liquidacion();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Liquidacion, e_LiquidacionDet);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void Consulta1(int n_IdEmpresa, int n_IdPlaya, int n_Idcajero, string c_Fecha, int n_Tipo)
        {
            CD_est_liquidacion miFun = new CD_est_liquidacion();
            miFun.mysConec = mysConec;

            miFun.Consulta1(n_IdEmpresa, n_IdPlaya, n_Idcajero, c_Fecha, n_Tipo);
            dtListar = miFun.dtListar;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void ImprimirLiquidacion(int n_IdRegistro)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_idreg";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdRegistro.ToString();


            c_NomArchivo = "RptLiquidacion.rpt";

            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "estacionamientos\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ESTACIONAMIENTO - LIQUIDACION DE CAJA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = false;
            xVisor.c_NombreArchivoExportar = "";
            xVisor.VerCrystal();
        }
    }
}
