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

namespace Helper.Formularios
{
    public partial class FrmConsultaRuc : Form
    {
        CookieContainer miCookie = new CookieContainer();
        Helper.Comunes.Funciones mifun = new Helper.Comunes.Funciones();

        public string Contribuyente_Nombre = "";
        public string Contribuyente_NumRUC = "";
        public string Contribuyente_Direccion = "";
        public string Contribuyente_Estado = "";
        public string Contribuyente_Condicion = "";
        public string Contribuyente_FchIniAct = "";
        public string Contribuyente_ActEco = "";
        public string Contribuyente_TipCon = "";

        public int n_EstadoForm = 1;    //  INDICA SU EL FORMULARIO ESRA CERRADO O OCULTO (1 = CERRADO ;  2 = OCULTO)

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteresvalidos = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" + (char)8;                                        // + (char)8;
        public FrmConsultaRuc()
        {
            InitializeComponent();
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
                //c_dato = QuitarEtiquetas(arrCabecera3[158, 0]);
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "N&uacute;mero de RUC:")
                {
                    TxtNom.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                    TxtNom.Text = TxtNom.Text.Substring(14, (TxtNom.Text.Length - 14));
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
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "Condici&oacute;n del Contribuyente:")
                {
                    TxtConCon.Text = QuitarEtiquetas(arrCabecera3[n_row + 3, 0]);
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "<!--Actividad(es) Econ&oacute;mica(s):-->")
                {
                    TxtActEco.Text = QuitarEtiquetas(arrCabecera3[n_row + 7, 0]);
                    TxtActEco.Text = TxtActEco.Text.Substring(32, (TxtActEco.Text.Length - 32));
                }
            }
        }
        string QuitarEtiquetas(string c_dato)
        {
            if (mifun.NulosC(c_dato) == "") { return ""; }

            string c_dato2 = c_dato;
            
            c_dato2 = c_dato2.Replace("<td  class=\"bg\" colspan=3>", "");
            c_dato2 = c_dato2.Replace("<td class=\"bg\" colspan=3>", "");
            c_dato2 = c_dato2.Replace("<td class=\"bgn\" colspan=1>", "");
            c_dato2 = c_dato2.Replace("<td width=\"27%\" colspan=1 class=\"bgn\">", "");
            c_dato2 = c_dato2.Replace("<td class=\"bg\" colspan=1>", "");
            c_dato2 = c_dato2.Replace("<td width=\"18%\" colspan=1  class=\"bgn\">", "");
            c_dato2 = c_dato2.Replace("<!--   <option value=\"00\" >", "");
            c_dato2 = c_dato2.Replace("</option>-->", "");
            c_dato2 = c_dato2.Replace("<div align=\"left\" class=\"msg\">", "");
            c_dato2 = c_dato2.Replace("<td class=\"bgn\"colspan=1>", "");
            c_dato2 = c_dato2.Replace("</div>", "");
            c_dato2 = c_dato2.Replace("</td>", "");

            c_dato2 = c_dato2.Trim();
            return c_dato2;
        }
        private void FrmConsultaRuc_Load(object sender, EventArgs e)
        {
            PicImagen.Image = TraerCapcha();
            this.Text = "HELPER - CONSULTA Nº R.U.C. SUNAT";
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
        void Enviar()
        { 
            Contribuyente_Nombre = TxtNom.Text;
            Contribuyente_NumRUC = TxtNUmRuc.Text;
            Contribuyente_Direccion = TxtDir.Text;
            Contribuyente_Estado = TxtEst.Text;
            Contribuyente_Condicion = TxtConCon.Text;
            Contribuyente_FchIniAct = TxtFchIni.Text;
            Contribuyente_ActEco = TxtActEco.Text;
            Contribuyente_TipCon = TxtTipCon.Text;
        }

        private void CmdEnv_Click(object sender, EventArgs e)
        {
            if (TxtNUmRuc.Text == "")
            {
                MessageBox.Show("¡ No se encontrado el numero de R.U.C. !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNUmRuc.Focus();
                return;
            }
            if (TxtNom.Text == "")
            {
                MessageBox.Show("¡ No se ha encontrado el nombre del contribuyente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNom.Focus();
                return;
            }
            n_EstadoForm = 2;
            Enviar();
            this.Hide();
        }

        private void CmdSal_Click(object sender, EventArgs e)
        {
            n_EstadoForm = 1;
            this.Close();
        }

        private void TxtNUmRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strCaracteresvalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
