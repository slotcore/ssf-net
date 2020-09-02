using System;
using System.Drawing;
using System.Data;
using System.IO;
using System.Diagnostics;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Windows.Forms;

namespace Helper
{
    public class Cls_DBGrid
    {
        private void DataTable_To_Excel(DataTable pDatos, string pFilePath)
        {
            try
            {
                if (pDatos != null && pDatos.Rows.Count > 0)
                {
                    IWorkbook workbook = null;
                    ISheet worksheet = null;

                    using (FileStream stream = new FileStream(pFilePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        string Ext = System.IO.Path.GetExtension(pFilePath); //<-Extension del archivo
                        switch (Ext.ToLower())
                        {
                            case ".xls":
                                HSSFWorkbook workbookH = new HSSFWorkbook();
                                NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                                dsi.Company = "Cutcsa"; dsi.Manager = "Departamento Informatico";
                                workbookH.DocumentSummaryInformation = dsi;
                                workbook = workbookH;
                                break;

                            case ".xlsx": workbook = new XSSFWorkbook(); break;
                        }

                        worksheet = workbook.CreateSheet(pDatos.TableName); //<-Usa el nombre de la tabla como nombre de la Hoja

                        //CREAR EN LA PRIMERA FILA LOS TITULOS DE LAS COLUMNAS
                        int iRow = 0;
                        if (pDatos.Columns.Count > 0)
                        {
                            int iCol = 0;
                            IRow fila = worksheet.CreateRow(iRow);
                            foreach (DataColumn columna in pDatos.Columns)
                            {
                                ICell cell = fila.CreateCell(iCol, CellType.String);
                                cell.SetCellValue(columna.ColumnName);
                                iCol++;
                            }
                            iRow++;
                        }

                        //FORMATOS PARA CIERTOS TIPOS DE DATOS
                        //ICellStyle _doubleCellStyle = workbook.CreateCellStyle();
                        //_doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.###");

                        //ICellStyle _intCellStyle = workbook.CreateCellStyle();
                        //_intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0");

                        //ICellStyle _boolCellStyle = workbook.CreateCellStyle();
                        //_boolCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("BOOLEAN");

                        ICellStyle _dateCellStyle = workbook.CreateCellStyle();
                        _dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd/MM/yyyy");

                        ICellStyle _dateTimeCellStyle = workbook.CreateCellStyle();
                        _dateTimeCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-MM-yyyy HH:mm:ss");

                        //AHORA CREAR UNA FILA POR CADA REGISTRO DE LA TABLA
                        foreach (DataRow row in pDatos.Rows)
                        {
                            IRow fila = worksheet.CreateRow(iRow);
                            int iCol = 0;
                            foreach (DataColumn column in pDatos.Columns)
                            {
                                ICell cell = null; //<-Representa la celda actual                               
                                object cellValue = row[iCol]; //<- El valor actual de la celda

                                switch (column.DataType.ToString())
                                {
                                    case "System.Boolean":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Boolean);

                                            if (Convert.ToBoolean(cellValue)) { cell.SetCellFormula("TRUE()"); }
                                            else { cell.SetCellFormula("FALSE()"); }

                                            //cell.CellStyle = _boolCellStyle;
                                        }
                                        break;

                                    case "System.String":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.String);
                                            cell.SetCellValue(Convert.ToString(cellValue));
                                        }
                                        break;

                                    case "System.Int32":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToInt32(cellValue));
                                            //cell.CellStyle = _intCellStyle;
                                        }
                                        break;
                                    case "System.Int64":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToInt64(cellValue));
                                            //cell.CellStyle = _intCellStyle;
                                        }
                                        break;
                                    case "System.Decimal":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToDouble(cellValue));
                                            //cell.CellStyle = _doubleCellStyle;
                                        }
                                        break;
                                    case "System.Double":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToDouble(cellValue));
                                            //cell.CellStyle = _doubleCellStyle;
                                        }
                                        break;

                                    case "System.DateTime":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToDateTime(cellValue));

                                            //Si No tiene valor de Hora, usar formato dd-MM-yyyy
                                            DateTime cDate = Convert.ToDateTime(cellValue);
                                            if (cDate != null && cDate.Hour > 0) { cell.CellStyle = _dateTimeCellStyle; }
                                            else { cell.CellStyle = _dateCellStyle; }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                iCol++;
                            }
                            iRow++;
                        }

                        workbook.Write(stream);
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DG_FormatearGrid(C1.Win.C1TrueDBGrid.C1TrueDBGrid xDg, string[,]  arrColumnas, DataTable dtDataTable, bool Alternar)
        {
            My_FormatearGrid(xDg,arrColumnas,dtDataTable,Alternar);
        }

        public bool DG_ExporExcel(C1.Win.C1TrueDBGrid.C1TrueDBGrid xDg
            , System.Windows.Forms.SaveFileDialog objCuadroDialogo )
        {
            bool boolExporto = false;
            objCuadroDialogo.Filter = "MS Excel (*.xls) |*.xls;*.xls|(*.xls) |*.xls|(*.*) |*.*";
                       
            if (objCuadroDialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xDg.ExportToExcel(objCuadroDialogo.FileName);
                boolExporto=true;
            }
            
            string filename = "Excel.exe";

            Process proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = filename;
            proc.StartInfo.Arguments = objCuadroDialogo.FileName;
            proc.Start();

            return boolExporto;
        }
        
        public bool DG_ExporExcel_2(C1.Win.C1TrueDBGrid.C1TrueDBGrid xDg
            , System.Windows.Forms.SaveFileDialog objCuadroDialogo)
        {
            bool boolExporto = false;
            //objCuadroDialogo.Filter = "MS Excel (*.xls) |*.xls;*.xls|(*.xls) |*.xls|(*.*) |*.*";
            objCuadroDialogo.Filter = "MS Excel (*.xlsx) |*.xlsx;*.xlsx|(*.xlsx) |*.xlsx|(*.*) |*.*";

            DataTable tCxC = (DataTable)xDg.DataSource;

            DataView dtView = new DataView(tCxC);
            DataTable dtTableWithOneColumn = dtView.ToTable("Hoja 1", true
                , "n_id"
                , "d_fchdoc"
                , "c_tipdocdes"
                , "c_numdocvis"
                , "c_nompro"
                , "d_fching"
                , "c_almacendes"
                , "c_desdocref"
                , "c_numdocref");

            dtTableWithOneColumn.Columns[0].ColumnName = "Corr.";
            dtTableWithOneColumn.Columns[1].ColumnName = "Fch.Doc.";
            dtTableWithOneColumn.Columns[2].ColumnName = "Tip.Doc.";
            dtTableWithOneColumn.Columns[3].ColumnName = "N° Documento";
            dtTableWithOneColumn.Columns[4].ColumnName = "Proveedor";
            dtTableWithOneColumn.Columns[5].ColumnName = "Fch.Ingreso";
            dtTableWithOneColumn.Columns[6].ColumnName = "Almacén";
            dtTableWithOneColumn.Columns[7].ColumnName = "Tip.Doc.Ref.";
            dtTableWithOneColumn.Columns[8].ColumnName = "N° Doc.Ref.";


            if (objCuadroDialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataTable_To_Excel(dtTableWithOneColumn, objCuadroDialogo.FileName);
                //xDg.ExportToExcel(objCuadroDialogo.FileName);
                boolExporto = true;
            }

            string filename = "Excel.exe";

            Process proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = filename;
            proc.StartInfo.Arguments = objCuadroDialogo.FileName;
            proc.Start();

            return boolExporto;
        }

        public bool DG_ExporExcel(C1.Win.C1TrueDBGrid.C1TrueDBGrid xDg
            , System.Windows.Forms.SaveFileDialog objCuadroDialogo
            , string[] columnGridNames
            , string[] columnHeaderNames)
        {
            bool boolExporto = false;
            //objCuadroDialogo.Filter = "MS Excel (*.xls) |*.xls;*.xls|(*.xls) |*.xls|(*.*) |*.*";
            objCuadroDialogo.Filter = "MS Excel (*.xlsx) |*.xlsx;*.xlsx|(*.xlsx) |*.xlsx|(*.*) |*.*";

            DataTable tCxC = (DataTable)xDg.DataSource;

            DataView dtView = new DataView(tCxC);
            DataTable dtTableWithOneColumn = dtView.ToTable("Hoja 1", true, columnGridNames);

            int index = 0;
            foreach (var columnHeaderName in columnHeaderNames)
            {
                dtTableWithOneColumn.Columns[index].ColumnName = columnHeaderName;
                index += 1;
            }

            if (objCuadroDialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataTable_To_Excel(dtTableWithOneColumn, objCuadroDialogo.FileName);
                //xDg.ExportToExcel(objCuadroDialogo.FileName);
                boolExporto = true;
            }

            string filename = "Excel.exe";

            Process proc = new Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = filename;
            proc.StartInfo.Arguments = objCuadroDialogo.FileName;
            proc.Start();

            return boolExporto;
        }

        public DataTable DG_Filtrar(DataTable DtTabla, string c_CadFiltro, C1.Win.C1TrueDBGrid.C1TrueDBGrid xDg)
        {
            DataTable xDtTemp;

            xDtTemp = DtTabla;

            if (c_CadFiltro != "")
            {
                Genericas funDatos = new Genericas();
                 xDtTemp = funDatos.DataTableFiltrar(DtTabla, c_CadFiltro);
            }

            xDg.DataSource = xDtTemp;
            return xDtTemp;
        }
        
        public string DG_LeerCondicionesFiltro(C1.Win.C1TrueDBGrid.C1TrueDBGrid xDg)
        {
            C1.Win.C1TrueDBGrid.C1DataColumnCollection t_Cols;
            string strtmp;
            int intCol = 0;
           
            t_Cols = xDg.Columns;
            strtmp = "";
            int n_numcri = 0;
            for (intCol = 0; intCol <= t_Cols.Count-1; intCol++)
            {
                if (xDg.Columns[intCol].FilterText != "")
                {
                    if (n_numcri == 1)
                    {
                        strtmp = strtmp + " AND ";
                    }

                    if (xDg.Columns[intCol].DataType.Name == "String")
                    {
                        strtmp = strtmp + xDg.Columns[intCol].DataField.ToString() + " LIKE '%" + xDg.Columns[intCol].FilterText + "%'";
                        n_numcri = n_numcri + 1;
                    }
                    if (xDg.Columns[intCol].DataType.Name == "Double")
                    {
                        strtmp = strtmp + xDg.Columns[intCol].DataField.ToString() + " = " + xDg.Columns[intCol].FilterText + "";
                        n_numcri = n_numcri + 1;
                    }
                    if (xDg.Columns[intCol].DataType.Name == "DateTime")
                    {
                        strtmp = strtmp + xDg.Columns[intCol].DataField.ToString() + " = '" + xDg.Columns[intCol].FilterText + "'";
                        n_numcri = n_numcri + 1;
                        //if (IsDate(xDg.Columns[intCol].FilterText) = False)
                        //{
                        //    //MessageBox.Show("Formato no valido para fecha, procure ingresar dd/MM/yyyy ", "Error del usuario", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        //    //Exit For
                        //}
                        //else
                        //{
                        //    strtmp = strtmp + xDg.Columns[intCol].DataField.ToString() + " = '" + xDg.Columns[intCol].FilterText + "'";
                        //}
                    }
                }
            }

            return strtmp;
        }  
        void My_FormatearGrid(C1.Win.C1TrueDBGrid.C1TrueDBGrid xDg, string[,]  arrColumnas, DataTable dtDataTable, bool boo_Alternar)
        {
            int n_NumCols;
            int A;
            int IntNumeroElementos;
                      
            Font MyFont;
            MyFont = new Font(xDg.Splits[0].Style.Font, FontStyle.Regular);
            xDg.Splits[0].Style.Font = MyFont;

            xDg.Columns.Clear();
            //while (xDg.Columns.Count != 0)
            //{
            //    xDg.Columns.RemoveAt(0);
            //}

            n_NumCols = xDg.Columns.Count;
            xDg.RowHeight = 22;
            //xDg.Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
            xDg.Style.VerticalAlignment = C1.Win.C1TrueDBGrid.AlignVertEnum.Center;
            xDg.Splits[0].ColumnCaptionHeight = 30;
            xDg.AlternatingRows = boo_Alternar;
            xDg.Splits[0].EvenRowStyle.BackColor = Color.AliceBlue;
            xDg.Splits[0].EvenRowStyle.BackColor2 = Color.AntiqueWhite;

            IntNumeroElementos = Convert.ToInt32(arrColumnas.GetLongLength(0))-1;
 
            #region ASIGNAR DATOS A LA GRILLA
            for (A = 0; A <= IntNumeroElementos; A++)
            {
                C1.Win.C1TrueDBGrid.C1DataColumn colColum = new C1.Win.C1TrueDBGrid.C1DataColumn();
             
                xDg.Columns.Insert(n_NumCols, colColum);
                xDg.Splits[0].DisplayColumns[colColum].Visible = true;

                xDg.Splits[0].DisplayColumns[colColum].HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;         // centramos el titulo de la columna
                
                if (Convert.ToInt32(arrColumnas[A, 1]) > 0) 
                {
                    xDg.Splits[0].DisplayColumns[colColum].Width = Convert.ToInt32(arrColumnas[A, 1]) ;                                     // establecemos el ancho de la columna
                }
                else
                {
                    xDg.Splits[0].DisplayColumns[colColum].Visible = false;
                }
                    
                xDg.Columns[0].Caption = arrColumnas[A, 0];                                                                                 // establecemos el titulo de la columna
                xDg.Columns[0].DataField = arrColumnas[A, 3];                                                                               // establecemos el campo de la columna

                // establecemos el alineamiento de la columna
                if (arrColumnas[A, 2] == "D")
                {
                    xDg.Splits[0].DisplayColumns[colColum].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far;
                    xDg.Columns[0].NumberFormat = "0.00";

                    if (arrColumnas.GetLength(1) == 5)
                    {
                        xDg.Columns[0].NumberFormat = arrColumnas[A, 4];
                    }
                    else
                    {
                        xDg.Columns[0].NumberFormat = "0.00";
                    }
                    //int n_row = 0;
                    //for(n_row =0; n_row <= dtDataTable.Rows.Count-1; n_row++)
                    //{
                    //    if (dtDataTable.Rows[n_row]["c_tit"] == arrColumnas[A, 0])
                    //    {
                    //        xDg.Columns[0].NumberFormat = dtDataTable.Rows[n_row]["c_format"].ToString();
                    //    }
                    //}
                }
                if (arrColumnas[A, 2] == "N")
                {
                    xDg.Splits[0].DisplayColumns[colColum].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far;
                    xDg.Columns[0].NumberFormat = "0";
                }   
                if (arrColumnas[A, 2] == "C")
                {
                    xDg.Splits[0].DisplayColumns[colColum].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near;
                }

                if (arrColumnas[A, 2] == "F")
                {
                    xDg.Splits[0].DisplayColumns[colColum].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
                    xDg.Columns[0].NumberFormat = "dd/MM/yyyy";
                }                    

                if (arrColumnas[A, 2] == "H")
                {
                    xDg.Splits[0].DisplayColumns[colColum].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
                    xDg.Columns[0].NumberFormat = "hh:mm:ss";
                }

                if (arrColumnas[A, 2] == "FH")
                {
                    xDg.Splits[0].DisplayColumns[colColum].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
                    xDg.Columns[0].NumberFormat = "dd/MM/yyyy hh:mm:ss";
                }

                if (arrColumnas[A, 2] == "B")
                {
                    C1.Win.C1TrueDBGrid.ValueItemCollection check1 = xDg.Columns[0].ValueItems.Values;

                    xDg.Columns[0].ValueItems.Translate = true;
                    xDg.Columns[0].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;

                    check1.Add(new C1.Win.C1TrueDBGrid.ValueItem("0", false));
                    check1.Add(new C1.Win.C1TrueDBGrid.ValueItem("1", true));

                    xDg.Columns[0].ValueItems.DefaultItem = 0;
                    
                    xDg.Splits[0].DisplayColumns[colColum].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
                }

                xDg.Splits[0].DisplayColumns[colColum].FetchStyle = true;       
            }
            #endregion

            xDg.AllowAddNew = false;
            xDg.AllowDelete = false;
            xDg.AllowUpdate = false;
            //xDg.ColumnFooters = true;      ' muestra el pie de pagina del grid
            xDg.FilterBar = true;               // muestra el filtro
            xDg.AllowFilter = false;

            xDg.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
            //xDg.HighLightRowStyle.BackColor = Color.DarkRed;
            xDg.HighLightRowStyle.BackColor = Color.OrangeRed;
            xDg.CurrentCellVisible = true;

            xDg.SetDataBinding(dtDataTable, "", true);
            xDg.DataSource = dtDataTable;
            xDg.Select();
            //''Para activar el filtro de filter drop down
            //'For A = 0 To UBound(xValores) - 1
            //'    xDg.Columns(A).FilterDropdown = True
            //'Next A
        }
    }
}
