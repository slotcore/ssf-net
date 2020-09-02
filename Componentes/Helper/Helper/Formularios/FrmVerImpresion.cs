using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

namespace Helper.Formularios
{
    public partial class FrmVerImpresion : Form
    {
        public string c_Titulo;
        public string c_PathRep;
        public Boolean b_VisPrev;
        public string[,] arrParametros;

        public string c_NombreServidor;
        public string c_NombreBD;
        public string c_Usuario;
        public string c_Contraseña;
        public string c_NombreArchivoExportar;
        public bool b_Exportar;
        ReportDocument reportdocument = new ReportDocument();

        ParameterFields paramFiels = new ParameterFields();
        public FrmVerImpresion()
        {
            InitializeComponent();
        }

        private void LoadReport()
        {
            this.Text = c_Titulo;
            this.Height = 663;
            this.Width = 939;

            int n_NumeroElementos;
            int n_fila;

            ConnectionInfo ci = new ConnectionInfo();
            ConnectionInfo iConnectionInfo = new ConnectionInfo();
            iConnectionInfo.ServerName = c_NombreServidor;
            iConnectionInfo.DatabaseName = c_NombreBD;
            iConnectionInfo.UserID = c_Usuario;
            iConnectionInfo.Password = c_Contraseña;

            iConnectionInfo.Type = ConnectionInfoType.SQL;

            n_NumeroElementos = Convert.ToInt32(arrParametros.GetLongLength(0));

            for (n_fila = 0; n_fila <= n_NumeroElementos - 1; n_fila++)
            {
                ParameterField param = new ParameterField();
                ParameterDiscreteValue discreteValue = new ParameterDiscreteValue();
                param.ParameterFieldName = arrParametros[n_fila, 0];
                discreteValue.Value = arrParametros[n_fila, 2];
                param.CurrentValues.Add(discreteValue);
                paramFiels.Add(param);
                //reportdocument.SetParameterValue(param.ParameterFieldName, discreteValue.Value); 
            }
            //MessageBox.Show(c_PathRep);
            reportdocument.Load(c_PathRep);

            SetDBLogonForReport(iConnectionInfo, reportdocument);

            Cr7.ReportSource = reportdocument;

            if (paramFiels.Count != 0)                             // PREGUNTAMOS SI EL REPORTE TIENE PARAMETROS
            {
                Cr7.ParameterFieldInfo = paramFiels;
            }
            Cr7.Refresh();

            if (b_VisPrev == false)
            {
                //Cr7.PrintReport();
                //reportdocument.PrintOptions.PrinterName = ImpresoraDefecto();
                //reportdocument.PrintToPrinter(1, false, 0, 0);




                //this.Text = "Imprimiendo Documento";
                //this.Width = 300;
                //this.Height = 100;
                //Cr7.Visible = false;
                ////reportdocument.SetParameterValue = paramFiels;
                //reportdocument.PrintOptions.PrinterName = ImpresoraDefecto();

                //reportdocument.PrintToPrinter(1, false, 0, 0);
                //this.Close();
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        string ImpresoraDefecto()
        {
            string NombreImpresora = "";//Donde guardare el nombre de la impresora por defecto

            //Busco la impresora por defecto
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (a.IsDefaultPrinter)
                {
                    NombreImpresora = PrinterSettings.InstalledPrinters[i].ToString();
                    break;
                }
            }
            return NombreImpresora;
        }
        void Exportar(ReportDocument DocImpresion, string c_archivo)
        {
            DocImpresion.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            DocImpresion.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = c_archivo;
            DocImpresion.ExportOptions.DestinationOptions = objDiskOpt;
            DocImpresion.Export();
        }
        private void FrmVerImpresion_Activated(object sender, EventArgs e)
        {
            if (b_Exportar == true)
            {
                Exportar(reportdocument, c_NombreArchivoExportar);
                this.Close();
            }
        }
        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        private void FrmVerImpresion_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
    }
}
