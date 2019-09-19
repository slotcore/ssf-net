using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_INVENTARIOLOTE
    {
        int _n_idemp;
        int _n_idite;
        int _n_iddocmov;
        DateTime _d_fchmov;
        string _c_numlot;
        int _n_idunimed;
        double _n_caning;
        double _n_cansal;
        DateTime ?_d_fchpro;
        DateTime ?_d_fchven;
        int ?_n_iddep;
        int ?_n_idpro;
        int ?_n_iddis;
        string _c_oriite;
        string _h_horing;
        string _h_horsal;
        int _n_estpro;

        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_iddocmov
        {
            get { return _n_iddocmov; }
            set { _n_iddocmov = value; }
        }
        public string c_numlot
        {
            get { return _c_numlot; }
            set { _c_numlot = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_caning
        {
            get { return _n_caning; }
            set { _n_caning = value; }
        }
        public double n_cansal
        {
            get { return _n_cansal; }
            set { _n_cansal = value; }
        }
        public DateTime d_fchmov
        {
            get { return _d_fchmov; }
            set { _d_fchmov = value; }
        }
        public DateTime ?d_fchpro
        {
            get { return _d_fchpro; }
            set { _d_fchpro = value; }
        }
        public DateTime ?d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public int ?n_iddep
        {
            get { return _n_iddep; }
            set { _n_iddep = value; }
        }
        public int ?n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int ?n_iddis
        {
            get { return _n_iddis; }
            set { _n_iddis = value; }
        }
        public string c_oriite
        {
            get { return _c_oriite; }
            set { _c_oriite = value; }
        }
        public string h_horing
        {
            get { return _h_horing; }
            set { _h_horing = value; }
        }
        public string h_horsal
        {
            get { return _h_horsal; }
            set { _h_horsal = value; }
        }
        public int n_estpro
        {
            get { return _n_estpro; }
            set { _n_estpro = value; }
        }
    }
}
