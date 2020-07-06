using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Planillas;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Planilla;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSF_NET_Produccion.ViewModels;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmOrdenProduccionSelItem : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        CN_mae_meses objMeses = new CN_mae_meses();
        

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Convertir funCon = new Convertir();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable dtProductos = new DataTable();

        public OrdenProduccionDetalleItem ordenProduccionDetalleItem = null;

        public FrmOrdenProduccionSelItem(List<OrdenProduccionDetalleItem> dtProductos)
        {
            InitializeComponent();

            CboMeses.DisplayMember = "c_despro";
            CboMeses.ValueMember = "n_idite";
            CboMeses.DataSource = dtProductos;
        }

        private void CmdAce_Click(object sender, EventArgs e)
        {
            ordenProduccionDetalleItem = CboMeses.SelectedItem as OrdenProduccionDetalleItem;
            this.Close();
        }

        private void CmdCan_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSelMesCumple_Load(object sender, EventArgs e)
        {
            //funDatos.ComboBoxCargarDataTable(CboMeses, dtProductos, "n_id", "c_des");
        }

        private void FrmSelMesCumple_Activated(object sender, EventArgs e)
        {

        }
    }
}
