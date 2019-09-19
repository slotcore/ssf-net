using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SIAC_Entidades.Planillas
{
    public class BE_TEMPUS_MOSTRAR
    {
        private string _c_idemp;
        private string _c_codemp;
        private int _n_mostrar;
        public string c_idemp
        {
            get { return _c_idemp; }
            set { _c_idemp = value; }
        }
        public string c_codemp
        {
            get { return _c_codemp; }
            set { _c_codemp = value; }
        }
        public int n_mostrar
        {
            get { return _n_mostrar; }
            set { _n_mostrar = value; }
        }
    }
}
