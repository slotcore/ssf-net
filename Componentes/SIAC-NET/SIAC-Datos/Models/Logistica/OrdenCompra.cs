using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Logistica
{

    public class OrdenCompra : ObjectBase
    {
        #region constructor
        public OrdenCompra()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idemp;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_anotra;
        private int _n_mestra;
        private DateTime _d_fchemi;
        private DateTime _d_fchent;
        private int _n_idloc;
        private int _n_idare;
        private int _n_idpersol;
        private int _n_idpri;
        private string _c_obs;
        private int _n_idest;
        private int _n_idmot;
        private int _n_idpro;
        private int _n_idmon;
        private double _n_impbru;
        private double _n_impigv;
        private double _n_imptot;
        private int _n_idconpag;
        private double _n_impina;

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

        public int n_idtipdoc
        {
            get
            {
                return _n_idtipdoc;
            }

            set
            {
                if (value != _n_idtipdoc)
                {
                    _n_idtipdoc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_numser
        {
            get
            {
                return _c_numser;
            }

            set
            {
                if (value != _c_numser)
                {
                    _c_numser = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_numdoc
        {
            get
            {
                return _c_numdoc;
            }

            set
            {
                if (value != _c_numdoc)
                {
                    _c_numdoc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_anotra
        {
            get
            {
                return _n_anotra;
            }

            set
            {
                if (value != _n_anotra)
                {
                    _n_anotra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_mestra
        {
            get
            {
                return _n_mestra;
            }

            set
            {
                if (value != _n_mestra)
                {
                    _n_mestra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime d_fchemi
        {
            get
            {
                return _d_fchemi;
            }

            set
            {
                if (value != _d_fchemi)
                {
                    _d_fchemi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime d_fchent
        {
            get
            {
                return _d_fchent;
            }

            set
            {
                if (value != _d_fchent)
                {
                    _d_fchent = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idloc
        {
            get
            {
                return _n_idloc;
            }

            set
            {
                if (value != _n_idloc)
                {
                    _n_idloc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idare
        {
            get
            {
                return _n_idare;
            }

            set
            {
                if (value != _n_idare)
                {
                    _n_idare = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idpersol
        {
            get
            {
                return _n_idpersol;
            }

            set
            {
                if (value != _n_idpersol)
                {
                    _n_idpersol = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idpri
        {
            get
            {
                return _n_idpri;
            }

            set
            {
                if (value != _n_idpri)
                {
                    _n_idpri = value;
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

        public int n_idest
        {
            get
            {
                return _n_idest;
            }

            set
            {
                if (value != _n_idest)
                {
                    _n_idest = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idmot
        {
            get
            {
                return _n_idmot;
            }

            set
            {
                if (value != _n_idmot)
                {
                    _n_idmot = value;
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

        public int n_idmon
        {
            get
            {
                return _n_idmon;
            }

            set
            {
                if (value != _n_idmon)
                {
                    _n_idmon = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_impbru
        {
            get
            {
                return _n_impbru;
            }

            set
            {
                if (value != _n_impbru)
                {
                    _n_impbru = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_impigv
        {
            get
            {
                return _n_impigv;
            }

            set
            {
                if (value != _n_impigv)
                {
                    _n_impigv = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_imptot
        {
            get
            {
                return _n_imptot;
            }

            set
            {
                if (value != _n_imptot)
                {
                    _n_imptot = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idconpag
        {
            get
            {
                return _n_idconpag;
            }

            set
            {
                if (value != _n_idconpag)
                {
                    _n_idconpag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_impina
        {
            get
            {
                return _n_impina;
            }

            set
            {
                if (value != _n_impina)
                {
                    _n_impina = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<OrdenCompraDetalle> _OrdenCompraDetalles;
        public ObservableListSource<OrdenCompraDetalle> OrdenCompraDetalles
        {
            get
            {
                if (_OrdenCompraDetalles == null)
                {
                    _OrdenCompraDetalles = OrdenCompraDetalle.FetchList(_n_id);
                }
                return _OrdenCompraDetalles;
            }

            set
            {
                if (value != _OrdenCompraDetalles)
                {
                    _OrdenCompraDetalles = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<OrdenCompra> FetchList(int n_idemp, int n_anotra)
        {
            List<OrdenCompra> m_listentidad = new List<OrdenCompra>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "log_ordencompra_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrdenCompra m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static OrdenCompra Fetch(int id)
        {
            OrdenCompra m_entidad = new OrdenCompra();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "log_ordencompra_traerregistro";
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
                            command.CommandText = "log_ordencompra_insertar";
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
                command.CommandText = "log_ordencompra_insertar";
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
                            command.CommandText = "log_ordencompra_actualizar";
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
                command.CommandText = "log_ordencompra_actualizar";
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
                            command.CommandText = "log_ordencompra_eliminar";
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

        private static OrdenCompra SetObject(MySqlDataReader reader)
        {
            return new OrdenCompra
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idtipdoc = reader.GetInt32("n_idtipdoc"),
                c_numser = reader.GetString("c_numser"),
                c_numdoc = reader.GetString("c_numdoc"),
                n_anotra = reader.GetInt32("n_anotra"),
                n_mestra = reader.GetInt32("n_mestra"),
                d_fchemi = reader.GetDateTime("d_fchemi"),
                d_fchent = reader.GetDateTime("d_fchent"),
                n_idloc = reader.GetInt32("n_idloc"),
                n_idare = reader.GetInt32("n_idare"),
                n_idpersol = reader.GetInt32("n_idpersol"),
                n_idpri = reader.GetInt32("n_idpri"),
                c_obs = reader.GetString("c_obs"),
                n_idest = reader.GetInt32("n_idest"),
                n_idmot = reader.GetInt32("n_idmot"),
                n_idpro = reader.GetInt32("n_idpro"),
                n_idmon = reader.GetInt32("n_idmon"),
                n_impbru = reader.GetDouble("n_impbru"),
                n_impigv = reader.GetDouble("n_impigv"),
                n_imptot = reader.GetDouble("n_imptot"),
                n_idconpag = reader.GetInt32("n_idconpag"),
                n_impina = reader.GetDouble("n_impina")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idtipdoc", n_idtipdoc));
            command.Parameters.Add(new MySqlParameter("@c_numser", c_numser));
            command.Parameters.Add(new MySqlParameter("@c_numdoc", c_numdoc));
            command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
            command.Parameters.Add(new MySqlParameter("@n_mestra", n_mestra));
            command.Parameters.Add(new MySqlParameter("@d_fchemi", d_fchemi));
            command.Parameters.Add(new MySqlParameter("@d_fchent", d_fchent));
            command.Parameters.Add(new MySqlParameter("@n_idloc", n_idloc));
            command.Parameters.Add(new MySqlParameter("@n_idare", n_idare));
            command.Parameters.Add(new MySqlParameter("@n_idpersol", n_idpersol));
            command.Parameters.Add(new MySqlParameter("@n_idpri", n_idpri));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_idest", n_idest));
            command.Parameters.Add(new MySqlParameter("@n_idmot", n_idmot));
            command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
            command.Parameters.Add(new MySqlParameter("@n_idmon", n_idmon));
            command.Parameters.Add(new MySqlParameter("@n_impbru", n_impbru));
            command.Parameters.Add(new MySqlParameter("@n_impigv", n_impigv));
            command.Parameters.Add(new MySqlParameter("@n_imptot", n_imptot));
            command.Parameters.Add(new MySqlParameter("@n_idconpag", n_idconpag));
            command.Parameters.Add(new MySqlParameter("@n_impina", n_impina));
        }

        #endregion
    }
}
