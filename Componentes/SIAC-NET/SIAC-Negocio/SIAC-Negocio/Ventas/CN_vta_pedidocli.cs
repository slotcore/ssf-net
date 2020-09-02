using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Ventas;
using Helper;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_pedidocli
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        
        public DataTable dtLisPedPend = new DataTable();
        public DataTable dtLisTodPedidos = new DataTable();
        public DataTable dtLista = new DataTable();

        public BE_VTA_PEDIDOCLI entPedCab = new BE_VTA_PEDIDOCLI();
        public List<BE_VTA_PEDIDOCLIDET> lstPedDet = new List<BE_VTA_PEDIDOCLIDET>();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Genericas funDatos = new Genericas();

        public bool ListarPedidosPendientes(int n_IdEmpresa, int n_IdCliente)
        {
            bool b_result = false;

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;

            if (miFun.ListarPedidosPendientes(n_IdEmpresa, n_IdCliente) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                b_result = true;
                dtLisPedPend = miFun.dtLisPedPend;
            }

            return b_result;
        }
        public bool ListarPedidosPendientes(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;

            if (miFun.ListarPedidosPendientes(n_IdEmpresa) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                b_result = true;
                dtLisPedPend = miFun.dtLisPedPend;
            }

            return b_result;
        }
        public bool ListarTodoPedidos(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;

            b_result = miFun.ListarTodoPedidos(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtLisTodPedidos = miFun.dtLisTodPedidos;
            }
            return b_result;
        }
        // ********************
        // ****    CRUD    ****
        // ********************
        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable dtResul = new DataTable();

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_idempresa, n_idmes, n_idano);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booresult = false;
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            BE_VTA_PEDIDOCLI entreque = new BE_VTA_PEDIDOCLI();

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;
            lstPedDet.Clear();
            if (miFun.TraerRegistro(n_IdRegistro) == true) 
            {
                dtCab = miFun.dtCabecera;
                dtDet = miFun.dtDetalle;
               
                entPedCab.n_idemp = Convert.ToInt32(dtCab.Rows[0]["n_idemp"]);
                entPedCab.n_id = Convert.ToInt32(dtCab.Rows[0]["n_id"]);
                entPedCab.n_anotra = Convert.ToInt32(dtCab.Rows[0]["n_anotra"]);
                entPedCab.n_mestra = Convert.ToInt32(dtCab.Rows[0]["n_mestra"]);
                entPedCab.n_idcli = Convert.ToInt32(dtCab.Rows[0]["n_idcli"]);
                entPedCab.n_idpunven = Convert.ToInt32(dtCab.Rows[0]["n_idpunven"]);
                entPedCab.n_idtipodoc = Convert.ToInt32(dtCab.Rows[0]["n_idtipodoc"]);
                entPedCab.c_numser = Convert.ToString(dtCab.Rows[0]["c_numser"]);
                entPedCab.c_numdoc = Convert.ToString(dtCab.Rows[0]["c_numdoc"]);
                entPedCab.d_fchreg = Convert.ToDateTime(dtCab.Rows[0]["d_fchreg"]);
                entPedCab.d_fchped = Convert.ToDateTime(dtCab.Rows[0]["d_fchped"]);
                entPedCab.d_fchent = Convert.ToDateTime(dtCab.Rows[0]["d_fchent"]);
                entPedCab.n_idconpag = Convert.ToInt32(dtCab.Rows[0]["n_idconpag"]);
                entPedCab.n_idven = Convert.ToInt32(dtCab.Rows[0]["n_idven"]);
                entPedCab.c_obs = Convert.ToString(dtCab.Rows[0]["c_obs"]);
                entPedCab.n_pedidtipdoc = Convert.ToInt32(dtCab.Rows[0]["n_pedidtipdoc"]);
                entPedCab.c_pednumser = Convert.ToString(dtCab.Rows[0]["c_pednumser"]);
                entPedCab.c_pednumdoc = Convert.ToString(dtCab.Rows[0]["c_pednumdoc"]);
                entPedCab.n_impbru = Convert.ToDouble(dtCab.Rows[0]["n_impbru"]);
                entPedCab.n_impigv = Convert.ToDouble(dtCab.Rows[0]["n_impigv"]);
                entPedCab.n_imptot = Convert.ToDouble(dtCab.Rows[0]["n_imptot"]);
                entPedCab.n_numite = Convert.ToDouble(dtCab.Rows[0]["n_numite"]);
                entPedCab.n_mulent = Convert.ToInt32(dtCab.Rows[0]["n_mulent"]);
                entPedCab.n_idestent = Convert.ToInt32(dtCab.Rows[0]["n_idestent"]);
                entPedCab.n_idest = Convert.ToInt32(dtCab.Rows[0]["n_idest"]);
                entPedCab.n_idmon = Convert.ToInt32(xFun.NulosN(dtCab.Rows[0]["n_idmon"]));
                int n_row = 0;

                for (n_row = 0; n_row <= dtDet.Rows.Count - 1; n_row++)
                {
                    BE_VTA_PEDIDOCLIDET detped = new BE_VTA_PEDIDOCLIDET();

                    detped.n_idped = Convert.ToInt32(dtDet.Rows[n_row]["n_idped"]);
                    detped.n_idite = Convert.ToInt32(dtDet.Rows[n_row]["n_idite"]);
                    detped.n_idunimed = Convert.ToInt32(dtDet.Rows[n_row]["n_idunimed"]);
                    detped.n_can = Convert.ToDouble(dtDet.Rows[n_row]["n_can"]);
                    detped.n_impbru = Convert.ToDouble(dtDet.Rows[n_row]["n_impbru"]);
                    detped.n_impigv = Convert.ToDouble(dtDet.Rows[n_row]["n_impigv"]);
                    detped.n_imptot = Convert.ToDouble(dtDet.Rows[n_row]["n_imptot"]);
                    detped.d_fchent = Convert.ToDateTime(dtDet.Rows[n_row]["d_fchent"]);
                    detped.n_entregado = Convert.ToInt32(dtDet.Rows[n_row]["n_entregado"]);

                    lstPedDet.Add(detped);
                }
                booresult = true;
            }
            return booresult;
        }
        public bool Insertar(BE_VTA_PEDIDOCLI entPedido, List<BE_VTA_PEDIDOCLIDET> lstDetalle)
        {

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entPedido, lstDetalle);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_PEDIDOCLI entPedido, List<BE_VTA_PEDIDOCLIDET> lstDetalle)
        {
            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entPedido, lstDetalle);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool ActualizarEnvioProduccion(int n_IdPedido, int n_IdEstado)
        {
            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.ActualizarEnvioProduccion(n_IdPedido, n_IdEstado);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public DataTable PedidosConSaldo(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable dtResult = new DataTable();
            CN_vta_guias objGui = new CN_vta_guias();
            string[,] arrCabeceraFlexFil = new string[7, 5];

            objGui.mysConec = mysConec;
            if (ListarPedidosPendientes(n_IdEmpresa, n_IdCliente) == true)
            {
                dtResult = dtLisPedPend;
            }

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Documento";
            arrCabeceraFlexFil[0, 1] = "100";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_numdoc";

            arrCabeceraFlexFil[1, 0] = "Fecha Pedido";
            arrCabeceraFlexFil[1, 1] = "70";
            arrCabeceraFlexFil[1, 2] = "F";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "d_fchped";

            arrCabeceraFlexFil[2, 0] = "Fecha ENtrega";
            arrCabeceraFlexFil[2, 1] = "70";
            arrCabeceraFlexFil[2, 2] = "F";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "d_fchent";

            arrCabeceraFlexFil[3, 0] = "Condicion Pago";
            arrCabeceraFlexFil[3, 1] = "200";
            arrCabeceraFlexFil[3, 2] = "C";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "c_desconpag";
            
            arrCabeceraFlexFil[4, 0] = "Orden de Compra";
            arrCabeceraFlexFil[4, 1] = "150";
            arrCabeceraFlexFil[4, 2] = "C";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "c_numordcom";

            
            arrCabeceraFlexFil[5, 0] = "Sel";
            arrCabeceraFlexFil[5, 1] = "40";
            arrCabeceraFlexFil[5, 2] = "B";
            arrCabeceraFlexFil[5, 3] = "";
            arrCabeceraFlexFil[5, 4] = "n_sel";

            arrCabeceraFlexFil[6, 0] = "n_id";
            arrCabeceraFlexFil[6, 1] = "0";
            arrCabeceraFlexFil[6, 2] = "C";
            arrCabeceraFlexFil[6, 3] = "";
            arrCabeceraFlexFil[6, 4] = "n_id";

            funDatos.Filtrar_CampoOrden = "c_numdoc";
            funDatos.Filtrar_Titulo = "Pedidos pendientes de entrega";
            funDatos.Filtrar_ColumnaCheck = 6;
            funDatos.Filtrar_ColumnaBusqueda = 7;
            funDatos.Filtrar_CampoBusqueda = "n_id";
            dtResult = funDatos.Filtrar(arrCabeceraFlexFil, dtResult);

            return dtResult;
        }
        public void MostrarEntregas(int n_IdPedidoCliente)
        {
            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;

            miFun.MostrarEntregas(n_IdPedidoCliente);

            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                dtLista = null;
                return;
            }
            dtLista = miFun.dtLista;
            return;
        }
        public void SeleccionarEntregas(int n_IdPedidoCliente, bool b_Seleccionar)
        {
            DataTable dtResult = new DataTable();
            CN_vta_guias objGui = new CN_vta_guias();
            string[,] arrCabeceraFlexFil = new string[8, 5];
            string n_ancho = "0";
            objGui.mysConec = mysConec;

            MostrarEntregas(n_IdPedidoCliente);

            n_ancho = "40";
            if (b_Seleccionar == false)  { n_ancho = "0"; }

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Cod. Producto";
            arrCabeceraFlexFil[0, 1] = "80";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_codpro";

            arrCabeceraFlexFil[1, 0] = "Descripcion";
            arrCabeceraFlexFil[1, 1] = "200";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "c_despro";

            arrCabeceraFlexFil[2, 0] = "Uni. Med.";
            arrCabeceraFlexFil[2, 1] = "40";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_abrpre";

            arrCabeceraFlexFil[3, 0] = "Cantidad Solicitada";
            arrCabeceraFlexFil[3, 1] = "70";
            arrCabeceraFlexFil[3, 2] = "D";
            arrCabeceraFlexFil[3, 3] = "0.00";
            arrCabeceraFlexFil[3, 4] = "n_can";
            
            arrCabeceraFlexFil[4, 0] = "Cantidad Entregada";
            arrCabeceraFlexFil[4, 1] = "70";
            arrCabeceraFlexFil[4, 2] = "D";
            arrCabeceraFlexFil[4, 3] = "0.00";
            arrCabeceraFlexFil[4, 4] = "n_canent";

            arrCabeceraFlexFil[5, 0] = "Saldo";
            arrCabeceraFlexFil[5, 1] = "70";
            arrCabeceraFlexFil[5, 2] = "D";
            arrCabeceraFlexFil[5, 3] = "0.00";
            arrCabeceraFlexFil[5, 4] = "n_saldo";
            
            arrCabeceraFlexFil[6, 0] = "Sel";
            arrCabeceraFlexFil[6, 1] = n_ancho;
            arrCabeceraFlexFil[6, 2] = "B";
            arrCabeceraFlexFil[6, 3] = "";
            arrCabeceraFlexFil[6, 4] = "n_sel";

            arrCabeceraFlexFil[7, 0] = "n_idItem";
            arrCabeceraFlexFil[7, 1] = "0";
            arrCabeceraFlexFil[7, 2] = "N";
            arrCabeceraFlexFil[7, 3] = "";
            arrCabeceraFlexFil[7, 4] = "n_idite";

            funDatos.Filtrar_CampoOrden = "c_despro";
            funDatos.Filtrar_Titulo = "Entregas del Pedido";
            funDatos.Filtrar_ColumnaCheck = 7;
            funDatos.Filtrar_ColumnaBusqueda = 8;
            funDatos.Filtrar_CampoBusqueda = "n_idite";
            funDatos.Filtrar_AplicarFiltro = false;
            dtResult = null;
            dtResult = funDatos.Filtrar(arrCabeceraFlexFil, dtLista);

            dtLista = dtResult;
            return;
        }
        public void ImprimirDocumento(double n_idPedido, int n_Tipo)
        {
            string c_Archivo = "";
            string c_Ruta = "";
            bool b_Exportar = false;
            string[,] arrPara = new string[1, 3];
            string[,] arrPara2 = new string[2, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idPedido.ToString();

            if (n_Tipo == 1)
            { 
                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\Rpt_PedidoCliente.rpt";
            }
            if (n_Tipo == 3)
            {
                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\Rpt_PedidoCliente_B.rpt";
            }

            if (n_Tipo == 2)
            {
                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\Rpt_PedidoClienteOrdPro.rpt";
            }
            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PEDIDOS - ORDEN DE COMPRA DEL CLIENTE";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = b_Exportar;
            xVisor.c_NombreArchivoExportar = c_Archivo;
            xVisor.VerCrystal();
        }
        public void PedidosCliente(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable dtResul = new DataTable();

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;

            miFun.PedidosCliente(n_IdEmpresa, n_IdCliente);

            if (miFun.booOcurrioError != true)
            {
                dtLista = miFun.dtLista;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public void SeleccionarPedidosCliente(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable dtResult = new DataTable();
            CD_vta_pedidocli o_Pedido = new CD_vta_pedidocli();
            string[,] arrCabeceraFlexFil = new string[5, 5];
            string n_ancho = "0";
            o_Pedido.mysConec = mysConec;
            o_Pedido.PedidosCliente(n_IdEmpresa, n_IdCliente);
            dtResult = o_Pedido.dtLista;

            n_ancho = "40";
            //if (b_Seleccionar == false) { n_ancho = "0"; }

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Pedido";
            arrCabeceraFlexFil[0, 1] = "100";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_pednumdoc";

            arrCabeceraFlexFil[1, 0] = "Fch. Pedido";
            arrCabeceraFlexFil[1, 1] = "80";
            arrCabeceraFlexFil[1, 2] = "F";
            arrCabeceraFlexFil[1, 3] = "dd/MM/yyyy";
            arrCabeceraFlexFil[1, 4] = "d_fchped";

            arrCabeceraFlexFil[2, 0] = "Condicion Pago";
            arrCabeceraFlexFil[2, 1] = "200";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_des";

            arrCabeceraFlexFil[3, 0] = "Sel";
            arrCabeceraFlexFil[3, 1] = n_ancho;
            arrCabeceraFlexFil[3, 2] = "B";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "n_sel";

            arrCabeceraFlexFil[4, 0] = "Id";
            arrCabeceraFlexFil[4, 1] = "0";
            arrCabeceraFlexFil[4, 2] = "N";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "n_id";

            funDatos.Filtrar_CampoOrden = "c_pednumdoc";
            funDatos.Filtrar_Titulo = "Pedidos del Cliente";
            funDatos.Filtrar_ColumnaCheck = 4;
            funDatos.Filtrar_ColumnaBusqueda = 5;
            funDatos.Filtrar_CampoBusqueda = "n_id";
            funDatos.Filtrar_AplicarFiltro = false;
            //dtResult = null;
            dtResult = funDatos.Filtrar(arrCabeceraFlexFil, dtResult);

            dtLista = dtResult;
            return;
        }
        public void CtaCtePedidos(int n_IdEmpresa, string c_FechaInicio, string c_FchFInal, int n_Tipo)
        {
            DataTable dtResul = new DataTable();

            CD_vta_pedidocli miFun = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;

            miFun.CtaCtePedidos(n_IdEmpresa, c_FechaInicio, c_FchFInal, n_Tipo);

            if (miFun.booOcurrioError != true)
            {
                dtLista = miFun.dtLista;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        //public DataTable BuscarPedidos(int n_IdEmpresa, int n_IdCliente, string c_DocumentosCargados)
        //{
        //    Helper.Genericas funDatos = new Helper.Genericas();
        //    string[,] arrCabeceraDg1 = new string[5, 4];
        //    DataTable dtResult = new DataTable();

        //    Consulta1(n_IdEmpresa, n_IdCliente);
        //    dtResult = dtLista;
        //    if (n_IdCliente != 0)
        //    {
        //        dtResult = funDatos.DataTableFiltrar(dtResult, "n_idcli = " + n_IdCliente.ToString() + "");  // FILTRAMOS LOS DOCUMENTOS DEL PROVEEDOR
        //    }
        //    if (c_DocumentosCargados != "")
        //    {
        //        dtResult = funDatos.DataTableFiltrar(dtResult, "n_id NOT IN (" + c_DocumentosCargados + ")");         // QUITAMOS LOS DOCUMENTOS QUE YA FUERON AGREGADOS
        //    }

        //    arrCabeceraDg1[0, 0] = "Cliente";
        //    arrCabeceraDg1[0, 1] = "200";
        //    arrCabeceraDg1[0, 2] = "C";
        //    arrCabeceraDg1[0, 3] = "c_nombre";

        //    arrCabeceraDg1[1, 0] = "Tip. Doc.";
        //    arrCabeceraDg1[1, 1] = "40";
        //    arrCabeceraDg1[1, 2] = "C";
        //    arrCabeceraDg1[1, 3] = "c_abr";

        //    arrCabeceraDg1[2, 0] = "Fch. Documento";
        //    arrCabeceraDg1[2, 1] = "80";
        //    arrCabeceraDg1[2, 2] = "F";
        //    arrCabeceraDg1[2, 3] = "d_fchdoc";

        //    arrCabeceraDg1[3, 0] = "Nº Documento";
        //    arrCabeceraDg1[3, 1] = "120";
        //    arrCabeceraDg1[3, 2] = "F";
        //    arrCabeceraDg1[3, 3] = "c_numdoc";

        //    arrCabeceraDg1[4, 0] = "Id";
        //    arrCabeceraDg1[4, 1] = "0";
        //    arrCabeceraDg1[4, 2] = "N";
        //    arrCabeceraDg1[4, 3] = "n_id";

        //    Genericas xFun = new Genericas();
        //    xFun.Buscar_CampoBusqueda = "n_id";
        //    xFun.Buscar_CadFiltro = "";
        //    dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

        //    return dtResult;
        //}
    }
}
