using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;
using SIAC_Negocio.Contabilidad;
using Helper;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_ctacte
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        DatosMySql FunMysql = new DatosMySql();

        public DataTable dtLista = new DataTable();
        public void ListarCtaCteCli(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda)
        {
            DataTable dtResul = new DataTable();

            CD_tes_ctacte miFun = new CD_tes_ctacte();
            miFun.mysConec = mysConec;

            miFun.ListarCtaCteCli(n_IdEmpresa, c_FechaInicio, c_FechaTermino, n_IdMoneda);
            dtLista = miFun.DtLista;

            if (dtResul == null)
            {

                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void ListarCtaCtePro(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda)
        {
            DataTable dtResul = new DataTable();

            CD_tes_ctacte miFun = new CD_tes_ctacte();
            miFun.mysConec = mysConec;

            miFun.ListarCtaCtePro(n_IdEmpresa, c_FechaInicio, c_FechaTermino, n_IdMoneda);
            dtLista = miFun.DtLista;

            if (dtResul == null)
            {

                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void ListarCtaCteProDetalle(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda, int n_TipoConsulta, string c_ClientesConsultar)
        {
            DataTable dtResul = new DataTable();

            CD_tes_ctacte miFun = new CD_tes_ctacte();
            miFun.mysConec = mysConec;

            miFun.ListarCtaCteProDetalle(n_IdEmpresa, c_FechaInicio, c_FechaTermino, n_IdMoneda, n_TipoConsulta, c_ClientesConsultar);
            dtLista = miFun.DtLista;

            if (dtResul == null)
            {

                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void ListarCtaCteCliDetalle(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda, int n_TipoConsulta, string c_ClientesConsultar)
        {
            DataTable dtResul = new DataTable();

            CD_tes_ctacte miFun = new CD_tes_ctacte();
            miFun.mysConec = mysConec;

            miFun.ListarCtaCteCliDetalle(n_IdEmpresa, c_FechaInicio, c_FechaTermino, n_IdMoneda, n_TipoConsulta, c_ClientesConsultar);
            dtLista = miFun.DtLista;

            if (dtResul == null)
            {

                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
    }
}
