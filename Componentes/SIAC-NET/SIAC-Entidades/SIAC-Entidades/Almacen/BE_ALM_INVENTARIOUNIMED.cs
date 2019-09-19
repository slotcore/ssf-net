using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_INVENTARIOUNIMED
    {
        private Int64 _n_idite;
        private int _n_id;
        private string _c_despre;
        private string _c_abrpre; 
        private int _n_idunimedbas;
        private double _n_canunimedbas;
        private int _n_default;
        private double _n_preuni;
        private double _n_preuniigv;

        public Int64 n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_despre
        {
            get { return _c_despre; }
            set { _c_despre = value; }
        }
        public string c_abrpre
        {
            get { return _c_abrpre; }
            set { _c_abrpre = value; }
        }
        public int n_idunimedbas
        {
            get { return _n_idunimedbas; }
            set { _n_idunimedbas = value; }
        }
        public double n_canunimedbas
        {
            get { return _n_canunimedbas; }
            set { _n_canunimedbas = value; }
        }
        public int n_default
        {
            get { return _n_default; }
            set { _n_default = value; }
        }
        public double n_preuni
        {
            get { return _n_preuni; }
            set { _n_preuni = value; }
        }
        public double n_preuniigv
        {
            get { return _n_preuniigv; }
            set { _n_preuniigv = value; }
        }
    }
}
