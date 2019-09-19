using MySql.Data.MySqlClient;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Ventas;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmHojaLiquidacion : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        public FrmHojaLiquidacion()
        {
            InitializeComponent();
        }

        private void CmdCan_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmHojaLiquidacion_Load(object sender, EventArgs e)
        {
            OptRes.Checked = true;
        }

        private void CmdPri_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(TxtFchIni.Text) > Convert.ToDateTime(TxtFchFin.Text))
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha termino ! ","", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            CN_vta_ventas funVentas= new CN_vta_ventas();
            funVentas.mysConec = mysConec;
            funVentas.STU_SISTEMA = STU_SISTEMA;

            if (OptRes.Checked == true)
            {
                funVentas.VerReporteLiquidacion(1,TxtFchIni.Text, TxtFchFin.Text);
            }
            else
            {
                funVentas.VerReporteLiquidacion(2, TxtFchIni.Text, TxtFchFin.Text);
            }
        }
    }
}
