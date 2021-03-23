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

    public class Movimiento : ObjectBase
    {
        #region constructor
        public Movimiento()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private int _n_idtipmov;

        private int _n_idclipro;

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

        public int n_idtipmov
        {
            get
            {
                return _n_idtipmov;
            }

            set
            {
                if (value != _n_idtipmov)
                {
                    _n_idtipmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idclipro
        {
            get
            {
                return _n_idclipro;
            }

            set
            {
                if (value != _n_idclipro)
                {
                    _n_idclipro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _d_fchdoc;
        public DateTime d_fchdoc
        {
            get
            {
                return _d_fchdoc;
            }

            set
            {
                if (value != _d_fchdoc)
                {
                    _d_fchdoc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _d_fching;
        public DateTime d_fching
        {
            get
            {
                return _d_fching;
            }

            set
            {
                if (value != _d_fching)
                {
                    _d_fching = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idtipdoc;
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

        private string _c_numser;
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

        private string _c_numdoc;
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

        private int _n_idalm;
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

        private int _n_anotra;
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

        private int _n_idmes;
        public int n_idmes
        {
            get
            {
                return _n_idmes;
            }

            set
            {
                if (value != _n_idmes)
                {
                    _n_idmes = value;
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

        private int _n_idtipope;
        public int n_idtipope
        {
            get
            {
                return _n_idtipope;
            }

            set
            {
                if (value != _n_idtipope)
                {
                    _n_idtipope = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_tipite;
        public int n_tipite
        {
            get
            {
                return _n_tipite;
            }

            set
            {
                if (value != _n_tipite)
                {
                    _n_tipite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_docrefidtipdoc;
        public int n_docrefidtipdoc
        {
            get
            {
                return _n_docrefidtipdoc;
            }

            set
            {
                if (value != _n_docrefidtipdoc)
                {
                    _n_docrefidtipdoc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_docrefnumser;
        public string c_docrefnumser
        {
            get
            {
                return _c_docrefnumser;
            }

            set
            {
                if (value != _c_docrefnumser)
                {
                    _c_docrefnumser = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_docrefnumdoc;
        public string c_docrefnumdoc
        {
            get
            {
                return _c_docrefnumdoc;
            }

            set
            {
                if (value != _c_docrefnumdoc)
                {
                    _c_docrefnumdoc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_docrefiddocref;
        public int n_docrefiddocref
        {
            get
            {
                return _n_docrefiddocref;
            }

            set
            {
                if (value != _n_docrefiddocref)
                {
                    _n_docrefiddocref = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_perid;
        public int n_perid
        {
            get
            {
                return _n_perid;
            }

            set
            {
                if (value != _n_perid)
                {
                    _n_perid = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_prolog;
        public int n_prolog
        {
            get
            {
                return _n_prolog;
            }

            set
            {
                if (value != _n_prolog)
                {
                    _n_prolog = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_iddoclog;
        public int n_iddoclog
        {
            get
            {
                return _n_iddoclog;
            }

            set
            {
                if (value != _n_iddoclog)
                {
                    _n_iddoclog = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<MovimientoDet> _MovimientoDets;
        public ObservableListSource<MovimientoDet> MovimientoDets
        {
            get
            {
                if (_MovimientoDets == null)
                {
                    _MovimientoDets = MovimientoDet.FetchList(_n_id);
                }
                return _MovimientoDets;
            }

            set
            {
                if (value != _MovimientoDets)
                {
                    _MovimientoDets = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<InventarioLote> _InventarioLotes;
        public ObservableListSource<InventarioLote> InventarioLotes
        {
            get
            {
                if (_InventarioLotes == null)
                {
                    _InventarioLotes = InventarioLote.FetchList(_n_id);
                }
                return _InventarioLotes;
            }

            set
            {
                if (value != _InventarioLotes)
                {
                    _InventarioLotes = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<Movimiento> FetchList(int n_idemp, int n_anotra)
        {
            List<Movimiento> m_listentidad = new List<Movimiento>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_movimientos_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Movimiento m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static Movimiento Fetch(int id)
        {
            Movimiento m_entidad = new Movimiento();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_movimientos_traerregistro";
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
                            command.CommandText = "alm_movimientos_insertar";
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
                command.CommandText = "alm_movimientos_insertar";
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
                            command.CommandText = "alm_movimientos_actualizar";
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
                command.CommandText = "alm_movimientos_actualizar";
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
                            command.CommandText = "alm_movimientos_eliminar";
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

        private static Movimiento SetObject(MySqlDataReader reader)
        {
            return new Movimiento
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idtipmov = reader.GetInt32("n_idtipmov"),
                n_idclipro = reader.GetInt32("n_idclipro")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idtipmov", n_idtipmov));
            command.Parameters.Add(new MySqlParameter("@n_idclipro", n_idclipro));
        }

        #endregion
    }
}
