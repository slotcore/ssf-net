using Helper;
using Helper.Comunes;
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
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmRepMovAlm : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        CN_alm_almacenes ObjAlm = new CN_alm_almacenes();
        CN_sun_tipope objTipOpe = new CN_sun_tipope();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_alm_inventario objItem = new CN_alm_inventario();

        DataTable dtAlmacenes = new DataTable();
        DataTable dtTipOpe = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtItem = new DataTable();

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        string[,] arrCabeceraFlex1 = new string[13, 5];

        public FrmRepMovAlm()
        {
            InitializeComponent();
        }
        void ConfigurarFormulario()
        {
            Cs1.Left = 0;
            Cs1.Top = 41;

            Cs1.Width = this.Width - 17;
            Cs1.Height = this.Height - 80;

            DataTable dtTable = new DataTable();

            arrCabeceraFlex1[0, 0] = "Tipo Documento";
            arrCabeceraFlex1[0, 1] = "120";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_abrsun";

            arrCabeceraFlex1[1, 0] = "Fch. Documento";
            arrCabeceraFlex1[1, 1] = "80";
            arrCabeceraFlex1[1, 2] = "F";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_des";

            arrCabeceraFlex1[2, 0] = "Nº Documento";
            arrCabeceraFlex1[2, 1] = "100";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_desunimedbas";

            arrCabeceraFlex1[3, 0] = "Proveedor";
            arrCabeceraFlex1[3, 1] = "150";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "0";
            arrCabeceraFlex1[3, 4] = "n_idunimed";

            arrCabeceraFlex1[4, 0] = "Item";
            arrCabeceraFlex1[4, 1] = "200";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "n_canunimedbas";

            arrCabeceraFlex1[5, 0] = "Cantidad";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_default";

            arrCabeceraFlex1[6, 0] = "Pre. Unitario";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_preuni";

            arrCabeceraFlex1[7, 0] = "Totak";
            arrCabeceraFlex1[7, 1] = "80";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_pretot";
            
            arrCabeceraFlex1[8, 0] = "Nº Lote";
            arrCabeceraFlex1[8, 1] = "80";
            arrCabeceraFlex1[8, 2] = "C";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "n_default";

            arrCabeceraFlex1[9, 0] = "Tipo Operacion";
            arrCabeceraFlex1[9, 1] = "150";
            arrCabeceraFlex1[9, 2] = "C";
            arrCabeceraFlex1[9, 3] = "0";
            arrCabeceraFlex1[9, 4] = "n_idunimedbas";

            arrCabeceraFlex1[10, 0] = "Tip. Doc. Compra";
            arrCabeceraFlex1[10, 1] = "40";
            arrCabeceraFlex1[10, 2] = "C";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "c_docpagtipdoc";

            arrCabeceraFlex1[11, 0] = "Nº Doc. Compra";
            arrCabeceraFlex1[11, 1] = "100";
            arrCabeceraFlex1[11, 2] = "C";
            arrCabeceraFlex1[11, 3] = "";
            arrCabeceraFlex1[11, 4] = "c_docpagnumdoc";

            arrCabeceraFlex1[12, 0] = "Fecha Doc. Compra";
            arrCabeceraFlex1[12, 1] = "70";
            arrCabeceraFlex1[12, 2] = "F";
            arrCabeceraFlex1[12, 3] = "";
            arrCabeceraFlex1[12, 4] = "c_docpagfchdoc";

            funFlex.FlexMostrarDatos(FgFlex, arrCabeceraFlex1, dtTable, 2, false);           
        }
        private void FrmRepMovAlm_Load(object sender, EventArgs e)
        {
            this.Text = "Almacen - Reporte de Movimientos";
            this.Width = 1057;
            this.Height = 629;

            ConfigurarFormulario();

            ObjAlm.mysConec = mysConec;
            dtAlmacenes = ObjAlm.ListarNuevo(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            
            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objItem.mysConec = mysConec;
            dtItem = objItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            objTipOpe.mysConec = mysConec;
            dtTipOpe = objTipOpe.Listar();

            funDatos.ComboBoxCargarDataTable(CboAlmacen, dtAlmacenes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipExi, dtTipExi, "n_id", "c_des");
                        
            OptIng.Focus();
            CboItem.Enabled = false;
            OptIng.Checked = true;
            OptEst1.Checked = true;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        private void Button2_Click(object sender, EventArgs e)
        {
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            mysConec = null;
            this.Close();
            Close();
        }
        private void FrmRepMovAlm_Resize(object sender, EventArgs e)
        {
            Cs1.Width = this.Width - 17;
            Cs1.Height = this.Height - 80;
        }
        private void OptIng_CheckedChanged(object sender, EventArgs e)
        {
            if (OptIng.Checked == true)
            {
                DataTable dtResul = new DataTable();

                dtResul = funDatos.DataTableFiltrar(dtTipOpe, "n_tipmov = 1");
                funDatos.ComboBoxCargarDataTable(CboTipOpe, dtResul, "n_id", "c_des");
            }
        }
        private void OptSal_CheckedChanged(object sender, EventArgs e)
        {
            if (OptSal.Checked==true)
            { 
                DataTable dtResul = new DataTable();

                dtResul = funDatos.DataTableFiltrar(dtTipOpe, "n_tipmov = 2");
                funDatos.ComboBoxCargarDataTable(CboTipOpe, dtResul, "n_id", "c_des");
            }
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ Ingrese fecha de inicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ Ingrese fecha final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            if (TxtFchIni.Value > TxtFchFin.Value)
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_tipMov = 0;
            int n_row = 0;
            if (OptIng.Checked == true)
            {
                n_tipMov = 1;
            }
            else
            {
                n_tipMov = 2;
            }
            
            FgFlex.Rows.Count = 2;

            CN_alm_movimientos objAlm = new CN_alm_movimientos();
            DataTable dtResult = new DataTable();
            DateTime dtFch;
            objAlm.mysConec = mysConec;
            dtResult = objAlm.VerMovimientosAlmacen(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboAlmacen.SelectedValue), 
                                                    Convert.ToInt32(CboTipOpe.SelectedValue), Convert.ToInt32(CboTipExi.SelectedValue),
                                                    Convert.ToInt32(CboItem.SelectedValue), n_tipMov, TxtFchIni.Text, TxtFchFin.Text);

            if (dtResult.Rows.Count == 0)
            {

            }
            else
            {
                if (OptEst2.Checked == true) { dtResult = funDatos.DataTableFiltrar(dtResult, "n_iddocven <> 0"); }
                if (OptEst3.Checked == true) { dtResult = funDatos.DataTableFiltrar(dtResult, "n_iddocven = 0"); }

                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    FgFlex.Rows.Count = FgFlex.Rows.Count + 1;
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 1, dtResult.Rows[n_row]["c_destipdoc"].ToString());
                    dtFch = Convert.ToDateTime(dtResult.Rows[n_row]["d_fchdoc"].ToString());
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 2, dtFch.ToString("dd/MM/yyyy"));
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 3, dtResult.Rows[n_row]["c_numdoc"].ToString());
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 4, dtResult.Rows[n_row]["c_nompro"].ToString());
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 5, dtResult.Rows[n_row]["c_desite"].ToString());
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 6, Convert.ToDouble(dtResult.Rows[n_row]["n_can"]).ToString("0.00"));
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 7, Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_preuni"])).ToString("0.00"));
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 8, Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_pretot"])).ToString("0.00"));
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 9, dtResult.Rows[n_row]["c_numlot"].ToString());
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 10, dtResult.Rows[n_row]["c_destipope"].ToString());
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 11, dtResult.Rows[n_row]["c_docpagtipdoc"].ToString());
                    FgFlex.SetData(FgFlex.Rows.Count - 1, 12, dtResult.Rows[n_row]["c_docpagnumdoc"].ToString());
                    
                    if (dtResult.Rows[n_row]["c_docpagfchdoc"].ToString() !="")
                    {
                        FgFlex.SetData(FgFlex.Rows.Count - 1, 13, dtResult.Rows[n_row]["c_docpagfchdoc"].ToString());
                    }
                }
            }
        }
        private void CboTipExi_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboTipExi.SelectedIndex) == 0) { return; }
            DataTable dtResult = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtItem, "(n_idtipexi = " + Convert.ToInt32(CboTipExi.SelectedValue) + ")");
            funDatos.ComboBoxCargarDataTable(CboItem, dtResult, "n_id", "c_despro");
            CboItem.Enabled = true;
        }
        private void CboTipExi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                CboTipExi.SelectedValue = 0;
                CboItem.SelectedValue = 0;
                CboItem.Enabled = false;
            }
        }
        private void CboAlmacen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                CboAlmacen.SelectedValue = 0;
            }
        }
        private void CboTipOpe_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                CboTipOpe.SelectedValue = 0;
            }
        }

        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            if (FgFlex.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            string c_titu2 = "";
            
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-ALM-MOV-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text+ "        FECHA FINAL : "+ TxtFchFin.Text;
            funFlex.ExportToExcel(FgFlex, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "MOVIMIENTOS DE ALMACEN", c_titu2, c_NomArchivo);
        }
    }
}
