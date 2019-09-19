using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIAC_Negocio.Sistema;
using MySql.Data.MySqlClient;

using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Metro.ColorTables;
using System.Diagnostics;

namespace SIAC_NET.Formularios
{
    public partial class FrmMenu3 : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        public MySqlConnection mysConeccion = new MySqlConnection();
        //MetroBillCommands _Commands = null; // All application commands
        MenuAlmacen _MenuAlmacen = null;

        public FrmMenu3()
        {
            InitializeComponent();
        }

        private void FrmMenu3_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmMenu3_Activated(object sender, EventArgs e)
        {
            //radialMenu1.Diameter = 500; // Make menu diameter larger
            //radialMenu1.IsOpen = true;
        }

        private void metroTabPanel1_Click(object sender, EventArgs e)
        {

        }

        private void metroTileItem1_Click(object sender, EventArgs e)
        {

        }

        private void metroTileItem14_Click(object sender, EventArgs e)
        {

        }

        private void metroTileItem1_Click_1(object sender, EventArgs e)
        {
            Debug.Assert(_MenuAlmacen == null);
            //_Commands.ClientCommands.New.Enabled = false; // Disable new client command to prevent re-entrancy
            _MenuAlmacen = new MenuAlmacen();
            //_MenuAlmacen.Commands = _Commands;
            this.ShowModalPanel(_MenuAlmacen, DevComponents.DotNetBar.Controls.eSlideSide.Left);
         
            //if (!_StartControl.Visible)
            //    _StartControl.SlideOutButtonVisible = false;
        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void labelX2_Click(object sender, EventArgs e)
        {

        }

        private void metroShell1_Click(object sender, EventArgs e)
        {

        }
    }
}