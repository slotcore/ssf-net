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

    public class Programacion : ObjectBase
    {
        #region constructor
        public Programacion()
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
        private int _n_idpro;
        private int _n_anotra;
        private int _n_mestra;
        private DateTime _d_fchini;
        private DateTime _d_fchfin;
        private DateTime _d_fchemi;
        private string _c_obs;
        private int _n_numhordia;
        private int _n_numdiapro;
        private string _c_numdocdet;
        private string _c_despro;

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

        public DateTime d_fchini
        {
            get
            {
                return _d_fchini;
            }

            set
            {
                if (value != _d_fchini)
                {
                    _d_fchini = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime d_fchfin
        {
            get
            {
                return _d_fchfin;
            }

            set
            {
                if (value != _d_fchfin)
                {
                    _d_fchfin = value;
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

        public int n_numhordia
        {
            get
            {
                return _n_numhordia;
            }

            set
            {
                if (value != _n_numhordia)
                {
                    _n_numhordia = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_numdiapro
        {
            get
            {
                return _n_numdiapro;
            }

            set
            {
                if (value != _n_numdiapro)
                {
                    _n_numdiapro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_numdocdet
        {
            get
            {
                return _c_numdocdet;
            }

            set
            {
                if (value != _c_numdocdet)
                {
                    _c_numdocdet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_despro
        {
            get
            {
                return _c_despro;
            }

            set
            {
                if (value != _c_despro)
                {
                    _c_despro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<ProgramacionDetPro> _ProgramacionDetPros;
        public ObservableListSource<ProgramacionDetPro> ProgramacionDetPros
        {
            get
            {
                if (_ProgramacionDetPros == null)
                {
                    _ProgramacionDetPros = ProgramacionDetPro.FetchList(_n_id);
                }
                return _ProgramacionDetPros;
            }

            set
            {
                if (value != _ProgramacionDetPros)
                {
                    _ProgramacionDetPros = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<Programacion> TraerPorRangoFecha(int n_idemp, DateTime d_fchini, DateTime d_fchfin)
        {
            List<Programacion> m_listentidad = new List<Programacion>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_programa_TraerPorRangoFecha";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@d_fchini", d_fchini));
                    command.Parameters.Add(new MySqlParameter("@d_fchfin", d_fchfin));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Programacion m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static List<Programacion> FetchList(int n_idemp, int n_anotra)
        {
            List<Programacion> m_listentidad = new List<Programacion>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_programa_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Programacion m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static Programacion Fetch(int id)
        {
            Programacion m_entidad = new Programacion();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_programa_traerregistro";
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
                            command.CommandText = "pro_programa_insertar";
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
                command.CommandText = "pro_programa_insertar";
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
                            command.CommandText = "pro_programa_actualizar";
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
                command.CommandText = "pro_programa_actualizar";
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
                            command.CommandText = "pro_programa_eliminar";
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

        private static Programacion SetObject(MySqlDataReader reader)
        {
            var prog = new Programacion
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idtipdoc = reader.GetInt32("n_idtipdoc"),
                c_numser = reader.GetString("c_numser"),
                c_numdoc = reader.GetString("c_numdoc"),
                c_numdocdet = reader.GetString("c_numdocdet"),
                n_idpro = reader.GetInt32("n_idpro"),
                n_anotra = reader.GetInt32("n_anotra"),
                n_mestra = reader.GetInt32("n_mestra"),
                d_fchini = reader.GetDateTime("d_fchini"),
                d_fchfin = reader.GetDateTime("d_fchfin"),
                d_fchemi = reader.GetDateTime("d_fchemi"),
                n_numhordia = reader.GetInt32("n_numhordia"),
                c_despro = reader.GetString("c_despro")
            };
            if (reader["c_obs"] != DBNull.Value)
                prog.c_obs = reader.GetString("c_obs");

            return prog;
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idtipdoc", n_idtipdoc));
            command.Parameters.Add(new MySqlParameter("@c_numser", c_numser));
        }

        #endregion
    }
}
