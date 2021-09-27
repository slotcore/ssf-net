using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using SIAC_Entidades.Logistica;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Net.Http;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Helper
{
    public class Genericas
    {
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public bool booOcurrioError = false;

        public string Buscar_Titulo;
        public string Buscar_CampoOrden;
        public string Buscar_CampoBusqueda;
        public string Buscar_CadFiltro;

        public int MostrarDatos_NumFilasCabecera = 0;

        public string Filtrar_Titulo;
        public string Filtrar_CampoOrden;
        public string Filtrar_CampoBusqueda;
        public int Filtrar_ColumnaBusqueda;
        public int Filtrar_ColumnaCheck;
        public bool Filtrar_AplicarFiltro;

        public string EXP_EMPRESA = "";
        public string EXP_RUC = "";
        public string EXP_TITULO1 = "";
        public string EXP_TITULO2 = "";

        public string Grafico_TituloGrafico = "";
        public string Grafico_PieGrafico = "";
        public string[] Grafico_TitulosY = {};
        public double[,] Grafico_ValoresX = {};
        public string[,] Grafico_Series = {};
        public bool Grafico_Legenda;
        public bool Grafico_Cabecera;
        public bool Grafico_Pie;

        Comunes.Funciones o_fun = new Comunes.Funciones();

        public static string GetString(MySqlDataReader reader, string key)
        {
            string value = string.Empty;

            if (reader[key] != DBNull.Value)
                value = reader.GetString(key);

            return value;
        }

        public static DateTime GetDateTime(MySqlDataReader reader, string key)
        {
            DateTime value = new DateTime();

            if (reader[key] != DBNull.Value)
                value = Convert.ToDateTime(GetString(reader, key));

            return value;
        }

        public static int GetInt(MySqlDataReader reader, string key)
        {
            int value = 0;

            if (reader[key] != DBNull.Value)
                value = reader.GetInt32(key);

            return value;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);
        
        #region ComboBoxCargarDataTable
        //*********************************************************************************************
        //** NOMBRE      : ComboBoxCargarDataTable
        //** TIPO        : Procedimiento
        //** DESCRIPCION : Carga un ComboBox con los datos provenientes de un Datatable
        //** PARAMETROS  : 
        //**               TIPO       |  NOMBRE          | DESCRIPCION
        //**               ----------------------------------------------
        //**               ComboBox   |  CmbControl      | Nombre del Control ComboBox a cargar
        //**               DataTable  |  DtMyTabla       | DataTable de donde se cargaran los datos
        //**               string     |  StrCampoValue   | Nombre del campo identificacion del DataTable
        //**               string     |  StrCampoDisplay | Nombre del Campo Descripcion del DataTable
        //** 
        //** DEVUELVE    : 
        //*********************************************************************************************
        public void ComboBoxCargarDataTable(ComboBox CmbControl, DataTable DtMyTabla, string StrCampoValue, string StrCampoDisplay)
        {
            CmbControl.DataSource = DtMyTabla;
            CmbControl.DisplayMember = StrCampoDisplay;   // INDICAMOS EL NOMBRE DEL CAMPO A MOSTRAR
            CmbControl.ValueMember = StrCampoValue;       // IDICAMOS EL NOMBRE DEL CAMPO VALUE 
            CmbControl.SelectedValue = 0;              // LE INDICAMOS QUE POR DEFECTO NO SELECCIONE NINGUNO ELEMENTO DEL COMBO BOX
        }
        #endregion ComboBoxCargarDataTable


        //public void DataTableToEntidad(DataTable dtDatos, object Entidad)
        //{
        //    int n_col = 0;
        //    int n_colEnt = 0;
        //    BE_LOG_ORDENREQUERIMIENTO entReqCab = new BE_LOG_ORDENREQUERIMIENTO();

        //    for (n_col = 0; n_colEnt <= (dtDatos.Columns.Count - 1); n_col++)
        //    {
        //        entReqCab.n_id = 1;
        //        if (entReqCab..name == "")
        //        { 
        //        }
        //        //for (n_colEnt = 0; entReqCab; n_colEnt++)
        //        //{ 

        //        //}
        //    }
        //}


        #region Funciones_DataTable
        //*********************************************************************************************
        //** NOMBRE      : DataTableBuscar
        //** TIPO        : Funcion
        //** DESCRIPCION : Busca un valor en un datatable y devuelve la primera coincidencia que encuentre
        //** PARAMETROS  : 
        //**               TIPO       | NOMBRE            | DESCRIPCION
        //**               ----------------------------------------------
        //**               DataTable  | dtOrigen          | DataTable donde se realizara la busqueda
        //**               string     | strCampoBusqueda  | Nombre del campo donde se realizara la busqueda
        //**               string     | strCampoRetornar  | Nombre del campo a retornar cuando se encuentre la coincidencia
        //**               string     | strValorBuscar    | Valor a buscar en el campo de busqueda
        //**               string     | strTipoDato       | Tipo de dato donde se realizara la busqueda (N = numerico; C = Caracter)
        //** 
        //** DEVUELVE    : Un objeto conteniendo el valor buscado
        //*********************************************************************************************
        public object DataTableBuscar(DataTable dtOrigen, string strCampoBusqueda, string strCampoRetornar, string strValorBuscar, string strTipoDato)
        {
            object objResutl;
            DataRow[] drResult;

            if (strValorBuscar == "") { return ""; }

            if (strTipoDato == "N")
            {
                drResult = dtOrigen.Select("" + strCampoBusqueda + " = " + Convert.ToInt32(strValorBuscar) + "");
            }
            else
            {
                drResult = dtOrigen.Select("" + strCampoBusqueda + " = '" + strValorBuscar + "'");
            }

            if (drResult.Length != 0)
            {
                // SI SE HA ENCONTRADO DATOSDEVUELVE EL CAMPO A RETORNAR
                objResutl = drResult[0][strCampoRetornar].ToString();
            }
            else
            {
                // SI NO SE HA ENCONTRADO DATOS SE DEVUELVE 0
                objResutl = 0;
            }

            return objResutl;
        }

        //*********************************************************************************************
        //** NOMBRE      : DataTableFiltrar
        //** TIPO        : Funcion
        //** DESCRIPCION : Filtra un DataTable de acuerdo a condiciones establecidas
        //** PARAMETROS  : 
        //**               TIPO       | NOMBRE             | DESCRIPCION
        //**               ----------------------------------------------
        //**               DataTable  | dtDataTableFiltrar | DataTable donde se realizara el filtro
        //**               string     | strCondicionFiltro | Condicion para efectuar el filtro
        //** 
        //** DEVUELVE    : DataTable
        //*********************************************************************************************
        public DataTable DataTableFiltrar(DataTable dtDataTableFiltrar, string strCondicionFiltro)
        {
            DataTable dtResult = new DataTable();
            dtResult = dtDataTableFiltrar.Clone();

            DataRow[] result = dtDataTableFiltrar.Select(strCondicionFiltro);
            foreach (DataRow row in result)
            {
                dtResult.ImportRow(row);
            }
            return dtResult;
        }
        public DataTable DataTableAgregarDataTable(DataTable dtOrigen, DataTable dtDestino, string strCondicionFiltro)
        {
            DataTable dtResult = new DataTable();
            if (dtDestino.Rows.Count == 0)
            {
                dtDestino = dtOrigen.Clone();
            }
            
            DataRow[] result = dtOrigen.Select(strCondicionFiltro);
            foreach (DataRow row in result)
            {
                dtDestino.ImportRow(row);
            }
            //dtDestino = dtResult;
            return dtDestino;
        }
        public DataTable DataTableFiltrar(DataTable dtDataTableFiltrar, string strCondicionFiltro, string c_campoOrdenar)
        {
            DataTable dtResult = new DataTable();
            DataTable dtResultFin = new DataTable();
            dtResult = dtDataTableFiltrar.Clone();
            dtResultFin = dtDataTableFiltrar.Clone();
            DataView dv = dtDataTableFiltrar.DefaultView;
            dv.Sort = c_campoOrdenar;
            dtResult = dv.ToTable();

            DataRow[] result = dtResult.Select(strCondicionFiltro);
            foreach (DataRow row in result)
            {
                dtResultFin.ImportRow(row);
            }
            return dtResultFin;
        }
        public DataTable DataTableOrdenar(DataTable dtDataTableFiltrar, string c_campoOrdenar)
        {
            DataTable dtResult = new DataTable();
            dtResult = dtDataTableFiltrar.Clone();
            DataView dv = dtDataTableFiltrar.DefaultView;
            dv.Sort = c_campoOrdenar;
            dtResult = dv.ToTable();
            return dtResult;
        }
        public void DataTanbleEliminarRegistro(ref DataTable dtDatos, string c_Campo, string c_Valor, string c_TipoDato)
        {
            DataRow[] rows;
            string c_cad = "";

            if (c_TipoDato == "C") { c_cad = "" + c_Campo + " = '" + c_Valor + "'"; }
            if (c_TipoDato == "N") { c_cad = "" + c_Campo + " = " + Convert.ToInt32(c_Valor) + ""; }

            rows = dtDatos.Select(c_cad);  //'UserName' is ColumnName
            
            foreach (DataRow row in rows)
                dtDatos.Rows.Remove(row);
            
            //for (n_row = 0; n_row <= dtDatos.Rows.Count - 1; n_row++)
            //{
            //    if (c_TipoDato == "C")
            //    {
            //        if (Convert.ToString(dtDatos.Rows[n_row][c_Campo]) == c_Valor)
            //        {
            //            dtDatos.Rows[n_row].Delete();
            //        }
            //    }
            //    if (c_TipoDato == "N")
            //    {
            //        if (Convert.ToUInt32(dtDatos.Rows[n_row][c_Campo]) == Convert.ToUInt32(c_Valor))
            //        {
            //            dtDatos.Rows.Remove(n_row);
            //        }
            //    }
            //}
            //dtDatos.AcceptChanges();

            //    dtResult = dtDataTableFiltrar.Clone();
            //DataView dv = dtDataTableFiltrar.DefaultView;
            //dv.Sort = c_campoOrdenar;
            //dtResult = dv.ToTable();
            //return dtResult;
        }
        public bool DataTableExisteColumna(string c_NombreColumna, DataTable dtDatos)
        {
            bool b_result = false;
            int n_numcol = dtDatos.Columns.Count;
            int n_col = 0;

            for (n_col = 0; n_col <= n_numcol - 1; n_col++)
            {

                if (dtDatos.Columns[n_col].ToString() == c_NombreColumna)
                {
                    b_result = true;
                    break;
                }
            }

            return b_result;
        }
        public string DataTableCadenaIN(DataTable dtdatos, string c_Campo)
        {
            string c_cadin = "";
            int n_row = 1;
            string c_cad = "";
            int n_FilaIni = 0;

            if (dtdatos.Rows.Count ==0) {return c_cad;}

            for (n_row = 0; n_row <= dtdatos.Rows.Count - 1; n_row++)
            {
                c_cad = o_fun.NulosC(dtdatos.Rows[n_row][c_Campo]).ToString();

                if (c_cad != "")
                {
                    if (n_row == n_FilaIni)
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
        public void DataTable_Exportar(DataTable dtDatos, string c_NombreArchivo, string[,] c_Columnas)
        {
            try {
                if (dtDatos == null || dtDatos.Columns.Count == 0)
                { 
                    StrErrorMensaje = "El data table esta vacio";
                    IntErrorNumber = 1;
                    booOcurrioError = false;
                }
                    //throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();
                
                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;
                
                int n_NumElementos = Convert.ToInt32(c_Columnas.GetLongLength(0)) - 1;

                workSheet.Cells[1, 1] = "EMPRESA : " + EXP_EMPRESA;
                workSheet.Cells[1, 5] = "FECHA : " + DateTime.Today.ToString("dd/MM/yyyy");
                workSheet.Cells[2, 1] = "Nº R.U.C. : " + EXP_RUC;

                workSheet.Cells[4, 1] = EXP_TITULO1;
                workSheet.Cells[5, 1] = EXP_TITULO2;

                // column headings
                int n_fil = 7;
                for (var i = 0; i <= n_NumElementos; i++)
                {
                    //workSheet.Cells[1, i + 1] = dtDatos.Columns[i].ColumnName;
                    workSheet.Cells[n_fil, i + 1] = c_Columnas[i, 0].ToString();
                }

                // rows
                n_fil = 8;
                for (var i = 0; i < dtDatos.Rows.Count - 1; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j <= n_NumElementos; j++)
                    {
                        //workSheet.Cells[i + 2, j + 1] = dtDatos.Rows[i][j];
                        workSheet.Cells[n_fil, j + 1] = dtDatos.Rows[i][c_Columnas[j, 4].ToString()];
                    }
                    n_fil = n_fil + 1;
                }

                // check file path
                if (!string.IsNullOrEmpty(c_NombreArchivo))
                {
                    try {
                        workSheet.SaveAs(c_NombreArchivo);
                        excelApp.Workbooks.Close();
                        excelApp.Quit();
                        //MessageBox.Show("El archivo se guard!");
                    }
                    catch (Exception ex) {
                        //throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            //+ ex.Message);
                        StrErrorMensaje = ex.Message;
                        IntErrorNumber = 1;
                        booOcurrioError = true;
                    }
                }
                else 
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex) 
            {
                //throw new Exception("ExportToExcel: \n" + ex.Message);
                StrErrorMensaje = ex.Message;
                IntErrorNumber = 1;
                booOcurrioError = true;
            }
        }

        public DataTable DataTableAPI(string c_Url)
        {
            DataTable dtRes = new DataTable();
            string c_json = LeerURL(c_Url);
            dtRes = JsonToDataTable(c_json);
            return dtRes;
        }
        #endregion Funciones_DataTable
        public DataTable Buscar(string[,] arrCabeceraDg1, DataTable dtConsulta)
        {
            Formularios.FrmBusqueda xForm = new Formularios.FrmBusqueda();
            DataTable DtResult = new DataTable();

            xForm.arrCabeceraDg1 = arrCabeceraDg1;
            xForm.dtConsulta = dtConsulta;
            //xForm.n_ColumnaBusqueda = 5;
            xForm.Buscar_CampoBusqueda = Buscar_CampoBusqueda;
            xForm.c_titulo = Buscar_Titulo;
            xForm.c_campoorden = Buscar_CampoOrden;
            xForm.c_CadFiltro = Buscar_CadFiltro;
            xForm.ShowDialog();
            DtResult = xForm.dtResul;
            return DtResult;
        }
        public DataTable Filtrar(string[,] arrCabeceraFlex, DataTable dtConsulta)
        {
            Formularios.FrmFiltrar xForm = new Formularios.FrmFiltrar();
            DataTable DtResult = new DataTable();

            xForm.arrCabeceraFlex = arrCabeceraFlex;
            xForm.dtConsulta = dtConsulta;

            //xForm.Buscar_CampoBusqueda = Buscar_CampoBusqueda;
            xForm.c_titulo = Filtrar_Titulo;
            xForm.c_campoorden = Filtrar_CampoOrden;
            xForm.c_campobusqueda = Filtrar_CampoBusqueda;
            xForm.n_columnabusqueda = Filtrar_ColumnaBusqueda;
            xForm.n_columnacheck = Filtrar_ColumnaCheck;
            //xForm.c_CadFiltro = Buscar_CadFiltro;
            xForm.b_ConFiltro = Filtrar_AplicarFiltro;
            xForm.ShowDialog();
            DtResult = xForm.dtResultado;
            return DtResult;
        }
        public DataTable Filtrar2(string[,] arrCabeceraFlex, DataTable dtConsulta)
        {
            Formularios.FrmFiltro2 xForm = new Formularios.FrmFiltro2();
            DataTable DtResult = new DataTable();
            Buscar_CadFiltro = "";
            xForm.arrCabeceraDg1 = arrCabeceraFlex;
            xForm.dtConsulta = dtConsulta;
            //xForm.n_ColumnaBusqueda = 5;
            //xForm.Buscar_CampoBusqueda = Filtrar_CampoBusqueda;
            xForm.c_titulo = Filtrar_Titulo;
            xForm.c_campoorden = Filtrar_CampoOrden;
            xForm.c_CadFiltro = Buscar_CadFiltro;
            xForm.ShowDialog();
            DtResult = xForm.dtResul;
            return DtResult;
        }
        public DataTable MostrarDatos(string[,] arrCabeceraFlex, DataTable dtConsulta)
        {
            Formularios.FrmVerDatos xForm = new Formularios.FrmVerDatos();
            DataTable DtResult = new DataTable();

            xForm.arrCabeceraFlex = arrCabeceraFlex;
            
            xForm.dtConsulta = dtConsulta;

            xForm.c_titulo = Filtrar_Titulo;
            if (MostrarDatos_NumFilasCabecera == 0)
            {
                xForm.n_num_filas_cabecera = 2;
            }
            else
            {
                xForm.n_num_filas_cabecera = MostrarDatos_NumFilasCabecera;
            }

            xForm.ShowDialog();
            DtResult = xForm.dtResultado;
            return DtResult;
        }
        public DataTable MostrarDatos(string[,] arrCabeceraFlex, DataTable dtConsulta, string[,] arrCabeceraFlexFix)
        {
            Formularios.FrmVerDatos xForm = new Formularios.FrmVerDatos();
            DataTable DtResult = new DataTable();
                        
            xForm.arrCabeceraFlex = arrCabeceraFlex;
            xForm.arrCabeceraFlexFix = arrCabeceraFlexFix;
            xForm.dtConsulta = dtConsulta;
            
            xForm.c_titulo = Filtrar_Titulo;
            if (MostrarDatos_NumFilasCabecera == 0)
            {
                xForm.n_num_filas_cabecera = 2;
            }
            else 
            { 
                xForm.n_num_filas_cabecera = MostrarDatos_NumFilasCabecera;
            }
            
            xForm.ShowDialog();
            DtResult = xForm.dtResultado;
            return DtResult;
        }
        public void VerGrafico()
        {
            Formularios.FrmVerGrafico xForm = new Formularios.FrmVerGrafico();
            xForm.c_TituloGrafico = Grafico_TituloGrafico;
            xForm.c_PieGrafico = Grafico_PieGrafico;
            xForm.c_TitulosY = Grafico_TitulosY;
            xForm.n_ValoresX = Grafico_ValoresX;
            xForm.c_Series = Grafico_Series;
            xForm.b_Legenda = Grafico_Legenda;
            xForm.b_Pie = Grafico_Pie;
            xForm.b_Cabecera = Grafico_Cabecera;
            xForm.ShowDialog();
        }
        public void IniEscribirSeccion(string sFileName, string Section, string Key, string c_Valor)
        {
            string path = sFileName;

            //StringBuilder temp = new StringBuilder(255);
            WritePrivateProfileString(Section, Key, c_Valor, sFileName);
            return;
        }
        public string IniLeerSeccion(string sFileName, string Section, string Key)
        {
            string path = sFileName;

            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, path);
            return temp.ToString();

        }
        string LeerURL(string c_Url)
        {
            //ejemplo ==> c_Url = "http://localhost:30050/api/usuario/traerusuario/admin,10520342,2";

            HttpClient http = new HttpClient();

            HttpResponseMessage response = http.GetAsync(new Uri(c_Url)).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            responseBody = responseBody.Replace("\\", "");
            responseBody = responseBody.Substring(1, responseBody.Length - 2);
            return responseBody;
        }
        DataTable JsonToDataTable(string c_Json)
        {
            DataTable dtres = (DataTable)JsonConvert.DeserializeObject(c_Json, (typeof(DataTable)));
            return dtres;
        }

        public static double RoundCorrect(double d, int decimals)
        {
            double multiplier = Math.Pow(10, decimals);

            if (d < 0)
                multiplier *= -1;

            return Math.Floor((d * multiplier) + 0.5) / multiplier;

        }
        public static decimal Round(double num, int decimals)
        {
            decimal d = Convert.ToDecimal(num);
            decimal factor = Convert.ToDecimal(Math.Pow(10, decimals));
            int sign = Math.Sign(d);
            return Decimal.Truncate(d * factor + 0.5m * sign) / factor;
        }
        //public string[] IniLeerSeccion(string sFileName, string sSection)
        //{
        //    //--------------------------------------------------------------------------
        //    // Lee una sección entera de un fichero INI                      (27/Feb/99)
        //    // Adaptada para devolver un array de string                     (04/Abr/01)
        //    //
        //    // Esta función devolverá un array de índice cero
        //    // con las claves y valores de la sección
        //    //
        //    // Parámetros de entrada:
        //    //   sFileName   Nombre del fichero INI
        //    //   sSection    Nombre de la sección a leer
        //    // Devuelve:
        //    //   Un array con el nombre de la clave y el valor
        //    //   Para leer los datos:
        //    //       For i = 0 To UBound(elArray) -1 Step 2
        //    //           sClave = elArray(i)
        //    //           sValor = elArray(i+1)
        //    //       Next
        //    //
        //    string[] aSeccion;
        //    string sBuffer;
        //    int n;
        //    //
        //    aSeccion = new string[0];
        //    //
        //    // El tamaño máximo para Windows 95
        //    sBuffer = new string('\0', 32767);
        //    //
        //    n = GetPrivateProfileSection(sSection, sBuffer, sBuffer.Length, sFileName);
        //    //
        //    if (n > 0)
        //    {
        //        // Cortar la cadena al número de caracteres devueltos
        //        // menos los dos últimos que indican el final de la cadena
        //        sBuffer = sBuffer.Substring(0, n - 2).TrimEnd();
        //        //
        //        // Cada una de las entradas estará separada por un Chr$(0)
        //        // y cada valor estará en la forma: clave = valor
        //        aSeccion = sBuffer.Split(new char[] { '\0', '=' });
        //    }
        //    // Devolver el array
        //    return aSeccion;
        //}  
    }
}
