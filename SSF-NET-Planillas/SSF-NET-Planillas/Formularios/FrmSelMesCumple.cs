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

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmSelMesCumple : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        CN_mae_meses objMeses = new CN_mae_meses();
        

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Convertir funCon = new Convertir();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable dtMeses = new DataTable();

        public FrmSelMesCumple()
        {
            InitializeComponent();
        }

        private void CmdAce_Click(object sender, EventArgs e)
        {
            CN_pla_empleados xFunPla = new CN_pla_empleados(STU_SISTEMA);
            xFunPla.STU_SISTEMA = STU_SISTEMA;
            xFunPla.Reporte2(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), CboMeses.Text);
        }

        private void CmdCan_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSelMesCumple_Load(object sender, EventArgs e)
        {
            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
            dtMeses = funDatos.DataTableFiltrar(dtMeses, "n_id NOT IN (0,13)");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }

        private void FrmSelMesCumple_Activated(object sender, EventArgs e)
        {

        }
    }
}
