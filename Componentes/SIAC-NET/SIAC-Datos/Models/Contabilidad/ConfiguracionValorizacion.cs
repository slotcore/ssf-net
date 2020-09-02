using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Contabilidad
{

    public class ConfiguracionValorizacion : ObjectBase
    {
        #region constructor
        public ConfiguracionValorizacion()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private string _c_des;

        private string _c_obs;

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

        public string c_des
        {
            get
            {
                return _c_des;
            }

            set
            {
                if (value != _c_des)
                {
                    _c_des = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_obs
        {
            get
            {
                return _c_obs;
            }

            set
            {
                if (value != _c_obs)
                {
                    _c_obs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_metval;
        public string c_metval
        {
            get
            {
                return _c_metval;
            }

            set
            {
                if (value != _c_metval)
                {
                    _c_metval = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_factdist;
        public string c_factdist
        {
            get
            {
                return _c_factdist;
            }

            set
            {
                if (value != _c_factdist)
                {
                    _c_factdist = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_tipdist;
        public string c_tipdist
        {
            get
            {
                return _c_tipdist;
            }

            set
            {
                if (value != _c_tipdist)
                {
                    _c_tipdist = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<ConfiguracionValorizacion> FetchList(int n_idemp)
        {
            List<ConfiguracionValorizacion> m_listentidad = new List<ConfiguracionValorizacion>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_configval_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ConfiguracionValorizacion m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ConfiguracionValorizacion Fetch(int id)
        {
            ConfiguracionValorizacion m_entidad = new ConfiguracionValorizacion();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_configval_traerregistro";
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
                            command.CommandText = "con_configval_insertar";
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
                command.CommandText = "con_configval_insertar";
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
                            command.CommandText = "con_configval_actualizar";
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
                command.CommandText = "con_configval_actualizar";
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
                            command.CommandText = "con_configval_eliminar";
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

        private static ConfiguracionValorizacion SetObject(MySqlDataReader reader)
        {
            return new ConfiguracionValorizacion
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                c_des = reader.GetString("c_des"),
                c_obs = reader.GetString("c_obs"),
                c_metval = reader.GetString("c_metval"),
                c_factdist = reader.GetString("c_factdist"),
                c_tipdist = reader.GetString("c_tipdist")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@c_des", c_des));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
            command.Parameters.Add(new MySqlParameter("@c_metval", c_metval));
            command.Parameters.Add(new MySqlParameter("@c_factdist", c_factdist));
            command.Parameters.Add(new MySqlParameter("@c_tipdist", c_tipdist));
        }

        #endregion
    }
}
