using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCCIONTAREASDET
    {
        private int _n_idpro;
        private int _n_idtar;
        private int _n_idper; 
        private int _n_id;
        private double _n_can;
        private string _c_obs;
        private string _c_horini;
        private string _c_horter;
        private double _n_pretar;
        private double _n_imptot;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idtar
        {
            get { return _n_idtar; }
            set { _n_idtar = value; }
        }
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
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
        public double n_pretar
        {
            get { return _n_pretar; }
            set { _n_pretar = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
    }
}
