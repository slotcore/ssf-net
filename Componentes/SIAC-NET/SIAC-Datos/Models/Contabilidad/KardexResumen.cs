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

    public class KardexResumen : ObjectBase
    {
        #region constructor
        public KardexResumen()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idemp;
        private int _n_anotra;
        private int _n_idtipexi;
        private string _c_tipexides;
        private string _c_codpro;
        private string _c_abrepre;
        private string _c_despro;
        private double _n_totmov;
        private double _n_stkini;
        private double _n_toting;
        private double _n_totsal;
        private double _n_saldo;
        private double _n_costoini;
        private double _n_costopromini;

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

        public int n_idtipexi
        {
            get
            {
                return _n_idtipexi;
            }

            set
            {
                if (value != _n_idtipexi)
                {
                    _n_idtipexi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_tipexides
        {
            get
            {
                return _c_tipexides;
            }

            set
            {
                if (value != _c_tipexides)
                {
                    _c_tipexides = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_codpro
        {
            get
            {
                return _c_codpro;
            }

            set
            {
                if (value != _c_codpro)
                {
                    _c_codpro = value;
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

        public string c_abrepre
        {
            get
            {
                return _c_abrepre;
            }

            set
            {
                if (value != _c_abrepre)
                {
                    _c_abrepre = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_totmov
        {
            get
            {
                return _n_totmov;
            }

            set
            {
                if (value != _n_totmov)
                {
                    _n_totmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_stkini
        {
            get
            {
                return _n_stkini;
            }

            set
            {
                if (value != _n_stkini)
                {
                    _n_stkini = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_toting
        {
            get
            {
                return _n_toting;
            }

            set
            {
                if (value != _n_toting)
                {
                    _n_toting = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_totsal
        {
            get
            {
                return _n_totsal;
            }

            set
            {
                if (value != _n_totsal)
                {
                    _n_totsal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_saldo
        {
            get
            {
                return _n_saldo;
            }

            set
            {
                if (value != _n_saldo)
                {
                    _n_saldo = value;
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

        public double n_costopromini
        {
            get
            {
                return _n_costopromini;
            }

            set
            {
                if (value != _n_costopromini)
                {
                    _n_costopromini = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<KardexResumen> FetchList(int n_idemp, int n_anotra)
        {
            List<KardexResumen> m_listentidad = new List<KardexResumen>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_kardexresumen_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KardexResumen m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static KardexResumen Fetch(int id)
        {
            KardexResumen m_entidad = new KardexResumen();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_kardexresumen_traerregistro";
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

        public static KardexResumen TraerKardexResumenPorItemMovimiento(int n_idemp
            , int n_idalm
            , int n_idite
            , DateTime d_fchini
            , DateTime d_fchfin)
        {
            KardexResumen m_entidad = null;

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_kardex_resumen_poritem_mov";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_idalm", n_idalm));
                    command.Parameters.Add(new MySqlParameter("@n_idite", n_idite));
                    command.Parameters.Add(new MySqlParameter("@c_fchini", d_fchini.ToString("dd/MM/yyyy")));
                    command.Parameters.Add(new MySqlParameter("@c_fchfin", d_fchfin.ToString("dd/MM/yyyy")));
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

        public static List<KardexResumen> TraerListaPorItem(int n_idemp
            , int n_idalm
            , string c_iditem
            , DateTime d_fchini
            , DateTime d_fchfin)
        {
            List<KardexResumen> l_entidad = new List<KardexResumen>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_kardex_resumen_listar_poritem_mov";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_idalm", n_idalm));
                    command.Parameters.Add(new MySqlParameter("@c_iditem", c_iditem));
                    command.Parameters.Add(new MySqlParameter("@c_fchini", d_fchini.ToString("dd/MM/yyyy")));
                    command.Parameters.Add(new MySqlParameter("@c_fchfin", d_fchfin.ToString("dd/MM/yyyy")));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l_entidad.Add(SetObject(reader));
                        }
                    }
                }
            }
            return l_entidad;
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
                            command.CommandText = "alm_kardexresumen_insertar";
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
                command.CommandText = "alm_kardexresumen_insertar";
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
                            command.CommandText = "alm_kardexresumen_actualizar";
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
                command.CommandText = "alm_kardexresumen_actualizar";
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
                            command.CommandText = "alm_kardexresumen_eliminar";
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

        private static KardexResumen SetObject(MySqlDataReader reader)
        {
            return new KardexResumen
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_anotra = reader.GetInt32("n_anotra"),
                n_idtipexi = reader.GetInt32("n_idtipexi"),
                c_tipexides = reader.GetString("c_tipexides"),
                c_codpro = reader.GetString("c_codpro"),
                c_despro = reader.GetString("c_despro"),
                c_abrepre = reader.GetString("c_abrepre"),
                n_totmov = reader.GetDouble("n_totmov"),
                n_stkini = reader.GetDouble("n_stkini"),
                n_toting = reader.GetDouble("n_toting"),
                n_totsal = reader.GetDouble("n_totsal"),
                n_saldo = reader.GetDouble("n_saldo"),
                n_costoini = reader.GetDouble("n_costoini"),
                n_costopromini = reader.GetDouble("n_costopromini")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
            command.Parameters.Add(new MySqlParameter("@n_idtipexi", n_idtipexi));
        }

        #endregion
    }
}
