using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_PEDIDOCEN
    {
        private int _n_idemp;
        private int _n_id;
        private string _c_codcli;
        private string _c_codpunven;
        private string _c_codpunent;
        private DateTime _d_fchemi;
        private DateTime _d_fchent;
        private DateTime _d_fchdes;
        private int _n_numite;
        private string _c_numped;
        private int _n_anotra;
        private int _n_mestra;
        private int _n_idguides;
        private int _n_despachado;
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
        public string c_codcli
        {
            get { return _c_codcli; }
            set { _c_codcli = value; }
        }
        public string c_codpunven
        {
            get { return _c_codpunven; }
            set { _c_codpunven = value; }
        }
        public string c_codpunent
        {
            get { return _c_codpunent; }
            set { _c_codpunent = value; }
        }
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public DateTime d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public DateTime d_fchdes
        {
            get { return _d_fchdes; }
            set { _d_fchdes = value; }
        }
        public int n_numite
        {
            get { return _n_numite; }
            set { _n_numite = value; }
        }
        public string c_numped
        {
            get { return _c_numped; }
            set { _c_numped = value; }
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
        public int n_idguides
        {
            get { return _n_idguides; }
            set { _n_idguides = value; }
        }
        public int n_despachado
        {
            get { return _n_despachado; }
            set { _n_despachado = value; }
        }
    }
}
