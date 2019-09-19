using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_LETRASDET
    {
        private int _n_idlet;
        private int _n_id;
        private string _c_numser;
        private string _c_numlet;
        private DateTime _d_fchemi;
        private DateTime _d_fchven;
        private double _n_impcap;
        private double _n_imppor;
        private double _n_impint;
        private double _n_implet;
        private int _n_diapla;
        private double _n_impsal;
        private string _c_imptex;
        public int n_idlet
        {
            get { return _n_idlet; }
            set { _n_idlet = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_numser
        {
            get { return _c_numser; }
            set { _c_numser = value; }
        }
        public string c_numlet
        {
            get { return _c_numlet; }
            set { _c_numlet = value; }
        }
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public DateTime d_fchven
        {
            get { return _d_fchven; }
            set { _d_fchven = value; }
        }
        public double n_impcap
        {
            get { return _n_impcap; }
            set { _n_impcap = value; }
        }
        public double n_imppor
        {
            get { return _n_imppor; }
            set { _n_imppor = value; }
        }
        public double n_impint
        {
            get { return _n_impint; }
            set { _n_impint = value; }
        }
        public double n_implet
        {
            get { return _n_implet; }
            set { _n_implet = value; }
        }
        public int n_diapla
        {
            get { return _n_diapla; }
            set { _n_diapla = value; }
        }
        public double n_impsal
        {
            get { return _n_impsal; }
            set { _n_impsal = value; }
        }
        public string c_imptex
        {
            get { return _c_imptex; }
            set { _c_imptex = value; }
        }
    }
}
