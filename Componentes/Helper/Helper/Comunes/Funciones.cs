using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace Helper.Comunes
{
    public class Funciones
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        #region NulosN
        //*********************************************************************************************
        //** NOMBRE      : NulosN
        //** TIPO        : Funcion
        //** DESCRIPCION : Devuelve sin un objeto es Nulo
        //** PARAMETROS  : 
        //**               TIPO       |  NOMBRE          | DESCRIPCION
        //**               ----------------------------------------------
        //**               object     |  ObjObjeto       | Objeto a eveluar
        //** 
        //** DEVUELVE    : object
        //*********************************************************************************************
        public object NulosN(object ObValor)
        {
            object objValorretorno = 0;
            if ((ObValor == null) || (ObValor.ToString() == "") || (ObValor.ToString() == "0.00"))
            {
                objValorretorno = 0;
            }
            else
            {
                objValorretorno = ObValor;
            }
            //double d;

            //if (ObValor.ToString() == "") { return 0;}

            //if (ObValor.ToString() != "") 
            //{
            //    objValorretorno = ObValor;
            //    //if (intTipoDato == 1) objValorretorno = Convert.ToInt16(ObValor);
            //    //if (intTipoDato == 2) objValorretorno = Convert.ToInt32(ObValor);
            //    //if (intTipoDato == 3) objValorretorno = Convert.ToInt64(ObValor);
            //    //if (intTipoDato == 4) objValorretorno = Convert.ToDouble(ObValor);
            //    //if (intTipoDato == 5) objValorretorno = Convert.ToDecimal(ObValor);
            //}

            return objValorretorno;
        }
        #endregion NulosN

        #region NulosC
        //*********************************************************************************************
        //** NOMBRE      : NulosC
        //** TIPO        : Funcion
        //** DESCRIPCION : Devuelve comillas simples si un objeto es Nulo
        //** PARAMETROS  : 
        //**               TIPO       |  NOMBRE          | DESCRIPCION
        //**               ----------------------------------------------
        //**               object     |  ObjObjeto       | Objeto a eveluar
        //** 
        //** DEVUELVE    : string
        //*********************************************************************************************
        public string NulosC(object ObValor)
        {
            if (ObValor == null)
            {
                return "";
            }
            else
            {
                return ObValor.ToString().Trim();
            }
        }
        #endregion NulosN

        #region CadenaBuscar
        //*********************************************************************************************
        //** NOMBRE      : CadenaBuscar
        //** TIPO        : Funcion
        //** DESCRIPCION : Devuelve una cadena cntenida dentro de otra
        //** PARAMETROS  : 
        //**               TIPO       |  NOMBRE             | DESCRIPCION
        //**               ----------------------------------------------
        //**               string     |  strCadenaBuscar    | Cadena donde se efectuara la busqueda
        //**               string     |  strCadenaEncontrar | Cadena a buscar
        //** 
        //** DEVUELVE    : string
        //*********************************************************************************************
        public string CadenaBuscar(string strCadenaBuscar, string strCadenaEncontrar)
        {
            string strResultado = "";

            if ((strCadenaBuscar != "") || (strCadenaEncontrar != "")) { 
                int first = strCadenaBuscar.IndexOf(strCadenaEncontrar); 

                if (first > 1) { strResultado = "SYSTEM." + strCadenaEncontrar;}
            }
            return strResultado;
        }
        #endregion CadenaBuscar

        #region CrearParametros
        //*********************************************************************************************
        //** NOMBRE      : CrearParametros
        //** TIPO        : Funcion
        //** DESCRIPCION : Devuelve devuelve un arrar de paramentos para un store
        //** PARAMETROS  : 
        //**               TIPO       |  NOMBRE          | DESCRIPCION
        //**               ----------------------------------------------
        //**               object     |  ObjObjeto       | Objeto a evaluar
        //** 
        //** DEVUELVE    : string[,]
        //*********************************************************************************************
        public string[,] CrearParametros(object objEntidad)
        {

            int iFila = 0;
            int iNumFilas = 0;
            var ent = objEntidad.GetType().GetProperties();
            string sTipoDato = "";

            //Obtengo el objeto anónimo
            var persona = objEntidad;

            //Obtengo el tipo del objeto anónimo
            Type tipoPersona = persona.GetType();

            //Obtengo todas las propiedades del objeto anónimo
            PropertyInfo[] propiedades = tipoPersona.GetProperties();
            //Para cada propiedad...

            foreach (PropertyInfo propiedad in propiedades)
            {
                iNumFilas = iNumFilas + 1;
            }

            string[,] arrParametros = new string[iNumFilas, 3];

            foreach (PropertyInfo propiedad in propiedades)
            {
                //... creo un objeto con el valor de la propiedad...
                object valorPropiedadActual = propiedad.GetValue(persona, null);
                //... y muestro la información por pantalla
                //MessageBox.Show( propiedad.Name +"  " + valorPropiedadActual.ToString());

                if (sTipoDato == "") { sTipoDato = CadenaBuscar(propiedad.PropertyType.FullName.ToUpper(), "INT32"); }
                if (sTipoDato == "") { sTipoDato = CadenaBuscar(propiedad.PropertyType.FullName.ToUpper(), "INT16"); }
                if (sTipoDato == "") { sTipoDato = CadenaBuscar(propiedad.PropertyType.FullName.ToUpper(), "INT64"); }
                if (sTipoDato == "") { sTipoDato = CadenaBuscar(propiedad.PropertyType.FullName.ToUpper(), "STRING"); }
                if (sTipoDato == "") { sTipoDato = CadenaBuscar(propiedad.PropertyType.FullName.ToUpper(), "DATETIME"); }
                if (sTipoDato == "") { sTipoDato = CadenaBuscar(propiedad.PropertyType.FullName.ToUpper(), "DOUBLE"); }
                if (sTipoDato == "") { sTipoDato = CadenaBuscar(propiedad.PropertyType.FullName.ToUpper(), "BYTE"); }

                if (valorPropiedadActual == null)
                {
                    arrParametros[iFila, 0] = "@" + propiedad.Name; arrParametros[iFila, 1] = sTipoDato; arrParametros[iFila, 2] = "null";
                }
                else
                {
                    if (sTipoDato == "SYSTEM.DATETIME")
                    {
                        arrParametros[iFila, 0] = "@" + propiedad.Name; 
                        arrParametros[iFila, 1] = sTipoDato; 
                        arrParametros[iFila, 2] = NulosC(valorPropiedadActual.ToString());
                    }
                    else 
                    {
                        if (sTipoDato == "SYSTEM.BYTE")
                        {
                             arrParametros[iFila, 0] = "@" + propiedad.Name; arrParametros[iFila, 1] = sTipoDato; arrParametros[iFila, 2] = NulosC(valorPropiedadActual);
                        }
                        else
                        { 
                            arrParametros[iFila, 0] = "@" + propiedad.Name; arrParametros[iFila, 1] = sTipoDato; arrParametros[iFila, 2] = NulosC(valorPropiedadActual);
                        }
                    }
                }
                iFila = iFila + 1;
                sTipoDato = "";
            }
            return arrParametros;
        }
        #endregion CrearParametros

        #region Mensajes de Error
        public void MensajeMostrarAviso(string strMensaje, string strSistemaTitulo)
        {
            MessageBox.Show(strMensaje, strSistemaTitulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public void MensajeMostrarPregunta(string strMensaje, string strSistemaTitulo)
        {
            MessageBox.Show(strMensaje, strSistemaTitulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public  void MensajeMostrarPeligro(string strMensaje, string strSistemaTitulo)
        {
            MessageBox.Show(strMensaje, strSistemaTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public  void MensajeMostrarAlto(string strMensaje, string strSistemaTitulo)
        {
            MessageBox.Show(strMensaje, strSistemaTitulo, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public  void MensajeMostrarError(string strMensaje, string strSistemaTitulo)
        {
            MessageBox.Show(strMensaje, strSistemaTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion Mensajes de Error
        public DateTime FechaSumarDias(DateTime datfecha, int intNumeroDias)
        {
            DateTime datResult = datfecha;
            datResult = datResult.AddDays(intNumeroDias);
            return datResult;
        }
        public bool EsFecha(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string f_Hora(object d_fechahora)
        {
            string c_cadena;
            c_cadena = "";

            if(d_fechahora != null)
            {
                DateTime dFecha = Convert.ToDateTime(d_fechahora);
                c_cadena = String.Format("{0:HH:mm}", dFecha);
            }
            //DateTime dt = new DateTime(2008, 3, 9, 16, 5, 7, 123);

            //String.Format("{0:y yy yyy yyyy}", dt);  // "8 08 008 2008"   year
            //String.Format("{0:M MM MMM MMMM}", dt);  // "3 03 Mar March"  month
            //String.Format("{0:d dd ddd dddd}", dt);  // "9 09 Sun Sunday" day
            //String.Format("{0:h hh H HH}", dt);  // "4 04 16 16"      hour 12/24
            //String.Format("{0:m mm}", dt);  // "5 05"            minute
            //String.Format("{0:s ss}", dt);  // "7 07"            second
            //String.Format("{0:f ff fff ffff}", dt);  // "1 12 123 1230"   sec.fraction
            //String.Format("{0:F FF FFF FFFF}", dt);  // "1 12 123 123"    without zeroes
            //String.Format("{0:t tt}", dt);  // "P PM"            A.M. or P.M.
            //String.Format("{0:z zz zzz}", dt);  // "-6 -06 -06:00"   time zone

            return c_cadena;
        }
        public bool ArchivoCopiar(string c_Origen, string c_RutaDestino, string c_NuevoNombre)
        {
            bool boo_Result = false;
            string c_archdestino = "";

            try
            {
                c_archdestino = c_RutaDestino + c_NuevoNombre;
                // VERIFICAMOSD QUE EL ARCHIVO DESTINO NO EXISTA
                if (System.IO.File.Exists(c_archdestino) == false)
                {
                    // COPIAMOS EL NUEVO ARCHIVO Y LO RENOMBRAMOS
                    System.IO.File.Copy(c_Origen, c_archdestino);
                    boo_Result = true;
                }
                else
                {
                    DialogResult Rpta = MessageBox.Show("El archivo de de destino : " + c_archdestino + " ya existe, ¿ Desea Reemplazarlo? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (DialogResult.Yes == Rpta)
                    {
                        // ELIMINAMOS EL ARCHIVO DESTINO EXISTENTE
                        System.IO.File.Delete(c_archdestino);
                        // COPIAMOS EL NUEVO ARCHIVO Y LO RENOMBRAMOS
                        System.IO.File.Copy(c_Origen, c_archdestino);
                        boo_Result = true;
                    }
                }
                return boo_Result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS LA CONECCION CERRADA
                booOcurrioError = true;
                StrErrorMensaje = exc.Message;
                IntErrorNumber = exc.HResult;
                return boo_Result;
            }
        }
        public bool ArchivoEliminar(string c_Origen)
        {
            bool boo_Result = false;
            try
            {
                // VERIFICAMOSD QUE EL ARCHIVO ORIGEN NO EXISTA
                if (System.IO.File.Exists("c_Origen") == true)
                {
                    // COPIAMOS EL NUEVO ARCHIVO Y LO RENOMBRAMOS
                    System.IO.File.Delete(c_Origen);
                    boo_Result = true;
                }
                else
                {
                    MessageBox.Show("El archivo : " + c_Origen + " no existe, ¡ Indique otro ! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                return boo_Result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS LA CONECCION CERRADA
                booOcurrioError = true;
                StrErrorMensaje = exc.Message;
                IntErrorNumber = exc.HResult;
                return boo_Result;
            }
        }
        public bool ArchivoExiste(string c_Origen)
        {
            bool boo_Result = false;
            try
            {
                // VERIFICAMOSD QUE EL ARCHIVO ORIGEN NO EXISTA
                if (System.IO.File.Exists(c_Origen) == true)
                {
                    boo_Result = true;
                }

                return boo_Result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS LA CONECCION CERRADA
                booOcurrioError = true;
                StrErrorMensaje = exc.Message;
                IntErrorNumber = exc.HResult;
                return boo_Result;
            }
        }
        public bool EsNumerico(string c_Numero)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(c_Numero), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }
        public bool EsEntero(string c_Numero)
        {
            try
            {
                Int32.Parse(c_Numero);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EsHora(string c_Hora)
        {
            bool isHora = true;
            int hh = Convert.ToInt32(c_Hora.Substring(0, 2));
            int mm = Convert.ToInt32(c_Hora.Substring(3, 2));

            if (hh > 24)
            {
                isHora = false;
                return isHora;
            }
            if (mm > 60)
            {
                isHora = false;
                return isHora;
            }

            return isHora;
        }
        public int Fecha_Intervalo(string c_FechaInicio, string c_FechaFinal)
        {
            int n_numdias = 0;
            DateTime d_fchini = Convert.ToDateTime(c_FechaInicio);
            DateTime d_fchfin = Convert.ToDateTime(c_FechaFinal);
            TimeSpan ts = d_fchfin - d_fchini;
            n_numdias =ts.Days + 1;
            return n_numdias;
        }
        public string FechaRestarDias(string c_Fecha, int n_NumeroDias)
        {
            DateTime d_fecha = Convert.ToDateTime(c_Fecha).AddDays(-n_NumeroDias);
            //DateTime.Today.AddDays(-20);
            return d_fecha.ToString("dd/MM/yyyy");
        }
        public bool Obj_ValidarError(bool b_OcurrioEror, string c_MensajeError)
        {
            bool b_Result = false;

            if (b_OcurrioEror == true)
            {
                MessageBox.Show("¡ Ha ocurrido el siguiente error: " + c_MensajeError + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                b_Result = true;
            }
            return b_Result;
        }
        public double PostivoNegativo(double n_ValorPositivo)
        {
            double n_Valor = 0;
            n_Valor = (n_ValorPositivo * 2);
            n_Valor = (n_ValorPositivo - n_Valor);
            return n_Valor;
        }
        public string HorasRestar(string c_HoraInicio, string c_HoraFinal)
        {
            string c_hora = "";

            if (c_HoraInicio == "") { return c_hora; }
            if (c_HoraFinal == "") { return c_hora; }
            
            if ((c_HoraInicio == "") && (c_HoraFinal ==""))
            {
                return c_hora;
            }
            DateTime h_horini = Convert.ToDateTime(c_HoraInicio);
            DateTime h_horfin = Convert.ToDateTime(c_HoraFinal);

            Helper.Comunes.Convertir funcon = new Helper.Comunes.Convertir();

            if (funcon.HoraEnDecimal(c_HoraInicio) > funcon.HoraEnDecimal(c_HoraFinal))
            {
                h_horfin = h_horfin.AddDays(1);
            }

            string c_hor = (h_horfin - h_horini).Hours.ToString("00");
            string c_min = (h_horfin - h_horini).Minutes.ToString("00");
            string c_seg = (h_horfin - h_horini).Seconds.ToString("00");
            c_hora = c_hor + ":" + c_min + ":" + c_seg;
            return c_hora;
        }
        public System.Drawing.Image Convertir_Bytes_Imagen(byte[] bytes)
        {
            if (bytes == null) return null;

            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bm = null;
            try
            {
                bm = new Bitmap(ms);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return bm;
        }
        public byte[] Convertir_Imagen_Bytes(System.Drawing.Image img)
        {
            if (img == null) { return null; }

            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }
        public string Horas_IntervaloDias(string c_HoraInicio, string HoraFinal)
        {
            string c_tiempo = "";
            TimeSpan diffTime = Convert.ToDateTime(HoraFinal) - Convert.ToDateTime(c_HoraInicio);
            //int days = diffTime.Days;
            //int hours = diffTime.Hours;
            //int minutes = diffTime.Minutes;
            //int seconds = diffTime.Seconds;
            int n_numhoradia = (diffTime.Days*24);
            c_tiempo = (diffTime.Hours + n_numhoradia).ToString("00") + ":" + diffTime.Minutes.ToString("00") + ":" + diffTime.Seconds.ToString("00");
            return c_tiempo;
        }
        public string ConvertirTitulo(string c_Cadena)
        {
            TextInfo ti = new CultureInfo("en-US", false).TextInfo;  //CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(c_Cadena.ToLower());
        }
    }
}
