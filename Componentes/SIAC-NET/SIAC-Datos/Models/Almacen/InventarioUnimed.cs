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

    public class InventarioUnimed : ObjectBase
    {
        #region constructor
        public InventarioUnimed()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idite;

        private string _c_despre;

        private string _c_abrpre;

        public int n_id
        {
            get
            {
                return _n_id;
            }

            set
            {
                if (value != _n_id)
                {
                    _n_id = value;
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

        public string c_despre
        {
            get
            {
                return _c_despre;
            }

            set
            {
                if (value != _c_despre)
                {
                    _c_despre = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_abrpre
        {
            get
            {
                return _c_abrpre;
            }

            set
            {
                if (value != _c_abrpre)
                {
                    _c_abrpre = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idunimedbas;
        public int n_idunimedbas
        {
            get
            {
                return _n_idunimedbas;
            }

            set
            {
                if (value != _n_idunimedbas)
                {
                    _n_idunimedbas = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_canunimedbas;
        public double n_canunimedbas
        {
            get
            {
                return _n_canunimedbas;
            }

            set
            {
                if (value != _n_canunimedbas)
                {
                    _n_canunimedbas = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_default;
        public int n_default
        {
            get
            {
                return _n_default;
            }

            set
            {
                if (value != _n_default)
                {
                    _n_default = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<InventarioUnimed> FetchList(int n_idite)
        {
            ObservableListSource<InventarioUnimed> m_listentidad = new ObservableListSource<InventarioUnimed>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventariounimed_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InventarioUnimed m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static InventarioUnimed Fetch(int id)
        {
            InventarioUnimed m_entidad = new InventarioUnimed();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventariounimed_traerregistro";
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
                            command.CommandText = "alm_inventariounimed_insertar";
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
                command.CommandText = "alm_inventariounimed_insertar";
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
                            command.CommandText = "alm_inventariounimed_actualizar";
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
                command.CommandText = "alm_inventariounimed_actualizar";
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
                            command.CommandText = "alm_inventariounimed_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
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

        private static InventarioUnimed SetObject(MySqlDataReader reader)
        {
            return new InventarioUnimed
            {
                n_id = reader.GetInt32("n_id"),
                n_idite = reader.GetInt32("n_idite"),
                c_despre = reader.GetString("c_despre"),
                c_abrpre = reader.GetString("c_abrpre"),
                n_idunimedbas = reader.GetInt32("n_idunimedbas"),
                n_canunimedbas = reader.GetDouble("n_canunimedbas"),
                n_default = reader.GetInt32("n_default")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@c_despre", c_despre));
            command.Parameters.Add(new MySqlParameter("@c_abrpre", c_abrpre));
            command.Parameters.Add(new MySqlParameter("@n_idunimedbas", n_idunimedbas));
            command.Parameters.Add(new MySqlParameter("@n_canunimedbas", n_canunimedbas));
            command.Parameters.Add(new MySqlParameter("@n_default", n_default));
        }

        #endregion
    }
}
