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

    public class InventarioInicialDet : ObjectBase
    {
        #region constructor
        public InventarioInicialDet()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idinvini;
        private int _n_idite;
        private int _n_idunimed;
        private double _n_can;
        private double _n_costounit;
        private string _c_codite;
        private string _c_desite;
        private string _c_desunimed;

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

        public int n_idinvini
        {
            get
            {
                return _n_idinvini;
            }

            set
            {
                if (value != _n_idinvini)
                {
                    _n_idinvini = value;
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

        public double n_costounit
        {
            get
            {
                return _n_costounit;
            }

            set
            {
                if (value != _n_costounit)
                {
                    _n_costounit = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_codite
        {
            get
            {
                return _c_codite;
            }

            set
            {
                if (value != _c_codite)
                {
                    _c_codite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_desite
        {
            get
            {
                return _c_desite;
            }

            set
            {
                if (value != _c_desite)
                {
                    _c_desite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_desunimed
        {
            get
            {
                return _c_desunimed;
            }

            set
            {
                if (value != _c_desunimed)
                {
                    _c_desunimed = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<InventarioInicialDet> FetchList(int n_idinvini)
        {
            ObservableListSource<InventarioInicialDet> m_listentidad = new ObservableListSource<InventarioInicialDet>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventarioinidet_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idinvini", n_idinvini));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InventarioInicialDet m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static InventarioInicialDet Fetch(int id)
        {
            InventarioInicialDet m_entidad = new InventarioInicialDet();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventarioinidet_traerregistro";
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_inventarioinidet_insertar";
                            AddParameters(command);
                            command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                            int rows = command.ExecuteNonQuery();
                            n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
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

        protected override void Insert(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "alm_inventarioinidet_insertar";
                AddParameters(command);
                command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                int rows = command.ExecuteNonQuery();
                n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_inventarioinidet_actualizar";
                            AddParameters(command);
                            int rows = command.ExecuteNonQuery();
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

        protected override void Update(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "alm_inventarioinidet_actualizar";
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
                            command.CommandText = "alm_inventarioinidet_eliminar";
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

        public static void DeleteAll(int n_idinvini, MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "alm_inventarioinidet_eliminar_todo";
                command.Parameters.Add(new MySqlParameter("@n_idinvini", n_idinvini));
                int rows = command.ExecuteNonQuery();
            }
        }

        #endregion

        #region metodos privados

        private static InventarioInicialDet SetObject(MySqlDataReader reader)
        {
            return new InventarioInicialDet
            {
                n_id = reader.GetInt32("n_id"),
                n_idinvini = reader.GetInt32("n_idinvini"),
                n_idite = reader.GetInt32("n_idite"),
                n_idunimed = reader.GetInt32("n_idunimed"),
                n_can = reader.GetDouble("n_can"),
                n_costounit = reader.GetDouble("n_costounit"),
                c_codite = reader.GetString("c_codite"),
                c_desite = reader.GetString("c_desite"),
                c_desunimed = reader.GetString("c_desunimed")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idinvini", n_idinvini));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_idunimed", n_idunimed));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
            command.Parameters.Add(new MySqlParameter("@n_costounit", n_costounit));
        }

        #endregion
    }
}
