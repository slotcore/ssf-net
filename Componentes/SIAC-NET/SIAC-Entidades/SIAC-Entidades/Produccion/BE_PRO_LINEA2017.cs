using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_LINEA2017
    {
        private int _n_idemp;
        private int _n_idpro;
        private int _n_id;
        private string _c_codlin;
        private string _c_deslin;
        private int _n_idunimed;
        private double _n_can;
        private double _n_numope;
        private double _n_efi;
        private string _h_tie;
        private double _n_prelin;

        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_codlin
        {
            get { return _c_codlin; }
            set { _c_codlin = value; }
        }
        public string c_deslin
        {
            get { return _c_deslin; }
            set { _c_deslin = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public double n_numope
        {
            get { return _n_numope; }
            set { _n_numope = value; }
        }
        public double n_efi
        {
            get { return _n_efi; }
            set { _n_efi = value; }
        }
        public string h_tie
        {
            get { return _h_tie; }
            set { _h_tie = value; }
        }
        public double n_prelin
        {
            get { return _n_prelin; }
            set { _n_prelin = value; }
        }
    }
}
