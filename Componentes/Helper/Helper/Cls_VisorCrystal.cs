using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Formularios;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Helper
{
    

    public class Cls_VisorCrystal
    {
        public string[,] arrParametros;
        public string c_Titulo;
        public string c_PathRep;
        public Boolean b_VisPrev;
        public string c_NombreServidor;
        public string c_NombreBD;
        public string c_Usuario;
        public string c_Contraseña;
        public string c_NombreArchivoExportar;
        public bool b_Exportar;
        public void VerCrystal()
        {
            FrmVerImpresion MiForm = new FrmVerImpresion();
            MiForm.c_Titulo = c_Titulo;
            MiForm.c_PathRep = c_PathRep;
            MiForm.b_VisPrev = b_VisPrev;
            MiForm.arrParametros = arrParametros;
            MiForm.c_NombreServidor = c_NombreServidor;
            MiForm.c_NombreBD = c_NombreBD;
            MiForm.c_Usuario = c_Usuario;
            MiForm.c_Contraseña = c_Contraseña;
            MiForm.c_NombreArchivoExportar = c_NombreArchivoExportar;
            MiForm.b_Exportar = b_Exportar;

            //if (b_VisPrev == true)
            //{
                MiForm.ShowDialog();
            //}
            //else
            //{
            //    Imprimir();
            //}
            
        }
        void Imprimir()
        {
            int n_NumeroElementos;
            int n_fila;
            ParameterFields paramFiels = new ParameterFields();
            ReportDocument reportdocument = new ReportDocument();

            ConnectionInfo ci = new ConnectionInfo();
            ConnectionInfo iConnectionInfo = new ConnectionInfo();
            iConnectionInfo.ServerName = c_NombreServidor;
            iConnectionInfo.DatabaseName = c_NombreBD;
            iConnectionInfo.UserID = c_Usuario;
            iConnectionInfo.Password = c_Contraseña;

            iConnectionInfo.Type = ConnectionInfoType.SQL;

            n_NumeroElementos = Convert.ToInt16(arrParametros.GetLongLength(0));

            for (n_fila = 0; n_fila <= n_NumeroElementos - 1; n_fila++)
            {
                ParameterField param = new ParameterField();
                ParameterDiscreteValue discreteValue = new ParameterDiscreteValue();
                param.ParameterFieldName = arrParametros[n_fila, 0];
                discreteValue.Value = arrParametros[n_fila, 2];
                param.CurrentValues.Add(discreteValue);
                paramFiels.Add(param);
            }

            
            reportdocument.Load(c_PathRep);
            SetDBLogonForReport(iConnectionInfo, reportdocument);

            //ParameterDiscreteValue pfDiscrete = new CrystalDecisions.Shared.ParameterDiscreteValue();
            //pfDiscrete.Value = salesOrderNo;
            //SetParameterValue("orderNo", reportdocument, pfDiscrete);

            //// cashier
            //ParameterDiscreteValue pfCashier = new CrystalDecisions.Shared.ParameterDiscreteValue();
            //pfCashier.Value = MembershipHelper.CurrentUserName;
            //SetParameterValue("cashier", reportdocument, pfCashier);

            //reportdocument.PrintToPrinter(1, false, 0, 1);

            //System.Drawing.Printing.PrintDocument pdoc = new System.Drawing.Printing.PrintDocument();
            //String strDefaultPrinter = pdoc.PrinterSettings.PrinterName;
            //reportdocument.PrintOptions.PrinterName = strDefaultPrinter;
            //reportdocument.PrintToPrinter(1, false, 0, 0);

            //Cr7.ReportSource = reportdocument;

            //if (paramFiels.Count != 0)                             // PREGUNTAMOS SI EL REPORTE TIENE PARAMETROS
            //{
            //    Cr7.ParameterFieldInfo = paramFiels;
            //}
            //Cr7.Refresh();
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
    }
}
