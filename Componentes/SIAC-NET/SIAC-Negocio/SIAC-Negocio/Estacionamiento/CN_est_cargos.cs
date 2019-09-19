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
    public class CN_est_cargos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public BE_EST_CARGOS e_Cargos = new BE_EST_CARGOS();
        public List<BE_EST_CARGOSCAB> l_CargosCab = new List<BE_EST_CARGOSCAB>();
        public List<BE_EST_CARGOSDET> l_CargosDet = new List<BE_EST_CARGOSDET>();

        Helper.Comunes.Funciones fungen = new Helper.Comunes.Funciones();
        CD_est_cargos miFun;

        public DataTable dtListar = new DataTable();
        public CN_est_cargos(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_cargos xFun = new CD_est_cargos(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }

        ~CN_est_cargos()
        {
            miFun = null;
            fungen = null;
            e_Cargos = null;
            l_CargosCab = null;
            l_CargosDet = null;
        }
        public DataTable Listar(int n_IdEmpresa, int AnoTrabajo, int MesTrabajo)
        {
            DataTable dtResul = new DataTable();

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
            DataTable dtCargoCab = new DataTable();
            DataTable dtCargoDet = new DataTable();
            int n_row = 0;

            if (miFun.TraerRegistro(n_IdRegistro) == true)
            {
                dtListar = miFun.dtListar;
                dtCargoCab = miFun.dtCargoCab;
                dtCargoDet = miFun.dtCargoDet;

                if (dtListar.Rows.Count != 0)
                {
                    e_Cargos.n_idemp = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
                    e_Cargos.n_id = Convert.ToInt16(dtListar.Rows[0]["n_id"]);
                    e_Cargos.n_idano = Convert.ToInt16(dtListar.Rows[0]["n_idano"]);
                    e_Cargos.n_idmes = Convert.ToInt16(dtListar.Rows[0]["n_idmes"]);
                    e_Cargos.n_idpla = Convert.ToInt16(dtListar.Rows[0]["n_idpla"]);
                    e_Cargos.d_fchemi = Convert.ToDateTime(dtListar.Rows[0]["d_fchemi"]);
                    e_Cargos.n_impbru = Convert.ToDouble(dtListar.Rows[0]["n_impbru"]);
                    e_Cargos.n_impigv = Convert.ToDouble(dtListar.Rows[0]["n_impigv"]);
                    e_Cargos.n_imptot = Convert.ToDouble(dtListar.Rows[0]["n_imptot"]);
                    e_Cargos.n_numrec = Convert.ToInt16(dtListar.Rows[0]["n_numrec"]);
                    e_Cargos.d_fchini = Convert.ToDateTime(dtListar.Rows[0]["d_fchini"]);
                    e_Cargos.c_obs = fungen.NulosC(dtListar.Rows[0]["c_obs"]);
                }

                if (dtCargoCab.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtCargoCab.Rows.Count - 1; n_row++)
                    { 
                        BE_EST_CARGOSCAB e_CargosCab = new BE_EST_CARGOSCAB();

                        e_CargosCab.n_idemp = Convert.ToInt16(dtCargoCab.Rows[n_row]["n_idemp"]);
                        e_CargosCab.n_idcar = Convert.ToInt16(dtCargoCab.Rows[n_row]["n_idcar"]);
                        e_CargosCab.n_id = Convert.ToInt16(dtCargoCab.Rows[n_row]["n_id"]);
                        e_CargosCab.n_idpla = Convert.ToInt16(dtCargoCab.Rows[n_row]["n_idpla"]);
                        e_CargosCab.n_idtipdoc = Convert.ToInt16(dtCargoCab.Rows[n_row]["n_idtipdoc"]);
                        e_CargosCab.c_numser = dtCargoCab.Rows[n_row]["c_numser"].ToString();
                        e_CargosCab.c_numdoc = dtCargoCab.Rows[n_row]["c_numdoc"].ToString();
                        e_CargosCab.d_fchemi = Convert.ToDateTime(dtCargoCab.Rows[n_row]["d_fchemi"]);
                        e_CargosCab.n_idcli = Convert.ToInt16(dtCargoCab.Rows[n_row]["n_idcli"]);
                        e_CargosCab.n_impbru = Convert.ToDouble(dtCargoCab.Rows[n_row]["n_impbru"]);
                        e_CargosCab.n_impigv = Convert.ToDouble(dtCargoCab.Rows[n_row]["n_impigv"]);
                        e_CargosCab.n_imptot = Convert.ToDouble(dtCargoCab.Rows[n_row]["n_imptot"]);
                        e_CargosCab.n_impsal = Convert.ToDouble(dtCargoCab.Rows[n_row]["n_impsal"]);
                        e_CargosCab.n_idtipdocfac = Convert.ToInt16(dtCargoCab.Rows[n_row]["n_idtipdocfac"]);
                        l_CargosCab.Add(e_CargosCab);
                    }
                }

                if (dtCargoDet.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtCargoDet.Rows.Count - 1; n_row++)
                    {
                        BE_EST_CARGOSDET e_CargosDet = new BE_EST_CARGOSDET();

                        e_CargosDet.n_idcar = Convert.ToInt16(dtCargoDet.Rows[n_row]["n_idcar"]);
                        e_CargosDet.n_idcab = Convert.ToInt16(dtCargoDet.Rows[n_row]["n_idcab"]);
                        e_CargosDet.n_idser = Convert.ToInt16(dtCargoDet.Rows[n_row]["n_idser"]);
                        e_CargosDet.n_idunimed = Convert.ToInt16(dtCargoDet.Rows[n_row]["n_idunimed"]);
                        e_CargosDet.n_impbru = Convert.ToDouble(dtCargoDet.Rows[n_row]["n_impbru"]);
                        e_CargosDet.n_impigv = Convert.ToDouble(dtCargoDet.Rows[n_row]["n_impigv"]);
                        e_CargosDet.n_imptot = Convert.ToDouble(dtCargoDet.Rows[n_row]["n_imptot"]);

                        l_CargosDet.Add(e_CargosDet);
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

            booOk = miFun.Eliminar(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Insertar(BE_EST_CARGOS e_Cargos, List<BE_EST_CARGOSCAB> l_CargosCabecera, List<BE_EST_CARGOSDET> l_CargosDetalle)
        {
            bool booOk = false;

            booOk = miFun.Insertar(e_Cargos, l_CargosCabecera, l_CargosDetalle);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_CARGOS e_Cargos, List<BE_EST_CARGOSCAB> l_CargosCabecera, List<BE_EST_CARGOSDET> l_CargosDetalle)
        {
            bool booOk = false;

            booOk = miFun.Actualizar(e_Cargos, l_CargosCabecera, l_CargosDetalle);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void Consulta1(int n_IdCliente)
        {
            DataTable dtResul = new DataTable();

            miFun.Consulta1(n_IdCliente);
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool AbonadoTieneDeuda(int n_IdCliente)
        {
            DataTable dtResul = new DataTable();
            bool b_resul = false;

            miFun.Consulta2(n_IdCliente);
            dtResul = miFun.dtListar;

            if (miFun.b_OcurrioError == false)
            {
                if (dtResul.Rows.Count == 0)
                {
                    b_resul = false;                      // FALSO SI NO TIENE DEUDA
                }
                else
                {
                    b_resul = true;                       // VERDADERO SI TIENE DEUDA
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return true;                                 // SI ES QUE HAY UN ERROR LA FUNCION DEVOLVERA QUE EL ABONADO TIENE DEUDA    
            }

            return b_resul;
        }
        public bool CambiarEstadoCargo(int n_IdCargo, int n_IdDocumentoPago, string c_FechaPago)
        {
            DataTable dtResul = new DataTable();
            bool b_result = false;
                        
            if (miFun.CambiarEstadoCargo(n_IdCargo, n_IdDocumentoPago, c_FechaPago) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool TieneCargosPagados(int n_IdCargo)
        {
            bool b_result = false;
            DataTable dtResul = new DataTable();
            
            miFun.Consulta3(n_IdCargo);
            dtListar = miFun.dtListar;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return true;                              // SI HA HABIDO ERROR LE PASA COMO VERDADERO PARA QUE NO LO BORRE
            }
            if (dtListar.Rows.Count != 0)
            {
                b_result = true;
            }
            return b_result;
        }
        public void ImprimirCargo(int n_IdCargo)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_idcar";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdCargo.ToString();

            c_NomArchivo = "Rpt_Cargos.rpt";

            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "estacionamientos\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ESTACIONAMIENTO - CARGOS A ABONADO";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = false;
            xVisor.c_NombreArchivoExportar = "";
            xVisor.VerCrystal();
        }
    }
}
