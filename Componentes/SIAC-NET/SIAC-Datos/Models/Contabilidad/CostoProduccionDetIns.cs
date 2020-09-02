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

    public class CostoProduccionDetIns : ObjectBase
    {
        #region constructor
        public CostoProduccionDetIns()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idcostoproddet;

        private int _n_idmov;

        private double _n_costoprom;

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

        public int n_idcostoproddet
        {
            get
            {
                return _n_idcostoproddet;
            }

            set
            {
                if (value != _n_idcostoproddet)
                {
                    _n_idcostoproddet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idite;
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

        public double n_costoprom
        {
            get
            {
                return _n_costoprom;
            }

            set
            {
                if (value != _n_costoprom)
                {
                    _n_costoprom = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_can;
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

        private string _c_descodins;
        public string c_descodins
        {
            get
            {
                return _c_descodins;
            }

            set
            {
                if (value != _c_descodins)
                {
                    _c_descodins = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desins;
        public string c_desins
        {
            get
            {
                return _c_desins;
            }

            set
            {
                if (value != _c_desins)
                {
                    _c_desins = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_destipmov;
        public string c_destipmov
        {
            get
            {
                return _c_destipmov;
            }

            set
            {
                if (value != _c_destipmov)
                {
                    _c_destipmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desunimed;
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

        public static ObservableListSource<CostoProduccionDetIns> FetchList(int n_idcostoproddet)
        {
            ObservableListSource<CostoProduccionDetIns> m_listentidad = new ObservableListSource<CostoProduccionDetIns>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddetins_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idcostoproddet", n_idcostoproddet));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionDetIns m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static CostoProduccionDetIns Fetch(int id)
        {
            CostoProduccionDetIns m_entidad = new CostoProduccionDetIns();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddetins_traerregistro";
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
                            command.CommandText = "con_costoproddetins_insertar";
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
                command.CommandText = "con_costoproddetins_insertar";
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
                            command.CommandText = "con_costoproddetins_actualizar";
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
                command.CommandText = "con_costoproddetins_actualizar";
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
                            command.CommandText = "con_costoproddetins_eliminar";
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

        public static CostoProduccionDetIns SetObject(MySqlDataReader reader)
        {
            return new CostoProduccionDetIns
            {
                n_id = reader.GetInt32("n_id"),
                n_idcostoproddet = reader.GetInt32("n_idcostoproddet"),
                n_idite = reader.GetInt32("n_idite"),
                n_idmov = reader.GetInt32("n_idmov"),
                n_costoprom = reader.GetDouble("n_costoprom"),
                n_can = reader.GetDouble("n_can"),
                c_descodins = reader.GetString("c_descodins"),
                c_desins = reader.GetString("c_desins"),
                c_destipmov = reader.GetString("c_destipmov"),
                c_desunimed = reader.GetString("c_desunimed")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idcostoproddet", n_idcostoproddet));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_idmov", n_idmov));
            command.Parameters.Add(new MySqlParameter("@n_costoprom", n_costoprom));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
        }

        #endregion
    }
}
