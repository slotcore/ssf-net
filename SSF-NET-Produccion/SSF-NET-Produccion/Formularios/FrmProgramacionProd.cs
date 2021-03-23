using MySql.Data.MySqlClient;
using SIAC_DATOS.Models.Produccion;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmProgramacionProd : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        public FrmProgramacionProd()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Programacion> programacions 
                    = Programacion.TraerPorRangoFecha(STU_SISTEMA.EMPRESAID, MonthViewProg.SelectionStart, MonthViewProg.SelectionEnd);

                CalendarProg.SetViewRange(MonthViewProg.SelectionStart, MonthViewProg.SelectionEnd);
                
                CalendarProg.Items.Clear();

                foreach (var programacion in programacions)
                {
                    foreach (var progDetPro in programacion.ProgramacionDetPros)
                    {
                        DateTime fchIni = new DateTime(progDetPro.d_fchpro.Year
                            , progDetPro.d_fchpro.Month, progDetPro.d_fchpro.Day
                            , progDetPro.h_horini.Hour, progDetPro.h_horini.Minute
                            , progDetPro.h_horini.Second);

                        DateTime fchFin = new DateTime(progDetPro.d_fchpro.Year
                            , progDetPro.d_fchpro.Month, progDetPro.d_fchpro.Day
                            , progDetPro.h_horfin.Hour, progDetPro.h_horfin.Minute
                            , progDetPro.h_horfin.Second);

                        var textoCal = string.Format("{1}{0}{2}{0}{3}{0}{4}"
                            , Environment.NewLine
                            , progDetPro.c_desite
                            , progDetPro.c_desordpro
                            , progDetPro.n_can
                            , progDetPro.c_desres);
                        CalendarItem cal = new CalendarItem(CalendarProg, fchIni, fchFin, textoCal);

                        if (CalendarProg.ViewIntersects(cal))
                        {
                            CalendarProg.AllowItemResize = false;
                            CalendarProg.AllowItemEdit = false;
                            CalendarProg.ItemsTimeFormat = "HH:mm";
                            CalendarProg.Items.Add(cal);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrio un error: {0}", ex.Message), "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
