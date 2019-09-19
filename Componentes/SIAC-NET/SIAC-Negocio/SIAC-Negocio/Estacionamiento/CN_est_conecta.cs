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
    public class CN_est_conecta
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public CN_est_conecta(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_conecta xFun = new CD_est_conecta(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            mysConec = xFun.mysConec;
        }

        ~CN_est_conecta()
        {
            mysConec.Close();
            mysConec = null;
        }
    }
}
