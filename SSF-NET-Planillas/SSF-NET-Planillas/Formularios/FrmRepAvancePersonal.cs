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
using SIAC_Negocio.Planilla;
using SIAC_Datos.Models.Planillas;
using SIAC_DATOS.Models.Produccion;
using SIAC_Datos.Classes;
using System.Diagnostics;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmRepAvancePersonal : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        ObservableListSource<Empleado> empleados = new ObservableListSource<Empleado>();
        Producto producto = null;
        DataTable tabla = null;

        public FrmRepAvancePersonal()
        {
            InitializeComponent();

            empleadoBindingSource.DataSource = empleados;
        }

        private void BtnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                string[,] arrCabeceraFlexFil = new string[4, 5];
                DataTable dtResult = new DataTable();
                string c_cadIN = string.Empty;
                Genericas funDatos = new Genericas();

                CN_pla_empleados objEmpleado = new CN_pla_empleados(STU_SISTEMA);
                objEmpleado.STU_SISTEMA = STU_SISTEMA;
                objEmpleado.Consulta1(STU_SISTEMA.EMPRESAID, c_cadIN);
                dtResult = objEmpleado.dtLista;
                objEmpleado = null;

                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Apellidos y Nombres";
                arrCabeceraFlexFil[0, 1] = "400";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "c_apenom";

                arrCabeceraFlexFil[1, 0] = "Nº DNI";
                arrCabeceraFlexFil[1, 1] = "80";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "c_numdocide";

                arrCabeceraFlexFil[2, 0] = "Sel.";
                arrCabeceraFlexFil[2, 1] = "40";
                arrCabeceraFlexFil[2, 2] = "B";
                arrCabeceraFlexFil[2, 3] = "n_sel";

                arrCabeceraFlexFil[3, 0] = "ID";
                arrCabeceraFlexFil[3, 1] = "0";
                arrCabeceraFlexFil[3, 2] = "N";
                arrCabeceraFlexFil[3, 3] = "n_id";

                funDatos.Filtrar_CampoOrden = "c_apenom";
                funDatos.Filtrar_Titulo = "Filtro de Trabajadores";
                funDatos.Filtrar_ColumnaCheck = 3;
                dtResult = funDatos.Filtrar2(arrCabeceraFlexFil, dtResult);

                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        for (int n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                        {
                            int n_id = Convert.ToInt32(dtResult.Rows[n_row]["n_id"]);
                            Empleado empleado = Empleado.Fetch(n_id);
                            empleados.Add(empleado);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error: {0}", ex.Message), "Agregar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string[,] arrCabeceraDg1 = new string[3, 4];
                DataTable dtResult = new DataTable();
                CN_alm_inventario objItems = new CN_alm_inventario();
                Genericas funDatos = new Genericas();

                objItems.mysConec = mysConec;
                dtResult = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

                dtResult = funDatos.DataTableFiltrar(dtResult, "n_idtipexi = 2");

                arrCabeceraDg1[0, 0] = "Producto";
                arrCabeceraDg1[0, 1] = "500";
                arrCabeceraDg1[0, 2] = "C";
                arrCabeceraDg1[0, 3] = "c_despro";

                arrCabeceraDg1[1, 0] = "Uni. Med.";
                arrCabeceraDg1[1, 1] = "60";
                arrCabeceraDg1[1, 2] = "C";
                arrCabeceraDg1[1, 3] = "c_abrpre";

                arrCabeceraDg1[2, 0] = "n_id";
                arrCabeceraDg1[2, 1] = "0";
                arrCabeceraDg1[2, 2] = "C";
                arrCabeceraDg1[2, 3] = "n_id";

                Genericas xFun = new Genericas();
                xFun.Buscar_CampoBusqueda = "n_id";
                xFun.Buscar_CadFiltro = "";
                xFun.Buscar_CampoOrden = "c_despro";
                dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

                if (dtResult == null) { return; }
                if (dtResult.Rows.Count == 0) { return; }

                int n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                TxtProducto.Text = dtResult.Rows[0]["c_despro"].ToString();

                producto = Producto.Fetch(n_id);
                ProductoReceta productoReceta = producto.ProductoRecetas.Where(p => p.n_act == 1).FirstOrDefault();
                ProductoRecetaLinea productoRecetaLinea = productoReceta.ProductoRecetaLineas.Where(p => p.n_act == 1).FirstOrDefault();

                productoRecetaLineaTareaBindingSource.DataSource = productoRecetaLinea.ProductoRecetaLineaTareas;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error: {0}", ex.Message), "Buscar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Convertir funCon = new Convertir();
            Funciones funFunciones = new Funciones();

            try
            {
                //Se traen los datos
                var productoRecetaLineaTarea = (ProductoRecetaLineaTarea)productoRecetaLineaTareaBindingSource.Current;
                if (productoRecetaLineaTarea == null)
                {
                    throw new Exception("No se selecciono tarea");
                }

                //Listado de empleados
                string c_idtra_in = string.Empty;
                var idsEmpleadosSeleccionados = empleados.Select(o => o.n_id).Distinct();
                if (idsEmpleadosSeleccionados.Count() > 0)
                {
                    int indice = 1;
                    foreach (var idEmpleado in idsEmpleadosSeleccionados)
                    {
                        if (indice == 1)
                        {
                            c_idtra_in = idEmpleado.ToString();
                        }
                        else
                        {
                            c_idtra_in = string.Format("{0}, {1}", c_idtra_in, idEmpleado);
                        }
                        indice++;
                    }

                }

                DgvResultado.DataSource = null;
                var listaEmpleadoAvance = Empleado.FetchEmpleadosAvance(producto.n_id
                    , STU_SISTEMA.EMPRESAID
                    , c_idtra_in
                    , productoRecetaLineaTarea.n_idtar
                    , DtpFechaInicio.Value
                    , DtpFechaFin.Value);

                if (listaEmpleadoAvance.Count == 0)
                {
                    throw new Exception("No se encontraron resultados");
                }

                //Se crea la tabla
                tabla = new DataTable();
                DataColumn column;
                DataRow row;

                column = new DataColumn
                {
                    DataType = Type.GetType("System.String"),
                    ColumnName = "Empleado"
                };
                tabla.Columns.Add(column);

                int numeroDias = Convert.ToInt32((DtpFechaFin.Value - DtpFechaInicio.Value).TotalDays) + 1;
                DateTime fechaInicio = DtpFechaInicio.Value;
                for (int i = 1; i <= numeroDias; i++)
                {
                    column = new DataColumn
                    {
                        DataType = Type.GetType("System.Double"),
                        ColumnName = fechaInicio.ToString("dd/MM")
                    };
                    tabla.Columns.Add(column);
                    fechaInicio = fechaInicio.AddDays(1);
                }

                //Se obtiene el listado de Ids de personal
                var listaIdsPersonal = listaEmpleadoAvance.Select(o => o.n_idper).Distinct();

                foreach (var idPer in listaIdsPersonal)
                {
                    var empleadoAvancePer = listaEmpleadoAvance.Where(o => o.n_idper == idPer).ToList();

                    row = tabla.NewRow();
                    row["Empleado"] = empleadoAvancePer.FirstOrDefault().c_apenom;

                    fechaInicio = DtpFechaInicio.Value;
                    for (int i = 1; i <= numeroDias; i++)
                    {
                        // Se busca registro de la fecha
                        var empleadoAvance = empleadoAvancePer
                            .Where(o => o.d_fchtra.Day == fechaInicio.Day
                                && o.d_fchtra.Month == fechaInicio.Month && o.d_fchtra.Year == fechaInicio.Year)
                            .FirstOrDefault();

                        if (empleadoAvance != null)
                        {
                            double n_horIni = funCon.HoraEnDecimal(empleadoAvance.c_horini);
                            double n_horFin = funCon.HoraEnDecimal(empleadoAvance.c_horter);
                            double n_numhor = funCon.HoraEnDecimal(funFunciones.HorasRestar(empleadoAvance.c_horini, empleadoAvance.c_horter));
                            double n_numhor_real = n_numhor;

                            string columnaFecha = fechaInicio.ToString("dd/MM");
                            //hora de almuerzo
                            if (n_horFin >= 13.5 && n_horIni <= 12.5)
                            {
                                n_numhor_real = n_numhor_real - 1;
                            }
                            //Kg/h/p
                            if (n_numhor_real > 0)
                            {
                                double n_kg_h_p = empleadoAvance.n_can / n_numhor_real;
                                row[columnaFecha] = Genericas.Round(n_kg_h_p, 2);
                            }
                            else
                            {
                                row[columnaFecha] = 0;
                            }
                        }

                        fechaInicio = fechaInicio.AddDays(1);
                    }

                    tabla.Rows.Add(row);
                }

                DgvResultado.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error: {0}", ex.Message), "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog objCuadroDialogo = new SaveFileDialog();

                objCuadroDialogo.Filter = "MS Excel (*.xlsx) |*.xlsx;*.xlsx|(*.xlsx) |*.xlsx|(*.*) |*.*";

                if (objCuadroDialogo.ShowDialog() == DialogResult.OK)
                {
                    tabla.TableName = "Reporte de Avances";
                    Cls_DBGrid.DataTable_To_Excel(tabla, objCuadroDialogo.FileName);

                    string filename = "Excel.exe";

                    Process proc = new Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = filename;
                    proc.StartInfo.Arguments = objCuadroDialogo.FileName;
                    proc.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error: {0}", ex.Message), "Exportar Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                var empleado = (Empleado)empleadoBindingSource.Current;

                if (empleado != null)
                {
                    empleados.Remove(empleado);
                }
                else
                {
                    throw new Exception("Debe de seleccionar un registro a eliminar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error: {0}", ex.Message), "Eliminar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
