using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using SIAC_Entidades;
using SIAC_Entidades.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_auditoria
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();
            CD_sys_auditoria miFun = new CD_sys_auditoria();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_SYS_AUDITORIA TraerRegistro(int n_IdRegistro)
        {
            BE_SYS_AUDITORIA entAuditoria = new BE_SYS_AUDITORIA();
            DataTable dtResult = new DataTable();

            CD_sys_auditoria miFun = new CD_sys_auditoria();
            miFun.mysConec = mysConec;
            dtResult = miFun.TraerRegistro(n_IdRegistro);

            if (dtResult.Rows.Count != 0)
            {
                entAuditoria.n_idemp = Convert.ToInt16(dtResult.Rows[0]["n_idemp"].ToString());
                entAuditoria.n_id = Convert.ToInt16(dtResult.Rows[0]["n_id"].ToString());
                entAuditoria.n_idano = Convert.ToInt16(dtResult.Rows[0]["n_idano"].ToString());
                entAuditoria.n_idmes = Convert.ToInt16(dtResult.Rows[0]["n_idmes"].ToString());
                entAuditoria.d_fchreg = Convert.ToDateTime(dtResult.Rows[0]["d_fchreg"].ToString());
                entAuditoria.h_horreg = Convert.ToDateTime(dtResult.Rows[0]["h_horreg"].ToString());
                entAuditoria.n_idtipope = Convert.ToInt16(dtResult.Rows[0]["n_idtipope"].ToString());
                entAuditoria.n_idusu = Convert.ToInt16(dtResult.Rows[0]["n_idusu"].ToString());
            }
            return entAuditoria;
        }
        public bool Insertar(BE_SYS_AUDITORIA entAuditoria)
        {
            BE_SYS_AUDITORIA entNewAuditoria = new BE_SYS_AUDITORIA();
            CD_sys_auditoria miFun = new CD_sys_auditoria();
            bool booOk = false;

            miFun.mysConec = mysConec;
            entNewAuditoria.n_idemp = entAuditoria.n_idemp;
            entNewAuditoria.n_id = entAuditoria.n_id;
            entNewAuditoria.n_idano = entAuditoria.n_idano;
            entNewAuditoria.n_idmes = entAuditoria.n_idmes;
            entNewAuditoria.d_fchreg = entAuditoria.d_fchreg;
            entNewAuditoria.h_horreg = entAuditoria.h_horreg;
            entNewAuditoria.n_idtipope = entAuditoria.n_idtipope;
            entNewAuditoria.n_idusu = entAuditoria.n_idusu;

            booOk = miFun.Insertar(entNewAuditoria);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SYS_AUDITORIA entAuditoria)
        {
            BE_SYS_AUDITORIA entNewAuditoria = new BE_SYS_AUDITORIA();
            CD_sys_auditoria miFun = new CD_sys_auditoria();
            bool booOk = false;

            miFun.mysConec = mysConec;
            entNewAuditoria.n_idemp = entAuditoria.n_idemp;
            entNewAuditoria.n_id = entAuditoria.n_id;
            entNewAuditoria.n_idano = entAuditoria.n_idano;
            entNewAuditoria.n_idmes = entAuditoria.n_idmes;
            entNewAuditoria.d_fchreg = entAuditoria.d_fchreg;
            entNewAuditoria.h_horreg = entAuditoria.h_horreg;
            entNewAuditoria.n_idtipope = entAuditoria.n_idtipope;
            entNewAuditoria.n_idusu = entAuditoria.n_idusu;

            booOk = miFun.Actualizar(entNewAuditoria);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_sys_auditoria miFun = new CD_sys_auditoria();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
