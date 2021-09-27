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

    public class ProductoRecetaLinea : ObjectBase
    {
        #region constructor
        public ProductoRecetaLinea()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idpro;

        private int _n_idrec;

        private string _c_codlin;

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

        public string c_codlin
        {
            get
            {
                return _c_codlin;
            }

            set
            {
                if (value != _c_codlin)
                {
                    _c_codlin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_deslin;
        public string c_deslin
        {
            get
            {
                return _c_deslin;
            }

            set
            {
                if (value != _c_deslin)
                {
                    _c_deslin = value;
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

        private double _n_numope;
        public double n_numope
        {
            get
            {
                return _n_numope;
            }

            set
            {
                if (value != _n_numope)
                {
                    _n_numope = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_efi;
        public double n_efi
        {
            get
            {
                return _n_efi;
            }

            set
            {
                if (value != _n_efi)
                {
                    _n_efi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_tiepro;
        public double n_tiepro
        {
            get
            {
                return _n_tiepro;
            }

            set
            {
                if (value != _n_tiepro)
                {
                    _n_tiepro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_prehorjor;
        public double n_prehorjor
        {
            get
            {
                return _n_prehorjor;
            }

            set
            {
                if (value != _n_prehorjor)
                {
                    _n_prehorjor = value;
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

        private ObservableListSource<ProductoRecetaLineaTarea> _ProductoRecetaLineaTareas;
        public ObservableListSource<ProductoRecetaLineaTarea> ProductoRecetaLineaTareas
        {
            get
            {
                if (_ProductoRecetaLineaTareas == null)
                {
                    _ProductoRecetaLineaTareas = ProductoRecetaLineaTarea.FetchList(_n_idpro);
                }
                return _ProductoRecetaLineaTareas;
            }

            set
            {
                if (value != _ProductoRecetaLineaTareas)
                {
                    _ProductoRecetaLineaTareas = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<ProductoRecetaLinea> FetchList(int n_idpro)
        {
            ObservableListSource<ProductoRecetaLinea> m_listentidad = new ObservableListSource<ProductoRecetaLinea>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetaslineas_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductoRecetaLinea m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ProductoRecetaLinea Fetch(int id)
        {
            ProductoRecetaLinea m_entidad = new ProductoRecetaLinea();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetaslineas_traerregistro";
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
                            command.CommandText = "pro_productosrecetaslineas_insertar";
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
                command.CommandText = "pro_productosrecetaslineas_insertar";
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
                            command.CommandText = "pro_productosrecetaslineas_actualizar";
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
                command.CommandText = "pro_productosrecetaslineas_actualizar";
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
                            command.CommandText = "pro_productosrecetaslineas_eliminar";
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

        private static ProductoRecetaLinea SetObject(MySqlDataReader reader)
        {
            return new ProductoRecetaLinea
            {
                n_id = Genericas.GetInt(reader, "n_id"),
                n_idpro = Genericas.GetInt(reader, "n_idpro"),
                n_idrec = Genericas.GetInt(reader, "n_idrec"),
                c_codlin = Genericas.GetString(reader, "c_codlin"),
                c_deslin = Genericas.GetString(reader, "c_deslin"),
                n_idunimed = Genericas.GetInt(reader, "n_idunimed"),
                n_idite = Genericas.GetInt(reader, "n_idite"),
                n_can = reader.GetDouble("n_can"),
                n_numope = reader.GetDouble("n_numope"),
                n_efi = reader.GetDouble("n_efi"),
                n_tiepro = reader.GetDouble("n_tiepro"),
                n_prehorjor = reader.GetDouble("n_prehorjor"),
                n_act = Genericas.GetInt(reader, "n_act"),
                c_obs = Genericas.GetString(reader, "c_obs")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
            command.Parameters.Add(new MySqlParameter("@n_idrec", n_idrec));
            command.Parameters.Add(new MySqlParameter("@c_codlin", c_codlin));
            command.Parameters.Add(new MySqlParameter("@c_deslin", c_deslin));
            command.Parameters.Add(new MySqlParameter("@n_idunimed", n_idunimed));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
            command.Parameters.Add(new MySqlParameter("@n_numope", n_numope));
            command.Parameters.Add(new MySqlParameter("@n_efi", n_efi));
            command.Parameters.Add(new MySqlParameter("@n_tiepro", n_tiepro));
            command.Parameters.Add(new MySqlParameter("@n_prehorjor", n_prehorjor));
            command.Parameters.Add(new MySqlParameter("@n_act", n_act));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
        }

        #endregion
    }
}
