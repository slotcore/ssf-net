using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VENTASDAT
    {
        private Int64 _n_idvta;
        private int _n_idcaj;
        private string _c_cajnom;
        private int _n_idloc;
        private string _c_locdes;
        private string _h_horemi;
        private string _c_numpla;
        private string _c_horini;
        private string _c_horfin;
        private string _c_tiempousu;
        public Int64 n_idvta
        {
            get { return _n_idvta; }
            set { _n_idvta = value; }
        }
        public int n_idcaj
        {
            get { return _n_idcaj; }
            set { _n_idcaj = value; }
        }
        public string c_cajnom
        {
            get { return _c_cajnom; }
            set { _c_cajnom = value; }
        }
        public int n_idloc
        {
            get { return _n_idloc; }
            set { _n_idloc = value; }
        }
        public string c_locdes
        {
            get { return _c_locdes; }
            set { _c_locdes = value; }
        }
        public string h_horemi
        {
            get { return _h_horemi; }
            set { _h_horemi = value; }
        }
        public string c_numpla
        {
            get { return _c_numpla; }
            set { _c_numpla = value; }
        }
        public string c_horini
        {
            get { return _c_horini; }
            set { _c_horini = value; }
        }
        public string c_horfin
        {
            get { return _c_horfin; }
            set { _c_horfin = value; }
        }
        public string c_tiempousu
        {
            get { return _c_tiempousu; }
            set { _c_tiempousu = value; }
        }
    }
}
