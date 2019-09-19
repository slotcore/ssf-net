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
//using DevComponents.AdvTree;
//using DevComponents.DotNetBar.Metro.ColorTables;
using System.Diagnostics;

namespace SIAC_NET.Formularios
{
    public partial class FrmPrincipal : DevComponents.DotNetBar.Metro.MetroForm
    {
        public MySqlConnection mysConeccion = new MySqlConnection();
        //MetroBillCommands _Commands = null; // All application commands
        //MenuAlmacen _MenuAlmacen = null;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}