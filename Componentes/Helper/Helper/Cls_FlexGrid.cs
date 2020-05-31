using C1.Win.C1FlexGrid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

//using Excel = Microsoft.Office.Interop.Excel;

namespace Helper
{
    public class Cls_FlexGrid
    {
        public int ExportToExcel_NumFilaCabecera = 2;                      // PARA SABER CUANTAS FILAS DE TITULO TIENE CONTROL FLEX
        public bool b_AlternarColor = true;
        Comunes.Funciones FunFunciones = new Comunes.Funciones();
        Genericas o_Generica = new Genericas();

        #region FlexColumnaCombo
        //*********************************************************************************************
        //** NOMBRE      : FlexColumnaCombo
        //** TIPO        : Metodo
        //** DESCRIPCION : Carga un ComboList en una columna especificada del contro FlexGrid
        //** PARAMETROS  : 
        //**               TIPO           |  NOMBRE            | DESCRIPCION
        //**               ----------------------------------------------------------------------------
        //**               C1FlexGrid     |  objControl        | Control FlexGrid al que se le cargara el ComboList
        //**               DataTable      |  dtDatos           | DataTable con los datos que se cargaran al ComboList
        //**               string         |  StrCampoMostrar   | Nombre del campo que se utilizara para cargar los datos
        //**               int            |  intColumnaNumero  | Numero de columna don de se creara el ComboList
        //**
        //** SINTAXIS    : 
        //**               
        //**               
        //** 
        //** DEVUELVE    : 
        //*********************************************************************************************
        public void FlexColumnaCombo(C1.Win.C1FlexGrid.C1FlexGrid objControl, DataTable dtDatos, string StrCampoMostrar, int intColumnaNumero)
        {
            int intFila = 0;
            string strCadena = "";
 
            if (dtDatos.Rows.Count != 0)                              // SI EL NUMERO DE REGISTRO ES DIFERENTE A 0 CONTINUA CON LA CARGA DE DATOS
            {
                // GUARDAMOS EN EL PRIMER REGISTRO
                strCadena = dtDatos.Rows[intFila][StrCampoMostrar].ToString();

                for (intFila = 1; intFila <= dtDatos.Rows.Count - 1; intFila++)
                {
                    strCadena = strCadena + "|" + dtDatos.Rows[intFila][StrCampoMostrar].ToString();
                }
            }
            else
            {
                strCadena = "";
            }

            objControl.Cols[intColumnaNumero].ComboList = strCadena;   // CARGAMOS LOS DATOS EN EL COMBOLIST
        }
        #endregion FlexColumnaCombo

        #region FlexSumarCol
        //*********************************************************************************************
        //** NOMBRE      : FlexColumnaCombo
        //** TIPO        : Metodo
        //** DESCRIPCION : Suma una columna determinada de un FlexGrid 
        //** PARAMETROS  : 
        //**               TIPO           |  NOMBRE            | DESCRIPCION
        //**               ----------------------------------------------------------------------------
        //**               C1FlexGrid     |  objControl        | Control FlexGrid al que se le cargara el ComboList
        //**               int            |  intCol            | Numero de la columa que se sumara
        //**               int            |  intRowIni         | Numero de fila desde donde se iniciara la suma
        //**               int            |  intRowfin         | Numero de fila donde terminara la suma
        //**
        //** SINTAXIS    : 
        //**               
        //**               
        //** 
        //** DEVUELVE    : 
        //*********************************************************************************************
        public double FlexSumarCol(C1.Win.C1FlexGrid.C1FlexGrid objControl, int intCol, int intRowIni, int intRowfin)
        {
            double douResult = 0;
            double douValor = 0;
            int intFila = 0;

            for (intFila = intRowIni; intFila <= intRowfin; intFila++)
            {
                douValor = Convert.ToDouble(FunFunciones.NulosN(objControl.GetData(intFila, intCol)));
                douResult = douResult + douValor;
            }

            return douResult;
        }
        #endregion

        #region FlexSumarRow
        //*********************************************************************************************
        //** NOMBRE      : FlexColumnaCombo
        //** TIPO        : Metodo
        //** DESCRIPCION : Suma una fila determinada de un FlexGrid 
        //** PARAMETROS  : 
        //**               TIPO           |  NOMBRE            | DESCRIPCION
        //**               ----------------------------------------------------------------------------
        //**               C1FlexGrid     |  objControl        | Control FlexGrid al que se le cargara el ComboList
        //**               int            |  intRow            | Numero de la fila que se sumara
        //**               int            |  intColIni         | Numero de columna desde donde se iniciara la suma
        //**               int            |  intColfin         | Numero de columna donde terminara la suma
        //**
        //** SINTAXIS    : 
        //**               
        //**               
        //** 
        //** DEVUELVE    : 
        //*********************************************************************************************
        public double FlexSumarRow(C1.Win.C1FlexGrid.C1FlexGrid objControl, int intRow, int intColIni, int intColfin)
        {
            double douResult = 0;
            double douValor = 0;
            int intColumna = 0;

            for (intColumna = intColIni; intColumna <= intColfin; intColumna++)
            {
                douValor = Convert.ToDouble(objControl.GetData(intRow, intColumna));
                douResult = douResult + douValor;
            }

            return douResult;
        }
        #endregion

        private void AddColumn(string strCaption, string strTipoDato, int intAncho, C1.Win.C1FlexGrid.C1FlexGrid  objControl)
        {
            C1.Win.C1FlexGrid.Column Col = objControl.Cols.Add();
            Col.Name = strCaption;
            Col.Caption = strCaption;
            Col.Width = intAncho;
            Col.TextAlignFixed = TextAlignEnum.CenterCenter;

                if ((strTipoDato == "C") || (strTipoDato == "M")) 
                {
                    Col.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                }

                if (strTipoDato == "N") 
                {
                    Col.Format = "0";
                    Col.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                }

                if (strTipoDato == "D")
                {
                    Col.Format = "0.00";
                    Col.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                }

                if (strTipoDato == "L") 
                {
                    Col.Format = "0";
                    Col.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                }

                if (strTipoDato == "F") 
                {
                    Col.Format = "dd/MM/yyyy";
                    Col.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                }

                if (strTipoDato == "H") 
                {
                    Col.Format = "hh:mm:ss";
                    Col.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                }
        }
        public void FlexMostrarDatos(C1.Win.C1FlexGrid.C1FlexGrid objControl, string[,] strColumnasGrid, DataTable dtDatos)
        { 
            int A;
            int intCol;
            int IntNumeroElementos = Convert.ToInt32(strColumnasGrid.GetLongLength(0)) - 1;
            int intFila;
            int B;
            string c_nomcam = "";
            if (dtDatos.Rows.Count != 0)
            {
                for (A = 0; A <= dtDatos.Rows.Count - 1; A++)
                {
                    objControl.Rows.Count = objControl.Rows.Count + 1;
                    intFila = objControl.Rows.Count - 1;
                    intCol = 1;

                    for (B = 0; B <= IntNumeroElementos; B++)
                    {
                        if (strColumnasGrid[B, 4] != "")
                        {
                            if (o_Generica.DataTableExisteColumna(strColumnasGrid[B, 4], dtDatos) == true)
                            {
                                objControl.SetData(intFila, intCol, dtDatos.Rows[A][strColumnasGrid[B, 4]]);
                            }
                        }
                        else
                        {
                            objControl.SetData(intFila, intCol, "");
                        }
                        intCol = intCol + 1;
                    }
                }
            }
        }
        public void FlexMostrarDatos(C1.Win.C1FlexGrid.C1FlexGrid objControl, string[,] strColumnasGrid, DataTable dtDatos, int intNumeroFilasTitulo, bool booMostrarDatos)
        {
            CellRange rng;

            objControl.Cols.Count = 1;                              // ELIMINAMOS TODAS LAS COLUMNAS
            objControl.Rows.Count = intNumeroFilasTitulo;           // SETEAMOS EL NUMERO DE FILAS = AL NUMERO DE FILAS PARA EL TITULO
            objControl.Rows.Fixed = intNumeroFilasTitulo;           // SETEAMOS EL NUMERO DE FILAS PARA LOS TITULOS
            objControl.Rows.MinSize = 20;
            if (b_AlternarColor == true)
            {
                objControl.Styles.Alternate.BackColor = Color.Beige;    // PONEMOS EL COLOR ALTERNADO
            }
            else
            {
                //objControl.Styles.Alternate.BackColor = Color.White;    // PONEMOS EL COLOR ALTERNADO
            }

            objControl.Cols.Count = 1;                              // ELIMINAMOS TODAS LAS COLUMNAS
            objControl.Rows.Count = intNumeroFilasTitulo;           // SETEAMOS EL NUMERO DE FILAS = AL NUMERO DE FILAS PARA EL TITULO
            objControl.Rows.Fixed = intNumeroFilasTitulo;           // SETEAMOS EL NUMERO DE FILAS PARA LOS TITULOS
            objControl.Rows.MinSize = 20;
            //objControl.Styles.Alternate.BackColor = Color.Beige;    // PONEMOS EL COLOR ALTERNADO

            //objControl.Styles.Normal.WordWrap = true;
            //objControl.AllowMerging = AllowMergingEnum.FixedOnly;
            objControl.AllowEditing = false;
            objControl.Styles.Normal.WordWrap = true;
            objControl.AllowMerging = AllowMergingEnum.FixedOnly;

            objControl.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            int A;
            int intCol;
            int intAncho;
            int IntNumeroElementos = Convert.ToInt32(strColumnasGrid.GetLongLength(0)) - 1;

            #region REGION UNA FILA
            if (intNumeroFilasTitulo == 0)
            {
                intCol = 1;
                for (A = 0; A <= IntNumeroElementos; A++)
                {
                    intAncho = Convert.ToInt16(strColumnasGrid[A, 1]);
                    AddColumn(strColumnasGrid[A, 0], strColumnasGrid[A, 2], intAncho, objControl);  // AGREGAMOS LAS COLUMNAS REQUERIDAS Y LOS PROPIEDADES DE LAS MISMA
                    // HACEMOS EL MERGING
                    objControl.Cols[intCol].AllowMerging = true;
                    //rng = objControl.GetCellRange(0, intCol, intNumeroFilasTitulo - 1, intCol);
                    //rng.Data = strColumnasGrid[A, 0];

                    if (strColumnasGrid[A, 2] == "B")
                    {
                        objControl.Cols[intCol].DataType = typeof(bool);
                    }

                    if (strColumnasGrid[A, 2] == "H")
                    {
                        objControl.Cols[intCol].EditMask = "00:00";
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    }

                    if (strColumnasGrid[A, 2] == "N")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    }
                    if (strColumnasGrid[A, 2] == "D")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    }
                    if (strColumnasGrid[A, 2] == "C")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    }

                    if (strColumnasGrid[A, 2] == "F")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    }

                    intCol = intCol + 1;
                }
            }

            if (intNumeroFilasTitulo == 1)
            {
                intCol = 1;
                for(A=0; A <= IntNumeroElementos;A++)
                {
                    intAncho = Convert.ToInt16(strColumnasGrid[A,1]);
                    AddColumn(strColumnasGrid[A, 0], strColumnasGrid[A, 2], intAncho, objControl);  // AGREGAMOS LAS COLUMNAS REQUERIDAS Y LOS PROPIEDADES DE LAS MISMA
                    // HACEMOS EL MERGING
                    objControl.Cols[intCol].AllowMerging = true;
                    rng = objControl.GetCellRange(0, intCol, intNumeroFilasTitulo - 1, intCol);
                    rng.Data = strColumnasGrid[A, 0];

                    if (strColumnasGrid[A, 2] == "B")
                    {
                        objControl.Cols[intCol].DataType = typeof(bool);
                    }

                    if (strColumnasGrid[A, 2] == "H") 
                    {   
                        objControl.Cols[intCol].EditMask = "00:00";
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    }

                    if (strColumnasGrid[A, 2] == "N") 
                    {   
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    }
                    if (strColumnasGrid[A, 2] == "D")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    }
                    if (strColumnasGrid[A, 2] == "C") 
                    {   
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    }

                    if (strColumnasGrid[A, 2] == "F") 
                    {   
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    }

                    intCol = intCol + 1;
                }
            }
            #endregion

            #region MAS DE DOS FILAS

            if (intNumeroFilasTitulo >= 2)
            {
                intCol = 1;
                for(A = 0; A <= IntNumeroElementos; A++)
                {
                    intAncho = Convert.ToInt16(strColumnasGrid[A, 1]);
                    AddColumn(strColumnasGrid[A, 0], strColumnasGrid[A, 2], intAncho, objControl);  // AGREGAMOS LAS COLUMNAS REQUERIDAS Y LOS PROPIEDADES DE LAS MISMA
                    
                    // COPIAMOS LOS TITULOS EN CADA FILA DE LA COLUMNA
                    rng = objControl.GetCellRange(0, intCol, intNumeroFilasTitulo - 1, intCol);
                    rng.Data = strColumnasGrid[A, 0];

                    // HACEMOS EL MERGING
                    Flex_FixUnirFilas(objControl, intCol, 0, intNumeroFilasTitulo - 1, strColumnasGrid[A, 0], objControl.Cols[intCol].Width);

                    if (strColumnasGrid[A, 2] == "B")
                    {
                        objControl.Cols[intCol].DataType = typeof(bool);
                    }

                    if (strColumnasGrid[A, 2] == "H") 
                    {    
                        objControl.Cols[intCol].DataType = typeof(string);
                        objControl.Cols[intCol].EditMask = "00:00";
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        if (strColumnasGrid[A, 3] != "") 
                        {    
                            objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        }
                    }

                    if (strColumnasGrid[A, 2] == "N") 
                    {
                        objControl.Cols[intCol].DataType = typeof(Int32);
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        if (strColumnasGrid[A, 3] != "" )
                        {    
                            objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        }
                    }
                    if (strColumnasGrid[A, 2] == "D")
                    {
                        objControl.Cols[intCol].DataType = typeof(Double);
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        if (strColumnasGrid[A, 3] != "")
                        {
                            objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        }
                    }

                    if ((strColumnasGrid[A, 2] == "S") || (strColumnasGrid[A, 2] == "C") )
                    {
                        objControl.Cols[intCol].DataType = typeof(string);
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    }

                    if (strColumnasGrid[A, 2] == "F") 
                    {
                        objControl.Cols[intCol].DataType = typeof(DateTime);
                        objControl.Cols[intCol].EditMask = "00/00/0000";
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        if (strColumnasGrid[A, 3] != "") 
                        {    
                            objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        }
                    }

                    intCol = intCol + 1;
                }

                //string c_dato = "";
                //int n_ancho = 0;
                //for (intCol = 1; intCol <= objControl.Cols.Count - 1; intCol++)
                //{
                //    c_dato = objControl.GetData(0, intCol).ToString();
                //    n_ancho = objControl.Cols[intCol].Width;
                //    Flex_UnirFilas(objControl, intCol, 0, intNumeroFilasTitulo - 1, c_dato, n_ancho);
                //}
            }
            #endregion

            int intFila;
            int B;
            string c_nomcam = "";

            //objControl.AllowMerging = AllowMergingEnum.Custom;
            //objControl.AllowMerging = AllowMergingEnum.Free;
            //objControl.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictAll;
            //objControl.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;

            if (booMostrarDatos == true)
            {
                if (dtDatos.Rows.Count != 0)
                {
                    for (A = 0; A <= dtDatos.Rows.Count - 1; A++)
                    {
                        //objControl.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;

                        objControl.Rows.Count = objControl.Rows.Count + 1;
                        //break;
                        intFila = objControl.Rows.Count - 1;
                        //objControl.Rows[intFila].AllowMerging = false;
                        //objControl.Cols[1].AllowMerging = false;
                        //objControl.Cols[2].AllowMerging = false;
                        intCol = 1;

                        for (B = 0; B <= IntNumeroElementos; B++)
                        {
                            if (strColumnasGrid[B, 4] != "")
                            {
                                if (strColumnasGrid[B, 2] != "F")
                                {
                                    if (o_Generica.DataTableExisteColumna(strColumnasGrid[B, 4], dtDatos) == true)
                                    {
                                        objControl.SetData(intFila, intCol, dtDatos.Rows[A][strColumnasGrid[B, 4]].ToString());
                                    }
                                }
                                else
                                {
                                    if (o_Generica.DataTableExisteColumna(strColumnasGrid[B, 4], dtDatos) == true)
                                    {
                                        if (FunFunciones.NulosC(dtDatos.Rows[A][strColumnasGrid[B, 4]]).ToString() != "")
                                        {
                                            objControl.SetData(intFila, intCol, dtDatos.Rows[A][strColumnasGrid[B, 4]].ToString());
                                        }
                                        else
                                        {
                                            objControl.SetData(intFila, intCol, null);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                objControl.SetData(intFila, intCol, "");
                            }
                            intCol = intCol + 1;
                        }
                        //objControl.Rows[intFila].AllowEditing = false;

                        //c_nomcam = strColumnasGrid[13, 4].ToString();
                        //if (c_nomcam == "esneg")
                        //{
                        //    if (dtDatos.Rows[A][strColumnasGrid[13, 4]].ToString() == "S")
                        //    {
                        //        CellStyle cs = objControl.Rows[intFila].StyleNew;
                        //        cs.Font = new Font(objControl.Font.Name, objControl.Font.Size, FontStyle.Bold);
                        //    }
                        //}
                        
                        //c_nomcam = strColumnasGrid[14, 4].ToString();
                        //if (c_nomcam == "esuni")
                        //{
                        //    if (dtDatos.Rows[A][strColumnasGrid[14, 4]].ToString() == "S")
                        //    {
                        //        rng = objControl.GetCellRange(intFila, 1, intFila, IntNumeroElementos);
                        //        rng.Data = dtDatos.Rows[A][strColumnasGrid[0, 4]].ToString();
                        //        objControl.Rows[intFila].AllowMerging = true;
                        //    }
                        //}

                        //c_nomcam = strColumnasGrid[15, 4].ToString();
                        //if (c_nomcam == "escol")
                        //{
                        //    if (dtDatos.Rows[A][strColumnasGrid[15, 4]].ToString() == "azul")
                        //    {
                        //        objControl.Rows[intFila].StyleNew.ForeColor = Color.Blue;
                        //    }
                        //}
                    }
                }

                //objControl.Styles.Normal.WordWrap = true;
                //objControl.AllowMerging = AllowMergingEnum.FixedOnly;

                //objControl.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                //objControl.AllowEditing = false;
            }
            else
            {
                //MessageBox.Show("No se ha econtrado registros para visualizar", "Funciones DATA", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        public void Flex_UniColumnas(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_Fila, int n_Columna1, int n_Columna2, string c_Dato, int n_Alto)
        {
            CellRange rng;
            
            objControl.Rows[n_Fila].AllowEditing = true;
            objControl.AllowMerging=AllowMergingEnum.Free;
            
            rng = objControl.GetCellRange(n_Fila, n_Columna1, n_Fila, n_Columna2);
            rng.Data = c_Dato;
            
            //objControl.Rows[n_Fila].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            C1.Win.C1FlexGrid.Row n_Row = objControl.Rows[n_Fila];
            n_Row.Height = n_Alto;
            //n_Row.TextAlignFixed = TextAlignEnum.CenterCenter;
            n_Row.AllowMerging = true;
            //objControl.AllowMerging= AllowMergingEnum.None;
        }
        public void Flex_UnirFilas(C1.Win.C1FlexGrid.C1FlexGrid objControl,int n_Columna1, int n_Fila1,  int n_Fila2, string c_Dato, int n_Ancho)
        {
            CellRange rng;
            
            objControl.AllowMerging = AllowMergingEnum.Free;
            rng = objControl.GetCellRange(n_Fila1, n_Columna1, n_Fila2, n_Columna1);
            rng.Data = c_Dato;

            C1.Win.C1FlexGrid.Column Col = objControl.Cols[n_Columna1];
            Col.Width = n_Ancho;          
            //Col.TextAlignFixed = TextAlignEnum.CenterCenter;
            //Col.AllowMerging = true;
        }
        public void Flex_UnirCeldas(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_Fila1, int n_Columna1, int n_Fila2, int n_Columna2, string c_Dato, int n_Ancho)
        {
            CellRange rng;

            objControl.AllowMerging = AllowMergingEnum.Free;
            rng = objControl.GetCellRange(n_Fila1, n_Columna1, n_Fila2, n_Columna2);
            rng.Data = c_Dato;

            C1.Win.C1FlexGrid.Column Col = objControl.Cols[n_Columna1];
            Col.Width = n_Ancho;
            //Col.TextAlignFixed = TextAlignEnum.CenterCenter;
            Col.AllowMerging = true;
        }
        public void Flex_FixUnirFilas(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_Columna1, int n_Fila1, int n_Fila2, string c_Dato, int n_Ancho)
        {
            CellRange rng;

            objControl.AllowMerging = AllowMergingEnum.FixedOnly;
            rng = objControl.GetCellRange(n_Fila1, n_Columna1, n_Fila2, n_Columna1);
            //rng = objControl.GetCellRange(n_Columna1, n_Fila1, n_Columna1, n_Fila2);
            rng.Data = c_Dato;

            C1.Win.C1FlexGrid.Column Col = objControl.Cols[n_Columna1];
            Col.Width = n_Ancho;
            Col.AllowMerging = true;
            Col.TextAlignFixed = TextAlignEnum.CenterCenter;
            //Col.TextAlign = TextAlignEnum.CenterCenter;
        }
        public void Flex_FixUniColumnas(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_Fila, int n_Columna1, int n_Columna2, string c_Dato, int n_Alto)
        {
            CellRange rng;

            objControl.AllowMerging = AllowMergingEnum.FixedOnly; 
            rng = objControl.GetCellRange(n_Fila, n_Columna1, n_Fila, n_Columna2);
            rng.Data = c_Dato;

            C1.Win.C1FlexGrid.Row n_Row = objControl.Rows[n_Fila];
            n_Row.Height = n_Alto;
            n_Row.AllowMerging = true;
        }
        public string Flex_ObtenerDatosCol(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_Columna)
        {
            int n_row = 0;
            string c_Cadena = "";

            for (n_row = 2; n_row <= objControl.Rows.Count - 1; n_row++)
            {
                if (n_row != 2) { c_Cadena = c_Cadena + ","; }
                c_Cadena = c_Cadena + objControl.GetData(n_row, n_Columna).ToString();
            }

            return c_Cadena;
        }
        public void FlexMostrarDatos(C1.Win.C1FlexGrid.C1FlexGrid objControl, string[,] strColumnasGrid, object objObjeto, int intNumeroFilasTitulo, bool booMostrarDatos)
        {
            CellRange rng;
            Comunes.Funciones MiFun = new Comunes.Funciones();
            string[,] arrParametrosObj = new string[100, 3];

            objControl.Cols.Count = 1;                              // ELIMINAMOS TODAS LAS COLUMNAS
            objControl.Rows.Count = intNumeroFilasTitulo;           // SETEAMOS EL NUMERO DE FILAS = AL NUMERO DE FILAS PARA EL TITULO
            objControl.Rows.Fixed = intNumeroFilasTitulo;           // SETEAMOS EL NUMERO DE FILAS PARA LOS TITULOS
            objControl.Rows.MinSize = 20;
            objControl.Styles.Alternate.BackColor = Color.Beige;    // PONEMOS EL COLOR ALTERNADO

            objControl.Styles.Normal.WordWrap = true;
            objControl.AllowMerging = AllowMergingEnum.FixedOnly;

            objControl.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            int A;
            int intCol;
            int intAncho;
            int IntNumeroElementos = Convert.ToInt32(strColumnasGrid.GetLongLength(0)) - 1;

            #region REGION UNA FILA
            if (intNumeroFilasTitulo == 1)
            {
                intCol = 1;
                for (A = 0; A <= IntNumeroElementos; A++)
                {
                    intAncho = Convert.ToInt16(strColumnasGrid[A, 1]);
                    AddColumn(strColumnasGrid[A, 0], strColumnasGrid[A, 2], intAncho, objControl);  // AGREGAMOS LAS COLUMNAS REQUERIDAS Y LOS PROPIEDADES DE LAS MISMA
                    // HACEMOS EL MERGING
                    objControl.Cols[intCol].AllowMerging = true;
                    rng = objControl.GetCellRange(0, intCol, intNumeroFilasTitulo - 1, intCol);
                    rng.Data = strColumnasGrid[A, 0];

                    //if (strColumnasGrid[A, 2] == "L") 
                    //{    
                    //    objControl.Cols[intCol].DataType = GetType(Boolean);
                    //}

                    if (strColumnasGrid[A, 2] == "H")
                    {
                        objControl.Cols[intCol].EditMask = "00:00";
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    }

                    if (strColumnasGrid[A, 2] == "N")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    }

                    if (strColumnasGrid[A, 2] == "C")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    }

                    if (strColumnasGrid[A, 2] == "F")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    }

                    intCol = intCol + 1;
                }
            }
            #endregion

            #region MAS DE DOS FILAS

            if (intNumeroFilasTitulo >= 2)
            {
                intCol = 1;
                for (A = 0; A <= IntNumeroElementos; A++)
                {
                    intAncho = Convert.ToInt16(strColumnasGrid[A, 1]);
                    AddColumn(strColumnasGrid[A, 0], strColumnasGrid[A, 2], intAncho, objControl);  // AGREGAMOS LAS COLUMNAS REQUERIDAS Y LOS PROPIEDADES DE LAS MISMA
                    // HACEMOS EL MERGING
                    objControl.Cols[intCol].AllowMerging = true;
                    rng = objControl.GetCellRange(0, intCol, intNumeroFilasTitulo - 1, intCol);
                    rng.Data = strColumnasGrid[A, 0];

                    if (strColumnasGrid[A, 2] == "B")
                    {
                        //objControl.Cols[intCol].DataType = GetType(Boolean);
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    }

                    if (strColumnasGrid[A, 2] == "H")
                    {
                        objControl.Cols[intCol].EditMask = "00:00";
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        if (strColumnasGrid[A, 3] != "")
                        {
                            objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        }
                    }

                    if (strColumnasGrid[A, 2] == "N")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        //if (strColumnasGrid[A, 3] != "")
                        //{
                        //    objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        //}
                    }

                    if (strColumnasGrid[A, 2] == "D")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        if (strColumnasGrid[A, 3] != "")
                        {
                            objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        }
                    }

                    if (strColumnasGrid[A, 2] == "C")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    }

                    if (strColumnasGrid[A, 2] == "F")
                    {
                        objControl.Cols[intCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        if (strColumnasGrid[A, 3] != "")
                        {
                            objControl.Cols[intCol].Format = strColumnasGrid[A, 3];
                        }
                    }

                    intCol = intCol + 1;
                }
            }
            #endregion

            //int intFila;
            //int B;
            if (booMostrarDatos == true)
            {
               
                objControl.DataSource = objObjeto;

                //arrParametrosObj = MiFun.CrearParametros(tipoPersona);

                //if (objObjeto.Count != 0)
                //{
                //    arrParametrosObj = MiFun.CrearParametros(objObjeto[0]);

                //    for (intFila = 0; intFila <= IntNumeroElementos - 1; intFila++)
                //    {

                //    }
                //}
                //if (dtDatos.Rows.Count != 0)
                //{
                //    for (A = 0; A <= dtDatos.Rows.Count - 1; A++)
                //    {
                //        objControl.Rows.Count = objControl.Rows.Count + 1;
                //        intFila = objControl.Rows.Count - 1;
                //        intCol = 1;

                //        for (B = 0; B <= IntNumeroElementos; B++)
                //        {
                //            objControl.SetData(intFila, intCol, dtDatos.Rows[A][strColumnasGrid[B, 4]]);
                //            intCol = intCol + 1;
                //        }
                //    }
                //}
                MessageBox.Show("Los datos se mostraron con exito", "Funciones DATA", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                //MessageBox.Show("No se ha econtrado registros para visualizar", "Funciones DATA", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        public void ExportToExcel(C1.Win.C1FlexGrid.C1FlexGrid objControl, string c_NomEmpresa, string c_NumRUC, string c_Titulo1, string c_Titulo2, string c_NomArchivo)
        {
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            Helper.Comunes.Funciones miFun = new Helper.Comunes.Funciones();

            try
            {
                excel.StandardFont = "Calibri";
                excel.StandardFontSize = 8;

                worksheet = workbook.ActiveSheet;

                worksheet.Name = "ExportedFromDatGrid";

                int n_col = 1;
                int n_row = 1;
                int n_fila = 7;
                int n_columna = 1;
                string c_letra = "";
                string c_rango1 = "";
                string c_rango2 = "";
                string c_dato = "";
                int n_numfixed = ExportToExcel_NumFilaCabecera - 1;  // PARA SABER CUANTAS FILAS DE TITULO TIENE CONTROL FLEX

                Microsoft.Office.Interop.Excel.Range Rango;

                worksheet.Cells[1, 1] = "EMPRESA     :  " + c_NomEmpresa;
                worksheet.Cells[2, 1] = "Nº R.U.C.   :  " + c_NumRUC;

                worksheet.Cells[4, 1] = c_Titulo1;
                worksheet.Cells[5, 1] = c_Titulo2;               

                for (n_row = 0; n_row <= objControl.Rows.Count - 1; n_row++)
                {
                    n_columna = 1;
                    for (n_col = 1; n_col <= objControl.Cols.Count - 1; n_col++)
                    {
                        if (objControl.Cols[n_col].DataType == typeof(string))
                        {
                            worksheet.Cells[n_fila, n_columna] = "'" + objControl.GetData(n_row, n_col);
                        }

                        if (objControl.Cols[n_col].DataType == typeof(DateTime))
                        {
                            //c_dato = objControl.GetData(n_row, n_col).ToString();
                            if (miFun.NulosC(objControl.GetData(n_row, n_col)) != "")
                            {
                                c_dato = objControl.GetData(n_row, n_col).ToString();
                                if (n_row <= n_numfixed)
                                {
                                    worksheet.Cells[n_fila, n_columna] = "'" + miFun.NulosC(c_dato);
                                }
                                else
                                { 
                                    worksheet.Cells[n_fila, n_columna] = "'" + miFun.NulosC(c_dato.Substring(0, 10));
                                }
                            }
                        }

                        if ((objControl.Cols[n_col].DataType == typeof(Int16)) || (objControl.Cols[n_col].DataType == typeof(Int32)))
                        {
                            worksheet.Cells[n_fila, n_columna] = objControl.GetData(n_row, n_col);    
                        }

                        if (objControl.Cols[n_col].DataType == typeof(Double))
                        {
                            worksheet.Cells[n_fila, n_columna] = objControl.GetData(n_row, n_col);    
                        }

                        if (n_row >= (n_numfixed + 1))
                        { 
                            // IMPRIMIMOS EL BORDE DE LA CELDA
                            c_letra = DevolverLetra(n_col);
                            c_rango1 = c_letra + n_fila.ToString();
                        
                            Rango = worksheet.get_Range(c_rango1);
                            Rango.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                                                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                        }
                        n_columna = n_columna + 1;
                    }
                    
                    n_fila = n_fila + 1;
                }

                int n_rowinicab = 0;

                for (n_col = 1; n_col <= objControl.Cols.Count - 1; n_col++)
                {
                    c_letra = DevolverLetra(n_col);
                    c_rango1 = c_letra + "7";
                    n_rowinicab = 7 + n_numfixed;
                    c_rango2 = c_letra + n_rowinicab.ToString();
                    //c_rango2 = c_letra + "8";
                                        
                    Rango = worksheet.get_Range(c_rango1, c_rango2);
                    Rango.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    Rango.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    Rango.Font.Size = 8;
                    Rango.Font.Bold = true;
                                      
                    workbook.Sheets[1].cells[7, n_col].ColumnWidth = objControl.Cols[n_col].Width / 5;

                    c_letra = DevolverLetra(n_col);
                    c_rango1 = c_letra + "7";
                    n_rowinicab = 7 + n_numfixed;
                    c_rango2 = c_letra + n_rowinicab.ToString();

                    Rango = worksheet.get_Range(c_rango1, c_rango2);
                    //Rango.Merge(false);
                    Rango.HorizontalAlignment = 3;
                    Rango.VerticalAlignment = 2;

                    Rango.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                                        Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                }

                c_letra = DevolverLetra(1);
                c_rango1 = c_letra + "7";
                c_letra = DevolverLetra(objControl.Cols.Count - 1);
                n_rowinicab = 7 + n_numfixed;
                c_rango2 = c_letra + n_rowinicab.ToString();

                // IMPRIMIMOS EL BORDE DE LA CABECERA
                Rango = worksheet.get_Range(c_rango1, c_rango2);
                Rango.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;
                //string c_nomarch = Directory.GetCurrentDirectory()+"\\" + "almacen-movimientos-"+DateTime.Now.ToString("dd-MM-yyyy")+".xls" ;
                string c_nomarch = Directory.GetCurrentDirectory() + "\\" + c_NomArchivo;
                
                workbook.SaveAs(@c_nomarch);
                MessageBox.Show("Los datos se exportaron con exito, se creo el archivo: "+ c_nomarch,"HELPER - Exportacion a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                workbook.Close();
                

                string filename = "Excel.exe";

                Process proc = new Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = filename;
                proc.StartInfo.Arguments = c_nomarch;
                proc.Start();

                return;
            }
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }
        string DevolverLetra(int Columna)
        {
            string c_letra = "";
            if (Columna == 1) { c_letra = "a"; }
            if (Columna == 2) { c_letra = "b"; }
            if (Columna == 3) { c_letra = "c"; }
            if (Columna == 4) { c_letra = "d"; }
            if (Columna == 5) { c_letra = "e"; }
            if (Columna == 6) { c_letra = "f"; }
            if (Columna == 7) { c_letra = "g"; }
            if (Columna == 8) { c_letra = "h"; }
            if (Columna == 9) { c_letra = "i"; }
            if (Columna == 10) { c_letra = "j"; }
            if (Columna == 11) { c_letra = "k"; }
            if (Columna == 12) { c_letra = "l"; }
            if (Columna == 13) { c_letra = "m"; }
            if (Columna == 14) { c_letra = "n"; }
            if (Columna == 15) { c_letra = "o"; }
            if (Columna == 16) { c_letra = "p"; }
            if (Columna == 17) { c_letra = "q"; }
            if (Columna == 18) { c_letra = "r"; }
            if (Columna == 19) { c_letra = "s"; }
            if (Columna == 20) { c_letra = "t"; }
            if (Columna == 21) { c_letra = "u"; }
            if (Columna == 22) { c_letra = "v"; }
            if (Columna == 23) { c_letra = "w"; }
            if (Columna == 24) { c_letra = "x"; }
            if (Columna == 25) { c_letra = "y"; }
            if (Columna == 26) { c_letra = "z"; }
            if (Columna == 27) { c_letra = "aa"; }
            if (Columna == 28) { c_letra = "ab"; }
            if (Columna == 29) { c_letra = "ac"; }
            if (Columna == 30) { c_letra = "ad"; }
            if (Columna == 31) { c_letra = "ae"; }
            if (Columna == 32) { c_letra = "af"; }
            if (Columna == 33) { c_letra = "ag"; }
            if (Columna == 34) { c_letra = "ah"; }
            if (Columna == 35) { c_letra = "ai"; }
            if (Columna == 36) { c_letra = "aj"; }
            if (Columna == 37) { c_letra = "ak"; }
            if (Columna == 38) { c_letra = "al"; }
            if (Columna == 39) { c_letra = "am"; }
            if (Columna == 40) { c_letra = "an"; }
            if (Columna == 41) { c_letra = "ao"; }
            if (Columna == 42) { c_letra = "ap"; }
            if (Columna == 43) { c_letra = "aq"; }
            if (Columna == 44) { c_letra = "ar"; }
            if (Columna == 45) { c_letra = "as"; }
            if (Columna == 46) { c_letra = "at"; }
            if (Columna == 47) { c_letra = "au"; }
            if (Columna == 48) { c_letra = "av"; }
            if (Columna == 49) { c_letra = "aw"; }
            if (Columna == 50) { c_letra = "ax"; }
            if (Columna == 51) { c_letra = "ay"; }
            if (Columna == 52) { c_letra = "az"; }
            return c_letra;
        }
        public void Flex_PintarCeldas(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_Fila, int n_Columna, int n_Fila2, int n_Columna2, Color n_ForeColor, Color n_BackColor)
        {
            if (n_Fila2 > n_Fila)
            {
                CellRange rg;

                rg = objControl.GetCellRange(n_Fila, n_Columna, n_Fila2, n_Columna2);
                rg.StyleNew.BackColor = n_BackColor;
                rg.StyleNew.ForeColor = n_ForeColor;
            }
        }
        public void Flex_PintarCeldas(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_Fila, int n_Columna, int n_Fila2, int n_Columna2, Color n_ForeColor)
        {
            CellRange rg;

            rg = objControl.GetCellRange(n_Fila, n_Columna, n_Fila2, n_Columna2);
            rg.StyleNew.ForeColor = n_ForeColor;
        }
        public string Flex_CadenaIN(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_ColumnaBusqueda, int n_FilaInicio)
        {
            string c_cadin = "";
            int n_row = 1;
            string c_cad = "";

            for (n_row = n_FilaInicio; n_row <= objControl.Rows.Count - 1; n_row++)
            {
                c_cad = FunFunciones.NulosC(objControl.GetData(n_row, n_ColumnaBusqueda));

                if (c_cad != "")
                {
                    if (n_row == n_FilaInicio)
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
        public string Flex_CadenaIN(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_ColumnaBusqueda, int n_FilaInicio, int n_ColumnaCheck)
        {
            string c_cadin = "";
            int n_row = 1;
            string c_cad = "";
            int n_ini = 0;

            for (n_row = n_FilaInicio; n_row <= objControl.Rows.Count - 1; n_row++)
            {
                if (FunFunciones.NulosC(objControl.GetData(n_row, n_ColumnaCheck)) == "True")
                { 
                    c_cad = FunFunciones.NulosC(objControl.GetData(n_row, n_ColumnaBusqueda));

                    if (c_cad != "")
                    {
                        if (n_ini == 0)
                        {
                            if (c_cad != "")
                            {
                                c_cadin = c_cadin + c_cad;
                                n_ini = n_ini + 1;
                            }
                        }
                        else
                        {
                            if (c_cad != "")
                            {
                                c_cadin = c_cadin + ", " + c_cad;
                                n_ini = n_ini + 1;
                            }
                        }
                    }
                }
            }
            return c_cadin;
        }
        public string Flex_CadenaIN_Fila(C1.Win.C1FlexGrid.C1FlexGrid objControl, int n_FilaBusqueda, int n_ColumnaInicio, int n_ColumnaFinal)
        {
            string c_cadin = "";
            int n_row = 1;
            int n_col = 1;
            string c_cad = "";

            for (n_col = n_ColumnaInicio; n_col <= n_ColumnaFinal; n_col++)
            {
                c_cad = FunFunciones.NulosC(objControl.GetData(n_FilaBusqueda, n_col));
                if (c_cad != "")
                {
                    if (n_col == n_ColumnaInicio)
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
    }
}
