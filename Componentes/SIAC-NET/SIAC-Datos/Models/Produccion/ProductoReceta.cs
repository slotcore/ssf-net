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

    public class ProductoReceta : ObjectBase
    {
        #region constructor
        public ProductoReceta()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idpro;

        private string _c_codrec;

        private string _c_des;

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

        public string c_codrec
        {
            get
            {
                return _c_codrec;
            }

            set
            {
                if (value != _c_codrec)
                {
                    _c_codrec = value;
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

        private int _n_prirec;
        public int n_prirec
        {
            get
            {
                return _n_prirec;
            }

            set
            {
                if (value != _n_prirec)
                {
                    _n_prirec = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_obs;
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

        private int _n_act;
        public int n_act
        {
            get
            {
                return _n_act;
            }

            set
            {
                if (value != _n_act)
                {
                    _n_act = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<ProductoRecetaInsumo> _ProductoRecetaInsumos;
        public ObservableListSource<ProductoRecetaInsumo> ProductoRecetaInsumos
        {
            get
            {
                if (_ProductoRecetaInsumos == null)
                {
                    _ProductoRecetaInsumos = ProductoRecetaInsumo.FetchList(_n_idpro, _n_id);
                }
                return _ProductoRecetaInsumos;
            }

            set
            {
                if (value != _ProductoRecetaInsumos)
                {
                    _ProductoRecetaInsumos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<ProductoRecetaLinea> _ProductoRecetaLineas;
        public ObservableListSource<ProductoRecetaLinea> ProductoRecetaLineas
        {
            get
            {
                if (_ProductoRecetaLineas == null)
                {
                    _ProductoRecetaLineas = ProductoRecetaLinea.FetchList(_n_idpro);
                }
                return _ProductoRecetaLineas;
            }

            set
            {
                if (value != _ProductoRecetaLineas)
                {
                    _ProductoRecetaLineas = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<ProductoReceta> FetchList(int n_idpro)
        {
            ObservableListSource<ProductoReceta> m_listentidad = new ObservableListSource<ProductoReceta>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetas_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductoReceta m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ProductoReceta Fetch(int id)
        {
            ProductoReceta m_entidad = new ProductoReceta();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetas_traerregistro";
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
                            command.CommandText = "pro_productosrecetas_insertar";
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
                command.CommandText = "pro_productosrecetas_insertar";
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
                            command.CommandText = "pro_productosrecetas_actualizar";
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
                command.CommandText = "pro_productosrecetas_actualizar";
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
                            command.CommandText = "pro_productosrecetas_eliminar";
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

        private static ProductoReceta SetObject(MySqlDataReader reader)
        {
            return new ProductoReceta
            {
                n_id = Genericas.GetInt(reader, "n_id"),
                n_idpro = Genericas.GetInt(reader, "n_idpro"),
                c_codrec = Genericas.GetString(reader, "c_codrec"),
                c_des = Genericas.GetString(reader, "c_des"),
                n_idunimed = Genericas.GetInt(reader, "n_idunimed"),
                n_can = reader.GetDouble("n_can"),
                n_prirec = Genericas.GetInt(reader, "n_prirec"),
                c_obs = Genericas.GetString(reader, "c_obs"),
                n_act = Genericas.GetInt(reader, "n_act")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
            command.Parameters.Add(new MySqlParameter("@c_codrec", c_codrec));
            command.Parameters.Add(new MySqlParameter("@c_des", c_des));
            command.Parameters.Add(new MySqlParameter("@n_idunimed", n_idunimed));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
            command.Parameters.Add(new MySqlParameter("@n_prirec", n_prirec));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_act", n_act));
        }

        #endregion
    }
}
