using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;

namespace SIAC_DATOS.Maestros
{
    public class CD_mae_clipro
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public double n_IdGenerado = 0;

        public DataTable ListarProveedor(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_esuni", "System.INT16", n_EsUnificado.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("mae_clipro_select_proveedor", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable ListarCliente(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_esuni", "System.INT16", n_EsUnificado.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("mae_clipro_select_cliente", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_esuni", "System.INT16", n_EsUnificado.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("mae_clipro_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable ClienteBuscar(string strRUCNumero)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"c_numdoc", "System.STRING",strRUCNumero}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("mae_clipro_cliente_buscar_ruc", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_MAE_CLIPRO TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_CLIPRO Ent_CliPro = new BE_MAE_CLIPRO();
            DatosMySql xMiFuncion = new DatosMySql();
            Helper.Comunes.Funciones fun = new Helper.Comunes.Funciones();
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("mae_clipro_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                Ent_CliPro.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                Ent_CliPro.n_idcatemp = Convert.ToInt16(DtResultado.Rows[0]["n_idcatemp"].ToString());
                Ent_CliPro.n_idtipcon = Convert.ToInt16(DtResultado.Rows[0]["n_idtipcon"].ToString());
                Ent_CliPro.n_idtipdoc = Convert.ToInt16(DtResultado.Rows[0]["n_idtipdoc"].ToString());
                Ent_CliPro.c_numdoc = DtResultado.Rows[0]["c_numdoc"].ToString();
                Ent_CliPro.c_nombre = DtResultado.Rows[0]["c_nombre"].ToString();
                Ent_CliPro.c_nomcli1 = DtResultado.Rows[0]["c_nomcli1"].ToString();
                Ent_CliPro.c_nomcli2 = DtResultado.Rows[0]["c_nomcli2"].ToString();
                Ent_CliPro.c_apecli1 = DtResultado.Rows[0]["c_apecli1"].ToString();
                Ent_CliPro.c_apecli2 = DtResultado.Rows[0]["c_apecli2"].ToString();
                Ent_CliPro.c_dir = DtResultado.Rows[0]["c_dir"].ToString();
                Ent_CliPro.c_tel = DtResultado.Rows[0]["c_tel"].ToString();
                Ent_CliPro.c_fax = DtResultado.Rows[0]["c_fax"].ToString();
                Ent_CliPro.c_nomcon = DtResultado.Rows[0]["c_nomcon"].ToString();
                Ent_CliPro.c_email = DtResultado.Rows[0]["c_email"].ToString();
                Ent_CliPro.c_pagweb = DtResultado.Rows[0]["c_pagweb"].ToString();

                if (DtResultado.Rows[0]["d_fchini"].ToString() != "")
                {
                    Ent_CliPro.d_fchini = Convert.ToDateTime(DtResultado.Rows[0]["d_fchini"].ToString());
                }

                Ent_CliPro.n_tipreg = Convert.ToInt16(DtResultado.Rows[0]["n_tipreg"].ToString());
                Ent_CliPro.c_letnumdoc = DtResultado.Rows[0]["c_letnumdoc"].ToString();
                Ent_CliPro.c_letgirdir = DtResultado.Rows[0]["c_letgirdir"].ToString();
                Ent_CliPro.c_letnomgir = DtResultado.Rows[0]["c_letnomgir"].ToString();
                
                if (DtResultado.Rows[0]["n_idpro"].ToString() != "")
                {
                    Ent_CliPro.n_idpro = Convert.ToInt16(DtResultado.Rows[0]["n_idpro"].ToString());
                }

                Ent_CliPro.n_estado = Convert.ToInt16(DtResultado.Rows[0]["n_estado"].ToString());

                if (DtResultado.Rows[0]["n_iddep"].ToString() != "")
                {
                    Ent_CliPro.n_iddep = Convert.ToInt16(DtResultado.Rows[0]["n_iddep"].ToString());
                }


                if (DtResultado.Rows[0]["n_iddis"].ToString() != "")
                {
                    Ent_CliPro.n_iddis = Convert.ToInt16(DtResultado.Rows[0]["n_iddis"].ToString());
                }

                Ent_CliPro.n_ageret = Convert.ToInt16(fun.NulosN(DtResultado.Rows[0]["n_ageret"]));
                Ent_CliPro.c_codcen = DtResultado.Rows[0]["c_codcen"].ToString();

                if (DtResultado.Rows[0]["n_idven"].ToString() != "")
                {
                    Ent_CliPro.n_idven = Convert.ToInt16(DtResultado.Rows[0]["n_idven"].ToString());
                }
                
                if (DtResultado.Rows[0]["n_idcondpag"].ToString() != "")
                {
                    Ent_CliPro.n_idcondpag = Convert.ToInt16(DtResultado.Rows[0]["n_idcondpag"].ToString());
                }

                Ent_CliPro.c_lettel = DtResultado.Rows[0]["c_lettel"].ToString();
                Ent_CliPro.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());
            }
            return Ent_CliPro;
        }
        public bool Insertar(BE_MAE_CLIPRO entCliPro)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("mae_clipro_insertar", entCliPro, mysConec, 0) == true)
            {
                n_IdGenerado = xMiFuncion.intIdGenerado;
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_MAE_CLIPRO entCliPro)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("mae_clipro_actualizar", entCliPro, mysConec, null) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_Id.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("mae_clipro_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
    }
}
