using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
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

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmRepItems : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        DataTable dtTipoExis = new DataTable();

        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();

        // OBJETOS DE ACCESO A DATOS
        Genericas funDatos = new Genericas();
        public FrmRepItems()
        {
            InitializeComponent();
        }

        private void FrmRepItems_Load(object sender, EventArgs e)
        {
            DataTableCargar();
            CargarCombos();
            this.Text = "Almacen - Reportes de Items";

            OptAct.Checked = true;
            Opt1.Checked = true;
            CboTipExis.Focus();
        }

        void DataTableCargar()
        {
            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();                               //  CARGAMOS TODOS LOS TIPOS DE ITEM
        }

        void CargarCombos()
        {
            funDatos.ComboBoxCargarDataTable(CboTipExis, dtTipoExis, "n_id", "c_des");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int n_EstadoActivo;

            if (CboTipExis.Text == "")
            {
                MessageBox.Show("¡ No ha indicado que tipo de producto desea vizualizar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipExis.Focus();
                return;
            }

            if (OptAct.Checked==true)
            {
                n_EstadoActivo = 1;
            }
            else
            {
                n_EstadoActivo = 2;
            }

            CN_alm_inventario objAlm = new CN_alm_inventario();

            if (Opt1.Checked == true)
            {
                objAlm.STU_SISTEMA = STU_SISTEMA;
                objAlm.ReporteListaItems(Convert.ToInt16(CboTipExis.SelectedValue), n_EstadoActivo); 
            }
            if (Opt2.Checked == true)
            {
                objAlm.STU_SISTEMA = STU_SISTEMA;
                objAlm.ReporteListaInventario(Convert.ToInt16(CboTipExis.SelectedValue), n_EstadoActivo);
            }
            if (Opt3.Checked == true)
            {
                objAlm.STU_SISTEMA = STU_SISTEMA;
                objAlm.ReporteStckMin(Convert.ToInt16(CboTipExis.SelectedValue), n_EstadoActivo);
            }
            if (Opt4.Checked == true)
            {
                objAlm.STU_SISTEMA = STU_SISTEMA;
                objAlm.ReporteStckMax(Convert.ToInt16(CboTipExis.SelectedValue), n_EstadoActivo);
            }
        }

        private void CmdSalir_Click(object sender, EventArgs e)
        {
            mysConec = null;
            this.Close();
            Close();
        }
            
    }
}
