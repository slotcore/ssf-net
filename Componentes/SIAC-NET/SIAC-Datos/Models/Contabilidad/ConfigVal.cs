using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_DATOS.Models.Contabilidad
{
    public class ConfigVal : ObjectBase
    {
        #region constructor
        public ConfigVal()
        {
            _IsNew = true;
        }

        #endregion

        #region enum
        public static class FactorDistribucion
        {
            public const string Cantidad = "CNT";
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

        private ObservableListSource<ConfigValCue> _ConfigValCues;
        public ObservableListSource<ConfigValCue> ConfigValCues
        {
            get
            {
                if (_ConfigValCues == null)
                {
                    _ConfigValCues = ConfigValCue.FetchList(_n_id);
                }
                return _ConfigValCues;
            }

            set
            {
                if (value != _ConfigValCues)
                {
                    _ConfigValCues = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<ConfigVal> FetchList(int n_idemp)
        {
            ObservableListSource<ConfigVal> m_listentidad = new ObservableListSource<ConfigVal>();

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
                            ConfigVal m_entidad = SetObject(reader);
                            m_entidad.IsNew = false;
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ConfigVal Fetch(int id)
        {
            ConfigVal m_entidad = new ConfigVal();

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
                            m_entidad.IsNew = false;
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_configval_insertar";
                            AddParameters(command);
                            command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                            int rows = command.ExecuteNonQuery();
                            n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
                        }
                        //
                        SaveChildren(connection, transaction);
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

        protected override void Insert(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "con_configval_insertar";
                AddParameters(command);
                command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                int rows = command.ExecuteNonQuery();
                n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
            }
            //
            SaveChildren(connection, transaction);
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
                    try
                    {
                        if (IsOld)
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
                        if (HasOldOrNewChildren)
                        {
                            SaveChildren(connection, transaction);
                        }
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

        protected override bool CheckHasOldOrNewChildren()
        {
            bool isChildOld = false;
            var childsOld = ConfigValCues.Where(o => o.IsNew == true || o.IsOld == true);
            if (childsOld != null)
            {
                if (childsOld.Count() > 0)
                    isChildOld = true;
            }
            if (!isChildOld)
            {
                if (ConfigValCues.GetRemoveItems().Count > 0)
                {
                    isChildOld = true;
                }
            }

            return isChildOld;
        }

        private void SaveChildren(MySqlConnection connection, MySqlTransaction transaction)
        {
            foreach (var hijo in ConfigValCues)
            {
                if (hijo.IsNew)
                    hijo.n_idconfigval = n_id;

                hijo.Save(connection, transaction);
            }

            foreach (var hijo in ConfigValCues.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
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
                            ConfigValCue.DeleteAll(n_id, connection, transaction);

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

        private static ConfigVal SetObject(MySqlDataReader reader)
        {
            return new ConfigVal
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
