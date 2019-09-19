using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Maestros;

namespace SIAC_DATOS.Estacionamiento
{
    public class CD_est_clientes
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtListar = new DataTable();
        public DataTable dtListarPlacas = new DataTable();
        public int n_idcargogenerado = 0;                                                        // ESTA  VARIABLE ALMACENA EL ID DEL CARGO GENERADO EN LA TABLA EST_OTROCARGOSCAB

        public List<BE_EST_CLIENTESPLACAS> l_ClientePlaca = new List<BE_EST_CLIENTESPLACAS>();
        public BE_EST_OTROCARGOSCAB e_carcab = new BE_EST_OTROCARGOSCAB();
        public List<BE_EST_OTROCARGOSDET> l_cardet = new List<BE_EST_OTROCARGOSDET>();

        MySqlConnection mysConec = new MySqlConnection();
        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public CD_est_clientes(string c_IPServidor, string c_NombreBD, string c_Usuario, string c_Passord, string c_Puerto)
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConec = hlpFuncion.ObtenerConexion(c_IPServidor, c_NombreBD, c_Usuario, c_Passord, c_Puerto);

            if (hlpFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }

        ~CD_est_clientes()
        {
            mysConec.Close();

            mysConec = null;
            xMiFuncion = null;
            xFun = null;
            xFunGen = null;
        }
        public void Listar()
        {
            //mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            string[,] arrParametros = new string[0, 3] {
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Listar2()
        {
            string[,] arrParametros = new string[0, 3] {
                                      };

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_listar2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void ListarNoActivos(int n_IdEmpresa, int n_IdPlaya)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idpla", "System.INT32", n_IdPlaya.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_listarnoactivos", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Listar3(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_listar3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idcli", "System.INT32", n_IdRegistro.ToString()}
                                      };
                dtListarPlacas = xMiFuncion.StoreDTLLenar("est_clientesplacas_obtenerregistro", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    dtListar = null;
                    dtListarPlacas = null;
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            booResult = true;
            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            booResult = xMiFuncion.StoreEjecutar("est_clientes_delete", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Anular(int n_IdRegistro)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            booResult = xMiFuncion.StoreEjecutar("est_clientes_anular", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Activar(int n_IdRegistro)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            booResult = xMiFuncion.StoreEjecutar("est_clientes_activar", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;

            }
            return booResult;
        }
        public bool Insertar(BE_EST_CLIENTES e_Cliente)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            BE_MAE_CLIPRO e_cli = new BE_MAE_CLIPRO();

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            e_cli.n_idemp = 0;  //e_Cliente.n_idemp;
            e_cli.n_id = 0;
            e_cli.n_idcatemp = 2;
            e_cli.n_idtipcon = e_Cliente.n_idtipcon;
            e_cli.n_idtipdoc = e_Cliente.n_idtipdocide;
            e_cli.c_numdoc  = e_Cliente.c_numdocide;
            e_cli.c_nombre  = e_Cliente.c_nom;
            e_cli.c_nomcli1 = e_Cliente.c_nom1;
            e_cli.c_nomcli2 = e_Cliente.c_nom2;
            e_cli.c_apecli1 = e_Cliente.c_ape1;
            e_cli.c_apecli2 = e_Cliente.c_ape2;
            e_cli.c_dir = e_Cliente.c_dir;
            e_cli.c_tel  = e_Cliente.c_numtel;
            e_cli.c_fax ="";
            e_cli.c_nomcon = e_Cliente.c_nom;
            e_cli.c_email = "";
            e_cli.c_pagweb = "";
            e_cli.n_estado = 1;
            e_cli.n_iddep = e_Cliente.n_iddep;
            e_cli.n_idpro = e_Cliente.n_idpro;
            e_cli.n_iddis = e_Cliente.n_iddis;
            e_cli.n_ageret = 0;
            e_cli.c_codcen = "";
            e_cli.n_idven = 0;
            e_cli.n_idcondpag = 0;
            e_cli.c_letnomgir = "";
            e_cli.c_letgirdir = "";
            e_cli.c_letnumdoc = "";
            e_cli.c_lettel = "";
            e_cli.n_tipreg = 1;
            e_cli.d_fchini = e_Cliente.d_fching;
            //e_cli.n_ageper = 0;

            booOk = xMiFuncion.StoreEjecutar("mae_clipro_insertar", e_cli, mysConec, 0);
            if (booOk == true)
            {
                e_Cliente.n_id =Convert.ToInt32(xMiFuncion.intIdGenerado);
                booOk = xMiFuncion.StoreEjecutar("est_clientes_insertar", e_Cliente, mysConec, null);

                if (booOk == true)
                {
                    for (n_row = 0; n_row <= l_ClientePlaca.Count - 1; n_row++)
                    {
                        l_ClientePlaca[n_row].n_idcli = e_Cliente.n_id;
                        if (xMiFuncion.StoreEjecutar("est_clientesplacas_insertar", l_ClientePlaca[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            return booOk;
                        }
                    }

                    CD_est_otrocargoscab o_otrocab = new CD_est_otrocargoscab();
                    o_otrocab.mysConec = mysConec;
                    e_carcab.n_idcli = e_Cliente.n_id;
                    if (o_otrocab.Insertar(e_carcab, l_cardet, 1) == true)
                    {
                        n_idcargogenerado = o_otrocab.n_idcargogenerado;
                    }
                    else
                    {
                        b_OcurrioError = o_otrocab.b_OcurrioError;
                        c_ErrorMensaje = o_otrocab.c_ErrorMensaje;
                        n_ErrorNumber = o_otrocab.n_ErrorNumber;
                        return booOk;
                    }

                    booOk = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return booOk;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }

            return booOk;
        }
        public bool Actualizar(BE_EST_CLIENTES e_Cliente)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            BE_MAE_CLIPRO e_cli = new BE_MAE_CLIPRO();

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            e_cli.n_idemp = 0;  //e_Cliente.n_idemp;
            e_cli.n_id = e_Cliente.n_id;
            e_cli.n_idcatemp = 2;
            e_cli.n_idtipcon = e_Cliente.n_idtipcon;
            e_cli.n_idtipdoc = e_Cliente.n_idtipdocide;
            e_cli.c_numdoc = e_Cliente.c_numdocide;
            e_cli.c_nombre = e_Cliente.c_nom;
            e_cli.c_nomcli1 = e_Cliente.c_nom1;
            e_cli.c_nomcli2 = e_Cliente.c_nom2;
            e_cli.c_apecli1 = e_Cliente.c_ape1;
            e_cli.c_apecli2 = e_Cliente.c_ape2;
            e_cli.c_dir = e_Cliente.c_dir;
            e_cli.c_tel = e_Cliente.c_numtel;
            e_cli.c_fax = "";
            e_cli.c_nomcon = e_Cliente.c_nom;
            e_cli.c_email = "";
            e_cli.c_pagweb = "";
            e_cli.n_estado = 1;
            e_cli.n_iddep = e_Cliente.n_iddep;
            e_cli.n_idpro = e_Cliente.n_idpro;
            e_cli.n_iddis = e_Cliente.n_iddis;
            e_cli.n_ageret = 0;
            e_cli.c_codcen = "";
            e_cli.n_idven = 0;
            e_cli.n_idcondpag = 0;
            e_cli.c_letnomgir = "";
            e_cli.c_letgirdir = "";
            e_cli.c_letnumdoc = "";
            e_cli.c_lettel = "";
            e_cli.n_tipreg = 1;
            e_cli.d_fchini = e_Cliente.d_fching;

            booOk = xMiFuncion.StoreEjecutar("mae_clipro_actualizar", e_cli, mysConec, null);

            if (booOk == true)
            { 
                booOk = xMiFuncion.StoreEjecutar("est_clientes_actualizar", e_Cliente, mysConec, null);
                if (booOk == true)
                {
                    if (booOk == true)
                    {
                        string[,] arrParametros = new string[1, 3] {
                                                    {"n_idcli", "System.INT32", e_Cliente.n_id.ToString()}
                                              };
                        booOk = xMiFuncion.StoreEjecutar("est_clientesplacas_delete", arrParametros, mysConec);
                        if (booOk == true)
                        {
                            for (n_row = 0; n_row <= l_ClientePlaca.Count - 1; n_row++)
                            {
                                booOk = xMiFuncion.StoreEjecutar("est_clientesplacas_insertar", l_ClientePlaca[n_row], mysConec, null);
                                if (booOk == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    return booOk;
                                }
                            }
                        }
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return booOk;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }

            return booOk;
        }
        public void Consulta1(int id_Empresa , int id_Playa)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT32", id_Empresa.ToString()},
                                            {"n_idpla", "System.INT32", id_Playa.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        //public void Consulta2(int n_IdEmpresa, int n_IdPlaya)
        //{
        //    string[,] arrParametros = new string[2, 3] {
        //                                    {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
        //                                    {"n_idpla", "System.INT32", n_IdPlaya.ToString()}
        //                                };

        //    dtListar = xMiFuncion.StoreDTLLenar("est_clientes_consulta1", arrParametros, mysConec);

        //    if (xMiFuncion.IntErrorNumber != 0)
        //    {
        //        dtListar = null;
        //        b_OcurrioError = xMiFuncion.booOcurrioError;
        //        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
        //        n_ErrorNumber = xMiFuncion.IntErrorNumber;
        //    }

        //    return;
        //}
        public void Consulta3(int n_IdEmpresa, int n_IdPlaya, string c_NumeroDocumento, int n_IdTipoDocumento)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idpla", "System.INT32", n_IdPlaya.ToString()},
                                            {"c_numdoc", "System.STRING", c_NumeroDocumento.ToString()},
                                            {"n_idtipdoc", "System.INT32", n_IdTipoDocumento.ToString()}
                                        };

            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_consulta3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta4(int n_IdEmpresa, int n_IdPlaya, string c_NumPlaca)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idpla", "System.INT32", n_IdPlaya.ToString()},
                                            {"c_numpla", "System.STRING", c_NumPlaca.ToString()}
                                        };

            dtListar = xMiFuncion.StoreDTLLenar("est_clientes_consulta4", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}
