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

    public class InventarioLote : ObjectBase
    {
        #region constructor
        public InventarioLote()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_idemp;

        private int _n_idite;

        private int _n_iddocmov;

        private DateTime _d_fchmov;

        public int n_idemp
        {
            get
            {
                return _n_idemp;
            }

            set
            {
                if (value != _n_idemp)
                {
                    _n_idemp = value;
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

        public int n_iddocmov
        {
            get
            {
                return _n_iddocmov;
            }

            set
            {
                if (value != _n_iddocmov)
                {
                    _n_iddocmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime d_fchmov
        {
            get
            {
                return _d_fchmov;
            }

            set
            {
                if (value != _d_fchmov)
                {
                    _d_fchmov = value;
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

        private int _n_idunimed;
        public int n_idunimed
        {
            get
            {
                return _n_idunimed;
            }

            set
            {
                if (value != _n_idunimed)
                {
                    _n_idunimed = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_caning;
        public double n_caning
        {
            get
            {
                return _n_caning;
            }

            set
            {
                if (value != _n_caning)
                {
                    _n_caning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_cansal;
        public double n_cansal
        {
            get
            {
                return _n_cansal;
            }

            set
            {
                if (value != _n_cansal)
                {
                    _n_cansal = value;
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

        private string _c_oriite;
        public string c_oriite
        {
            get
            {
                return _c_oriite;
            }

            set
            {
                if (value != _c_oriite)
                {
                    _c_oriite = value;
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
        #endregion

        #region metodos publicos

        public static ObservableListSource<InventarioLote> FetchList(int n_idmov)
        {
            ObservableListSource<InventarioLote> m_listentidad = new ObservableListSource<InventarioLote>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventariolotes_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idmov));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InventarioLote m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static InventarioLote Fetch(int id)
        {
            InventarioLote m_entidad = new InventarioLote();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventariolotes_traerregistro";
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
                            command.CommandText = "alm_inventariolotes_insertar";
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
                command.CommandText = "alm_inventariolotes_insertar";
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
                            command.CommandText = "alm_inventariolotes_actualizar";
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
                command.CommandText = "alm_inventariolotes_actualizar";
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
                            command.CommandText = "alm_inventariolotes_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_idemp));
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

        private static InventarioLote SetObject(MySqlDataReader reader)
        {
            return new InventarioLote
            {
                n_idemp = reader.GetInt32("n_id"),
                n_idite = reader.GetInt32("n_idite"),
                n_iddocmov = reader.GetInt32("n_iddocmov"),
                d_fchmov = reader.GetDateTime("d_fchmov")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_iddocmov", n_iddocmov));
            command.Parameters.Add(new MySqlParameter("@d_fchmov", d_fchmov));
        }

        #endregion
    }
}
