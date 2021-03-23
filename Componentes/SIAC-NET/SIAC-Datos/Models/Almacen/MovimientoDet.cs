using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Almacen
{

    public class MovimientoDet : ObjectBase
    {
        #region constructor
        public MovimientoDet()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_idmov;

        private int _n_idite;

        private int _n_idpre;

        private double _n_can;

        public int n_idmov
        {
            get
            {
                return _n_idmov;
            }

            set
            {
                if (value != _n_idmov)
                {
                    _n_idmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idite
        {
            get
            {
                return _n_idite;
            }

            set
            {
                if (value != _n_idite)
                {
                    _n_idite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idpre
        {
            get
            {
                return _n_idpre;
            }

            set
            {
                if (value != _n_idpre)
                {
                    _n_idpre = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_can
        {
            get
            {
                return _n_can;
            }

            set
            {
                if (value != _n_can)
                {
                    _n_can = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idalm;
        public int n_idalm
        {
            get
            {
                return _n_idalm;
            }

            set
            {
                if (value != _n_idalm)
                {
                    _n_idalm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_numlot;
        public string c_numlot
        {
            get
            {
                return _c_numlot;
            }

            set
            {
                if (value != _c_numlot)
                {
                    _c_numlot = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idtippro;
        public int n_idtippro
        {
            get
            {
                return _n_idtippro;
            }

            set
            {
                if (value != _n_idtippro)
                {
                    _n_idtippro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _d_fchpro;
        public DateTime d_fchpro
        {
            get
            {
                return _d_fchpro;
            }

            set
            {
                if (value != _d_fchpro)
                {
                    _d_fchpro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _d_fchven;
        public DateTime d_fchven
        {
            get
            {
                return _d_fchven;
            }

            set
            {
                if (value != _d_fchven)
                {
                    _d_fchven = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_iddep;
        public int n_iddep
        {
            get
            {
                return _n_iddep;
            }

            set
            {
                if (value != _n_iddep)
                {
                    _n_iddep = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idpro;
        public int n_idpro
        {
            get
            {
                return _n_idpro;
            }

            set
            {
                if (value != _n_idpro)
                {
                    _n_idpro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_iddis;
        public int n_iddis
        {
            get
            {
                return _n_iddis;
            }

            set
            {
                if (value != _n_iddis)
                {
                    _n_iddis = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desori;
        public string c_desori
        {
            get
            {
                return _c_desori;
            }

            set
            {
                if (value != _c_desori)
                {
                    _c_desori = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_marca;
        public string c_marca
        {
            get
            {
                return _c_marca;
            }

            set
            {
                if (value != _c_marca)
                {
                    _c_marca = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _h_horing;
        public DateTime h_horing
        {
            get
            {
                return _h_horing;
            }

            set
            {
                if (value != _h_horing)
                {
                    _h_horing = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _h_horsal;
        public DateTime h_horsal
        {
            get
            {
                return _h_horsal;
            }

            set
            {
                if (value != _h_horsal)
                {
                    _h_horsal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_estpro;
        public int n_estpro
        {
            get
            {
                return _n_estpro;
            }

            set
            {
                if (value != _n_estpro)
                {
                    _n_estpro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_preuni;
        public double n_preuni
        {
            get
            {
                return _n_preuni;
            }

            set
            {
                if (value != _n_preuni)
                {
                    _n_preuni = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_pretot;
        public double n_pretot
        {
            get
            {
                return _n_pretot;
            }

            set
            {
                if (value != _n_pretot)
                {
                    _n_pretot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<MovimientoDet> FetchList(int n_idmov)
        {
            ObservableListSource<MovimientoDet> m_listentidad = new ObservableListSource<MovimientoDet>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_movimientosdet_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idmov", n_idmov));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MovimientoDet m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static MovimientoDet Fetch(int id)
        {
            MovimientoDet m_entidad = new MovimientoDet();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_movimientosdet_traerregistro";
                    command.Parameters.Add(new MySqlParameter("@n_id", id));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            m_entidad = SetObject(reader);
                        }
                    }
                }
            }
            return m_entidad;
        }

        protected override void Insert()
        {
            using (MySqlConnection connection = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_movimientosdet_insertar";
                            AddParameters(command);
                            int rows = command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        protected override void Insert(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "alm_movimientosdet_insertar";
                AddParameters(command);
                int rows = command.ExecuteNonQuery();
            }
        }

        protected override void Update()
        {
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        try
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_movimientosdet_actualizar";
                            AddParameters(command);
                            int rows = command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        protected override void Update(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "alm_movimientosdet_actualizar";
                AddParameters(command);
                int rows = command.ExecuteNonQuery();
            }
        }

        public override void Delete()
        {
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        try
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_movimientosdet_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_idmov));
                            int rows = command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        #endregion

        #region metodos privados

        private static MovimientoDet SetObject(MySqlDataReader reader)
        {
            return new MovimientoDet
            {
                n_idmov = reader.GetInt32("n_id"),
                n_idite = reader.GetInt32("n_idite"),
                n_idpre = reader.GetInt32("n_idpre"),
                n_can = reader.GetInt32("n_can")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_idmov", n_idmov));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_idpre", n_idpre));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
        }

        #endregion
    }
}
