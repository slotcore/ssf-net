using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_PUNVENCLI
    {
        private int _n_idemp;
        private int _n_idcli;
        private int _n_id;
        private string _c_codcen;
        private string _c_des;
        private string _c_dir;
        private int _n_iddis;
        private int _n_idpro;
        private int _n_iddep;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_codcen
        {
            get { return _c_codcen; }
            set { _c_codcen = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public string c_dir
        {
            get { return _c_dir; }
            set { _c_dir = value; }
        }
        public int n_iddis
        {
            get { return _n_iddis; }
            set { _n_iddis = value; }
        }
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_iddep
        {
            get { return _n_iddep; }
            set { _n_iddep = value; }
        }
    }
}
