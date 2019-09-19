using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Logistica;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;
using Helper;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_programa
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public BE_PRO_PROGRAMA entPrograma = new BE_PRO_PROGRAMA();
        public List<BE_PRO_PROGRAMADET> lstProgramaDet = new List<BE_PRO_PROGRAMADET>();
        public List<BE_PRO_PROGRAMADETPRO> lstProgramaDetPro = new List<BE_PRO_PROGRAMADETPRO>();
        public List<BE_PRO_PROGRAMADETPROCRON> lstProgramaDetProCron = new List<BE_PRO_PROGRAMADETPROCRON>();

        //public List<BE_PRO_PROGRAMADETPROLIN> lstprogramadetprolin = new List<BE_PRO_PROGRAMADETPROLIN>();
        //public List<BE_PRO_PROGRAMADETPROLINDET> lstprogramadetprolindet = new List<BE_PRO_PROGRAMADETPROLINDET>();
        
        public DataTable dtListar = new DataTable();
        public DataTable dtConsulta = new DataTable();

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFundat = new Helper.Genericas();
        DatosMySql FunMysql = new DatosMySql();

        public bool Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_Result = false;

            CD_pro_programa miFun = new CD_pro_programa();
            miFun.mysConec = mysConec;

            b_Result = miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);
            if (b_Result == true)
            {
                dtListar = miFun.dtPrograma;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_Result;
        }
        public bool TraerRegistro(int n_Idregistro)
        {
            bool b_Result = false;
            int n_Fila = 0;
            CD_pro_programa miFun = new CD_pro_programa();
            DataTable dtPro = new DataTable();
            DataTable dtProDet = new DataTable();
            DataTable dtProDetPro = new DataTable();
            DataTable dtProDetProCron = new DataTable();
            //DataTable dtProDetProLin = new DataTable();
            //DataTable dtProDetProLindet = new DataTable();
            
            miFun.mysConec = mysConec;

            b_Result = miFun.TraerRegistro(n_Idregistro);
            if (b_Result == true)
            {
                dtPro = miFun.dtPrograma;
                dtProDet = miFun.dtProgramaDet;
                dtProDetPro = miFun.dtProgramaDetPro;
                dtProDetProCron = miFun.dtProgramaDetProCron;

                //dtProDetProLin = miFun.dtProgramaDetProLin;
                //dtProDetProLindet = miFun.dtProgramaDetProLindet;

                if (dtPro.Rows.Count != 0)
                {
                    entPrograma.n_idemp = Convert.ToInt32(dtPro.Rows[0]["n_idemp"]);
                    entPrograma.n_id = Convert.ToInt32(dtPro.Rows[0]["n_id"]);
                    entPrograma.n_idtipdoc = Convert.ToInt16(dtPro.Rows[0]["n_idtipdoc"]);
                    entPrograma.n_idpro = Convert.ToInt16(dtPro.Rows[0]["n_idpro"]);
                    entPrograma.c_numser = dtPro.Rows[0]["c_numser"].ToString();
                    entPrograma.c_numdoc = dtPro.Rows[0]["c_numdoc"].ToString();
                    entPrograma.n_anotra = Convert.ToInt16(dtPro.Rows[0]["n_anotra"]);
                    entPrograma.n_mestra = Convert.ToInt16(dtPro.Rows[0]["n_mestra"]);
                    entPrograma.d_fchini = Convert.ToDateTime(dtPro.Rows[0]["d_fchini"]);
                    entPrograma.d_fchfin = Convert.ToDateTime(dtPro.Rows[0]["d_fchfin"]);
                    entPrograma.d_fchemi = Convert.ToDateTime(dtPro.Rows[0]["d_fchemi"]);
                    entPrograma.c_obs = dtPro.Rows[0]["c_obs"].ToString();
                    entPrograma.n_numhordia = Convert.ToInt32(dtPro.Rows[0]["n_numhordia"]);
                    entPrograma.n_numdiapro = Convert.ToInt32(dtPro.Rows[0]["n_numdiapro"]);
                }
                if (dtProDet.Rows.Count != 0)
                {
                    n_Fila = 0;
                    for (n_Fila = 0; n_Fila <= dtProDet.Rows.Count-1; n_Fila++)
                    { 
                        BE_PRO_PROGRAMADET entProgramaDet = new BE_PRO_PROGRAMADET();
                        entProgramaDet.n_idpro = Convert.ToInt32(dtProDet.Rows[n_Fila]["n_idpro"]);
                        entProgramaDet.n_idordpro = Convert.ToInt32(dtProDet.Rows[n_Fila]["n_idordpro"]);
                        lstProgramaDet.Add(entProgramaDet);
                    }
                }
                if (dtProDetPro.Rows.Count != 0)
                {
                    n_Fila = 0;
                    for (n_Fila = 0; n_Fila <= dtProDetPro.Rows.Count - 1; n_Fila++)
                    {
                        BE_PRO_PROGRAMADETPRO entProgramaDetPro = new BE_PRO_PROGRAMADETPRO();

                        entProgramaDetPro.n_idpro = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_idpro"]);
                        entProgramaDetPro.n_idordpro = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_idordpro"]);
                        entProgramaDetPro.n_idite = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_idite"]);
                        entProgramaDetPro.n_idrec = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_idrec"]);
                        entProgramaDetPro.n_idunimed = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_idunimed"]);
                        entProgramaDetPro.n_can = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_can"]);
                        if (dtProDetPro.Rows[n_Fila]["d_fchent"].ToString() == "")
                        {
                            entProgramaDetPro.d_fchent = null;
                        }
                        else 
                        {
                            entProgramaDetPro.d_fchent = Convert.ToDateTime(dtProDetPro.Rows[n_Fila]["d_fchent"]);
                        }

                        if (dtProDetPro.Rows[n_Fila]["d_fchpro"].ToString() == "")
                        {
                            entProgramaDetPro.d_fchpro = null;
                        }
                        else
                        { 
                            entProgramaDetPro.d_fchpro = Convert.ToDateTime(dtProDetPro.Rows[n_Fila]["d_fchpro"]);
                        }
                        entProgramaDetPro.h_horini = dtProDetPro.Rows[n_Fila]["h_horini"].ToString();
                        if (dtProDetPro.Rows[n_Fila]["h_horfin"].ToString() == "")
                        {
                            entProgramaDetPro.h_horfin = null;
                        }
                        else
                        {
                            entProgramaDetPro.h_horfin = dtProDetPro.Rows[n_Fila]["h_horfin"].ToString();
                        }
                        entProgramaDetPro.n_idres = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_idres"]);
                        entProgramaDetPro.n_idlin = Convert.ToInt32(dtProDetPro.Rows[n_Fila]["n_idlin"]);

                        lstProgramaDetPro.Add(entProgramaDetPro);
                    }
                }

                if (dtProDetProCron.Rows.Count != 0)
                {
                    n_Fila = 0;
                    for (n_Fila = 0; n_Fila <= dtProDetProCron.Rows.Count - 1; n_Fila++)
                    {
                        BE_PRO_PROGRAMADETPROCRON entCron = new BE_PRO_PROGRAMADETPROCRON();

                        entCron.n_idpro = Convert.ToInt32(dtProDetProCron.Rows[n_Fila]["n_idpro"]);
                        entCron.n_idordpro = Convert.ToInt32(dtProDetProCron.Rows[n_Fila]["n_idordpro"]);
                        entCron.n_idproducto = Convert.ToInt32(dtProDetProCron.Rows[n_Fila]["n_idproducto"]);
                        entCron.d_fchpro = Convert.ToDateTime(dtProDetProCron.Rows[n_Fila]["d_fchpro"]);
                        entCron.d_fchent = Convert.ToDateTime(dtProDetProCron.Rows[n_Fila]["d_fchent"]);
                        entCron.n_can = Convert.ToDouble(dtProDetProCron.Rows[n_Fila]["n_can"]);

                        lstProgramaDetProCron.Add(entCron);
                    }
                }

                //if (dtProDetProLindet.Rows.Count != 0)
                //{
                //    n_Fila = 0;
                //    for (n_Fila = 0; n_Fila <= dtProDetProLindet.Rows.Count - 1; n_Fila++)
                //    {
                //        BE_PRO_PROGRAMADETPROLINDET entProgramaDetProLinDet = new BE_PRO_PROGRAMADETPROLINDET();

                //        entProgramaDetProLinDet.n_idpro = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_idpro"]);
                //        entProgramaDetProLinDet.n_idordpro = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_idordpro"]);
                //        entProgramaDetProLinDet.n_idite = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_idite"]);
                //        entProgramaDetProLinDet.n_idrec = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_idrec"]);
                //        entProgramaDetProLinDet.n_idlin = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_idlin"]);
                //        entProgramaDetProLinDet.n_idtar = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_idtar"]);
                //        entProgramaDetProLinDet.n_porefi = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_porefi"]);
                //        entProgramaDetProLinDet.n_cankilpro = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_cankilpro"]);
                //        entProgramaDetProLinDet.n_numpertar = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_numpertar"]);
                //        entProgramaDetProLinDet.n_idequipo = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_idequipo"]);
                //        entProgramaDetProLinDet.n_canequi = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_canequi"]);
                //        entProgramaDetProLinDet.n_numpertarequ = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_numpertarequ"]);
                //        entProgramaDetProLinDet.n_capkilporper = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_capkilporper"]);
                //        entProgramaDetProLinDet.n_capkilporhorlin = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_capkilporhorlin"]);
                //        entProgramaDetProLinDet.n_capkilporlintietra = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_capkilporlintietra"]);
                //        entProgramaDetProLinDet.n_numpercal = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_numpercal"]);
                //        entProgramaDetProLinDet.n_totprotietra = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_totprotietra"]);
                //        entProgramaDetProLinDet.n_porefiuni = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_porefiuni"]);
                //        entProgramaDetProLinDet.n_porefitot = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_porefitot"]);
                //        entProgramaDetProLinDet.n_costar = Convert.ToDouble(dtProDetProLindet.Rows[n_Fila]["n_costar"]);
                //        entProgramaDetProLinDet.n_ord = Convert.ToInt32(dtProDetProLindet.Rows[n_Fila]["n_ord"]);

                //        lstprogramadetprolindet.Add(entProgramaDetProLinDet);
                //    }
                //}
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_Result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            int n_row = 0;
            CD_pro_programa miFun = new CD_pro_programa();
            CD_pro_ordenproduccion miOrdPro = new CD_pro_ordenproduccion();
            DataTable dtResul = new DataTable();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            dtResul = miFun.dtProgramaDet;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == true)
            {
                // mysConec.Open(); 
                //miOrdPro.mysConec = mysConec;

                for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                {
                    mysConec = FunMysql.ReAbrirConeccion(mysConec);
                    miOrdPro.mysConec = mysConec;
                    booResult = miOrdPro.ActualizarEstadoOrdenProduccion(Convert.ToInt32(dtResul.Rows[n_row]["n_idordpro"]), 1);                                         // ACTUALIZAMOS EL ESTADO DE LA ORDEN DE PRODUCCION A 1 = PENDIENTE
                    if (booResult == false)
                    {
                        booOcurrioError = miOrdPro.booOcurrioError;
                        StrErrorMensaje = miOrdPro.StrErrorMensaje;
                        IntErrorNumber = miOrdPro.IntErrorNumber;
                    }
                }

                //for (n_row = 0; n_row <= lstProgramaDet.Count - 1; n_row++)
                //{
                //    booResult = miOrdPro.ActualizarEstadoOrdenProduccion(lstProgramaDet[n_row].n_idordpro, 1);                                         // ACTUALIZAMOS EL ESTADO DE LA ORDEN DE PRODUCCION A 1 = PENDIENTE
                //    if (booResult == false)
                //    {
                //        booOcurrioError = miOrdPro.booOcurrioError;
                //        StrErrorMensaje = miOrdPro.StrErrorMensaje;
                //        IntErrorNumber = miOrdPro.IntErrorNumber;
                //    }
                //}
            }
            else
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_PROGRAMA entProgramaU, List<BE_PRO_PROGRAMADET> lstProgramaDetU, List<BE_PRO_PROGRAMADETPRO> lstProgramaDetProU,
                List<BE_PRO_PROGRAMADETPROCRON> lstProgramaDetProCronU)
        {
            CD_pro_programa miFun = new CD_pro_programa();
            CD_pro_ordenproduccion miOrdPro = new CD_pro_ordenproduccion();
            bool booOk = false;
            int n_fila = 0;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entProgramaU, lstProgramaDetU, lstProgramaDetProU, lstProgramaDetProCronU);
            if (booOk == true)
            {
                
                // ACTUALIZAMOS EL ESTADO DE LAS ORDENES DE PRODUCCION CARGADAS
                for (n_fila = 0; n_fila <= lstProgramaDetProU.Count - 1; n_fila++)
                {
                    mysConec = FunMysql.ReAbrirConeccion(mysConec);
                    miOrdPro.mysConec = mysConec;
                    booOk = miOrdPro.ActualizarEstadoOrdenProduccion(lstProgramaDetProU[n_fila].n_idordpro, 3);                                         // ACTUALIZAMOS EL ESTADO A 3 = PROCESADO                    
                    if (booOk == false)
                    {
                        booOcurrioError = miOrdPro.booOcurrioError;
                        StrErrorMensaje = miOrdPro.StrErrorMensaje;
                        IntErrorNumber = miOrdPro.IntErrorNumber;
                    }
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar(BE_PRO_PROGRAMA entProgramaU, List<BE_PRO_PROGRAMADET> lstProgramaDetU, List<BE_PRO_PROGRAMADETPRO> lstProgramaDetProU,
                List<BE_PRO_PROGRAMADETPROCRON> lstProgramaDetProCronU)
        {
            CD_pro_programa miFun = new CD_pro_programa();
            CD_pro_ordenproduccion miOrdPro = new CD_pro_ordenproduccion();
            bool booOk = false;
            int n_fila = 0;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entProgramaU, lstProgramaDetU, lstProgramaDetProU, lstProgramaDetProCronU);
            if (booOk == true)
            {
                
                // ACTUALIZAMOS EL ESTADO DE LAS ORDENES DE PRODUCCION CARGADAS
                for (n_fila = 0; n_fila <= lstProgramaDetProU.Count - 1; n_fila++)
                {
                    mysConec = FunMysql.ReAbrirConeccion(mysConec);
                    miOrdPro.mysConec = mysConec;
                    booOk = miOrdPro.ActualizarEstadoOrdenProduccion(lstProgramaDetProU[n_fila].n_idordpro, 3);                                         // ACTUALIZAMOS EL ESTADO A 3 = PROCESADO                    
                    if (booOk == false)
                    {
                        booOcurrioError = miOrdPro.booOcurrioError;
                        StrErrorMensaje = miOrdPro.StrErrorMensaje;
                        IntErrorNumber = miOrdPro.IntErrorNumber;
                    }
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public bool Consulta1(int n_IdEmpresa, int n_IdPrograma)
        {
            bool b_Result = false;

            CD_pro_programa miFun = new CD_pro_programa();
            miFun.mysConec = mysConec;

            b_Result = miFun.Consulta1(n_IdEmpresa, n_IdPrograma);
            if (b_Result == true)
            {
                dtConsulta = miFun.dtConsulta;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_Result;
        }
        public bool Consulta2(int n_IdEmpresa)
        {
            bool b_Result = false;

            CD_pro_programa miFun = new CD_pro_programa();
            miFun.mysConec = mysConec;

            b_Result = miFun.Consulta2(n_IdEmpresa);
            if (b_Result == true)
            {
                dtConsulta = miFun.dtConsulta;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_Result;
        }
    }
}
