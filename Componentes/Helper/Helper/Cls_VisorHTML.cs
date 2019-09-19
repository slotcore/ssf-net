using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Cls_VisorHTML
    {
        public string c_NombreArchivo;
        public string c_TituloForm;

        public void VerHtml()
        { 
            Formularios.FrmVerHtml xFrm = new Formularios.FrmVerHtml();
            xFrm.c_NombreArchivo = c_NombreArchivo;
            xFrm.c_TituloForm = c_TituloForm;
            xFrm.ShowDialog();
        }
    }
}
