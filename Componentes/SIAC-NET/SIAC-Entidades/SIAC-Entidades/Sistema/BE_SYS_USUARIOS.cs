using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Sistema
{
    public class BE_SYS_USUARIOS
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_nom;
        private string _c_apepat;
        private string _c_apemat;
        private string _c_usuario;
        private string _c_pass;
        private int _n_activo;
        private int _n_idperfil;
        private string _c_email;
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
        public string c_nom
        {
            get { return _c_nom; }
            set { _c_nom = value; }
        }
        public string c_apepat
        {
            get { return _c_apepat; }
            set { _c_apepat = value; }
        }
        public string c_apemat
        {
            get { return _c_apemat; }
            set { _c_apemat = value; }
        }
        public string c_usuario
        {
            get { return _c_usuario; }
            set { _c_usuario = value; }
        }
        public string c_pass
        {
            get { return _c_pass; }
            set { _c_pass = value; }
        }
        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }
        public int n_idperfil
        {
            get { return _n_idperfil; }
            set { _n_idperfil = value; }
        }
        public string c_email
        {
            get { return _c_email; }
            set { _c_email = value; }
        }
    }
}
