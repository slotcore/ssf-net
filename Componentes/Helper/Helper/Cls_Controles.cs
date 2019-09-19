using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Helper
{
    public class Cls_Controles
    {
        public void dtpText(DateTimePicker objFecha, string c_dato)
        {
            if (c_dato == "")
            {
                objFecha.Text = "";
                objFecha.CustomFormat = " ";
                objFecha.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                objFecha.Text = c_dato;
                objFecha.CustomFormat = "dd/MM/yyyy";
                objFecha.Format = DateTimePickerFormat.Custom;
            }
        }
        public void dtpBlanquea(DateTimePicker objFecha)
        {
            objFecha.Text = "";
            objFecha.CustomFormat = " ";
            objFecha.Format = DateTimePickerFormat.Custom;
        }
    }
}
