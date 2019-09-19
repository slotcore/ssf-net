using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_SERVICIOS
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_anotra;
        private int _n_mestra;
        private DateTime _d_fchini;
        private DateTime _d_fchfin;
        private double _n_imptot;
        private string _c_obs;
        private int _n_idtipser;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_anotra
        {
            get { return _n_anotra; }
            set { _n_anotra = value; }
        }
        public int n_mestra
        {
            get { return _n_mestra; }
            set { _n_mestra = value; }
        }
        public DateTime d_fchini
        {
            get { return _d_fchini; }
            set { _d_fchini = value; }
        }
        public DateTime d_fchfin
        {
            get { return _d_fchfin; }
            set { _d_fchfin = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_idtipser
        {
            get { return _n_idtipser; }
            set { _n_idtipser = value; }
        }
    }
}
