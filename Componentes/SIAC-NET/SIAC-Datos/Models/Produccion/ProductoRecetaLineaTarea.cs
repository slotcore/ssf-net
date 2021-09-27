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

    public class ProductoRecetaLineaTarea : ObjectBase
    {
        #region constructor
        public ProductoRecetaLineaTarea()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_idpro;

        private int _n_idrec;

        private int _n_idlin;

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

        public int n_idlin
        {
            get
            {
                return _n_idlin;
            }

            set
            {
                if (value != _n_idlin)
                {
                    _n_idlin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idtar;
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

        private double _n_porefi;
        public double n_porefi
        {
            get
            {
                return _n_porefi;
            }

            set
            {
                if (value != _n_porefi)
                {
                    _n_porefi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_cankilpro;
        public double n_cankilpro
        {
            get
            {
                return _n_cankilpro;
            }

            set
            {
                if (value != _n_cankilpro)
                {
                    _n_cankilpro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_numpertar;
        public int n_numpertar
        {
            get
            {
                return _n_numpertar;
            }

            set
            {
                if (value != _n_numpertar)
                {
                    _n_numpertar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idequipo;
        public int n_idequipo
        {
            get
            {
                return _n_idequipo;
            }

            set
            {
                if (value != _n_idequipo)
                {
                    _n_idequipo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_canequi;
        public int n_canequi
        {
            get
            {
                return _n_canequi;
            }

            set
            {
                if (value != _n_canequi)
                {
                    _n_canequi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_numpertarequ;
        public int n_numpertarequ
        {
            get
            {
                return _n_numpertarequ;
            }

            set
            {
                if (value != _n_numpertarequ)
                {
                    _n_numpertarequ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_capkilporper;
        public double n_capkilporper
        {
            get
            {
                return _n_capkilporper;
            }

            set
            {
                if (value != _n_capkilporper)
                {
                    _n_capkilporper = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_capkilporhorlin;
        public double n_capkilporhorlin
        {
            get
            {
                return _n_capkilporhorlin;
            }

            set
            {
                if (value != _n_capkilporhorlin)
                {
                    _n_capkilporhorlin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_capkilporlintietra;
        public double n_capkilporlintietra
        {
            get
            {
                return _n_capkilporlintietra;
            }

            set
            {
                if (value != _n_capkilporlintietra)
                {
                    _n_capkilporlintietra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_numpercal;
        public double n_numpercal
        {
            get
            {
                return _n_numpercal;
            }

            set
            {
                if (value != _n_numpercal)
                {
                    _n_numpercal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_totprotietra;
        public double n_totprotietra
        {
            get
            {
                return _n_totprotietra;
            }

            set
            {
                if (value != _n_totprotietra)
                {
                    _n_totprotietra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_porefiuni;
        public double n_porefiuni
        {
            get
            {
                return _n_porefiuni;
            }

            set
            {
                if (value != _n_porefiuni)
                {
                    _n_porefiuni = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_porefitot;
        public double n_porefitot
        {
            get
            {
                return _n_porefitot;
            }

            set
            {
                if (value != _n_porefitot)
                {
                    _n_porefitot = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_costar;
        public double n_costar
        {
            get
            {
                return _n_costar;
            }

            set
            {
                if (value != _n_costar)
                {
                    _n_costar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_ord;
        public int n_ord
        {
            get
            {
                return _n_ord;
            }

            set
            {
                if (value != _n_ord)
                {
                    _n_ord = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_kghper;
        public double n_kghper
        {
            get
            {
                return _n_kghper;
            }

            set
            {
                if (value != _n_kghper)
                {
                    _n_kghper = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<ProductoRecetaLineaTarea> FetchList(int n_idpro)
        {
            ObservableListSource<ProductoRecetaLineaTarea> m_listentidad = new ObservableListSource<ProductoRecetaLineaTarea>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetaslineastareas_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductoRecetaLineaTarea m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ProductoRecetaLineaTarea Fetch(int id)
        {
            ProductoRecetaLineaTarea m_entidad = new ProductoRecetaLineaTarea();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productosrecetaslineastareas_traerregistro";
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
                            command.CommandText = "pro_productosrecetaslineastareas_insertar";
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
                command.CommandText = "pro_productosrecetaslineastareas_insertar";
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
                            command.CommandText = "pro_productosrecetaslineastareas_actualizar";
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
                command.CommandText = "pro_productosrecetaslineastareas_actualizar";
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
                            command.CommandText = "pro_productosrecetaslineastareas_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_idlin));
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

        private static ProductoRecetaLineaTarea SetObject(MySqlDataReader reader)
        {
            return new ProductoRecetaLineaTarea
            {
                n_idpro = Genericas.GetInt(reader, "n_idpro"),
                n_idrec = Genericas.GetInt(reader, "n_idrec"),
                n_idlin = Genericas.GetInt(reader, "n_idlin"),
                n_idtar = Genericas.GetInt(reader, "n_idtar"),
                n_porefi = reader.GetDouble("n_porefi"),
                n_cankilpro = reader.GetDouble("n_cankilpro"),
                n_numpertar = Genericas.GetInt(reader, "n_numpertar"),
                n_idequipo = Genericas.GetInt(reader, "n_idequipo"),
                n_canequi = Genericas.GetInt(reader, "n_canequi"),
                n_numpertarequ = Genericas.GetInt(reader, "n_numpertarequ"),
                n_capkilporper = reader.GetDouble("n_capkilporper"),
                n_capkilporhorlin = reader.GetDouble("n_capkilporhorlin"),
                n_capkilporlintietra = reader.GetDouble("n_capkilporlintietra"),
                n_numpercal = reader.GetDouble("n_numpercal"),
                n_totprotietra = reader.GetDouble("n_totprotietra"),
                n_porefiuni = reader.GetDouble("n_porefiuni"),
                n_porefitot = reader.GetDouble("n_porefitot"),
                n_costar = reader.GetDouble("n_costar"),
                n_ord = Genericas.GetInt(reader, "n_ord"),
                n_kghper = reader.GetDouble("n_kghper")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
            command.Parameters.Add(new MySqlParameter("@n_idrec", n_idrec));
            command.Parameters.Add(new MySqlParameter("@n_idlin", n_idlin));
            command.Parameters.Add(new MySqlParameter("@n_idtar", n_idtar));
            command.Parameters.Add(new MySqlParameter("@n_porefi", n_porefi));
            command.Parameters.Add(new MySqlParameter("@n_cankilpro", n_cankilpro));
            command.Parameters.Add(new MySqlParameter("@n_numpertar", n_numpertar));
            command.Parameters.Add(new MySqlParameter("@n_idequipo", n_idequipo));
            command.Parameters.Add(new MySqlParameter("@n_canequi", n_canequi));
            command.Parameters.Add(new MySqlParameter("@n_numpertarequ", n_numpertarequ));
            command.Parameters.Add(new MySqlParameter("@n_capkilporper", n_capkilporper));
            command.Parameters.Add(new MySqlParameter("@n_capkilporhorlin", n_capkilporhorlin));
            command.Parameters.Add(new MySqlParameter("@n_capkilporlintietra", n_capkilporlintietra));
            command.Parameters.Add(new MySqlParameter("@n_numpercal", n_numpercal));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
            command.Parameters.Add(new MySqlParameter("@n_totprotietra", n_totprotietra));
        }

        #endregion
    }
}
