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

    public class ItemMovimiento : ObjectBase
    {
        #region constructor
        public ItemMovimiento()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idite;
        private int _n_idalm;
        private double _n_saldoini;
        private double _n_costoini;
        private double _n_costounipromini;
        private string _c_codite;
        private string _c_desite;
        private string _c_desalm;

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

        public int n_idalm
        {
            get
            {
                return _n_idalm;
            }

            set
            {
                if (value != _n_idalm)
                {
                    _n_idalm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_saldoini
        {
            get
            {
                return _n_saldoini;
            }

            set
            {
                if (value != _n_saldoini)
                {
                    _n_saldoini = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costoini
        {
            get
            {
                return _n_costoini;
            }

            set
            {
                if (value != _n_costoini)
                {
                    _n_costoini = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costounipromini
        {
            get
            {
                return _n_costounipromini;
            }

            set
            {
                if (value != _n_costounipromini)
                {
                    _n_costounipromini = value;
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

        public string c_desalm
        {
            get
            {
                return _c_desalm;
            }

            set
            {
                if (value != _c_desalm)
                {
                    _c_desalm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<ItemMovimientoDetalle> _ItemMovimientoDetalles;
        public ObservableListSource<ItemMovimientoDetalle> ItemMovimientoDetalles
        {
            get
            {
                return _ItemMovimientoDetalles;
            }

            set
            {
                if (value != _ItemMovimientoDetalles)
                {
                    _ItemMovimientoDetalles = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<ItemMovimiento> FetchList(int n_idemp, int n_anotra)
        {
            List<ItemMovimiento> m_listentidad = new List<ItemMovimiento>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproduccionmovimiento_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemMovimiento m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static List<ItemMovimiento> TraerMovimientosPorTipo(int n_idemp, int n_anotra, int n_idmes, int n_idTipoItem)
        {
            List<ItemMovimiento> m_listentidad = new List<ItemMovimiento>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproduccionmovimiento_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemMovimiento m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static List<ItemMovimiento> TraerMovimientoPorParte(int n_idprod)
        {
            List<ItemMovimiento> m_lstentidad = new List<ItemMovimiento>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproduccionmovimiento_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idprod));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m_lstentidad.Add(SetObject(reader));
                        }
                    }
                }
            }
            return m_lstentidad;
        }


        public static ItemMovimiento TraerMovimientoPorFecha(int n_idite, int n_idalm, DateTime d_fchini, DateTime d_fchfin)
        {
            ItemMovimiento m_entidad = new ItemMovimiento();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproduccionmovimiento_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idite));
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

        public static ItemMovimiento Fetch(int id)
        {
            ItemMovimiento m_entidad = new ItemMovimiento();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproduccionmovimiento_traerregistro";
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
                            command.CommandText = "con_costoproduccionmovimiento_insertar";
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
                command.CommandText = "con_costoproduccionmovimiento_insertar";
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
                            command.CommandText = "con_costoproduccionmovimiento_actualizar";
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
                command.CommandText = "con_costoproduccionmovimiento_actualizar";
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
                            command.CommandText = "con_costoproduccionmovimiento_eliminar";
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

        public static ItemMovimiento SetObject(MySqlDataReader reader)
        {
            return new ItemMovimiento
            {
                n_id = reader.GetInt32("n_id"),
                n_idite = reader.GetInt32("n_idite"),
                n_idalm = reader.GetInt32("n_idalm"),
                c_codite = reader.GetString("c_codite"),
                c_desite = reader.GetString("c_desite"),
                c_desalm = reader.GetString("c_desalm")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_idalm", n_idalm));
        }

        #endregion
    }
}
