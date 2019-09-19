using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Helper
{
    public class Cls_IO
    {
        public string c_err_mensaje;
        public int n_err_numero;

        public void DataTableToCVS(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                IEnumerable<String> headerValues = sourceTable.Columns
                    .OfType<DataColumn>()
                    .Select(column => QuoteValue(column.ColumnName));

                writer.WriteLine(String.Join(",", headerValues));
            }

            IEnumerable<String> items = null;

            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString()));
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
        }
        private static string QuoteValue(string value)
        {
            return String.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }
        public bool Fil_GenerarTxt(DataTable dtDatos, string c_NombreArchivo, string c_RutaDestino, int n_RestarColumnas)
        {
            bool b_result = false;
            int n_row = 0;
            int n_col = 0;
            string c_linea = "";
            string c_arch = c_RutaDestino + "\\" + c_NombreArchivo;
            Helper.Comunes.Funciones funfun = new Helper.Comunes.Funciones();
            try 
            {
                StreamWriter oSW = new StreamWriter(c_arch);

                for (n_row = 0; n_row <= dtDatos.Rows.Count - 1; n_row++)
                {
                    c_linea = "";
                    for (n_col = 0; n_col <= dtDatos.Columns.Count - (n_RestarColumnas + 1); n_col++)
                    {
                        if (n_col == 0)
                        {
                            if (dtDatos.Columns[n_col].DataType.Name == "DateTime")
                            {
                                if (funfun.NulosC(dtDatos.Rows[n_row][n_col]) != "")
                                {
                                    c_linea = dtDatos.Rows[n_row][n_col].ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                c_linea = dtDatos.Rows[n_row][n_col].ToString();
                            }
                        }
                        else
                        {
                            if (dtDatos.Columns[n_col].DataType.Name == "DateTime")
                            {
                                if (funfun.NulosC(dtDatos.Rows[n_row][n_col]) != "")
                                { 
                                    c_linea = c_linea + "|" + dtDatos.Rows[n_row][n_col].ToString().Substring(0,10);
                                }
                            }
                            else
                            { 
                                c_linea = c_linea + "|" + dtDatos.Rows[n_row][n_col].ToString();
                            }
                        }
                    }
                    c_linea = c_linea + "|";
                    oSW.WriteLine(c_linea);
                }
                oSW.Flush();
                oSW.Close();
                b_result = true;
                return b_result;
            }
            catch (Exception e)
            {
                c_err_mensaje = e.Message;
                n_err_numero = e.HResult;
                return b_result;
            }
        }
        public bool Fil_EliminarArchivo(string c_Archivo)
        {
            bool b_result = false;
            try
            {
                if (System.IO.File.Exists(c_Archivo))             // PREGUNTAMOS SI EXISTE EL ARCHIVO
                {
                    System.IO.File.Delete(c_Archivo);              // ELIMINAMOS EL ARCHIVO
                }
                else
                {
                    c_err_mensaje = "Archivo no exite";
                    n_err_numero = 1;
                    return b_result;
                }
                b_result = true;
                return b_result;
            }
            catch (Exception ex)
            {
                c_err_mensaje = ex.Message;
                n_err_numero = ex.HResult;
                return b_result;
            }
        }
        public bool Fil_CopiarArchivo(string c_ArchivoOrigen, string c_CarpetaDestino)
        {
            bool b_result = false;
//            string System.IO.P
            string c_archivo = Path.GetFileName(c_ArchivoOrigen);
            try
            {
                if (System.IO.File.Exists(c_ArchivoOrigen))             // PREGUNTAMOS SI EXISTE EL ARCHIVO ORIGINAL
                {
                    if (System.IO.Directory.Exists(c_CarpetaDestino))   // PREGUNTAMOS SI LA CARPETA DESTINO EXISTE
                    {
                        System.IO.File.Copy(c_ArchivoOrigen, c_CarpetaDestino + "\\" + c_archivo, true);  // COPIAMOS EL ARCHIVO
                    }
                    else
                    {
                        c_err_mensaje = "Directorio destino no existe";
                        n_err_numero = 2;
                        return b_result;
                    }
                }
                else
                {
                    c_err_mensaje = "Archivo origen no exite";
                    n_err_numero = 1;
                    return b_result;
                }
                b_result = true;
                return b_result;
            }
            catch (Exception ex)
            {
                c_err_mensaje = ex.Message;
                n_err_numero = ex.HResult;
                return b_result;
            }
        }
        public bool Zip_Descomprimir(string c_RutaArchivoComprimido, string c_RutaDestino, string c_TipoArchivo)
        {
            bool b_result = false;
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(c_RutaArchivoComprimido))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(c_TipoArchivo, StringComparison.OrdinalIgnoreCase))
                        {
                            entry.ExtractToFile(Path.Combine(c_RutaDestino, entry.FullName));
                        }
                    }
                }
                
                b_result = true;
                return b_result;
            }
            catch (Exception ex)
            {
                c_err_mensaje = ex.Message;
                n_err_numero = ex.HResult;
                return b_result;
            }
        }
        public string[] Dir_LeerDirectorio(string c_Ruta, string c_TiposdeArchivo)
        {
            string[] c_ListaArchivos = new string[] { };

            try
            {
                DirectoryInfo directory = new DirectoryInfo(c_Ruta);
                FileInfo[] files = directory.GetFiles(c_TiposdeArchivo);

                Array.Resize(ref c_ListaArchivos, files.Length);
                //DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    c_ListaArchivos[i]= ((FileInfo)files[i]).FullName;

                    //Console.WriteLine(((FileInfo)files[i]).FullName);
                }
                //for (int i = 0; i < directories.Length; i++)
                //{
                //    Console.WriteLine(((DirectoryInfo)directories[i]).FullName);
                //}

                return c_ListaArchivos;
            }
            catch (Exception ex)
            {
                c_err_mensaje = ex.Message;
                n_err_numero = ex.HResult;
                return c_ListaArchivos;
            }
        }
        public string[] LeerLineaArchivo(string c_NombreArchivo)
        {
            int n_fil = 0;
            string line = "";
            string[] arrDatos = new string[200];

            try
            {
                using (StreamReader sr = new StreamReader(c_NombreArchivo, false))
                {
                    n_fil = 0;
                    while ((line = sr.ReadLine()) != null)
                    {

                        arrDatos[n_fil] = line;
                        n_fil = n_fil + 1;
                    }
                }
                return arrDatos;
            }
            catch (Exception)
            {
                Console.WriteLine("El archivo no se puede leer");
                return null;
            }
        }
    }
}
