using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;

namespace SSF_NET.Formularios
{
    public partial class FrmConsultaRuc : Form
    {
        //Private miCookie As New CookieContainer
        CookieContainer miCookie = new CookieContainer();
        Helper.Comunes.Funciones mifun = new Helper.Comunes.Funciones();

        public FrmConsultaRuc()
        {
            InitializeComponent();
        }
        Image TraerCapcha()
        {
            string c_url = string.Format("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
            HttpWebRequest enlaceReniec = (HttpWebRequest)WebRequest.Create(c_url);
            enlaceReniec.CookieContainer = miCookie;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            enlaceReniec.Credentials = CredentialCache.DefaultCredentials;
            WebResponse respuesta_web = enlaceReniec.GetResponse();
            Stream imgCaptchaBinario = respuesta_web.GetResponseStream();
            return Image.FromStream(imgCaptchaBinario);
        }
        void ConsultarRuc(string c_NumeroRUC, string c_CodigoCAPCHA)
        { 
            string c_url = string.Format("http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc={0}&codigo={1}&tipdoc=1", c_NumeroRUC, c_CodigoCAPCHA);
            HttpWebRequest enlaceReniec = (HttpWebRequest)WebRequest.Create(c_url);
            enlaceReniec.CookieContainer = miCookie;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            enlaceReniec.Credentials = CredentialCache.DefaultCredentials;

            WebResponse respuesta_web = enlaceReniec.GetResponse();
            Stream myStream = respuesta_web.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);
        
            string c_dato = "";  //myStreamReader.ReadToEnd();
            string[,] arrCabecera3 = new string[999, 2];
            int n_row = 0;

            while ((c_dato = myStreamReader.ReadLine()) != null)
             {
                arrCabecera3[n_row, 0] = c_dato;
                arrCabecera3[n_row, 1] = n_row.ToString();
                // Find what we need and save it in the result
                n_row = n_row + 1;
            }

            // DETERMINAMOS SI HUBO ERROR
            for (n_row = 0; n_row < 999; n_row++)
            {
                c_dato = QuitarEtiquetas(arrCabecera3[n_row, 0]);

                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "La aplicaci&oacute;n ha retornado el siguiente problema :")
                {
                    c_dato = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                    c_dato = c_dato + " " + QuitarEtiquetas(arrCabecera3[n_row + 4, 0]);

                    MessageBox.Show(c_dato, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Blanquea();
                    return;
                }
            }

            // SI TODO SALIO BIEN MOSTRAMOS LA INFORMACION
            for (n_row = 0; n_row < 999; n_row++)
            {

                c_dato = QuitarEtiquetas(arrCabecera3[186, 0]);

                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "N&uacute;mero de RUC:")
                {
                    TxtNom.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                    TxtNom.Text = TxtNom.Text.Substring(12, (TxtNom.Text.Length - 12));
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "Tipo Contribuyente:")
                {
                    TxtTipCon.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "Fecha de Inicio de Actividades:")
                {
                    TxtFchIni.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "Direcci&oacute;n del Domicilio Fiscal:")
                {
                    TxtDir.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "Estado del Contribuyente:")
                {
                    TxtEst.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "<!--Actividad(es) Econ&oacute;mica(s):-->")
                {
                    TxtNom.Text = QuitarEtiquetas(arrCabecera3[n_row + 7, 0]);
                    TxtNom.Text = TxtNom.Text.Substring(32, (TxtNom.Text.Length - 32));
                }
            }
        }
        string QuitarEtiquetas(string c_dato)
        {
            if (mifun.NulosC(c_dato) == "") { return ""; }

            string c_dato2 = c_dato;
            //c_dato2 = c_dato.Remove(0,25);
            c_dato2 = c_dato2.Replace("<td  class=\"bg\" colspan=3>", "");
            c_dato2 = c_dato2.Replace("<td class=\"bg\" colspan=3>", "");
            c_dato2 = c_dato2.Replace("<td class=\"bgn\" colspan=1>", "");
            c_dato2 = c_dato2.Replace("<td width=\"27%\" colspan=1 class=\"bgn\">", "");
            c_dato2 = c_dato2.Replace("<td class=\"bg\" colspan=1>", "");
            c_dato2 = c_dato2.Replace("<td width=\"18%\" colspan=1  class=\"bgn\">", "");
            c_dato2 = c_dato2.Replace("<!--   <option value=\"00\" >", "");
            c_dato2 = c_dato2.Replace("</option>-->", "");
            c_dato2 = c_dato2.Replace("<div align=\"left\" class=\"msg\">","");
            c_dato2 = c_dato2.Replace("</div>", "");
            c_dato2 = c_dato2.Replace("</td>", "");

            c_dato2 = c_dato2.Trim();
            return c_dato2;
        }

        private void FrmConsultaRuc_Load(object sender, EventArgs e)
        {
            PicImagen.Image = TraerCapcha();
        }

        private void CmdCon_Click(object sender, EventArgs e)
        {
            if (TxtNUmRuc.Text == "")
            {
                MessageBox.Show("¡ Debe indicar el numero de R.U.C. !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNUmRuc.Focus();
                return;
            }
            if (TxtClave.Text == "")
            {
                MessageBox.Show("¡ Debe indicar el codigo capcha !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtClave.Focus();
                return;
            }
            ConsultarRuc(TxtNUmRuc.Text, TxtClave.Text);
        }

        private void CmdCle_Click(object sender, EventArgs e)
        {
            Blanquea();
        }
        void Blanquea()
        {
            TxtNom.Text = "";
            TxtNUmRuc.Text = "";
            TxtClave.Text = "";
            PicImagen.Image = TraerCapcha();

            TxtDir.Text = "";
            TxtFchIni.Text = "";
            TxtTipCon.Text = "";
            TxtConCon.Text = "";
            TxtActEco.Text = "";
            TxtEst.Text = "";
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
