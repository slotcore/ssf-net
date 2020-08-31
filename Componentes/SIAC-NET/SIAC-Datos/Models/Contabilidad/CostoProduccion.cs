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
    public class CostoProduccion : ObjectBase
    {
        #region constructor

        public CostoProduccion()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private int _n_anotra;

        private int _n_idmes;

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

        private int _n_idconfigval;
        public int n_idconfigval
        {
            get
            {
                return _n_idconfigval;
            }

            set
            {
                if (value != _n_idconfigval)
                {
                    _n_idconfigval = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idresp;
        public int n_idresp
        {
            get
            {
                return _n_idresp;
            }

            set
            {
                if (value != _n_idresp)
                {
                    _n_idresp = value;
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

        private string _c_des;
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

        private double _n_costomod;
        public double n_costomod
        {
            get
            {
                return _n_costomod;
            }

            set
            {
                if (value != _n_costomod)
                {
                    _n_costomod = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_costocif;
        public double n_costocif
        {
            get
            {
                return _n_costocif;
            }

            set
            {
                if (value != _n_costocif)
                {
                    _n_costocif = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desmes;
        public string c_desmes
        {
            get
            {
                return _c_desmes;
            }

            set
            {
                if (value != _c_desmes)
                {
                    _c_desmes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desconfigval;
        public string c_desconfigval
        {
            get
            {
                return _c_desconfigval;
            }

            set
            {
                if (value != _c_desconfigval)
                {
                    _c_desconfigval = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_numdocvis;
        public string c_numdocvis
        {
            get
            {
                return _c_numdocvis;
            }

            set
            {
                if (value != _c_numdocvis)
                {
                    _c_numdocvis = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desresp;
        public string c_desresp
        {
            get
            {
                return _c_desresp;
            }

            set
            {
                if (value != _c_desresp)
                {
                    _c_desresp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private List<CostoProduccionDet> _CostoProduccionDets;
        public List<CostoProduccionDet> CostoProduccionDets
        {
            get
            {
                if (_CostoProduccionDets == null)
                {
                    _CostoProduccionDets = CostoProduccionDet.FetchList(_n_id);
                }
                return _CostoProduccionDets;
            }

            set
            {
                if (value != _CostoProduccionDets)
                {
                    _CostoProduccionDets = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region metodos publicos

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
                            command.CommandText = "con_costroprod_insertar";
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
                command.CommandText = "con_costroprod_insertar";
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
                            command.CommandText = "con_costoprod_actualizar";
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
                command.CommandText = "con_costoprod_actualizar";
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
                            command.CommandText = "con_costoprod_delete";
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

        #region Metodos Estaticos

        public static List<CostoProduccion> FetchList(int n_idemp, int n_anotra)
        {
            List<CostoProduccion> m_listentidad = new List<CostoProduccion>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprod_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccion m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static CostoProduccion Fetch(int id)
        {
            CostoProduccion m_entidad = new CostoProduccion();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprod_traerregistro";
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

        public static List<CostoProduccionDet> ListarPartesdeProduccion(int idEmp, int anho, int mes)
        {
            List<CostoProduccionDet> costoProduccionDets = new List<CostoProduccionDet>();

            return costoProduccionDets;
        }

        public static bool DocumentoExiste(int idemp, string numser, string numdoc)
        {
            return false;
        }

        #endregion

        #region metodos privados

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
            command.Parameters.Add(new MySqlParameter("@n_idmes", n_idmes));
            command.Parameters.Add(new MySqlParameter("@n_idconfigval", n_idconfigval));
            command.Parameters.Add(new MySqlParameter("@n_idresp", n_idresp));
            command.Parameters.Add(new MySqlParameter("@c_numser", c_numser));
            command.Parameters.Add(new MySqlParameter("@c_numdoc", c_numdoc));
            command.Parameters.Add(new MySqlParameter("@c_des", c_des));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_costomod", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_costocif", c_obs));
        }

        private static CostoProduccion SetObject(MySqlDataReader reader)
        {
            return new CostoProduccion
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_anotra = reader.GetInt32("n_anotra"),
                n_idmes = reader.GetInt32("n_idmes"),
                n_idconfigval = reader.GetInt32("n_idconfigval"),
                n_idresp = reader.GetInt32("n_idresp"),
                c_numser = reader.GetString("c_numser"),
                c_numdoc = reader.GetString("c_numdoc"),
                c_des = reader.GetString("c_des"),
                c_obs = reader.GetString("c_obs"),
                n_costomod = reader.GetDouble("n_costomod"),
                n_costocif = reader.GetDouble("n_costocif"),
                c_desmes = reader.GetString("c_desmes"),
                c_numdocvis = reader.GetString("c_numdocvis"),
                c_desconfigval = reader.GetString("c_desconfigval"),
                c_desresp = reader.GetString("c_desresp")
            };
        }

        #endregion
    }
}
