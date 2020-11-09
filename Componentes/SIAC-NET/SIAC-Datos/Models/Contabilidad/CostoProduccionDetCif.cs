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

    public class CostoProduccionDetCif : ObjectBase
    {
        #region constructor
        public CostoProduccionDetCif()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idcostoproddet;
        private int _n_idcue;
        private double _n_impt;
        private string _c_descue;

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

        public int n_idcue
        {
            get
            {
                return _n_idcue;
            }

            set
            {
                if (value != _n_idcue)
                {
                    _n_idcue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_impt
        {
            get
            {
                return _n_impt;
            }

            set
            {
                if (value != _n_impt)
                {
                    _n_impt = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_descue
        {
            get
            {
                return _c_descue;
            }

            set
            {
                if (value != _c_descue)
                {
                    _c_descue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<CostoProduccionDetCif> FetchList(int n_idcostoproddet)
        {
            ObservableListSource<CostoProduccionDetCif> m_listentidad = new ObservableListSource<CostoProduccionDetCif>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddetcif_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idcostoproddet", n_idcostoproddet));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionDetCif m_entidad = SetObject(reader);
                            m_entidad.IsNew = false;
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static CostoProduccionDetCif Fetch(int id)
        {
            CostoProduccionDetCif m_entidad = new CostoProduccionDetCif();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddetcif_traerregistro";
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
                            command.CommandText = "con_costoproddetcif_insertar";
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
                command.CommandText = "con_costoproddetcif_insertar";
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
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        try
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_costoproddetcif_actualizar";
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
                command.CommandText = "con_costoproddetcif_actualizar";
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_costoproddetcif_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
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

        public override void Delete(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "con_costoproddetcif_eliminar";
                command.Parameters.Add(new MySqlParameter("@n_id", n_id));
                int rows = command.ExecuteNonQuery();
            }
        }

        public static void DeleteAll(int n_idcostoproddet, MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "con_costoproddetcif_eliminar_todo";
                command.Parameters.Add(new MySqlParameter("@n_idcostoproddet", n_idcostoproddet));
                int rows = command.ExecuteNonQuery();
            }
        }

        #endregion

        #region metodos privados

        private static CostoProduccionDetCif SetObject(MySqlDataReader reader)
        {
            return new CostoProduccionDetCif
            {
                n_id = reader.GetInt32("n_id"),
                n_idcostoproddet = reader.GetInt32("n_idcostoproddet"),
                n_idcue = reader.GetInt32("n_idcue"),
                n_impt = reader.GetDouble("n_impt"),
                c_descue = reader.GetString("c_descue")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idcostoproddet", n_idcostoproddet));
            command.Parameters.Add(new MySqlParameter("@n_idcue", n_idcue));
            command.Parameters.Add(new MySqlParameter("@n_impt", n_impt));
        }

        #endregion
    }
}
