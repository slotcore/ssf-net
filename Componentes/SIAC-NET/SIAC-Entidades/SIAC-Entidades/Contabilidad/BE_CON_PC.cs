using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_PC
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_cuecon;
        private string _c_des;
        private int _n_ctadesdeb;
        private int _n_ctadeshab;
        private int _n_iddes;
        private int _n_iddes2;
        private int _n_iddes3;
        private int _n_tipo;
        private string _c_tipsal;
        private int _n_dissegsal;
        private int _n_documentar;
        private int _n_idmodulo;
        private int _n_desctabal;

        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_cuecon
        {
            get { return _c_cuecon; }
            set { _c_cuecon = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public int n_ctadesdeb
        {
            get { return _n_ctadesdeb; }
            set { _n_ctadesdeb = value; }
        }
        public int n_ctadeshab
        {
            get { return _n_ctadeshab; }
            set { _n_ctadeshab = value; }
        }
        public int n_iddes
        {
            get { return _n_iddes; }
            set { _n_iddes = value; }
        }
        public int n_iddes2
        {
            get { return _n_iddes2; }
            set { _n_iddes2 = value; }
        }
        public int n_iddes3
        {
            get { return _n_iddes3; }
            set { _n_iddes3 = value; }
        }
        public int n_tipo
        {
            get { return _n_tipo; }
            set { _n_tipo = value; }
        }
        public string c_tipsal
        {
            get { return _c_tipsal; }
            set { _c_tipsal = value; }
        }
        public int n_dissegsal
        {
            get { return _n_dissegsal; }
            set { _n_dissegsal = value; }
        }
        public int n_documentar
        {
            get { return _n_documentar; }
            set { _n_documentar = value; }
        }
        public int n_idmodulo
        {
            get { return _n_idmodulo; }
            set { _n_idmodulo = value; }
        }
        public int n_desctabal
        {
            get { return _n_desctabal; }
            set { _n_desctabal = value; }
        }
    }
}
