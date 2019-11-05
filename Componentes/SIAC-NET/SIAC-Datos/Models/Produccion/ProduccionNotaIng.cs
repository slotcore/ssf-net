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
    public class ProduccionNotaIng: ObjectBase
    {
        public ProduccionNotaIng()
        {
            _IsNew = true;
            _c_idproduccionnotaing = Guid.NewGuid().ToString();
        }
        public ProduccionNotaIng(int idProduccion)
        {
            _IsNew = true;
            _c_idproduccionnotaing = Guid.NewGuid().ToString();
            _n_idproduccion = idProduccion;
        }

        private string _c_idproduccionnotaing;

        public string c_idproduccionnotaing
        {
            get
            {
                return _c_idproduccionnotaing;
            }

            set
            {
                if (value != _c_idproduccionnotaing)
                {
                    _c_idproduccionnotaing = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idproduccion;

        public int n_idproduccion
        {
            get
            {
                return _n_idproduccion;
            }

            set
            {
                if (value != _n_idproduccion)
                {
                    _n_idproduccion = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idnoting;

        public int n_idnoting
        {
            get
            {
                return _n_idnoting;
            }

            set
            {
                if (value != _n_idnoting)
                {
                    _n_idnoting = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _materiaprima;

        public string materiaprima
        {
            get
            {
                return _materiaprima;
            }

            set
            {
                if (value != _materiaprima)
                {
                    _materiaprima = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _cantidadmateriaprima;

        public double cantidadmateriaprima
        {
            get
            {
                return _cantidadmateriaprima;
            }

            set
            {
                if (value != _cantidadmateriaprima)
                {
                    _cantidadmateriaprima = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _numnoting;

        public string numnoting
        {
            get
            {
                return _numnoting;
            }

            set
            {
                if (value != _numnoting)
                {
                    _numnoting = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void CopyOf(ProduccionNotaIng produccionNotaIng)
        {
            _IsNew = produccionNotaIng.IsNew;
            _IsOld = produccionNotaIng.IsOld;
            _c_idproduccionnotaing = produccionNotaIng.c_idproduccionnotaing;
            _n_idproduccion = produccionNotaIng.n_idproduccion;
            _n_idnoting = produccionNotaIng.n_idnoting;
        }

        public static ObservableListSource<ProduccionNotaIng> Fetch(int idproduccion)
        {
            ObservableListSource<ProduccionNotaIng> _ProduccionNotaIngs = new ObservableListSource<ProduccionNotaIng>();

            using (MySqlConnection connection = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = SIAC_DATOS.Properties.Resources.q_pro_produccionnotaing_fetch;
                    command.Parameters.Add(new MySqlParameter("@n_idproduccion", idproduccion));
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProduccionNotaIng periodoLaboral = new ProduccionNotaIng
                            {
                                c_idproduccionnotaing = reader.GetString("c_idproduccionnotaing"),
                                n_idproduccion = reader.GetInt32("n_idproduccion"),
                                n_idnoting = reader.GetInt32("n_idnoting"),
                                numnoting = reader.GetString("numnoting"),
                                cantidadmateriaprima = reader.GetDouble("cantidadmateriaprima"),
                                materiaprima = reader.GetString("materiaprima")
                            };
                            periodoLaboral.IsNew = false;
                            _ProduccionNotaIngs.Add(periodoLaboral);
                        }
                    }
                }
            }

            return _ProduccionNotaIngs;
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
                            command.CommandText = SIAC_DATOS.Properties.Resources.q_pro_produccionnotaing_insert;
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
                command.CommandText = SIAC_DATOS.Properties.Resources.q_pro_produccionnotaing_insert;
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
                            command.CommandText = SIAC_DATOS.Properties.Resources.q_pro_produccionnotaing_update;
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
                command.CommandText = SIAC_DATOS.Properties.Resources.q_pro_produccionnotaing_update;
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
                            command.CommandText = SIAC_DATOS.Properties.Resources.q_pro_produccionnotaing_delete;
                            command.Parameters.Add(new MySqlParameter("@c_idproduccionnotaing", c_idproduccionnotaing));
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
            command.Parameters.Add(new MySqlParameter("@c_idproduccionnotaing", c_idproduccionnotaing));
            command.Parameters.Add(new MySqlParameter("@n_idproduccion", n_idproduccion));
            command.Parameters.Add(new MySqlParameter("@n_idnoting", n_idnoting));
        }
    }
}
