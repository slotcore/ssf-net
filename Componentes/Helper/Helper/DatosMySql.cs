using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace Helper
{
    public class DatosMySql
    {
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public bool booOcurrioError = false;       
        public Int64 intIdGenerado = 0;

        public enum TipoDato
        {
            _bigint,
            _binary,
            _bit,
            _char,
            _cursor,
            _datetime,
            _decimal,
            _float,
            _image,
            _int,
            _money,
            _nchar,
            _ntext,
            _numeric,
            _nvarchar,
            _real,
            _smalldatetime,
            _smallint,
            _smallmoney,
            _sql_variant,
            _table,
            _text,
            _timestamp,
            _tinyint,
            _uniqueidentifier,
            _varbinary,
            _varchar,
            _xml
        };

        public MySqlConnection ObtenerConexion(string strHostNombre, string strBDNombre, string strUsuario, string strPassword)
        {
            //3032
            //3306
            MySqlConnection conectar = new MySqlConnection("server=" + strHostNombre + "; port=3306; database=" + strBDNombre + "; Uid=" + strUsuario + "; pwd=" + strPassword + "; default command timeout=120");

            try
            {
                conectar.Open();
                return conectar;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return null;
            }
        }
        public MySqlConnection ObtenerConexion(string strHostNombre, string strBDNombre, string strUsuario, string strPassword, string c_Puerto)
        {
            try
            {
                string connectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;
                //MySqlConnection conectar = new MySqlConnection("server=" + strHostNombre + "; port=" + c_Puerto + "; database=" + strBDNombre + "; Uid=" + strUsuario + "; pwd=" + strPassword + "; default command timeout=120");
                MySqlConnection conectar = new MySqlConnection(connectionString);
                conectar.Open();
                return conectar;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return null;
            }
        }
        public MySqlConnection ObtenerConexion(string c_CadConeccion)
        {
            try
            {
                MySqlConnection conectar = new MySqlConnection(c_CadConeccion);
                conectar.Open();
                return conectar;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return null;
            }
        }
        public MySqlConnection ReAbrirConeccion(MySqlConnection xConeccion)
        {
            while (xConeccion.State == ConnectionState.Closed)
            {
                xConeccion.Open();
            }
            return xConeccion;

            //    if (xConeccion.State == 0)
            //{
            //    xConeccion.Open();
            //}
        }
        public Boolean EjecutarSQL(string c_Sql, MySqlConnection xConeccion)
        {
            MySqlCommand xCmd = new MySqlCommand(c_Sql, xConeccion);
            bool booResult = false;

            try
            {
                xCmd.ExecuteNonQuery();
                booResult = true;
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message;
                IntErrorNumber = exc.HResult;
                return booResult;
            }
        }
        public DataTable TraerSQL(string c_CadenaSQL, MySqlConnection xConeccion)
        {
            DataTable dt = new DataTable();
            //dt = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(c_CadenaSQL, xConeccion);
                MySqlDataAdapter returnVal = new MySqlDataAdapter(c_CadenaSQL, xConeccion);
                returnVal.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return dt;
            }
        }
        #region StoreDTLLenar
        //*********************************************************************************************
        //** NOMBRE      : StoreDTLLenar
        //** TIPO        : Metodo
        //** DESCRIPCION : Ejecuta un store en el motor de datos y devuelve un DataTable con los datos recuperados
        //** PARAMETROS  : 
        //**               TIPO           |  NOMBRE          | DESCRIPCION
        //**               ----------------------------------------------
        //**               string         |  StrNombreStore  | Nombre del Procedimiento Almacenado a ejecutar
        //**               string[N,3]    |  arrParametros   | Array de parametros que requiere el Procedimiento Almacenado
        //**                                                   [0,0]  = Indica el nombre del parametro del Procedimiento Almacenado
        //**                                                   [0,1]  = Indica el tipo de dato del Procedimiento Almacenado
        //**                                                   [0,2]  = Indica el Valor que se le pasara al parametro
        //**               MySqlConnection|  mysConeccion    | Coneccion MySql 
        //** 
        //** SINTAXIS    : 
        //**               string[,] arrParametros = new string[1, 3] {
        //**                                                              {"n_idret", "SYSTEM.INT16", intRetencionId.ToString()}
        //**                                                          };
        //**               StoreDTLLenar("mi_procedimieto"), arrParametros, MiConeccionMySQL) 
        //** 
        //** DEVUELVE    : DataTable
        //*********************************************************************************************
        public DataTable StoreDTLLenar(string StrNombreStore, string[,] arrParametros, MySqlConnection mysConeccion)
        {
            int iFila = 0;
            int IntNumeroElementos;
            Comunes.Funciones MiFun = new Comunes.Funciones();
            DataTable DtResul = new DataTable();

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand cmd = new MySqlCommand(StrNombreStore, mysConeccion);
                cmd.CommandType = CommandType.StoredProcedure;

                IntNumeroElementos = Convert.ToInt32(arrParametros.GetLongLength(0));

                if (IntNumeroElementos != 0)
                {
                    for (iFila = 0; iFila <= IntNumeroElementos - 1; iFila++)
                    {
                        if ((arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT32") || (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT16") || (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT64"))
                        {
                            cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Int32);
                            if (arrParametros[iFila, 2] == "null")
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                            }
                            else
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                            }
                        }

                        if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DOUBLE")
                        {
                            cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Decimal);
                            if (arrParametros[iFila, 2] == "null")
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                            }
                            else
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDouble(arrParametros[iFila, 2]);
                            }
                        }

                        if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.STRING")
                        {
                            cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.VarChar);
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToString(arrParametros[iFila, 2]);
                        }

                        if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.TEXT")
                        {
                            cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Text);
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToString(arrParametros[iFila, 2]);
                        }

                        if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DATETIME")
                        {
                            cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Date);

                            if (arrParametros[iFila, 2] == "null")
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                            }
                            else
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDateTime(arrParametros[iFila, 2]);
                            }
                        }
                    }
                }
                
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(DtResul);

                return DtResul;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return null;
            }
        }
   #endregion StoreDTLLenar

        //#region ComboBoxCargarDataTable
        ////*********************************************************************************************
        ////** NOMBRE      : ComboBoxCargarDataTable
        ////** TIPO        : Procedimiento
        ////** DESCRIPCION : Carga un ComboBox con los datos provenientes de un Datatable
        ////** PARAMETROS  : 
        ////**               TIPO       |  NOMBRE          | DESCRIPCION
        ////**               ----------------------------------------------
        ////**               ComboBox   |  CmbControl      | Nombre del Control ComboBox a cargar
        ////**               DataTable  |  DtMyTabla       | DataTable de donde se cargaran los datos
        ////**               string     |  StrCampoValue   | Nombre del campo identificacion del DataTable
        ////**               string     |  StrCampoDisplay | Nombre del Campo Descripcion del DataTable
        ////** 
        ////** DEVUELVE    : 
        ////*********************************************************************************************
        //public void ComboBoxCargarDataTable(ComboBox CmbControl, DataTable DtMyTabla, string StrCampoValue, string StrCampoDisplay)
        //{
        //    CmbControl.DataSource = DtMyTabla;
        //    CmbControl.DisplayMember = StrCampoDisplay;   // INDICAMOS EL NOMBRE DEL CAMPO A MOSTRAR
        //    CmbControl.ValueMember = StrCampoValue;       // IDICAMOS EL NOMBRE DEL CAMPO VALUE 
        //    CmbControl.SelectedValue = 0;              // LE INDICAMOS QUE POR DEFECTO NO SELECCIONE NINGUNO ELEMENTO DEL COMBO BOX
        //}
        //#endregion ComboBoxCargarDataTable
        
        //#region Funciones_DataTable
        ////*********************************************************************************************
        ////** NOMBRE      : DataTableBuscar
        ////** TIPO        : Funcion
        ////** DESCRIPCION : Busca un valor en un datatable y devuelve la primera coincidencia que encuentre
        ////** PARAMETROS  : 
        ////**               TIPO       | NOMBRE            | DESCRIPCION
        ////**               ----------------------------------------------
        ////**               DataTable  | dtOrigen          | DataTable donde se realizara la busqueda
        ////**               string     | strCampoBusqueda  | Nombre del campo donde se realizara la busqueda
        ////**               string     | strCampoRetornar  | Nombre del campo a retornar cuando se encuentre la coincidencia
        ////**               string     | strValorBuscar    | Valor a buscar en el campo de busqueda
        ////**               string     | strTipoDato       | Tipo de dato donde se realizara la busqueda (N = numerico; C = Caracter)
        ////** 
        ////** DEVUELVE    : Un objeto conteniendo el valor buscado
        ////*********************************************************************************************
        //public object DataTableBuscar(DataTable dtOrigen,string strCampoBusqueda, string strCampoRetornar,string strValorBuscar, string strTipoDato)
        //{
        //    object objResutl;
        //    DataRow[] drResult;
            
        //    if (strTipoDato == "N")
        //    {
        //        drResult = dtOrigen.Select("" + strCampoBusqueda + " = " + Convert.ToInt32(strValorBuscar) + "");
        //    }
        //    else
        //    {
        //        drResult = dtOrigen.Select("" + strCampoBusqueda + " = '" + strValorBuscar + "'");
        //    }

        //    if (drResult.Length != 0)
        //    {
        //        // SI SE HA ENCONTRADO DATOSDEVUELVE EL CAMPO A RETORNAR
        //        objResutl = Convert.ToInt32(drResult[0][strCampoRetornar].ToString());
        //    }
        //    else
        //    {
        //        // SI NO SE HA ENCONTRADO DATOS SE DEVUELVE NULL
        //        objResutl = null;
        //    }

        //    return objResutl;
        //}

        ////*********************************************************************************************
        ////** NOMBRE      : DataTableFiltrar
        ////** TIPO        : Funcion
        ////** DESCRIPCION : Filtra un DataTable de acuerdo a condiciones establecidas
        ////** PARAMETROS  : 
        ////**               TIPO       | NOMBRE             | DESCRIPCION
        ////**               ----------------------------------------------
        ////**               DataTable  | dtDataTableFiltrar | DataTable donde se realizara el filtro
        ////**               string     | strCondicionFiltro | Condicion para efectuar el filtro
        ////** 
        ////** DEVUELVE    : DataTable
        ////*********************************************************************************************
        //public DataTable DataTableFiltrar(DataTable dtDataTableFiltrar, string strCondicionFiltro)
        //{
        //    DataTable dtResult = new DataTable();
        //    dtResult = dtDataTableFiltrar.Clone();

        //    DataRow[] result = dtDataTableFiltrar.Select(strCondicionFiltro);
        //    foreach (DataRow row in result)
        //    {
        //        dtResult.ImportRow(row);
        //    }
        //    return dtResult;
        //}
        //public DataTable DataTableFiltrar(DataTable dtDataTableFiltrar, string strCondicionFiltro, string c_campoOrdenar)
        //{
        //    DataTable dtResult = new DataTable();
        //    DataTable dtResultFin = new DataTable();
        //    dtResult = dtDataTableFiltrar.Clone();
        //    dtResultFin = dtDataTableFiltrar.Clone();
        //    DataView dv = dtDataTableFiltrar.DefaultView;
        //    dv.Sort = c_campoOrdenar;
        //    dtResult = dv.ToTable();

        //    DataRow[] result = dtResult.Select(strCondicionFiltro);
        //    foreach (DataRow row in result)
        //    {
        //        dtResultFin.ImportRow(row);
        //    }
        //    return dtResultFin;
        //}

        //#endregion Funciones_DataTable

        #region StoreEjecutar
        // SOBRECARGA DEL METODO StoreEjecutar
        public bool StoreEjecutar(string StrNombreStore, string[,] arrParametros, MySqlConnection mysConeccion, int? intPocisionEntity)
        {
            bool bolOk = false;
            int iFila = 0;
            int IntNumeroElementos;
            Comunes.Funciones MiFun = new Comunes.Funciones();

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(StrNombreStore, mysConeccion);
                cmd.CommandType = CommandType.StoredProcedure;

                IntNumeroElementos = Convert.ToInt32(arrParametros.GetLongLength(0));

                for (iFila = 0; iFila <= IntNumeroElementos - 1; iFila++)
                {
                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT64")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Int64);

                        if (intPocisionEntity != null)
                        {
                            if (iFila == intPocisionEntity)
                            {
                                cmd.Parameters[iFila].Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                            }
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                        }
                    }

                    if ((arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT32") || (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT16"))
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Int32);
                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                        }
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DOUBLE")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Decimal);
                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDouble(arrParametros[iFila, 2]);
                        }
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.STRING")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.VarChar);
                        cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToString(arrParametros[iFila, 2]);
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DATETIME")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Date);

                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDateTime(arrParametros[iFila, 2]);
                        }
                    }
                }

                cmd.ExecuteNonQuery();

                for (iFila = 0; iFila <= IntNumeroElementos - 1; iFila++)
                {
                    if (cmd.Parameters[iFila].Direction == ParameterDirection.Output)
                    {
                        intIdGenerado = Int64.Parse(cmd.Parameters[iFila].Value.ToString());
                    }
                }

                adapter.SelectCommand = cmd;
                bolOk = true;
                return bolOk;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return bolOk;
            }
        }
        #region StoreEjecutar1
        //*********************************************************************************************
        //** NOMBRE      : StoreEjecutar
        //** TIPO        : Metodo
        //** DESCRIPCION : Ejecuta un store en el motor de datos
        //** PARAMETROS  : 
        //**               TIPO           |  NOMBRE          | DESCRIPCION
        //**               ----------------------------------------------
        //**               string         |  StrNombreStore  | Nombre del Procedimiento Almacenado a ejecutar
        //**               string[N,3]    |  arrParametros   | Array de parametros que requiere el Procedimiento Almacenado
        //**                                                   [0,0]  = Indica el nombre del parametro del Procedimiento Almacenado
        //**                                                   [0,1]  = Indica el tipo de dato del Procedimiento Almacenado
        //**                                                   [0,2]  = Indica el Valor que se le pasara al parametro
        //**               MySqlConnection|  mysConeccion    | Coneccion MySql 
        //** 
        //** SINTAXIS    : 
        //**               string[,] arrParametros = new string[1, 3] {
        //**                                                              {"n_idret", "SYSTEM.INT16", intRetencionId.ToString()}
        //**                                                          };
        //**               StoreEjecutar("mi_procedimieto"), arrParametros, MiConeccionMySQL) 
        //** 
        //** DEVUELVE    : Boolean
        //*********************************************************************************************
        public bool StoreEjecutar(string StrNombreStore, string[,] arrParametros, MySqlConnection mysConeccion)
        {
            bool bolOk = false;
            int iFila = 0;
            int IntNumeroElementos;
            Comunes.Funciones MiFun = new Comunes.Funciones();

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(StrNombreStore, mysConeccion);
                cmd.CommandType = CommandType.StoredProcedure;

                IntNumeroElementos = Convert.ToInt32(arrParametros.GetLongLength(0));

                for (iFila = 0; iFila <= IntNumeroElementos - 1; iFila++)
                {

                    if ((arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT32") || (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT16") || (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT64"))
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Int32);
                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                        }
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DOUBLE")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Decimal);
                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDouble(arrParametros[iFila, 2]);
                        }
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.STRING")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.VarChar);
                        cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToString(arrParametros[iFila, 2]);
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DATETIME")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Date);

                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDateTime(arrParametros[iFila, 2]);
                        }
                    }
                }

                cmd.ExecuteNonQuery();

                adapter.SelectCommand = cmd;
                bolOk = true;
                return bolOk;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return bolOk;
            }
        }
        #endregion Store1jecutar1

        #region StoreEjecutar2
        //*********************************************************************************************
        //** NOMBRE      : StoreEjecutar
        //** TIPO        : Metodo
        //** DESCRIPCION : Ejecuta un store en el motor de datos, los valores para el store los recoje del ibjeto entidad, 
        //**               para ello lee el objeto y obtiene el nombre del parametro, tipo de dato y el valor
        //** PARAMETROS  : 
        //**               TIPO           |  NOMBRE            | DESCRIPCION
        //**               ------------------------------------------------
        //**               string         |  StrNombreStore    | Nombre del Procedimiento Almacenado a ejecutar
        //**               object         |  Entidad           | Objeto de tipo Entdad
        //**               MySqlConnection|  mysConeccion      | Coneccion MySql 
        //**               int            |  intPocisionEntity | Indica la posicion del elemento que actuara como Identity de la tabla
        //** SINTAXIS    : 
        //**               BE_MAE_RETENCION entRetencion = new BE_MAE_RETENCION();
        //** 
        //**               entRetencion.n_idret = 4;
        //**               entRetencion.c_des = "Mi descripcion";
        //**               entRetencion.n_tas = 444;
        //**               entRetencion.n_idcueconcom = 444;
        //**               entRetencion.n_idcueconven = 444;
        //**
        //**               StoreEjecutar("mi_procedimieto", entRetencion, MiConeccionMySQL, 0) 
        //** 
        //** DEVUELVE    : Boolean
        //*********************************************************************************************
        public bool StoreEjecutar(string StrNombreStore, object ObjEntidad, MySqlConnection mysConeccion, int? intPocisionEntity)
        {
            bool bolOk = false;
            
            int iFila = 0;
            int IntNumeroElementos;
            Comunes.Funciones MiFun = new Comunes.Funciones();
            string[,] arrParametros = new string[100, 3];

            arrParametros = MiFun.CrearParametros(ObjEntidad);

            try
            {
                //DbParameter dbParameter 
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(StrNombreStore, mysConeccion);
                cmd.CommandType = CommandType.StoredProcedure;

                IntNumeroElementos = Convert.ToInt32(arrParametros.GetLongLength(0));

                for (iFila = 0; iFila <= IntNumeroElementos - 1; iFila++)
                {
                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT64")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Int64);

                        if (intPocisionEntity != null)
                        {
                            if (iFila == intPocisionEntity)
                            {
                                cmd.Parameters[iFila].Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                            }
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                        }
                    }

                    if ((arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT32") || (arrParametros[iFila, 1].ToUpper() == "SYSTEM.INT16"))
                    {
                        //cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Int32);
                        //if (arrParametros[iFila, 2] == "null")
                        //{
                        //    cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        //}
                        //else
                        //{
                        //    cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                        //}

                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Int32);

                        if (intPocisionEntity != null) 
                        {
                            if (iFila == intPocisionEntity)
                            {
                                cmd.Parameters[iFila].Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                if (arrParametros[iFila, 2] != "null")
                                {
                                    cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                                }
                                else
                                {
                                    cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                                }
                            }
                        }
                        else
                        {
                            if (arrParametros[iFila, 2] != "null")
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToInt32(arrParametros[iFila, 2]);
                            }
                            else
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                            }
                        }
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DOUBLE")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Decimal);
                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDouble(arrParametros[iFila, 2]);
                        }
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.STRING")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.VarChar);
                        if (arrParametros[iFila, 2] == "")
                        {
                            if (arrParametros[iFila, 0].Substring(1, 1) == "h")
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = "null";
                            }
                            else
                            { 
                                cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                            }
                        }
                        else 
                        { 
                            if (arrParametros[iFila, 0].Substring(1, 1) == "h")
                            {
                                if (arrParametros[iFila, 2] == "null")
                                {
                                    cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                                }
                                else
                                { 
                                    cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDateTime(arrParametros[iFila, 2]).ToString("HH:mm:ss").Substring(0, 8);
                                }
                            }
                            else
                            {
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToString(arrParametros[iFila, 2]);
                            }
                             
                        }
                    }

                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.TEXT")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Text);
                        if (arrParametros[iFila, 2] == "null")
                        {
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else 
                        { 
                            cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToString(arrParametros[iFila, 2]);
                        }
                    }
                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.BYTE")
                    {
                        cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.MediumBlob);
                        cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToString(arrParametros[iFila, 2]);
                    }
                    if (arrParametros[iFila, 1].ToUpper() == "SYSTEM.DATETIME")
                    {
                        if (arrParametros[iFila, 2] == "null")
                        {
                            if (arrParametros[iFila, 0].Substring(1, 1) == "d")
                            {
                                // SI EL CAMPO ES DE TIPO FECHA
                                cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Date);
                            }
                            //else
                            //{
                            //    // SI EL CAMPO ES DE TIPO HORA
                            //    cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Time);
                            //    //cmd.Parameters[arrParametros[iFila, 0]].Value = "00:00:00";
                            //}
                            cmd.Parameters[arrParametros[iFila, 0]].Value = null;
                        }
                        else
                        {
                            if (arrParametros[iFila, 0].Substring(1, 1) == "d")            
                            {
                                // SI EL CAMPO ES DE TIPO FECHA
                                cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.Date);
                                cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDateTime(arrParametros[iFila, 2]).ToString("yyyy-MM-dd");
                            }
                            //else
                            //{
                            //    // SI EL CAMPO ES DE TIPO HORA
                            //    cmd.Parameters.Add(arrParametros[iFila, 0], MySqlDbType.VarChar);
                            //    cmd.Parameters[arrParametros[iFila, 0]].Value = Convert.ToDateTime(arrParametros[iFila, 2]).ToString("HH:mm:ss").Substring(0,8);
                            //}
                        }
                    }
                }

                cmd.ExecuteNonQuery();

                for (iFila = 0; iFila <= IntNumeroElementos - 1; iFila++)
                {
                    if (cmd.Parameters[iFila].Direction ==  ParameterDirection.Output)
                    {
                        intIdGenerado = Int64.Parse(cmd.Parameters[iFila].Value.ToString());
                    }
                }  
                
                adapter.SelectCommand = cmd;
                bolOk = true;
                return bolOk;
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return bolOk;
            }
        }
        #endregion StoreEjecutar2
    #endregion StoreEjecutar
    }
}
