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

    public class ProgramacionDetPro : ObjectBase
    {
        #region constructor
        public ProgramacionDetPro()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_idpro;
        private int _n_idordpro;
        private int _n_idite;
        private int _n_idrec;
        private int _n_idlin;
        private int _n_idunimed;
        private double _n_can;
        private DateTime _d_fchent;
        private DateTime _d_fchpro;
        private DateTime _h_horini;
        private DateTime _h_horfin;
        private int _n_idres;
        private int _n_solmat;

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

        public int n_idordpro
        {
            get
            {
                return _n_idordpro;
            }

            set
            {
                if (value != _n_idordpro)
                {
                    _n_idordpro = value;
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

        public DateTime d_fchpro
        {
            get
            {
                return _d_fchpro;
            }

            set
            {
                if (value != _d_fchpro)
                {
                    _d_fchpro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime h_horini
        {
            get
            {
                return _h_horini;
            }

            set
            {
                if (value != _h_horini)
                {
                    _h_horini = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime h_horfin
        {
            get
            {
                return _h_horfin;
            }

            set
            {
                if (value != _h_horfin)
                {
                    _h_horfin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idres
        {
            get
            {
                return _n_idres;
            }

            set
            {
                if (value != _n_idres)
                {
                    _n_idres = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_solmat
        {
            get
            {
                return _n_solmat;
            }

            set
            {
                if (value != _n_solmat)
                {
                    _n_solmat = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desordpro;
        public string c_desordpro
        {
            get
            {
                return _c_desordpro;
            }

            set
            {
                if (value != _c_desordpro)
                {
                    _c_desordpro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desite;
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

        private string _c_desrec;
        public string c_desrec
        {
            get
            {
                return _c_desrec;
            }

            set
            {
                if (value != _c_desrec)
                {
                    _c_desrec = value;
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

        private string _c_desunimed;
        public string c_desunimed
        {
            get
            {
                return _c_desunimed;
            }

            set
            {
                if (value != _c_desunimed)
                {
                    _c_desunimed = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desres;
        public string c_desres
        {
            get
            {
                return _c_desres;
            }

            set
            {
                if (value != _c_desres)
                {
                    _c_desres = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<ProgramacionDetPro> FetchList(int n_idpro)
        {
            ObservableListSource<ProgramacionDetPro> m_listentidad = new ObservableListSource<ProgramacionDetPro>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_programadetpro_listar_2";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProgramacionDetPro m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ProgramacionDetPro Fetch(int id)
        {
            ProgramacionDetPro m_entidad = new ProgramacionDetPro();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_programadetpro_traerregistro";
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
                            command.CommandText = "pro_programadetpro_insertar";
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
                command.CommandText = "pro_programadetpro_insertar";
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
                            command.CommandText = "pro_programadetpro_actualizar";
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
                command.CommandText = "pro_programadetpro_actualizar";
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
                            command.CommandText = "pro_programadetpro_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_idpro));
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

        private static ProgramacionDetPro SetObject(MySqlDataReader reader)
        {
            var prog = new ProgramacionDetPro
            {
                n_idpro = reader.GetInt32("n_idpro"),
                n_idordpro = reader.GetInt32("n_idordpro"),
                n_idite = reader.GetInt32("n_idite"),
                n_idrec = reader.GetInt32("n_idrec"),
                n_idunimed = reader.GetInt32("n_idunimed"),
                n_can = reader.GetDouble("n_can"),
                d_fchent = reader.GetDateTime("d_fchent"),
                d_fchpro = reader.GetDateTime("d_fchpro"),
                n_idres = reader.GetInt32("n_idres"),
                n_idlin = reader.GetInt32("n_idlin"),
                c_desite = reader.GetString("c_desite"),
                c_desres = reader.GetString("c_desres")
            };
            if (reader["c_desordpro"] != DBNull.Value)
                prog.c_desordpro = reader.GetString("c_desordpro");

            if (reader["c_deslin"] != DBNull.Value)
                prog.c_deslin = reader.GetString("c_deslin");

            if (reader["c_desrec"] != DBNull.Value)
                prog.c_desrec = reader.GetString("c_desrec");

            if (reader["h_horini"] != DBNull.Value)
                prog.h_horini = Convert.ToDateTime(reader.GetString("h_horini"));
            else
                prog.h_horini = Convert.ToDateTime(new DateTime(prog.d_fchpro.Year, prog.d_fchpro.Month, prog.d_fchpro.Day, 8, 0, 0));

            if (reader["h_horfin"] != DBNull.Value)
                prog.h_horfin = Convert.ToDateTime(reader.GetString("h_horfin"));
            else
                prog.h_horfin = Convert.ToDateTime(new DateTime(prog.d_fchpro.Year, prog.d_fchpro.Month, prog.d_fchpro.Day, 9, 0, 0));

            return prog;
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
            command.Parameters.Add(new MySqlParameter("@n_idordpro", n_idordpro));
            command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
            command.Parameters.Add(new MySqlParameter("@n_idrec", n_idrec));
        }

        #endregion
    }
}
