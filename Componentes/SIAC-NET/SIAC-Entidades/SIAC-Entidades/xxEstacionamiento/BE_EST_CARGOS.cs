using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_CARGOS
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idano;
        private int _n_idmes;
        private int _n_idpla;
        private DateTime _d_fchemi;
        private double _n_impbru;
        private double _n_impigv;
        private double _n_imptot;
        private int _n_numrec;
        private DateTime _d_fchini;
        private string _c_obs;
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
        public int n_idano
        {
            get { return _n_idano; }
            set { _n_idano = value; }
        }
        public int n_idmes
        {
            get { return _n_idmes; }
            set { _n_idmes = value; }
        }
        public int n_idpla
        {
            get { return _n_idpla; }
            set { _n_idpla = value; }
        }
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public double n_impbru
        {
            get { return _n_impbru; }
            set { _n_impbru = value; }
        }
        public double n_impigv
        {
            get { return _n_impigv; }
            set { _n_impigv = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
        public int n_numrec
        {
            get { return _n_numrec; }
            set { _n_numrec = value; }
        }
        public DateTime d_fchini
        {
            get { return _d_fchini; }
            set { _d_fchini = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
    }
}
