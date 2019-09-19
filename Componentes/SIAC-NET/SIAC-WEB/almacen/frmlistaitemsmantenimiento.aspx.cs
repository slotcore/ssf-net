using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIAC_WEB.almacen
{
    public partial class frmlistaitemsmantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Text = Request.QueryString["TextBox1"];
        }
    }
}