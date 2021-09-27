using Helper;
using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Produccion
{

    public class ProductoRecetaInsumo : ObjectBase
    {
        #region constructor
        public ProductoRecetaInsumo()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_idpro;

        private int _n_idrec;

        private int _n_idite;

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

        public int n_idrec
        {
            get
            {
                return _n_idrec;
            }

            set
            {
                if (value != _n_idrec)
                {
                    _n_idrec = value;
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

        private int _n_inspri;
        public int n_inspri
        {
            get
            {
                return _n_inspri;
            }

            set
            {
                if (value != _n_inspri)
                {
                    _n_inspri = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<ProductoRecetaInsumo> FetchList(int n_idpro, int n_idrec)
        {
            ObservableListSource<ProductoRecetaInsumo> m_listentidad = new ObservableListSource<ProductoRecetaInsumo>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetasinsumos_listar_v2";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
                    command.Parameters.Add(new MySqlParameter("@n_idrec", n_idrec));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductoRecetaInsumo m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ProductoRecetaInsumo Fetch(int id)
        {
            ProductoRecetaInsumo m_entidad = new ProductoRecetaInsumo();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetasinsumos_traerregistro";
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
                            command.CommandText = "pro_productosrecetasinsumos_insertar";
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
                command.CommandText = "pro_productosrecetasinsumos_insertar";
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
                            command.CommandText = "pro_productosrecetasinsumos_actualizar";
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
                command.CommandText = "pro_productosrecetasinsumos_actualizar";
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
                            command.CommandText = "pro_productosrecetasinsumos_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_idpro));
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

        private static ProductoRecetaInsumo SetObject(MySqlDataReader reader)
        {
            return new ProductoRecetaInsumo
            {
                n_idpro = Genericas.GetInt(reader, "n_idpro"),
                n_idrec = Genericas.GetInt(reader, "n_idrec"),
                n_idite = Genericas.GetInt(reader, "n_idite"),
                n_idunimed = Genericas.GetInt(reader, "n_idunimed"),
                n_can = reader.GetDouble("n_can"),
                n_inspri = Genericas.GetInt(reader, "n_inspri")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
            command.Parameters.Add(new MySqlParameter("@n_idrec", n_idrec));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_idunimed", n_idunimed));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
            command.Parameters.Add(new MySqlParameter("@n_inspri", n_inspri));
        }

        #endregion
    }
}
