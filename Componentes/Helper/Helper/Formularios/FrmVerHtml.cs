using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper.Formularios
{
    public partial class FrmVerHtml : Form
    {
        public string c_NombreArchivo;
        public string c_TituloForm;
        public FrmVerHtml()
        {
            InitializeComponent();
        }

        private void FrmVerHtml_Load(object sender, EventArgs e)
        {
            this.Text = c_TituloForm;
            //webBrowser1.Navigate("@file:///" + c_NombreArchivo);
            webBrowser1.Url = new Uri("file:///" + c_NombreArchivo);

            //string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            //string myFile = Path.Combine(applicationDirectory, "Sample.html");
            //webMain.Url = new Uri("file:///" + myFile);
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
