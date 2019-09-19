using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using SIAC_Negocio;

namespace SIAC_WEB.almacen
{
    public partial class frmlistaitems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //ClientScript.RegisterClientScriptInclude("bloque", "../publico/script/modulos.js");
            string scriptJQuery = "../publico/script/modulos.js";
            ClientScript.RegisterClientScriptInclude("bloque", Page.ResolveClientUrl(scriptJQuery));

            ////Se llama a un javascript cuando la página carga

            //string mensaje = "Este mensaje se genera desde c#";

            //ClientScript.RegisterStartupScript(this.GetType(), "myScript", "<script>javascript:mostrarMensaje('" + mensaje + "');</script>");
        }



        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable DtResult = new DataTable();
            SIAC_Negocio.Almacen.CN_alm_inventario FunAlmacen = new SIAC_Negocio.Almacen.CN_alm_inventario();

            FunAlmacen.mysConec = Conectar();
            DtResult = FunAlmacen.Listar(1);
            String jSonString = ConvertDataTableTojSonString(DtResult);
            
            //ClientScript.RegisterStartupScript(this.GetType(), "ssssssss", "genera_tabla('cuerpoform','tabla','" + jSonString + "');", true);


            //ClientScript.RegisterStartupScript(this.GetType(), "miscript", "mensaje('" + jSonString + "');", true);

            ClientScript.RegisterStartupScript(this.GetType(), "tuscript", "parsear('" + jSonString + "');", true);   
            

            //Llamar funcion MostrarOcultarDiv() desde codigo C#
            // Ambas lineas de código funcionan.
            //ClientScript.RegisterStartupScript(this.GetType(), "myScript", "MostrarOcultarDiv();", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "myScript", "<script>javascript:MostrarOcultarDiv();</script>");
        }


        public String ConvertDataTableTojSonString(DataTable dataTable)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }

            string cadena = serializer.Serialize(tableRows);

            cadena = "{ " + "\"" + "tabla" + "\"" + " :" + cadena + "}";
            return cadena;
        }

        MySqlConnection Conectar()
        {
            MySqlConnection mysConectar = new MySqlConnection();

            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConectar = hlpFuncion.ObtenerConexion("localhost", "data01", "root", "12345678");

            if (mysConectar.State == ConnectionState.Open)
            {
                //MessageBox.Show(" Se conecto con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                //MessageBox.Show("No se pudo abrir la BD ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //mysConectar = Nothing;
            }
            return mysConectar;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable DtResult = new DataTable();
            SIAC_Negocio.Almacen.CN_alm_inventario FunAlmacen = new SIAC_Negocio.Almacen.CN_alm_inventario();

            FunAlmacen.mysConec = Conectar();
            DtResult = FunAlmacen.Listar(1);
            String jSonString = ConvertDataTableTojSonString(DtResult);

            //ClientScript.RegisterStartupScript(this.GetType(), "tuscript", "parsear('" + jSonString + "');", true);   
            ClientScript.RegisterStartupScript(this.GetType(), "miscript", "mensaje('" + jSonString + "');", true);
        }
    }
}