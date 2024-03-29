﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_SERVICIOS
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idpla;
        private string _c_des;
        private string _c_cod;
        private int _n_idunimed;
        private double _n_impbru;
        private double _n_imptot;
        private int _n_idmon;
        private int _n_idfan;
        private int _n_idcla;
        private int _n_idsubcla;
        private int _n_idtipexi;
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
        public int n_idpla
        {
            get { return _n_idpla; }
            set { _n_idpla = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public string c_cod
        {
            get { return _c_cod; }
            set { _c_cod = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_impbru
        {
            get { return _n_impbru; }
            set { _n_impbru = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public int n_idfan
        {
            get { return _n_idfan; }
            set { _n_idfan = value; }
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
        public int n_idtipexi
        {
            get { return _n_idtipexi; }
            set { _n_idtipexi = value; }
        }
    }
}
