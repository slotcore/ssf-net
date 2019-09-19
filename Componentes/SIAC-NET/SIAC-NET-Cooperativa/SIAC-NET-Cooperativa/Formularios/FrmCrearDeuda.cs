using Helper;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Cooperativa;
using SIAC_Negocio.Maestros;
using SIAC_Objetos;
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
using SIAC_Entidades.Cooperativa;
using SIAC_Negocio.Sunat;

namespace SIAC_NET_Cooperativa.Formularios
{
    public partial class FrmCrearDeuda : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        public int N_SOCIOID;
        public string C_NOMSOCIO;
        public string C_SOCIOTIPSOC;
        public string C_SOCIONUMRUC;
        public int N_SOCIOTIPO;
        public int N_TIPOOPERACION;

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Helper.Comunes.Funciones funGen = new Helper.Comunes.Funciones();

        CN_mae_meses objMeses = new CN_mae_meses();
        CN_coo_conceptos objConcep = new CN_coo_conceptos();
        CN_coo_sociospuestos objSocPuestos = new CN_coo_sociospuestos();

        DataTable dtMeses = new DataTable();
        DataTable dtPuestos = new DataTable();
        DataTable dtConcepto = new DataTable();
        DataTable dtPuestosNA = new DataTable();

        bool b_Agregando;
        string[,] arrCabeceraFlexCar = new string[3, 5];
        public FrmCrearDeuda()
        {
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        void ConfigurarFormulario()
        {
            DataTable dtResul = new DataTable();
            this.Height = 433;
            this.Width = 586;

            arrCabeceraFlexCar[0, 0] = "Concepto";
            arrCabeceraFlexCar[0, 1] = "300";
            arrCabeceraFlexCar[0, 2] = "C";
            arrCabeceraFlexCar[0, 3] = "";
            arrCabeceraFlexCar[0, 4] = "";

            arrCabeceraFlexCar[1, 0] = "Importe";
            arrCabeceraFlexCar[1, 1] = "80";
            arrCabeceraFlexCar[1, 2] = "D";
            arrCabeceraFlexCar[1, 3] = "";
            arrCabeceraFlexCar[1, 4] = "";

            arrCabeceraFlexCar[2, 0] = "idconepto";
            arrCabeceraFlexCar[2, 1] = "0";
            arrCabeceraFlexCar[2, 2] = "C";
            arrCabeceraFlexCar[2, 3] = "";
            arrCabeceraFlexCar[2, 4] = "";

            funFlex.FlexMostrarDatos(FgLista, arrCabeceraFlexCar, dtMeses, 2, false);
        }
        private void FrmCrearDeuda_Activated(object sender, EventArgs e)
        {
            TxtNumDoc.Text = C_SOCIONUMRUC;
            TxtNomSoc.Text = C_NOMSOCIO;
            TxtTipSoc.Text = C_SOCIOTIPSOC;
        }
        private void CmdCan_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmCrearDeuda_Load(object sender, EventArgs e)
        {
            b_Agregando = true;
            ConfigurarFormulario();
            DataCargar();
            CargarCombos();
            FgLista.Rows.Count = 15;
            b_Agregando = false;
            if (N_TIPOOPERACION == 1)
            {
                this.Text = "COOPERATIVA - Generacion de Deuda";
                LblDato.Text = "Generacion de Deuda";
            }
            else
            {
                this.Text = "COOPERATIVA - Pago Adelantado";
                LblDato.Text = "Pago Adelantado";
            }
        }
        void CargarCombos()
        {
            DataTable dtResult = new DataTable();
            dtResult = funDatos.DataTableFiltrar(dtPuestos, "(n_idsoc = " + N_SOCIOID.ToString() +")");
            funDatos.ComboBoxCargarDataTable(CboPue, dtResult, "n_id", "c_puesto");

            if (N_TIPOOPERACION == 1)          // NOS ASEGURAMOS QUE SOLO MUESTRE LOS MESES ANTERIORES O EL ACTUAL
            {
                dtResult = funDatos.DataTableFiltrar(dtMeses, "((n_id > 0) AND (n_id <= " + STU_SISTEMA.MESTRABAJO + "))");
            }
            else
            {
                dtResult = funDatos.DataTableFiltrar(dtMeses, "((n_id > " + STU_SISTEMA.MESTRABAJO + ") AND (n_id < 13))");
            }
            
            funDatos.ComboBoxCargarDataTable(CboMesTra, dtResult, "n_id", "c_des");
        }
        void DataCargar()
        {
            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objConcep.mysConec = mysConec;
            dtConcepto =  objConcep.Listar(STU_SISTEMA.EMPRESAID);
            dtConcepto = funDatos.DataTableOrdenar(dtConcepto, "c_des");

            objSocPuestos.mysConec = mysConec;
            objSocPuestos.Consulta1(STU_SISTEMA.EMPRESAID,1);       // TRAEMOS TODOS LOS PUESTOS ACTIVOS
            dtPuestos = objSocPuestos.dtPuestosSocios;

            objSocPuestos.Consulta1(STU_SISTEMA.EMPRESAID, 2);      // TRAEMOS TODOS LOS PUESTOS INACTIVOS
            dtPuestosNA = objSocPuestos.dtPuestosSocios;
        }
        private void FgLista_EnterCell(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }

            DataTable dtResult = new DataTable();

            if (FgLista.Col == 1)
            {
                if (Convert.ToInt16(CboPue.SelectedValue) == 0) { return; }
                FgLista.AllowEditing = true;

                if (ChkActivar.Checked == false)
                { 
                    dtResult = funDatos.DataTableFiltrar(dtPuestos, "n_id = " + CboPue.SelectedValue.ToString() + "");
                }
                if (ChkActivar.Checked == true)
                {
                    dtResult = funDatos.DataTableFiltrar(dtPuestosNA, "n_id = " + CboPue.SelectedValue.ToString() + "");
                }

                if (Convert.ToInt16(dtResult.Rows[0]["n_idtippue"]) == 1)
                {
                    dtResult = funDatos.DataTableFiltrar(dtConcepto, "c_cod like '" + "70" + "*'");
                }
                else
                {
                    dtResult = funDatos.DataTableFiltrar(dtConcepto, "c_cod like '" + "75" + "*'");
                }
                //if (N_SOCIOTIPO == 1)
                //{ 
                //    dtResult = funDatos.DataTableFiltrar(dtConcepto, "c_cod like '" + "70" + "*'");
                //}
                //else
                //{
                //    dtResult = funDatos.DataTableFiltrar(dtConcepto, "c_cod like '" + "75" + "*'");
                //}

                dtResult = funDatos.DataTableOrdenar(dtResult, "c_des");
                funFlex.FlexColumnaCombo(FgLista, dtResult, "c_des", 1);    // ITEMS
            }
        }
        private void FgLista_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (b_Agregando == true) { return; }
            double n_valor = 0;

            if (FgLista.Col == 1)
            { 
                string c_dato = FgLista.GetData(FgLista.Row,1).ToString();
                double n_precio = Convert.ToDouble(funDatos.DataTableBuscar(dtConcepto, "c_des", "n_imp", c_dato, "C").ToString());
                int n_idcon = Convert.ToInt32(funDatos.DataTableBuscar(dtConcepto, "c_des", "n_id", c_dato, "C").ToString());

                FgLista.SetData(FgLista.Row, 2, n_precio.ToString("0.00"));
                FgLista.SetData(FgLista.Row, 3, n_idcon.ToString());
            }

            if (FgLista.Col == 2)
            {
                n_valor = Convert.ToDouble(FgLista.GetData(FgLista.Row, 2).ToString());
                FgLista.SetData(FgLista.Row, 2, n_valor.ToString("0.00"));
            }
        }
        bool ValidarDatos()
        {
            bool b_Result = false;
            if (Convert.ToInt32(CboPue.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado en numero de puesto al que se generara el cargo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPue.Focus();
                return b_Result;
            }
            if (Convert.ToInt32(CboMesTra.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el mes en que se creara el cargo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesTra.Focus();
                return b_Result;
            }

            int n_row = 0;
            int n_blancos = 0;
            for (n_row = 2; n_row <= FgLista.Rows.Count - 1; n_row++)
            {
                if (funGen.NulosC(FgLista.GetData(n_row, 1)) != "")
                {
                    n_blancos = n_blancos + 1;
                }
            }
            if (n_blancos == 0)
            { 
                MessageBox.Show("¡ No ha especificado ningun concepto para la creacion de este cargo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgLista.Focus();
                return b_Result;
            }

            b_Result = true;
            return b_Result;
        }
        private void CmdAce_Click(object sender, EventArgs e)
        {
            if (ValidarDatos() == false) { return; }

            BE_COO_CARGOSCAB entCab = new BE_COO_CARGOSCAB();
            List<BE_COO_CARGOSDET> lstDet = new List<BE_COO_CARGOSDET>();
            int n_row = 0;
            
            double n_impbru = 0;
            double n_igv = 0;
            double n_impnet = 0;
            string c_numdoc = "";
            int n_idtipdoc = 0;
            DataTable dtResul = new DataTable();

            n_idtipdoc = 82;

            CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
            objTipDoc.mysConec = mysConec;
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, n_idtipdoc, "0001");
            
            entCab.n_idemp = STU_SISTEMA.EMPRESAID;

            if (N_TIPOOPERACION == 1)
            {
                if (N_SOCIOTIPO == 1)
                {
                    entCab.n_idcar = 44;                     // ASIGNAMOS EL CARGO DE RECIBOS
                }
                else
                {
                    entCab.n_idcar = 43;                     // ASIGNAMOS EL CARGO DE BOLETAS DE VENTA
                }
            }
            else
            {
                CN_coo_cargos xfunCar = new CN_coo_cargos();
                xfunCar.mysConec = mysConec;
                xfunCar.STU_SISTEMA = STU_SISTEMA;

                if (N_SOCIOTIPO == 1)
                {
                    xfunCar.ObtenerMesValido(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, 81);
                }
                else
                {
                    xfunCar.ObtenerMesValido(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, 4);
                }
                dtResul = xfunCar.dtLista;
                entCab.n_idcar = Convert.ToInt32(dtResul.Rows[0]["n_id"]);
            }

            entCab.n_idsoc = N_SOCIOID;
            entCab.n_idsocpue = Convert.ToInt32(CboPue.SelectedValue);
            entCab.n_id = 0;
            entCab.n_idtipdoc = n_idtipdoc;
            entCab.c_numser = "0001";
            entCab.c_numdoc = c_numdoc;
            entCab.d_fchemi = DateTime.Now;
            entCab.d_fchven = DateTime.Now; 
            entCab.c_glosa = "";
            entCab.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entCab.n_mestra = Convert.ToInt32(CboMesTra.SelectedValue);
            entCab.n_iddocpag = 0;
            

            int n_afecto = 0;
            double n_valor = 0;
            double n_TasaIGV = 18;

            for(n_row=2; n_row <= FgLista.Rows.Count-1; n_row++)
            {
                Helper.Comunes.Funciones fun = new Helper.Comunes.Funciones();
                
                if (fun.NulosC(FgLista.GetData(n_row, 1)) != "")
                { 
                    BE_COO_CARGOSDET entDet = new BE_COO_CARGOSDET();
                
                    entDet.n_idemp = STU_SISTEMA.EMPRESAID;
                    entDet.n_idcar = entCab.n_idcar;
                    entDet.n_idsoc = N_SOCIOID;
                    entDet.n_idpue = Convert.ToInt32(CboPue.SelectedValue);
                    entDet.n_idcon= Convert.ToInt32(FgLista.GetData(n_row,3).ToString());
                    entDet.n_can = 1;
                                
                    n_valor = Convert.ToDouble(FgLista.GetData(n_row,2).ToString());
                    n_afecto = Convert.ToInt32(funDatos.DataTableBuscar(dtConcepto,"n_id","n_afeigv",entDet.n_idcon.ToString(),"N").ToString());
                                    
                    entDet.n_impbru = n_valor;
                    entDet.n_imptotbru = (n_valor * 1);
                    if (n_afecto == 2)         // INDICA QUE EL CONCEPTO ES INAFECTO AL IGV
                    {
                        entDet.n_impnet =  n_valor;
                        entDet.n_imptotnet = (n_valor * 1);
                    }
                    else
                    {
                        entDet.n_impnet =  n_valor * ((n_TasaIGV / 100) + 1);
                        entDet.n_imptotnet = (n_valor * ((n_TasaIGV / 100) + 1) * 1);
                    }

                    //entDet.n_imptotbru = Convert.ToDouble(FgLista.GetData(n_row,2).ToString());;
                    //entDet.n_imptotnet = Convert.ToDouble(FgLista.GetData(n_row,2).ToString());;
                    entDet.n_idcor = 0;
                    entDet.n_pagado = 0;
                    entDet.n_iddocpag = 0;

                    n_impbru = n_impbru + entDet.n_imptotbru;
                    n_impnet = n_impnet + entDet.n_imptotnet;
                    n_igv = n_igv + (entDet.n_imptotnet - entDet.n_imptotbru);

                    lstDet.Add(entDet);
                }
            }

            entCab.n_impbru = n_impbru;
            entCab.n_impigv = n_igv;
            entCab.n_imptot = n_impnet;
            entCab.n_impsal = n_impnet;
            
            CN_coo_cargoscab funCar = new CN_coo_cargoscab();
            funCar.mysConec = mysConec;
            if (funCar.InsertarCargo(entCab, lstDet) == true)
            {
                MessageBox.Show("¡ El cargo se genero con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            else
            {
                MessageBox.Show("¡ No se pudo generar el cargo por el siguiente motivo: " + funCar .StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void FgLista_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (b_Agregando == true) { return; }

            DataTable dtResult = new DataTable();

            if (FgLista.Col == 1)
            {
                string c_dato = FgLista.GetData(FgLista.Row, 1).ToString();
                string c_idcon = funDatos.DataTableBuscar(dtConcepto, "c_des", "n_id", c_dato, "N").ToString();
                FgLista.SetData(FgLista.Row, 3, c_idcon);
            }
        }

        private void CboPue_SelectedValueChanged(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            //DataTable dtResult = new DataTable();
            //dtResult = funDatos.DataTableFiltrar(dtPuestos,"n_id = " + CboPue.SelectedValue.ToString() +"");
            //if (dtResult.Rows.Count != 0)
            //{

            //}
            FgLista.Rows.Count = 2;
            FgLista.Rows.Count = 15;
        }

        private void ChkActivar_CheckedChanged(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }

            DataTable dtResult = new DataTable();
            b_Agregando = true;
            if (ChkActivar.Checked == true)
            {
                dtResult = funDatos.DataTableFiltrar(dtPuestosNA, "(n_idsoc = " + N_SOCIOID.ToString() + ")");
                funDatos.ComboBoxCargarDataTable(CboPue, dtResult, "n_id", "c_puesto");
            }
            else 
            {
                dtResult = funDatos.DataTableFiltrar(dtPuestos, "(n_idsoc = " + N_SOCIOID.ToString() + ")");
                funDatos.ComboBoxCargarDataTable(CboPue, dtResult, "n_id", "c_puesto");
            }
            b_Agregando = false;
        }
    }
}
