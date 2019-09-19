using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Maestros
{
    public class BE_MAE_ALMDOC_CONSULTA : BE_MAE_ALMDOC
    {
        private string _c_desdoc;
        private string _c_desalm;

        public string c_desdoc
        {
            get { return _c_desdoc; }
            set { _c_desdoc = value; }
        }
        public string c_desalm
        {
            get { return _c_desalm; }
            set { _c_desalm = value; }
        }

    }
}
