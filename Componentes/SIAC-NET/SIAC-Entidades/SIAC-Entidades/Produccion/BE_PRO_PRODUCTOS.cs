using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCTOS: ICloneable
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_cod;
        private string _c_despro;
        private int _n_idunimed;
        private int _n_idfam;
        private int _n_idcla;
        private int _n_idsubcla;
        private int _n_idtip;
        private string _c_obs;
        private int _n_act;
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
        public string c_cod
        {
            get { return _c_cod; }
            set { _c_cod = value; }
        }
        public string c_despro
        {
            get { return _c_despro; }
            set { _c_despro = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public int n_idfam
        {
            get { return _n_idfam; }
            set { _n_idfam = value; }
        }
        public int n_idcla
        {
            get { return _n_idcla; }
            set { _n_idcla = value; }
        }
        public int n_idsubcla
        {
            get { return _n_idsubcla; }
            set { _n_idsubcla = value; }
        }
        public int n_idtip
        {
            get { return _n_idtip; }
            set { _n_idtip = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_act
        {
            get { return _n_act; }
            set { _n_act = value; }
        }
        public object Clone() 
        {
          return MemberwiseClone(); 
	    }
    }
}
