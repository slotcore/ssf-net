using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_SOLICITUDTAREASDET
    {
        private int _n_idsol;
        private int _n_idsoltar;
        private int _n_idper;
        private string _c_obs;
        private string _c_horini;
        private string _c_horter;
        private double _n_can;
        private string _c_numhortra;
        private double _n_numhortra;
        private double _n_canmaxpro;
        private double _n_preunipro;
        private double _n_prehorpag;
        private double _n_imppaghrstra;
        private double _n_subsidio;
        private int _n_numenv;
        private double _n_pesbru;

        public int n_idsol
        {
            get { return _n_idsol; }
            set { _n_idsol = value; }
        }
        public int n_idsoltar
        {
            get { return _n_idsoltar; }
            set { _n_idsoltar = value; }
        }
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public string c_horini
        {
            get { return _c_horini; }
            set { _c_horini = value; }
        }
        public string c_horter
        {
            get { return _c_horter; }
            set { _c_horter = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public string c_numhortra
        {
            get { return _c_numhortra; }
            set { _c_numhortra = value; }
        }
        public double n_numhortra
        {
            get { return _n_numhortra; }
            set { _n_numhortra = value; }
        }
        public double n_canmaxpro
        {
            get { return _n_canmaxpro; }
            set { _n_canmaxpro = value; }
        }
        public double n_preunipro
        {
            get { return _n_preunipro; }
            set { _n_preunipro = value; }
        }
        public double n_prehorpag
        {
            get { return _n_prehorpag; }
            set { _n_prehorpag = value; }
        }
        public double n_imppaghrstra
        {
            get { return _n_imppaghrstra; }
            set { _n_imppaghrstra = value; }
        }
        public double n_subsidio
        {
            get { return _n_subsidio; }
            set { _n_subsidio = value; }
        }
        public int n_numenv
        {
            get { return _n_numenv; }
            set { _n_numenv = value; }
        }
        public double n_pesbru
        {
            get { return _n_pesbru; }
            set { _n_pesbru = value; }
        }
    }
}
