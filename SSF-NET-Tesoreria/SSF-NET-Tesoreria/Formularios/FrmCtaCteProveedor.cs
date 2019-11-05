using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Tesoreria;

namespace SSF_NET_Tesoreria.Formularios
{
    public partial class FrmCtaCteProveedor : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objItems = new CN_alm_inventario();

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabecera1 = new string[5, 5];
        string[,] arrCabecera2 = new string[13, 5];
        string[,] arrCabecera3 = new string[7, 5];
        public FrmCtaCteProveedor()
        {
            InitializeComponent();
        }

        private void FrmCtaCteProveedor_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            //objIte.mysConec = mysConec;
            //dtItem = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);

            objPro.mysConec = mysConec;
            dtProv = objPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "TESORERIA - CUENTA CORRIENTE PROVEEDORES";
            TxtFchIni.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
            OptRes.Checked = true;
            OptSol.Checked = true;
            OptTodDoc.Checked = true;

            //funControl.dtpBlanquea(TxtFchIni);
            //funControl.dtpBlanquea(TxtFchFin);

            arrCabeceraFlex1[0, 0] = "Proveedor";
            arrCabeceraFlex1[0, 1] = "355";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgPro, arrCabeceraFlex1, dtLista, 1, false);
            FgPro.AllowEditing = true;

            FgDatos.Cols.Count = 5;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
            SetearCabecera1();
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº R.U.C.";
            arrCabecera1[0, 1] = "100";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numdoc";

            arrCabecera1[1, 0] = "Proveedor";
            arrCabecera1[1, 1] = "400";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_nombre";

            arrCabecera1[2, 0] = "Cargos";
            arrCabecera1[2, 1] = "70";
            arrCabecera1[2, 2] = "D";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "n_cargos";

            arrCabecera1[3, 0] = "Abonos";
            arrCabecera1[3, 1] = "70";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "n_abonos";

            arrCabecera1[4, 0] = "Saldo";
            arrCabecera1[4, 1] = "70";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "n_saldo";
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Nº Registro";
            arrCabecera2[0, 1] = "70";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_numreg";

            arrCabecera2[1, 0] = "Origen";
            arrCabecera2[1, 1] = "250";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_deslib";

            arrCabecera2[2, 0] = "T.D.";
            arrCabecera2[2, 1] = "40";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_abr";

            arrCabecera2[3, 0] = "Nº Documento";
            arrCabecera2[3, 1] = "100";
            arrCabecera2[3, 2] = "C";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "c_numdoc";

            arrCabecera2[4, 0] = "Fch. Emision";
            arrCabecera2[4, 1] = "70";
            arrCabecera2[4, 2] = "F";
            arrCabecera2[4, 3] = "dd/MM/yyyy";
            arrCabecera2[4, 4] = "d_fchdoc";

            arrCabecera2[5, 0] = "Fch. Vencimiento";
            arrCabecera2[5, 1] = "70";
            arrCabecera2[5, 2] = "F";
            arrCabecera2[5, 3] = "dd/MM/yyyy";
            arrCabecera2[5, 4] = "d_fchven";

            arrCabecera2[6, 0] = "Moneda";
            arrCabecera2[6, 1] = "40";
            arrCabecera2[6, 2] = "C";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "c_mondes";

            arrCabecera2[7, 0] = "Importe";
            arrCabecera2[7, 1] = "80";
            arrCabecera2[7, 2] = "D";
            arrCabecera2[7, 3] = "0.00";
            arrCabecera2[7, 4] = "n_imptotven";

            arrCabecera2[8, 0] = "T.C.";
            arrCabecera2[8, 1] = "50";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = "0.000";
            arrCabecera2[8, 4] = "n_tc";

            arrCabecera2[9, 0] = "Debe";
            arrCabecera2[9, 1] = "80";
            arrCabecera2[9, 2] = "D";
            arrCabecera2[9, 3] = "0.00";
            arrCabecera2[9, 4] = "n_debe";

            arrCabecera2[10, 0] = "Haber";
            arrCabecera2[10, 1] = "80";
            arrCabecera2[10, 2] = "D";
            arrCabecera2[10, 3] = "0.00";
            arrCabecera2[10, 4] = "n_haber";

            arrCabecera2[11, 0] = "Saldo";
            arrCabecera2[11, 1] = "80";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = "0.00";
            arrCabecera2[11, 4] = "n_saldo";

            arrCabecera2[12, 0] = "Id";
            arrCabecera2[12, 1] = "0";
            arrCabecera2[12, 2] = "D";
            arrCabecera2[12, 3] = "";
            arrCabecera2[12, 4] = "n_id";
        }
        void Cabecera3()
        {
            arrCabecera3[0, 0] = "Codigo";
            arrCabecera3[0, 1] = "120";
            arrCabecera3[0, 2] = "C";
            arrCabecera3[0, 3] = "";
            arrCabecera3[0, 4] = "c_codpro";

            arrCabecera3[1, 0] = "Producto";
            arrCabecera3[1, 1] = "300";
            arrCabecera3[1, 2] = "C";
            arrCabecera3[1, 3] = "";
            arrCabecera3[1, 4] = "c_despro";

            arrCabecera3[2, 0] = "Uni. Med.";
            arrCabecera3[2, 1] = "40";
            arrCabecera3[2, 2] = "C";
            arrCabecera3[2, 3] = "";
            arrCabecera3[2, 4] = "c_desunimed";

            arrCabecera3[3, 0] = "Cantidad";
            arrCabecera3[3, 1] = "80";
            arrCabecera3[3, 2] = "D";
            arrCabecera3[3, 3] = "0.00";
            arrCabecera3[3, 4] = "n_totcan";

            arrCabecera3[4, 0] = "Importe Soles";
            arrCabecera3[4, 1] = "80";
            arrCabecera3[4, 2] = "D";
            arrCabecera3[4, 3] = "0.00";
            arrCabecera3[4, 4] = "n_impsol";

            arrCabecera3[5, 0] = "Importe Dolares";
            arrCabecera3[5, 1] = "80";
            arrCabecera3[5, 2] = "D";
            arrCabecera3[5, 3] = "0.00";
            arrCabecera3[5, 4] = "n_impdol";

            arrCabecera3[6, 0] = "Importe Exp. Soles";
            arrCabecera3[6, 1] = "80";
            arrCabecera3[6, 2] = "D";
            arrCabecera3[6, 3] = "0.00";
            arrCabecera3[6, 4] = "n_impexpsol";
        }

        private void FrmCtaCteProveedor_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ToolBuscar_Click(object sender, EventArgs e)
        {
            EjecutarConsulta();
        }
        void EjecutarConsulta()
        {
            int n_idmon = 0;
            int n_tipsal = 0;
            string c_cadin = CadenaIN();

            int n_camconsulta = 0;        //  INDICA AL REPORTE QUE CAMPO SE USARA PARA EFECTURA LA BUSQEDA DE FECHA  1 = FECHA DE DOCUMENTO   ; 2 = FECHA DE REGISTRO
            if (funFunciones.NulosC(TxtFchIni.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha de inicio para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (funFunciones.NulosC(TxtFchFin.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha final para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);

            if (d_fchini > d_fchfin)
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (OptRes.Checked == true) { n_camconsulta = 1; }
            if (OptDet.Checked == true) { n_camconsulta = 2; }

            if (OptTodDoc.Checked == true) { n_tipsal = 1; }
            if (OptPen.Checked == true) { n_tipsal = 2; }
            if (OptPag.Checked == true) { n_tipsal = 3; }

            if (OptTod.Checked == true) { n_idmon = 0; }
            if (OptSol.Checked == true) { n_idmon = 115; }
            if (OptDol.Checked == true) { n_idmon = 151; }
            
            CN_tes_ctacte functacte = new CN_tes_ctacte();
            functacte.mysConec = mysConec;
            functacte.STU_SISTEMA = STU_SISTEMA;

            if (OptRes.Checked == true)
            {
                functacte.ListarCtaCtePro(STU_SISTEMA.EMPRESAID, TxtFchIni.Text.Substring(0, 10), TxtFchFin.Text.Substring(0, 10), n_idmon);
                dtLista = functacte.dtLista;
                if (n_tipsal == 2) { dtLista = funDatos.DataTableFiltrar(dtLista,"n_saldo > 0");}
                if (n_tipsal == 3) { dtLista = funDatos.DataTableFiltrar(dtLista, "n_saldo = 0"); }

                FgDatos.Cols.Count = 5;
                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
                SetearCabecera1();
                ResumenMostrarTotales();
            }
            if (OptDet.Checked == true)
            {
                var fechaInicio = TxtFchIni.Value.ToString("dd/MM/yyyy");
                var fechaFin = TxtFchFin.Value.ToString("dd/MM/yyyy");
                functacte.ListarCtaCteProDetalle(STU_SISTEMA.EMPRESAID, fechaInicio, fechaFin, n_idmon, n_tipsal, c_cadin);
                dtLista = functacte.dtLista;
                //dtLista = funDatos.DataTableOrdenar(dtLista, "c_dato");
                //if (c_cadin != "") { dtLista = funDatos.DataTableFiltrar(dtLista, "n_idcli IN (" + c_cadin + ")"); }
                FgDatos.Cols.Count = 12;
                Cabecera2();
                funFlex.b_AlternarColor = false;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 3, true);
                SetearCabecera2();
                AgruparResultado();    
            }
        }
        void AgruparResultado()
        {
            int n_fil = 0;
            double n_val = 0;
            int n_filalt = 0;
            string c_dato = "";
            double n_pro = 0;
            int n_filcolor1 = 0;
            int n_filcolor2 = 0;
            int n_color = 1;
            double n_total = 0;
            double n_valor1 = 0;
            double n_valor2 = 0;

            for (n_fil = 3; n_fil <= FgDatos.Rows.Count - 1; n_fil++)
            {
                n_val = Convert.ToDouble(FgDatos.GetData(n_fil, 13));

                if (n_val == 99999)
                {
                    c_dato = "";
                }
                n_filalt = FgDatos.Rows[n_fil].Height;
                c_dato = FgDatos.GetData(n_fil, 2).ToString();

                if (n_val == 0)
                {
                    funFlex.Flex_PintarCeldas(FgDatos, n_fil, 1, n_fil, 12, Color.Blue, Color.Transparent);
                }
                else
                {
                    if (n_pro != 0)
                    {
                        if (n_val == 99999)
                        {
                            funFlex.Flex_PintarCeldas(FgDatos, n_fil, 1, n_fil, 12, Color.Blue, Color.Transparent);
                        }
                        else 
                        {
                            if (n_val != n_pro)
                            {
                                if (n_color == 1)
                                {
                                    funFlex.Flex_PintarCeldas(FgDatos, n_filcolor1, 1, n_filcolor2, 12, Color.Black, Color.Beige); n_color = 2;
                                }
                                else
                                {
                                    funFlex.Flex_PintarCeldas(FgDatos, n_filcolor1, 1, n_filcolor2, 12, Color.Black, Color.White); n_color = 1;
                                }

                                n_pro = n_val;
                                n_filcolor1 = n_fil;
                                n_filcolor2 = n_fil;
                                n_total = Convert.ToDouble(FgDatos.GetData(n_fil, 12));
                            }
                            else
                            {
                                n_filcolor2 = n_filcolor2 + 1;
                                n_valor1 = Convert.ToDouble(FgDatos.GetData(n_fil, 10));
                                n_valor2 = Convert.ToDouble(FgDatos.GetData(n_fil, 11));
                                n_total = ((n_total - n_valor1) + n_valor2);
                                FgDatos.SetData(n_fil, 12, n_total.ToString("0.00"));
                            }
                        }
                    }
                    else
                    {
                        n_pro = n_val;
                        n_filcolor1 = n_fil;
                        n_filcolor2 = n_fil;
                        n_total = Convert.ToDouble(FgDatos.GetData(n_fil, 12));
                    }
                    
                }
            }

            if (n_color == 1)
            {
                funFlex.Flex_PintarCeldas(FgDatos, n_filcolor1, 1, n_filcolor2, 12, Color.Black, Color.Beige); n_color = 2;
            }
            else
            {
                funFlex.Flex_PintarCeldas(FgDatos, n_filcolor1, 1, n_filcolor2, 12, Color.Black, Color.White); n_color = 1;
            }
        }
        void MostrarDetalle(DataTable dtResultado)
        {
            int n_row = 0;
            int n_filfle = 0;
            string c_dato ="";
            double n_saldo = 0;
            int n_newdoc = 0;
            int n_filini = 0;
            int n_filfin = 0;
            double n_prototdeb = 0;
            double n_protothab = 0;
            double n_proimptot = 0;
            double n_prosal = 0;

            double n_gratotdeb = 0;
            double n_gratothab = 0;
            double n_graimptot = 0;
            double n_grasal = 0;

            for (n_row = 0; n_row <= dtResultado.Rows.Count - 1; n_row++)
            {
                FgDatos.Rows.Count = FgDatos.Rows.Count +1;
                n_filfle = FgDatos.Rows.Count -1;

                if (dtResultado.Rows[n_row]["n_ord"].ToString() == "0")
                {
                    if (n_row != 0)
                    {
                        n_filfin = FgDatos.Rows.Count - 2;
                        funFlex.Flex_PintarCeldas(FgDatos, n_filini, 1, n_filfin, 12, Color.Black, Color.Beige);

                        n_graimptot = n_graimptot + n_proimptot;
                        n_gratotdeb = n_gratotdeb + n_prototdeb;
                        n_gratothab = n_gratothab + n_protothab;
                        n_grasal = n_grasal + n_prosal;

                        //FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                        n_filfle = FgDatos.Rows.Count - 1;

                        FgDatos.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom; // = AllowMergingEnum.FixedOnly;
                        funFlex.Flex_UniColumnas(FgDatos, n_filfle, 1, 6, "TOTAL PROVEEDOOR ==>", 1);
                        funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 5, n_filfle, 7, Color.Blue, Color.White);

                        c_dato = n_proimptot.ToString("0.00");
                        FgDatos.SetData(n_filfle, 8, c_dato);
                        funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 8, n_filfle, 8, Color.Blue, Color.White);

                        c_dato = n_prototdeb.ToString("0.00");
                        FgDatos.SetData(n_filfle, 10, c_dato);
                        funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 10, n_filfle, 10, Color.Blue, Color.White);

                        c_dato = n_protothab.ToString("0.00");
                        FgDatos.SetData(n_filfle, 11, c_dato);
                        funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 11, n_filfle, 11, Color.Blue, Color.White);

                        c_dato = n_prosal.ToString("0.00");
                        FgDatos.SetData(n_filfle, 12, c_dato);
                        funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 12, n_filfle, 12, Color.Blue, Color.White);

                        FgDatos.Rows.Count = FgDatos.Rows.Count + 2;
                    }

                    //if (n_row == 0) { n_filini = FgDatos.Rows.Count - 1; }
                    n_filfle = FgDatos.Rows.Count - 1;
                    FgDatos.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictAll; // = AllowMergingEnum.FixedOnly;
                    c_dato = dtResultado.Rows[n_row]["c_numruc"].ToString();


                    funFlex.Flex_UniColumnas(FgDatos, n_filfle, 1, 6, c_dato, 1);
                    funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 1, n_filfle, 6, Color.Blue, Color.White);
                    n_newdoc = 0;
                    n_filini = FgDatos.Rows.Count - 1; 
                    n_prototdeb = 0;
                    n_protothab = 0;
                    n_proimptot = 0;
                    n_prosal = 0;
                }
                if (dtResultado.Rows[n_row]["n_ord"].ToString() == "1")
                {
                    if (n_newdoc == 1) 
                    {
                        // PINTAMOS DE BEIGE
                        n_filfin = FgDatos.Rows.Count - 2;
                        funFlex.Flex_PintarCeldas(FgDatos,n_filini, 1, n_filfin, 12,Color.Black,Color.Beige);

                        // PINTAMOS DE BLANCO LA ULTIMA FILA
                        funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 1, FgDatos.Rows.Count - 1, 12, Color.Blue, Color.White);

                        FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                        n_filini = FgDatos.Rows.Count - 1;
                        n_filfle = FgDatos.Rows.Count - 1;
                    }
                    
                    //FgDatos.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.None;
                    c_dato = dtResultado.Rows[n_row]["c_numreg"].ToString();
                    FgDatos.SetData(n_filfle, 1, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_deslib"].ToString();
                    FgDatos.SetData(n_filfle, 2, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_abr"].ToString();
                    FgDatos.SetData(n_filfle, 3, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_numdoc"].ToString();
                    FgDatos.SetData(n_filfle, 4, c_dato);
                    c_dato = dtResultado.Rows[n_row]["d_fchdoc"].ToString();
                    FgDatos.SetData(n_filfle, 5, c_dato);
                    c_dato = dtResultado.Rows[n_row]["d_fchven"].ToString();
                    FgDatos.SetData(n_filfle, 6, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_mondes"].ToString();
                    FgDatos.SetData(n_filfle, 7, c_dato);
                    c_dato = Convert.ToDouble(funFunciones.NulosN(dtResultado.Rows[n_row]["n_imptotven"])).ToString("0.00");
                    FgDatos.SetData(n_filfle, 8, c_dato);
                    c_dato = Convert.ToDouble(funFunciones.NulosN(dtResultado.Rows[n_row]["n_tc"])).ToString("0.000");
                    FgDatos.SetData(n_filfle, 9, c_dato);
                    c_dato = Convert.ToDouble(funFunciones.NulosN(dtResultado.Rows[n_row]["n_debe"])).ToString("0.00");
                    FgDatos.SetData(n_filfle, 10, c_dato);
                    c_dato = Convert.ToDouble(funFunciones.NulosN(dtResultado.Rows[n_row]["n_haber"])).ToString("0.00");
                    FgDatos.SetData(n_filfle, 11, c_dato);
                    c_dato = Convert.ToDouble(funFunciones.NulosN(dtResultado.Rows[n_row]["n_saldo"])).ToString("0.00");
                    FgDatos.SetData(n_filfle, 12, c_dato);

                    n_saldo = Convert.ToDouble(dtResultado.Rows[n_row]["n_saldo"]);

                    n_proimptot = n_proimptot + Convert.ToDouble(dtResultado.Rows[n_row]["n_imptotven"]);
                    n_prototdeb = n_prototdeb + Convert.ToDouble(dtResultado.Rows[n_row]["n_debe"]);
                    n_protothab = n_protothab + Convert.ToDouble(dtResultado.Rows[n_row]["n_haber"]);
                    n_prosal = n_prosal + Convert.ToDouble(dtResultado.Rows[n_row]["n_saldo"]);

                    if (n_newdoc == 0) 
                    {
                        n_filini = FgDatos.Rows.Count - 1;
                        n_newdoc = 1; 
                    }
                }
                if (dtResultado.Rows[n_row]["n_ord"].ToString() == "2")
                {
                    //FgDatos.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.None;
                    c_dato = dtResultado.Rows[n_row]["c_numreg"].ToString();
                    FgDatos.SetData(n_filfle, 1, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_deslib"].ToString();
                    FgDatos.SetData(n_filfle, 2, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_abr"].ToString();
                    FgDatos.SetData(n_filfle, 3, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_numdoc"].ToString();
                    FgDatos.SetData(n_filfle, 4, c_dato);
                    c_dato = dtResultado.Rows[n_row]["d_fchdoc"].ToString();
                    FgDatos.SetData(n_filfle, 5, c_dato);
                    c_dato = dtResultado.Rows[n_row]["d_fchven"].ToString();
                    FgDatos.SetData(n_filfle, 6, c_dato);
                    c_dato = dtResultado.Rows[n_row]["c_mondes"].ToString();
                    FgDatos.SetData(n_filfle, 7, c_dato);
                    c_dato = Convert.ToDouble(dtResultado.Rows[n_row]["n_imptotven"]).ToString("0.00");
                    FgDatos.SetData(n_filfle, 8, c_dato);
                    c_dato = Convert.ToDouble(dtResultado.Rows[n_row]["n_tc"]).ToString("0.000");
                    FgDatos.SetData(n_filfle, 9, c_dato);
                    c_dato = Convert.ToDouble(dtResultado.Rows[n_row]["n_debe"]).ToString("0.00");
                    FgDatos.SetData(n_filfle, 10, c_dato);
                    c_dato = Convert.ToDouble(dtResultado.Rows[n_row]["n_haber"]).ToString("0.00");
                    FgDatos.SetData(n_filfle, 11, c_dato);
                    n_saldo = (n_saldo - Convert.ToDouble(dtResultado.Rows[n_row]["n_debe"]));
                    c_dato = n_saldo.ToString("0.00");
                    FgDatos.SetData(n_filfle, 12, c_dato);

                    //n_proimptot = 0;
                    n_prototdeb = n_prototdeb + Convert.ToDouble(dtResultado.Rows[n_row]["n_debe"]);
                    n_protothab = n_protothab + Convert.ToDouble(dtResultado.Rows[n_row]["n_haber"]);
                    n_prosal = n_prosal - Convert.ToDouble(dtResultado.Rows[n_row]["n_debe"]);
                }
            }

            n_filfin = FgDatos.Rows.Count - 1;
            funFlex.Flex_PintarCeldas(FgDatos, n_filini, 1, n_filfin, 12, Color.Black, Color.Beige);

            n_graimptot = n_graimptot + n_proimptot;
            n_gratotdeb = n_gratotdeb + n_prototdeb;
            n_gratothab = n_gratothab + n_protothab;
            n_grasal = n_grasal + n_prosal;

            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            n_filfle = FgDatos.Rows.Count - 1;

            //FgDatos.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            funFlex.Flex_UniColumnas(FgDatos, n_filfle, 5, 7, "TOTAL PROVEEDOOR ==>", 1);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 5, n_filfle, 7, Color.Blue, Color.White);
            
            c_dato = n_proimptot.ToString("0.00");
            FgDatos.SetData(n_filfle, 8, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 8, n_filfle, 8, Color.Blue, Color.White);

            c_dato = n_prototdeb.ToString("0.00");
            FgDatos.SetData(n_filfle, 10, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 10, n_filfle, 10, Color.Blue, Color.White);

            c_dato = n_protothab.ToString("0.00");
            FgDatos.SetData(n_filfle, 11, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 11, n_filfle, 11, Color.Blue, Color.White);

            c_dato = n_prosal.ToString("0.00");
            FgDatos.SetData(n_filfle, 12, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 12, n_filfle, 12, Color.Blue, Color.White);

            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            n_filfle = FgDatos.Rows.Count - 1;

            funFlex.Flex_UniColumnas(FgDatos, n_filfle, 5, 7, "TOTAL ==>", 1);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 5, n_filfle, 7, Color.Blue, Color.White);

            c_dato = n_graimptot.ToString("0.00");
            FgDatos.SetData(n_filfle, 8, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 8, n_filfle, 8, Color.Blue, Color.White);

            c_dato = n_gratotdeb.ToString("0.00");
            FgDatos.SetData(n_filfle, 10, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 10, n_filfle, 10, Color.Blue, Color.White);

            c_dato = n_gratothab.ToString("0.00");
            FgDatos.SetData(n_filfle, 11, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 11, n_filfle, 11, Color.Blue, Color.White);

            c_dato = n_grasal.ToString("0.00");
            FgDatos.SetData(n_filfle, 12, c_dato);
            funFlex.Flex_PintarCeldas(FgDatos, n_filfle, 12, n_filfle, 12, Color.Blue, Color.White);

            //n_filfin = FgDatos.Rows.Count - 1;
            //funFlex.Flex_PintarCeldas(FgDatos, n_filini, 1, n_filfin, 12, Color.Black, Color.Beige);
        }
        string CadenaIN()
        {
            string c_cadin = "";
            int n_row = 1;
            string c_cad = "";

            for (n_row = 1; n_row <= FgPro.Rows.Count - 1; n_row++)
            {
                c_cad = funFunciones.NulosC(FgPro.GetData(n_row, 2));

                if (c_cad != "")
                {
                    if (n_row == 1)
                    {
                        if (c_cad != "")
                        {
                            c_cadin = c_cadin + c_cad;
                        }
                    }
                    else
                    {
                        if (c_cad != "")
                        {
                            c_cadin = c_cadin + ", " + c_cad;
                        }
                    }
                }
            }
            return c_cadin;
        }
         void SetearCabecera1()
        {
            //funFlex.Flex_UniColumnas(FgDatos, 0, 4, 5, "MONEDA NACIONAL", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 6, 7, "MONEDA EXTRANJERA", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 8, 10, "EXPRESADO EN MN", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 11, 13, "EXPRESADO EN ME", 1);
        }
        void SetearCabecera2()
        {
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 1, 9, "DATOS DEL DOCUMENTO", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 10, 12, "CARGOS Y ABONOS", 1);
        }

        private void TxtFchIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchFin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                funControl.dtpBlanquea(TxtFchFin);
            }
        }

        private void TxtFchIni_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                funControl.dtpBlanquea(TxtFchIni);
            }       
        }

        private void TxtFchFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }        
        }

        private void TxtFchIni_ValueChanged(object sender, EventArgs e)
        {
            TxtFchIni.CustomFormat = "dd/MM/yyyy";
            TxtFchIni.Format = DateTimePickerFormat.Custom;        
        }

        private void TxtFchFin_ValueChanged(object sender, EventArgs e)
        {
            TxtFchFin.CustomFormat = "dd/MM/yyyy";
            TxtFchFin.Format = DateTimePickerFormat.Custom;        
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_cad = "";
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-01-" + ".xls";
            if (OptRes.Checked == true) { c_cad = "RESUMEM X CLIENTES"; }
            if (OptDet.Checked == true) { c_cad = "DETALLADO X CLIENTE"; }
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "CUENTA CORRIENTE - PROVEEDORES", c_cad, c_nomarch);   
        }

        private void CmdAddPro_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgPro.GetData(FgPro.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado un proveedor en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgPro.Focus();
                return;
            }
            FgPro.Rows.Count = FgPro.Rows.Count + 1;        
        }

        private void CmdDelPro_Click(object sender, EventArgs e)
        {
            if (FgPro.Rows.Count == 1) { return; }
            FgPro.RemoveItem(FgPro.Row);        
        }

        private void FgPro_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgPro.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                objPro.mysConec = mysConec;
                dtResult = objPro.BuscarCliPro(dtProv, 1, "n_id", "");
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_nombre"].ToString();
                        FgPro.SetData(FgPro.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgPro.SetData(FgPro.Row, 2, c_dato);

                        FgPro.Rows.Count = FgPro.Rows.Count + 1;
                    }
                }
            }
        }

        private void OptRes_CheckedChanged(object sender, EventArgs e)
        {
            if (OptRes.Checked == true)
            {
                groupBox6.Enabled = false;
                FgPro.Rows.Count = 1;
                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
            }
        }

        private void OptDet_CheckedChanged(object sender, EventArgs e)
        {
            if (OptDet.Checked == true)
            {
                groupBox6.Enabled = true;
                OptTodDoc.Checked = true;
                FgPro.Cols[1].ComboList = "...";
                Cabecera2();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 3, false);
                SetearCabecera2();
            }
        }
        void ResumenMostrarTotales()
        {
            double n_total1 = 0;
            double n_total2 = 0;
            double n_total3 = 0;

            n_total1 = funFlex.FlexSumarCol(FgDatos, 3, 3, FgDatos.Rows.Count - 1);
            n_total2 = funFlex.FlexSumarCol(FgDatos, 4, 3, FgDatos.Rows.Count - 1);
            n_total3 = funFlex.FlexSumarCol(FgDatos, 5, 3, FgDatos.Rows.Count - 1);

            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 2, "TOTAL ==>");
            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 2, FgDatos.Rows.Count - 1, 5, Color.Blue, Color.Transparent);
     
            FgDatos.SetData(FgDatos.Rows.Count - 1, 3, n_total1.ToString("0.00"));
            FgDatos.SetData(FgDatos.Rows.Count - 1, 4, n_total2.ToString("0.00"));
            FgDatos.SetData(FgDatos.Rows.Count - 1, 5, n_total3.ToString("0.00"));
        }
    }
}
