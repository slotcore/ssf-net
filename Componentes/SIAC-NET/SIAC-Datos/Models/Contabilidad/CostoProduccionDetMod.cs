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

    public class CostoProduccionDetMod : ObjectBase
    {
        #region constructor
        public CostoProduccionDetMod()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idcostoproddet;

        private int _n_idprosoltar;

        private int _n_idtar;

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

        public int n_idprosoltar
        {
            get
            {
                return _n_idprosoltar;
            }

            set
            {
                if (value != _n_idprosoltar)
                {
                    _n_idprosoltar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idtar
        {
            get
            {
                return _n_idtar;
            }

            set
            {
                if (value != _n_idtar)
                {
                    _n_idtar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idtra;
        public int n_idtra
        {
            get
            {
                return _n_idtra;
            }

            set
            {
                if (value != _n_idtra)
                {
                    _n_idtra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_costo;
        public double n_costo
        {
            get
            {
                return _n_costo;
            }

            set
            {
                if (value != _n_costo)
                {
                    _n_costo = value;
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

        private string _c_horini;
        public string c_horini
        {
            get
            {
                return _c_horini;
            }

            set
            {
                if (value != _c_horini)
                {
                    _c_horini = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_horfin;
        public string c_horfin
        {
            get
            {
                return _c_horfin;
            }

            set
            {
                if (value != _c_horfin)
                {
                    _c_horfin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_destra;
        public string c_destra
        {
            get
            {
                return _c_destra;
            }

            set
            {
                if (value != _c_destra)
                {
                    _c_destra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_destar;
        public string c_destar
        {
            get
            {
                return _c_destar;
            }

            set
            {
                if (value != _c_destar)
                {
                    _c_destar = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<CostoProduccionDetMod> FetchList(int n_idcostoproddet)
        {
            List<CostoProduccionDetMod> m_listentidad = new List<CostoProduccionDetMod>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddetmod_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idcostoproddet", n_idcostoproddet));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionDetMod m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static CostoProduccionDetMod Fetch(int id)
        {
            CostoProduccionDetMod m_entidad = new CostoProduccionDetMod();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddetmod_traerregistro";
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
                            command.CommandText = "con_costoproddetmod_insertar";
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
                command.CommandText = "con_costoproddetmod_insertar";
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
                            command.CommandText = "con_costoproddetmod_actualizar";
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
                command.CommandText = "con_costoproddetmod_actualizar";
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
                            command.CommandText = "con_costoproddetmod_eliminar";
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

        private static CostoProduccionDetMod SetObject(MySqlDataReader reader)
        {
            return new CostoProduccionDetMod
            {
                n_id = reader.GetInt32("n_id"),
                n_idcostoproddet = reader.GetInt32("n_idcostoproddet"),
                n_idprosoltar = reader.GetInt32("n_idprosoltar"),
                n_idtar = reader.GetInt32("n_idtar"),
                n_idtra = reader.GetInt32("n_idtra"),
                n_costo = reader.GetDouble("n_costo"),
                n_can = reader.GetDouble("n_can"),
                c_horini = reader.GetString("c_horini"),
                c_horfin = reader.GetString("c_horfin"),
                c_destra = reader.GetString("c_destra"),
                c_destar = reader.GetString("c_destar")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idcostoproddet", n_idcostoproddet));
            command.Parameters.Add(new MySqlParameter("@n_idprosoltar", n_idprosoltar));
            command.Parameters.Add(new MySqlParameter("@n_idtar", n_idtar));
            command.Parameters.Add(new MySqlParameter("@n_idtra", n_idtra));
            command.Parameters.Add(new MySqlParameter("@n_costo", n_costo));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
        }

        #endregion
    }
}
