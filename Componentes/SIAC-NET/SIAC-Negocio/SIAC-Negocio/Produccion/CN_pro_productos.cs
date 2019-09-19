using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;
using Helper;
namespace SIAC_Negocio.Produccion
{
    public class CN_pro_productos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtListar = new DataTable();
        public BE_PRO_PRODUCTOS entRegistro = new BE_PRO_PRODUCTOS();
        public List<BE_PRO_PRODUCTOSRECETAS> lstRecetas = new List<BE_PRO_PRODUCTOSRECETAS>();
        public List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
        public List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineas = new List<BE_PRO_PRODUCTOSRECETASLINEAS>();
        public List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTar = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();
        public List<BE_PRO_PRODUCTOSUBILOC> lstUbiLoc = new List<BE_PRO_PRODUCTOSUBILOC>();
        public List<BE_PRO_PRODUCTOSUBILOCALM> lstUbiLocAlm = new List<BE_PRO_PRODUCTOSUBILOCALM>();

        Genericas miFunGen = new Genericas();
        Helper.Comunes.Funciones miFunFun = new Helper.Comunes.Funciones();
        public bool Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            bool b_result = false;

            CD_pro_productos miFun = new CD_pro_productos();
            miFun.mysConec = mysConec;

            b_result = miFun.Listar(n_IdEmpresa, n_EsUnificado);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool booResult = false;
            DataTable DtResultado = new DataTable();
            int n_fila = 0;
            CD_pro_productos miFun = new CD_pro_productos();
            CD_pro_productosrecetas miFunRec = new CD_pro_productosrecetas();
            CD_pro_productosrecetasinsumos miFunRecIns = new CD_pro_productosrecetasinsumos();
            CD_pro_productosrecetaslineas miFunLin = new CD_pro_productosrecetaslineas();
            CD_pro_productosrecetaslineastareas miFunLinTar = new CD_pro_productosrecetaslineastareas();
            CD_pro_productosubiloc miFunUbiLoc = new CD_pro_productosubiloc();
            CD_pro_productosubilocalm miFunUbiLocAlm = new CD_pro_productosubilocalm();

            miFun.mysConec = mysConec;
            lstRecetas.Clear();
            lstLineas.Clear();
            lstRecetasIns.Clear();
            lstLineasTar.Clear();
            lstUbiLoc.Clear();
            lstUbiLocAlm.Clear();

            booResult = miFun.TraerRegistro(n_IdRegistro);
            if (booResult == true)
            {
                DtResultado = miFun.dtRegistro;

                if (DtResultado.Rows.Count != 0)
                {
                    entRegistro.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());
                    entRegistro.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                    entRegistro.c_cod = DtResultado.Rows[0]["c_cod"].ToString();
                    entRegistro.c_despro = DtResultado.Rows[0]["c_despro"].ToString();
                    entRegistro.n_idunimed = Convert.ToInt16(DtResultado.Rows[0]["n_idunimed"].ToString());
                    entRegistro.n_idfam = Convert.ToInt16(DtResultado.Rows[0]["n_idfam"].ToString());
                    entRegistro.n_idcla = Convert.ToInt16(DtResultado.Rows[0]["n_idcla"].ToString());
                    entRegistro.n_idsubcla = Convert.ToInt16(DtResultado.Rows[0]["n_idsubcla"].ToString());
                    entRegistro.n_idtip = Convert.ToInt16(DtResultado.Rows[0]["n_idtip"].ToString());
                    entRegistro.c_obs = DtResultado.Rows[0]["c_obs"].ToString();
                    entRegistro.n_act = Convert.ToInt16(DtResultado.Rows[0]["n_act"]);
                }

                // ********************************
                // TRAEMOS LAS RECETAS DEL PRODUCTO
                miFunRec.mysConec = mysConec;
                if (miFunRec.Listar(entRegistro.n_id) == true)
                {
                    DtResultado = miFunRec.dtRecetas;
                    
                    for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                    {
                        BE_PRO_PRODUCTOSRECETAS entRecetas = new BE_PRO_PRODUCTOSRECETAS();

                        entRecetas.n_idpro = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idpro"].ToString());
                        entRecetas.n_id = Convert.ToInt16(DtResultado.Rows[n_fila]["n_id"].ToString());
                        entRecetas.c_codrec = DtResultado.Rows[n_fila]["c_codrec"].ToString();
                        entRecetas.c_des = DtResultado.Rows[n_fila]["c_des"].ToString();
                        entRecetas.n_idunimed = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idunimed"].ToString());
                        entRecetas.n_can = Convert.ToInt16(DtResultado.Rows[n_fila]["n_can"].ToString());
                        entRecetas.n_prirec = Convert.ToInt16(miFunFun.NulosN(DtResultado.Rows[n_fila]["n_prirec"]));
                        entRecetas.c_obs = DtResultado.Rows[n_fila]["c_obs"].ToString();
                        entRecetas.n_act = Convert.ToInt16(DtResultado.Rows[n_fila]["n_act"].ToString());

                        lstRecetas.Add(entRecetas);
                    }
                                        
                    // *******************************
                    // TRAEMOS LAS LINEAS DE LA RECETA
                    miFunLin.mysConec = mysConec;
                    if (miFunLin.Listar(entRegistro.n_id) == true)
                    {
                        DtResultado = miFunLin.dtLineas;
                        for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                        {
                            BE_PRO_PRODUCTOSRECETASLINEAS entLineas = new BE_PRO_PRODUCTOSRECETASLINEAS();

                            entLineas.n_idpro = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idpro"].ToString());
                            entLineas.n_idrec = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idrec"].ToString());
                            entLineas.n_id = Convert.ToInt16(DtResultado.Rows[n_fila]["n_id"].ToString());
                            entLineas.c_codlin = DtResultado.Rows[n_fila]["c_codlin"].ToString();
                            entLineas.c_deslin = DtResultado.Rows[n_fila]["c_deslin"].ToString();
                            entLineas.n_idunimed = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idunimed"].ToString());
                            entLineas.n_idite = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idite"].ToString());
                            entLineas.n_can = Convert.ToInt16(DtResultado.Rows[n_fila]["n_can"].ToString());
                            entLineas.n_numope = Convert.ToInt16(DtResultado.Rows[n_fila]["n_numope"].ToString());
                            entLineas.n_efi = Convert.ToDouble(DtResultado.Rows[n_fila]["n_efi"].ToString());
                            entLineas.n_tiepro = Convert.ToDouble(DtResultado.Rows[n_fila]["n_tiepro"]);
                            entLineas.n_prehorjor = Convert.ToDouble(DtResultado.Rows[n_fila]["n_prehorjor"].ToString());
                            entLineas.n_act = Convert.ToInt16(DtResultado.Rows[n_fila]["n_act"].ToString());
                            entLineas.c_obs = DtResultado.Rows[n_fila]["c_obs"].ToString();
                            lstLineas.Add(entLineas);
                        }

                        // *******************
                        // TRAEMOS LOS INSUMOS
                        miFunRecIns.mysConec = mysConec;
                        if (miFunRecIns.Listar(entRegistro.n_id) == true)
                        {
                            DtResultado = miFunRecIns.dtInsumos;
                            for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                            {
                                BE_PRO_PRODUCTOSRECETASINSUMOS entRectasIns = new BE_PRO_PRODUCTOSRECETASINSUMOS();

                                entRectasIns.n_idpro = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idpro"].ToString());
                                entRectasIns.n_idrec = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idrec"].ToString());
                                entRectasIns.n_idite = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idite"].ToString());
                                entRectasIns.n_idunimed = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idunimed"].ToString());
                                entRectasIns.n_can = Convert.ToDouble(DtResultado.Rows[n_fila]["n_can"].ToString());
                                if (DtResultado.Rows[n_fila]["n_inspri"].ToString() != "")
                                {
                                    entRectasIns.n_inspri = Convert.ToInt16(DtResultado.Rows[n_fila]["n_inspri"].ToString());
                                }
                                else
                                {
                                    entRectasIns.n_inspri = 0;
                                }
                                lstRecetasIns.Add(entRectasIns);
                            }
                            // ******************
                            // TRAEMOS LOS TAREAS
                            miFunLinTar.mysConec = mysConec;
                            if (miFunLinTar.Listar(entRegistro.n_id) == true)
                            {
                                DtResultado = miFunLinTar.dtLineasTar;
                                for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                                {
                                    BE_PRO_PRODUCTOSRECETASLINEASTAREAS entLineasTar = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();
                                    
                                    entLineasTar.n_idpro = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idpro"].ToString());
                                    entLineasTar.n_idrec = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idrec"].ToString());
                                    entLineasTar.n_idlin = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idlin"].ToString());
                                    entLineasTar.n_idtar = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idtar"].ToString());
                                    entLineasTar.n_porefi = Convert.ToDouble(DtResultado.Rows[n_fila]["n_porefi"].ToString());
                                    entLineasTar.n_numpertar = Convert.ToInt16(DtResultado.Rows[n_fila]["n_numpertar"].ToString());
                                    entLineasTar.n_idequipo = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idequipo"].ToString());
                                    entLineasTar.n_canequi = Convert.ToInt16(DtResultado.Rows[n_fila]["n_canequi"].ToString());
                                    entLineasTar.n_numpertarequ = Convert.ToInt16(DtResultado.Rows[n_fila]["n_numpertarequ"].ToString());
                                    entLineasTar.n_capkilporper = Convert.ToDouble(DtResultado.Rows[n_fila]["n_capkilporper"].ToString());
                                    entLineasTar.n_capkilporhorlin = Convert.ToDouble(DtResultado.Rows[n_fila]["n_capkilporhorlin"].ToString());
                                    entLineasTar.n_capkilporlintietra = Convert.ToDouble(DtResultado.Rows[n_fila]["n_capkilporlintietra"].ToString());
                                    entLineasTar.n_numpercal = Convert.ToInt16(DtResultado.Rows[n_fila]["n_numpercal"].ToString());
                                    entLineasTar.n_totprotietra = Convert.ToDouble(DtResultado.Rows[n_fila]["n_totprotietra"].ToString());
                                    entLineasTar.n_porefiuni = Convert.ToDouble(DtResultado.Rows[n_fila]["n_porefiuni"].ToString());
                                    entLineasTar.n_porefitot = Convert.ToDouble(DtResultado.Rows[n_fila]["n_porefitot"].ToString());
                                    entLineasTar.n_costar = Convert.ToDouble(DtResultado.Rows[n_fila]["n_costar"].ToString());
                                    entLineasTar.n_ord = Convert.ToInt16(DtResultado.Rows[n_fila]["n_ord"].ToString());

                                    lstLineasTar.Add(entLineasTar);
                                }
                                // ****************************************************
                                // TRAEMOS LOS LOCALES DONDE SE ENCUENTRA LA MERCADERIA
                                miFunUbiLoc.mysConec = mysConec;
                                if (miFunUbiLoc.Listar(entRegistro.n_id) == true)
                                {
                                    DtResultado = miFunUbiLoc.dtUbiLoc;
                                    for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                                    {
                                        BE_PRO_PRODUCTOSUBILOC entUbiLoc = new BE_PRO_PRODUCTOSUBILOC();

                                        entUbiLoc.n_idpro = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idpro"].ToString());
                                        entUbiLoc.n_idloc = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idloc"].ToString());
                                        
                                        lstUbiLoc.Add(entUbiLoc);
                                    }
                                    // ****************************************************
                                    // TRAEMOS LOS ALMACENES DONDE SE UBICARAN LOS PRODUCTOS
                                    miFunUbiLocAlm.mysConec = mysConec;
                                    if (miFunUbiLocAlm.Listar(entRegistro.n_id) == true)
                                    {
                                        DtResultado = miFunUbiLocAlm.dtUbiLocAlm;
                                        for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                                        {
                                            BE_PRO_PRODUCTOSUBILOCALM entUbiLocAlm = new BE_PRO_PRODUCTOSUBILOCALM();

                                            entUbiLocAlm.n_idpro = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idpro"].ToString());
                                            entUbiLocAlm.n_idloc = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idloc"].ToString());
                                            entUbiLocAlm.n_idalm = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idalm"].ToString());
                                            entUbiLocAlm.n_canalmmin = Convert.ToDouble(DtResultado.Rows[n_fila]["n_canalmmin"].ToString());
                                            entUbiLocAlm.n_canalmmax = Convert.ToDouble(DtResultado.Rows[n_fila]["n_canalmmax"].ToString());
                                            lstUbiLocAlm.Add(entUbiLocAlm);
                                        }
                                    }
                                    else
                                    {
                                        booOcurrioError = miFunLin.booOcurrioError;
                                        StrErrorMensaje = miFunLin.StrErrorMensaje;
                                        IntErrorNumber = miFunLin.IntErrorNumber;
                                    }
                                }
                                else
                                {
                                    booOcurrioError = miFunLin.booOcurrioError;
                                    StrErrorMensaje = miFunLin.StrErrorMensaje;
                                    IntErrorNumber = miFunLin.IntErrorNumber;
                                }
                            }
                            else
                            {
                                booOcurrioError = miFunLin.booOcurrioError;
                                StrErrorMensaje = miFunLin.StrErrorMensaje;
                                IntErrorNumber = miFunLin.IntErrorNumber;
                            }
                        }
                        else
                        {
                            booOcurrioError = miFunLin.booOcurrioError;
                            StrErrorMensaje = miFunLin.StrErrorMensaje;
                            IntErrorNumber = miFunLin.IntErrorNumber;
                        }
                    }
                    else
                    {
                        booOcurrioError = miFunLin.booOcurrioError;
                        StrErrorMensaje = miFunLin.StrErrorMensaje;
                        IntErrorNumber = miFunLin.IntErrorNumber;
                    }
                }
                else
                {
                    booOcurrioError = miFunRec.booOcurrioError;
                    StrErrorMensaje = miFunRec.StrErrorMensaje;
                    IntErrorNumber = miFunRec.IntErrorNumber;
                }
            }
            else 
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_PRODUCTOS entProducto, List<BE_PRO_PRODUCTOSRECETAS> lstRecetas, List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns,
            List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineas, List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTar)
        {
            CD_pro_productos miFun = new CD_pro_productos();           
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entProducto, lstRecetas, lstRecetasIns, lstLineas, lstLineasTar);
            if (booOk == true)
            {
                
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar(BE_PRO_PRODUCTOS entProductoU, List<BE_PRO_PRODUCTOSRECETAS> lstRecetasU, List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasInsU,
            List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineasU, List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTarU)
        {
            CD_pro_productos miFun = new CD_pro_productos();
            
            bool booOk = false;

            miFun.mysConec = mysConec;
            miFun.lstRecetaBus = lstRecetas;
            miFun.lstRecetaInsBus = lstRecetasIns;
            miFun.lstLineaBus = lstLineas;
            miFun.lstLineaTarBus = lstLineasTar;

            if (miFun.Actualizar(entProductoU, lstRecetasU, lstRecetasInsU, lstLineasU, lstLineasTarU) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else 
            { 
                booOk = true;
            }
            return booOk;
        }
    }
}
