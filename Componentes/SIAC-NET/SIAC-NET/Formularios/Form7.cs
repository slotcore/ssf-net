using System;
using System.Windows.Forms;
using CrystalDecisions.Windows;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource ;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace SIAC_NET.Formularios
{
    public partial class Form7 : Form
    {
        public MySql.Data.MySqlClient.MySqlConnection Coneccion;
        public string c_Ruta;
        public string c_NomArchivo;
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            c_NomArchivo = "RPT_ComPunVen.rpt";
            c_Ruta = @"C:\siac-net\reportes\ventas\" + c_NomArchivo;
            carga();
        }

        private void carga()
        {

            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(c_Ruta);
                
            ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = 2;
            ParameterValues currentParameterValues = new ParameterValues();
            currentParameterValues.Add(parameterDiscreteValue);
            ParameterField parameterField = new ParameterField();
            parameterField.Name = "idventa";
            parameterField.CurrentValues = currentParameterValues;
            ParameterFields parameterFields = new ParameterFields();
            parameterFields.Add(parameterField);

            Cr7.ParameterFieldInfo = parameterFields;
            Cr7.ReportSource = reportdocument;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

    }
}
