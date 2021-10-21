using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using SIAC_DATOS.Models.Sunat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Almacen
{

    public class InventarioInicial : ObjectBase
    {
        #region constructor
        public InventarioInicial()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idemp;
        private int _n_idalm;
        private int _n_idmon;
        private string _c_numser;
        private string _c_numdoc;
        private string _c_des;
        private DateTime _d_fchvig;
        private int _n_activo;
        private string _c_desemp;
        private string _c_desalm;
        private string _c_desmon;

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

        public DateTime d_fchvig
        {
            get
            {
                return _d_fchvig;
            }

            set
            {
                if (value != _d_fchvig)
                {
                    _d_fchvig = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_activo
        {
            get
            {
                return _n_activo;
            }

            set
            {
                if (value != _n_activo)
                {
                    _n_activo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_desemp
        {
            get
            {
                return _c_desemp;
            }

            set
            {
                if (value != _c_desemp)
                {
                    _c_desemp = value;
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

        public string c_desmon
        {
            get
            {
                return _c_desmon;
            }

            set
            {
                if (value != _c_desmon)
                {
                    _c_desmon = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<InventarioInicialDet> _InventarioInicialDets;
        public ObservableListSource<InventarioInicialDet> InventarioInicialDets
        {
            get
            {
                if (_InventarioInicialDets == null)
                {
                    _InventarioInicialDets = InventarioInicialDet.FetchList(_n_id);
                }
                return _InventarioInicialDets;
            }

            set
            {
                if (value != _InventarioInicialDets)
                {
                    _InventarioInicialDets = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<InventarioInicial> FetchList(int n_idemp)
        {
            List<InventarioInicial> m_listentidad = new List<InventarioInicial>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventarioini_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InventarioInicial m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static InventarioInicial Fetch(int id)
        {
            InventarioInicial m_entidad = new InventarioInicial();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventarioini_traerregistro";
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

        public static InventarioInicial TraerInventario(int id)
        {
            InventarioInicial m_entidad = new InventarioInicial();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventarioini_traerregistro";
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_inventarioini_insertar";
                            AddParameters(command);
                            command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                            int rows = command.ExecuteNonQuery();
                            n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
                        }
                        //
                        SaveChildren(connection, transaction);
                        //
                        GrabarMovimiento(connection, transaction, true);
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
                command.CommandText = "alm_inventarioini_insertar";
                AddParameters(command);
                command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                int rows = command.ExecuteNonQuery();
                n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
            }
            //
            SaveChildren(connection, transaction);
            //
            GrabarMovimiento(connection, transaction, true);
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
                    try
                    {
                        if (IsOld)
                        {
                            using (MySqlCommand command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.CommandText = "alm_inventarioini_actualizar";
                                AddParameters(command);
                                int rows = command.ExecuteNonQuery();
                            }
                        }
                        if (HasOldOrNewChildren)
                        {
                            SaveChildren(connection, transaction);
                        }
                        //
                        GrabarMovimiento(connection, transaction, false);
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

        protected override void Update(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "alm_inventarioini_actualizar";
                AddParameters(command);
                int rows = command.ExecuteNonQuery();
            }
            if (HasOldOrNewChildren)
            {
                SaveChildren(connection, transaction);
            }
            //
            GrabarMovimiento(connection, transaction, false);
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
                            InventarioInicialDet.DeleteAll(n_id, connection, transaction);

                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "alm_inventarioini_eliminar";
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

        protected override bool CheckHasOldOrNewChildren()
        {
            bool isChildOld = false;
            var childsOld = InventarioInicialDets.Where(o => o.IsNew == true || o.IsOld == true);
            if (childsOld != null)
            {
                if (childsOld.Count() > 0)
                    isChildOld = true;
            }
            if (!isChildOld)
            {
                if (InventarioInicialDets.GetRemoveItems().Count > 0)
                {
                    isChildOld = true;
                }
            }

            return isChildOld;
        }

        private void SaveChildren(MySqlConnection connection, MySqlTransaction transaction)
        {
            foreach (var hijo in InventarioInicialDets)
            {
                if (hijo.IsNew)
                    hijo.n_idinvini = n_id;

                hijo.Save(connection, transaction);
            }

            foreach (var hijo in InventarioInicialDets.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
            }
        }


        private void GrabarMovimiento(MySqlConnection connection, MySqlTransaction transaction, bool isNew)
        {
            if (!isNew)
            {
                Movimiento movimientoAnterior = Movimiento.FetchPorInventario(this.n_id);
                if (movimientoAnterior != null)
                    movimientoAnterior.Delete(connection, transaction);
            }

            Movimiento movimientoNuevo = new Movimiento();
            movimientoNuevo.d_fchdoc = this.d_fchvig;
            movimientoNuevo.d_fching = this.d_fchvig;
            movimientoNuevo.n_idemp = this.n_idemp;
            movimientoNuevo.n_idtipmov = 1;
            movimientoNuevo.n_idclipro = 10293;
            movimientoNuevo.n_idtipdoc = 49;
            movimientoNuevo.c_numser = "0001";
            movimientoNuevo.c_numdoc = TipoDocumento.UltimoNumero(this.n_idemp, 49, movimientoNuevo.c_numser);
            movimientoNuevo.n_idalm = this.n_idalm;
            movimientoNuevo.n_anotra = this.d_fchvig.Year;
            movimientoNuevo.n_idmes = this.d_fchvig.Month;
            movimientoNuevo.n_idtipope = 2;
            movimientoNuevo.n_tipite = 2;
            movimientoNuevo.n_docrefidtipdoc = 97;
            movimientoNuevo.n_docrefiddocref = this.n_id;
            movimientoNuevo.c_docrefnumser = this.c_numser;
            movimientoNuevo.c_docrefnumdoc = this._c_numdoc;
            movimientoNuevo.n_perid = 0;
            movimientoNuevo.n_prolog = 1;

            movimientoNuevo.MovimientoDets = new ObservableListSource<MovimientoDet>();

            foreach (var hijo in InventarioInicialDets)
            {
                MovimientoDet movimientoDet = new MovimientoDet();
                movimientoDet.n_idite = hijo.n_idite;
                movimientoDet.n_idpre = hijo.n_idunimed;
                movimientoDet.n_can = hijo.n_can;
                movimientoDet.c_numlot = "SIN LOTE";
                movimientoDet.n_idtippro = 6;

                movimientoNuevo.MovimientoDets.Add(movimientoDet);
            }

            movimientoNuevo.Save();
        }

        #endregion

        #region metodos privados

        private static InventarioInicial SetObject(MySqlDataReader reader)
        {
            return new InventarioInicial
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idalm = reader.GetInt32("n_idalm"),
                n_idmon = reader.GetInt32("n_idmon"),
                c_numser = reader.GetString("c_numser"),
                c_numdoc = reader.GetString("c_numdoc"),
                c_des = reader.GetString("c_des"),
                d_fchvig = reader.GetDateTime("d_fchvig"),
                n_activo = reader.GetInt32("n_activo"),
                c_desemp = reader.GetString("c_desemp"),
                c_desalm = reader.GetString("c_desalm"),
                c_desmon = reader.GetString("c_desmon")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idalm", n_idalm));
            command.Parameters.Add(new MySqlParameter("@n_idmon", n_idmon));
            command.Parameters.Add(new MySqlParameter("@c_numser", c_numser));
            command.Parameters.Add(new MySqlParameter("@c_numdoc", c_numdoc));
            command.Parameters.Add(new MySqlParameter("@c_des", c_des));
            command.Parameters.Add(new MySqlParameter("@d_fchvig", d_fchvig));
            command.Parameters.Add(new MySqlParameter("@n_activo", n_activo));
        }

        #endregion
    }
}
