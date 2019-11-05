using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Datos.Models.Planillas
{
    public class PeriodoLaboral: ObjectBase
    {
        public PeriodoLaboral()
        {
            _IsNew = true;
            _n_idperiodolaboral = Guid.NewGuid().ToString();
            _d_fchini = DateTime.Today;
            _d_fchfin = DateTime.Today;
        }

        public PeriodoLaboral(int parentId)
        {
            _IsNew = true;
            _n_idperiodolaboral = Guid.NewGuid().ToString();
            _n_idempleado = parentId;
            _d_fchini = DateTime.Today;
            _d_fchfin = DateTime.Today;
        }

        private string _n_idperiodolaboral;

        private int _n_idempleado;

        private int _n_idcategoria;

        private int _n_idfinperiodo;

        private int _n_corr;

        private DateTime _d_fchini;

        private DateTime _d_fchfin;

        private string _categoria;

        private string _finperiodo;

        public string n_idperiodolaboral
        {
            get
            {
                return _n_idperiodolaboral;
            }

            set
            {
                if (value != _n_idperiodolaboral)
                {
                    _n_idperiodolaboral = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string finperiodo
        {
            get
            {
                return _finperiodo;
            }

            set
            {
                if (value != _finperiodo)
                {
                    _finperiodo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string categoria
        {
            get
            {
                return _categoria;
            }

            set
            {
                if (value != _categoria)
                {
                    _categoria = value;
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

        public int n_corr
        {
            get
            {
                return _n_corr;
            }

            set
            {
                if (value != _n_corr)
                {
                    _n_corr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idfinperiodo
        {
            get
            {
                return _n_idfinperiodo;
            }

            set
            {
                if (value != _n_idfinperiodo)
                {
                    _n_idfinperiodo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idcategoria
        {
            get
            {
                return _n_idcategoria;
            }

            set
            {
                if (value != _n_idcategoria)
                {
                    _n_idcategoria = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idempleado
        {
            get
            {
                return _n_idempleado;
            }

            set
            {
                if (value != _n_idempleado)
                {
                    _n_idempleado = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void CopyOf(PeriodoLaboral periodoLaboral)
        {
            _IsNew = periodoLaboral.IsNew;
            _IsOld = periodoLaboral.IsOld;
            _n_idperiodolaboral = periodoLaboral.n_idperiodolaboral;
            _n_idempleado = periodoLaboral.n_idempleado;
            _n_idcategoria = periodoLaboral.n_idcategoria;
            _n_idfinperiodo = periodoLaboral.n_idfinperiodo;
            _n_corr = periodoLaboral.n_corr;
            _d_fchini = periodoLaboral.d_fchini;
            _d_fchfin = periodoLaboral.d_fchfin;
            _categoria = periodoLaboral.categoria;
            _finperiodo = periodoLaboral.finperiodo;
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
                            command.CommandText = SIAC_DATOS.Properties.Resources.q_pla_periodolaboral_insert;
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
                command.CommandText = SIAC_DATOS.Properties.Resources.q_pla_periodolaboral_insert;
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
                            command.CommandText = SIAC_DATOS.Properties.Resources.q_pla_periodolaboral_update;
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
                command.CommandText = SIAC_DATOS.Properties.Resources.q_pla_periodolaboral_update;
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
                            command.CommandText = SIAC_DATOS.Properties.Resources.q_pla_periodolaboral_delete;
                            command.Parameters.Add(new MySqlParameter("@n_idperiodolaboral", n_idperiodolaboral));
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

        public void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_idperiodolaboral", n_idperiodolaboral));
            command.Parameters.Add(new MySqlParameter("@n_idempleado", n_idempleado));
            command.Parameters.Add(new MySqlParameter("@n_idcategoria", n_idcategoria));
            command.Parameters.Add(new MySqlParameter("@n_idfinperiodo", n_idfinperiodo));
            command.Parameters.Add(new MySqlParameter("@n_corr", n_corr));
            command.Parameters.Add(new MySqlParameter("@d_fchini", d_fchini));
            command.Parameters.Add(new MySqlParameter("@d_fchfin", d_fchfin));
        }
    }
}
