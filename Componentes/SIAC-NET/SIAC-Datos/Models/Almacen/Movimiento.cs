using Helper;
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
        private DateTime _d_fchdoc;
        private DateTime _d_fching;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_idalm;
        private int _n_anotra;
        private int _n_idmes;
        private string _c_obs;
        private int _n_idtipope;
        private int _n_tipite;
        private int _n_docrefidtipdoc;
        private string _c_docrefnumser;
        private string _c_docrefnumdoc;
        private int _n_docrefiddocref;
        private int _n_perid;
        private int _n_prolog;
        private int _n_iddoclog;

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

        public static Movimiento FetchPorInventario(int n_idinvini)
        {
            Movimiento m_entidad = null;

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_movimientos_traerregistro_por_inventario";
                    command.Parameters.Add(new MySqlParameter("@n_idinvini", n_idinvini));
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_movimientos_insertar";
                            AddParameters(command);
                            command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                            int rows = command.ExecuteNonQuery();
                            n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
                        }
                        //
                        SaveChildren(connection, transaction);
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
                command.CommandText = "alm_movimientos_insertar";
                AddParameters(command);
                command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                int rows = command.ExecuteNonQuery();
                n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
            }
            //
            SaveChildren(connection, transaction);
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

        private void SaveChildren(MySqlConnection connection, MySqlTransaction transaction)
        {
            foreach (var hijo in MovimientoDets)
            {
                if (hijo.IsNew)
                    hijo.n_idmov = n_id;

                hijo.Save(connection, transaction);
            }

            foreach (var hijo in MovimientoDets.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
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
                            MovimientoDet.DeleteAll(n_id, connection, transaction);

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
                n_idclipro = reader.GetInt32("n_idclipro"),
                d_fchdoc = Genericas.GetDateTime(reader, "d_fchdoc"),
                d_fching = Genericas.GetDateTime(reader, "d_fching"),
                n_idtipdoc = Genericas.GetInt(reader, "n_idtipdoc"),
                c_numser = Genericas.GetString(reader, "c_numser"),
                c_numdoc = Genericas.GetString(reader, "c_numdoc"),
                n_idalm = Genericas.GetInt(reader, "n_idalm"),
                n_anotra = Genericas.GetInt(reader, "n_anotra"),
                n_idmes = Genericas.GetInt(reader, "n_idmes"),
                c_obs = Genericas.GetString(reader, "c_obs"),
                n_idtipope = Genericas.GetInt(reader, "n_idtipope"),
                n_tipite = Genericas.GetInt(reader, "n_tipite"),
                n_docrefidtipdoc = Genericas.GetInt(reader, "n_docrefidtipdoc"),
                c_docrefnumser = Genericas.GetString(reader, "c_docrefnumser"),
                c_docrefnumdoc = Genericas.GetString(reader, "c_docrefnumdoc"),
                n_docrefiddocref = Genericas.GetInt(reader, "n_docrefiddocref"),
                n_perid = Genericas.GetInt(reader, "n_perid"),
                n_prolog = Genericas.GetInt(reader, "n_prolog"),
                n_iddoclog = Genericas.GetInt(reader, "n_iddoclog")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idtipmov", n_idtipmov));
            command.Parameters.Add(new MySqlParameter("@n_idclipro", n_idclipro));
            command.Parameters.Add(new MySqlParameter("@d_fchdoc", d_fchdoc));
            command.Parameters.Add(new MySqlParameter("@d_fching", d_fching));
            command.Parameters.Add(new MySqlParameter("@n_idtipdoc", n_idtipdoc));
            command.Parameters.Add(new MySqlParameter("@c_numser", c_numser));
            command.Parameters.Add(new MySqlParameter("@c_numdoc", c_numdoc));
            command.Parameters.Add(new MySqlParameter("@n_idalm", n_idalm));
            command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
            command.Parameters.Add(new MySqlParameter("@n_idmes", n_idmes));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_idtipope", n_idtipope));
            command.Parameters.Add(new MySqlParameter("@n_tipite", n_tipite));
            command.Parameters.Add(new MySqlParameter("@n_docrefidtipdoc", n_docrefidtipdoc));
            command.Parameters.Add(new MySqlParameter("@c_docrefnumser", c_docrefnumser));
            command.Parameters.Add(new MySqlParameter("@c_docrefnumdoc", c_docrefnumdoc));
            command.Parameters.Add(new MySqlParameter("@n_docrefiddocref", n_docrefiddocref));
            command.Parameters.Add(new MySqlParameter("@n_perid", n_perid));
            command.Parameters.Add(new MySqlParameter("@n_prolog", n_prolog));
            command.Parameters.Add(new MySqlParameter("@n_iddoclog", n_iddoclog));
        }

        #endregion
    }
}
