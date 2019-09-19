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
    public partial class FrmConsultaDNI : Form
    {
        CookieContainer miCookie = new CookieContainer();
        Helper.Comunes.Funciones mifun = new Helper.Comunes.Funciones();

        public string Ciudadano_Nombre = "";
        public string Ciudadano_NumDNI = "";
        public string Ciudadano_ApePat = "";
        public string Ciudadano_ApeMat = "";

        public int n_EstadoForm = 1;    //  INDICA SU EL FORMULARIO ESRA CERRADO O OCULTO (1 = CERRADO ;  2 = OCULTO)

        public FrmConsultaDNI()
        {
            InitializeComponent();
        }

        private void CmdCon_Click(object sender, EventArgs e)
        {
            if (TxtNumDni.Text == "")
            {
                MessageBox.Show("¡ Debe indicar el numero de D.N.I.. !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDni.Focus();
                return;
            }
            if (TxtClave.Text == "")
            {
                MessageBox.Show("¡ Debe indicar el codigo capcha !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtClave.Focus();
                return;
            }
            ConsultarRuc(TxtNumDni.Text, TxtClave.Text);
        }

        private void CmdCle_Click(object sender, EventArgs e)
        {
            Blanquea();
        }
        Image TraerCapcha()
        {
            string c_url = string.Format("https://cel.reniec.gob.pe/valreg/codigo.do");
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
            string c_url = string.Format("https://cel.reniec.gob.pe/valreg/valreg.do?accion=buscar&nuDni={0}&imagen={1}", c_NumeroRUC, c_CodigoCAPCHA);
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
                c_dato = QuitarEtiquetas(arrCabecera3[158, 0]);

                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "N&uacute;mero de RUC:")
                {
                    TxtApePat.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                    TxtApePat.Text = TxtApePat.Text.Substring(14, (TxtApePat.Text.Length - 14));
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "Tipo Contribuyente:")
                {
                    TxtApeMat.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
                }
                if (QuitarEtiquetas(arrCabecera3[n_row, 0]) == "Fecha de Inicio de Actividades:")
                {
                    TxtNom.Text = QuitarEtiquetas(arrCabecera3[n_row + 1, 0]);
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

        private void FrmConsultaDNI_Load(object sender, EventArgs e)
        {
            PicImagen.Image = TraerCapcha();
        }
        void Blanquea()
        {
            TxtApePat.Text = "";
            TxtNumDni.Text = "";
            TxtClave.Text = "";
            PicImagen.Image = TraerCapcha();

            TxtApePat.Text = "";
            TxtApeMat.Text = "";
        }
        void Enviar()
        {
            Ciudadano_Nombre = TxtNom.Text;
            Ciudadano_NumDNI = TxtNumDni.Text;
            Ciudadano_ApePat = TxtApePat.Text;
            Ciudadano_ApeMat = TxtApeMat.Text;
        }

        private void CmdEnv_Click(object sender, EventArgs e)
        {
            if (TxtNumDni.Text == "")
            {
                MessageBox.Show("¡ No se encontrado el numero de D.N.I. !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDni.Focus();
                return;
            }
            if (TxtApePat.Text == "")
            {
                MessageBox.Show("¡ No se ha encontrado datos con el Nº DNI indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtApePat.Focus();
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
    }
}
