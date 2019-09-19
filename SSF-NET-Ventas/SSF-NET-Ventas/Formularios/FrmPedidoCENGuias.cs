using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
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
using System.Xml;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmPedidoCENGuias : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        string[,] arrCabeceraFlex1 = new string[11, 5];
        bool b_agregando = false;

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        CN_vta_chofer objCho = new CN_vta_chofer();
        CN_vta_vehiculo objVeh = new CN_vta_vehiculo();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_vta_emptra objEmtra = new CN_vta_emptra();
        CN_vta_punvencli objPunVen = new CN_vta_punvencli();
        CN_sys_empresalocal objEmlLoc = new CN_sys_empresalocal();
        CN_vta_pedidocen objPedCEN = new CN_vta_pedidocen();

        DataTable dtCho = new DataTable();
        DataTable dtVeh = new DataTable();
        DataTable dtEmpTra = new DataTable();
        DataTable dtPunVen = new DataTable();
        DataTable dtPunPar = new DataTable();

        public FrmPedidoCENGuias()
        {
            InitializeComponent();
        }

        private void FrmPedidoCENGuias_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            DataTableCargar();
            MostrarPedidos();
        }
        void ConfigurarFormulario()
        {
            this.Height = 496;
            this.Width = 988;

            b_agregando = true;
            DataTable dtItem = new DataTable();
            this.Text = "VENTAS - Generacion Guias de Remision a Pedidos CEN ";

            TxtPunVen.Text = "";
            TxtPunEnt.Text = "";

            arrCabeceraFlex1[0, 0] = "Nº Pedido";
            arrCabeceraFlex1[0, 1] = "140";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Fch. Emision";
            arrCabeceraFlex1[1, 1] = "68";
            arrCabeceraFlex1[1, 2] = "F";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Fch. Entrega";
            arrCabeceraFlex1[2, 1] = "68";
            arrCabeceraFlex1[2, 2] = "F";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Punto de Venta";
            arrCabeceraFlex1[3, 1] = "130";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            arrCabeceraFlex1[4, 0] = "Punto Entrega";
            arrCabeceraFlex1[4, 1] = "130";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "Empresa Transporte";
            arrCabeceraFlex1[5, 1] = "180";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "n_can";

            arrCabeceraFlex1[6, 0] = "Chofer";
            arrCabeceraFlex1[6, 1] = "120";
            arrCabeceraFlex1[6, 2] = "C";
            arrCabeceraFlex1[6, 3] = "";
            arrCabeceraFlex1[6, 4] = "n_can";

            arrCabeceraFlex1[7, 0] = "Vehiculo";
            arrCabeceraFlex1[7, 1] = "60";
            arrCabeceraFlex1[7, 2] = "C";
            arrCabeceraFlex1[7, 3] = "";
            arrCabeceraFlex1[7, 4] = "n_can";

            arrCabeceraFlex1[8, 0] = "Guia";
            arrCabeceraFlex1[8, 1] = "40";
            arrCabeceraFlex1[8, 2] = "B";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "n_can";

            arrCabeceraFlex1[9, 0] = "Id";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "n_can";

            arrCabeceraFlex1[10, 0] = "IdCliente";
            arrCabeceraFlex1[10, 1] = "0";
            arrCabeceraFlex1[10, 2] = "N";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtItem, 2, false);
            b_agregando = false;
        }
        void MostrarPedidos()
        {
            int n_row = 0;
            CN_vta_pedidocen miFun = new CN_vta_pedidocen();
            DataTable dtLista = new DataTable();
            miFun.mysConec = mysConec;
            b_agregando = true;
            miFun.TraerPendienteEnvio(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
            dtLista = miFun.dtLista;

            if (dtLista.Rows.Count == 0)
            {
                MessageBox.Show("¡ No hay pedidos pendiente, no se puede emitir guias !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            FgItems.Rows.Count = 2;
            for (n_row = 0; n_row <= dtLista.Rows.Count - 1; n_row++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                FgItems.SetData(FgItems.Rows.Count -1, 1, dtLista.Rows[n_row]["c_numped"].ToString());
                FgItems.SetData(FgItems.Rows.Count -1, 2, dtLista.Rows[n_row]["d_fchemi"].ToString());
                FgItems.SetData(FgItems.Rows.Count -1, 3, dtLista.Rows[n_row]["d_fchent"].ToString());
                FgItems.SetData(FgItems.Rows.Count -1, 4, dtLista.Rows[n_row]["c_punvendes"].ToString());
                FgItems.SetData(FgItems.Rows.Count -1, 5, dtLista.Rows[n_row]["c_punentdes"].ToString());
                FgItems.SetData(FgItems.Rows.Count - 1, 10, dtLista.Rows[n_row]["n_id"].ToString());
                FgItems.SetData(FgItems.Rows.Count - 1, 11, dtLista.Rows[n_row]["n_idcli"].ToString());

            }
            TxtPunVen.Text = FgItems.GetData(FgItems.Row, 4).ToString();
            TxtPunEnt.Text = FgItems.GetData(FgItems.Row, 5).ToString();
            b_agregando = false;
        }
        void DataTableCargar()
        {
            objCho.mysConec = mysConec;
            dtCho = objCho.Listar(STU_SISTEMA.EMPRESAID);

            objVeh.mysConec = mysConec;
            dtVeh = objVeh.Listar(STU_SISTEMA.EMPRESAID);

            objEmtra.mysConec = mysConec;
            dtEmpTra = objEmtra.Listar(STU_SISTEMA.EMPRESAID);

            objPunVen.mysConec = mysConec;
            dtPunVen = objPunVen.Listar();

            funFlex.FlexColumnaCombo(FgItems, dtEmpTra, "c_nombre", 6);
            funFlex.FlexColumnaCombo(FgItems, dtCho, "c_nomcho", 7);
            funFlex.FlexColumnaCombo(FgItems, dtVeh, "c_numpla", 8);

            objEmlLoc.mysConec = mysConec;
            dtPunPar = objEmlLoc.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);   

            funDatos.ComboBoxCargarDataTable(CboPunPar, dtPunPar, "n_id", "c_des");
            //CboPunPar.SelectedValue()
        }

        private void CmdSal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmdGen_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboPunPar.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el punto de partida de las guias !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            BE_VTA_GUIAS eGuiaCab = new BE_VTA_GUIAS();
            List<BE_VTA_GUIASDET> lGuiaDet = new List<BE_VTA_GUIASDET>();
            List<BE_VTA_GUIASDOC> lstGuiasDoc = new List<BE_VTA_GUIASDOC>();
            CN_vta_guias miFunGui = new CN_vta_guias();
            
            DataTable dtItem = new DataTable();  // AQUI CARGAMOS LOS ITEMS DE LOS PEDIDOS

            objTipDoc.mysConec = mysConec;
            miFunGui.mysConec = mysConec;
            objPedCEN.mysConec = mysConec;

            int n_row = 0;
            string c_dato = "";
            string c_cadin = "";
            int n_numdoc = 0;

            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgItems.GetData(n_row, 9)) == "True")
                {
                    c_dato = FgItems.GetData(n_row, 1).ToString();

                    if (funFunciones.NulosC(FgItems.GetData(n_row, 4)) == "")
                    {
                        MessageBox.Show("¡ No ha indicado el punto de venta, para el pedido Nº: " + c_dato + ", vaya a la opcion punto de venta del cliente y asigene este nuevo punto de venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (funFunciones.NulosC(FgItems.GetData(n_row, 5)) == "")
                    {
                        MessageBox.Show("¡ No ha indicado el punto de entrega, para el pedido Nº: " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (funFunciones.NulosC(FgItems.GetData(n_row, 6)) == "")
                    {
                        MessageBox.Show("¡ No ha indicado el nombre del chofer, para el pedido Nº: " + c_dato  + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (funFunciones.NulosC(FgItems.GetData(n_row, 7)) == "")
                    {
                        MessageBox.Show("¡ No ha indicado la unidad de transporte, para el pedido Nº: " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
            }

   
            // PREPARAMOS LA CADNA IN PARA TRAER LOS ITEMS DE LOS PEDIDOS
            c_cadin = "";
            n_numdoc = 0;
            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            { 
                if (funFunciones.NulosC(FgItems.GetData(n_row, 9)) == "True")
                {
                    n_numdoc = n_numdoc + 1;
                    if (n_numdoc == 1) 
                    { 
                        c_cadin = c_cadin + FgItems.GetData(n_row, 10).ToString();
                    }
                    else
                    {
                        c_cadin = c_cadin + ", " + FgItems.GetData(n_row, 10).ToString();
                    }
                    
                }
            }

            objPedCEN.mysConec = mysConec;
            objPedCEN.TraerDetallePedidos(STU_SISTEMA.EMPRESAID, c_cadin);
            dtItem = objPedCEN.dtLista;

            for (n_row = 0; n_row <= dtItem.Rows.Count - 1; n_row++)
            {
                if (Convert.ToInt32(funFunciones.NulosN(dtItem.Rows[n_row]["n_iditem"])) == 0)
                {
                    MessageBox.Show("¡ El item con el codigo Nº " + dtItem.Rows[n_row]["c_coditecen"].ToString()  + " del pedido Nº: " + c_dato + " no esta vinculado a un item del sistema, vaya a la opcion maestro productos CEN en el menu ventas y asignele un item del sistema !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            
            int n_idcliente = 0;
            int n_idpunvencli = 0;
            string c_despunlle = "";
            string c_numdoc = "";
            int n_idchofer = 0;
            int n_idemptra = 0;
            int n_idvehiculo =0;
            string c_punpar = "";
            int n_idpedido = 0;
            string c_numpedido ="";
            string c_fchPed = "";
            string c_FchEnt = "";
            int n_idpunpar  = 0;
            int n_idpunlle = 0;
            
            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgItems.GetData(n_row, 9)) == "True")
                {
                    n_idcliente = Convert.ToInt32(FgItems.GetData(n_row, 11).ToString());
                    
                    c_dato = FgItems.GetData(n_row, 4).ToString();
                    c_dato = funDatos.DataTableBuscar(dtPunVen, "c_des", "n_id", c_dato, "C").ToString();
                    n_idpunvencli = Convert.ToInt32(c_dato);
                    
                    c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 10, "0001");

                    c_dato = FgItems.GetData(n_row, 7).ToString();
                    c_dato = funDatos.DataTableBuscar(dtCho, "c_nomcho", "n_id", c_dato, "C").ToString();
                    n_idchofer = Convert.ToInt32(c_dato);

                    c_dato = FgItems.GetData(n_row, 6).ToString();
                    c_dato = funDatos.DataTableBuscar(dtEmpTra, "c_nombre", "n_id", c_dato, "C").ToString();
                    n_idemptra = Convert.ToInt32(c_dato);

                    c_dato = FgItems.GetData(n_row, 8).ToString();
                    c_dato = funDatos.DataTableBuscar(dtVeh, "c_numpla", "n_id", c_dato, "C").ToString();
                    n_idvehiculo = Convert.ToInt32(c_dato);

                    c_dato = FgItems.GetData(n_row, 10).ToString();
                    n_idpedido = Convert.ToInt32(c_dato);

                    c_numpedido = FgItems.GetData(n_row, 1).ToString();

                    c_fchPed = FgItems.GetData(n_row, 2).ToString();
                    c_FchEnt = FgItems.GetData(n_row, 3).ToString();

                    n_idpunpar = Convert.ToInt16(CboPunPar.SelectedValue);
                    c_dato = funDatos.DataTableBuscar(dtPunPar, "n_id", "c_dir", n_idpunpar.ToString(), "N").ToString();
                    c_punpar = c_dato;

                    c_dato = FgItems.GetData(n_row, 5).ToString();
                    c_dato = funDatos.DataTableBuscar(dtPunVen, "c_des", "n_id", c_dato, "C").ToString();
                    n_idpunlle = Convert.ToInt32(c_dato);
                    c_dato = funDatos.DataTableBuscar(dtPunVen, "n_id", "c_dir", n_idpunlle.ToString(), "C").ToString();
                    c_despunlle = c_dato;

                    eGuiaCab.n_idemp = STU_SISTEMA.EMPRESAID;
                    eGuiaCab.n_id = 0;
                    eGuiaCab.n_idtipdoc = 10;
                    eGuiaCab.n_idcli = n_idcliente;
                    eGuiaCab.n_idpunvencli = n_idpunvencli;
                    eGuiaCab.c_dirpunlle = c_despunlle;
                    eGuiaCab.c_numser = "0001";
                    eGuiaCab.c_numdoc = c_numdoc;
                    eGuiaCab.d_fchdoc = DateTime.Now;
                    eGuiaCab.n_idmottra = 1;
                    eGuiaCab.n_idcho = n_idchofer;
                    eGuiaCab.n_idemptra = n_idemptra;
                    eGuiaCab.n_idvehtra = n_idvehiculo;
                    eGuiaCab.c_dirpunpar = c_punpar;
                    eGuiaCab.n_idtipdocref = 79;           // LE INDICAMOS QUE EL TIPO DE DOCUMENTO DE REFERENCIA EL EL PEDIDO CEN DE CLIENTE
                    eGuiaCab.n_iddocref = n_idpedido;
                    eGuiaCab.c_numdocref = c_numpedido;
                    eGuiaCab.d_fchpeddocref = Convert.ToDateTime(c_fchPed);
                    eGuiaCab.d_fchentdocref = Convert.ToDateTime(c_FchEnt);
                    eGuiaCab.n_anulado = 1;
                    eGuiaCab.c_numordcom = c_numpedido;
                    eGuiaCab.n_tipgui = 1;
                    eGuiaCab.n_idpunpar = n_idpunpar;
                    eGuiaCab.n_idpunlle = n_idpunlle;
                    eGuiaCab.n_idmes = STU_SISTEMA.MESTRABAJO;
                    eGuiaCab.n_idano = STU_SISTEMA.ANOTRABAJO;
                    eGuiaCab.n_chkalmsal = 1;
                    eGuiaCab.n_chkalming = 1;
                    //eGuiaCab.n_iddocven = 0;
                    eGuiaCab.n_tipori = 1;

                    // AGREGAMOS EL DETALLE DE LA GUIA
                    int n_fil = 0;
                    lGuiaDet.Clear();
                    for (n_fil = 0; n_fil <=dtItem.Rows.Count-1; n_fil++)
                    {
                        if (Convert.ToInt32(dtItem.Rows[n_fil]["n_idped"]) == n_idpedido)
                        {
                            BE_VTA_GUIASDET eDetalle = new BE_VTA_GUIASDET();

                            eDetalle.n_idgui = 0;
                            eDetalle.n_idite =Convert.ToInt32( dtItem.Rows[n_fil]["n_iditem"]);
                            eDetalle.n_idunimed = Convert.ToInt32(dtItem.Rows[n_fil]["n_idunimed"]);
                            eDetalle.n_canpro = Convert.ToDouble(dtItem.Rows[n_fil]["n_canpro"]);
                            eDetalle.d_fchpro = null;
                            eDetalle.d_fchven = null;
                            eDetalle.c_numlot = "";
                            eDetalle.n_idtipexi = 2;
                            eDetalle.n_iddocref = 0;

                            lGuiaDet.Add(eDetalle);
                        }
                    }

                    // AGREGAMOS LOS DOCUMENTOS ADJUNTOS DE LA GUIA
                    n_fil = 0;
                    lstGuiasDoc.Clear();
                    BE_VTA_GUIASDOC e_GuiaDoc = new BE_VTA_GUIASDOC();
                    BE_VTA_GUIASDATOS e_GuiaDatos = new BE_VTA_GUIASDATOS();

                    e_GuiaDoc.n_idgui = 0;
                    e_GuiaDoc.n_idtipdoc = 79;
                    //e_GuiaDoc.c_numdoc = eGuiaCab.c_numser + "-" + eGuiaCab.c_numdoc;
                    e_GuiaDoc.c_numdoc = "";
                    e_GuiaDoc.n_iddoc = n_idpedido;

                    lstGuiasDoc.Add(e_GuiaDoc);


                    miFunGui.LstDetalle = lGuiaDet;
                    if (miFunGui.Insertar(eGuiaCab, lstGuiasDoc, e_GuiaDatos) == true)
                    {
                        c_numpedido = FgItems.GetData(n_row, 1).ToString();
                        // ACTUALIZAMOS EL ID DE LA GUIA DE DESPACHO EN EL PEDIDO CEN
                        if (objPedCEN.ActualizarGuiaDespacho(n_idpedido, Convert.ToInt32(miFunGui.n_IdGenerado), 2) == false) 
                        {
                            MessageBox.Show("¡ No se pudo actualizar la guia de desapacho para el pedido Nº " + c_numpedido + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                    }
                }
            }
            MessageBox.Show("¡ Los pedidos se despacharon con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            MostrarPedidos();
        }

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (b_agregando == true) { return; }
            if ((FgItems.Col == 6) || (FgItems.Col == 7) || (FgItems.Col == 8))
            {
                if (funFunciones.NulosC(FgItems.GetData(FgItems.Row, 9)) == "True")
                {
                    FgItems.AllowEditing = true;
                }
                else 
                {
                    FgItems.AllowEditing = false;
                }
            }
            else
            {
                FgItems.AllowEditing = false;
            }

            if (FgItems.Col == 9)
            {
                FgItems.AllowEditing = true;
            }
        }

        private void FgItems_RowColChange(object sender, EventArgs e)
        {
            if (b_agregando == true) { return; }
            TxtPunVen.Text = FgItems.GetData(FgItems.Row,4).ToString();
            TxtPunEnt.Text = FgItems.GetData(FgItems.Row, 5).ToString();
        }
    }
}
