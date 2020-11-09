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

    public class Almacen : ObjectBase
    {
        #region constructor
        public Almacen()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private int _n_idlocal;

        private string _c_des;

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

        public int n_idlocal
        {
            get
            {
                return _n_idlocal;
            }

            set
            {
                if (value != _n_idlocal)
                {
                    _n_idlocal = value;
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

        private int _n_estado;
        public int n_estado
        {
            get
            {
                return _n_estado;
            }

            set
            {
                if (value != _n_estado)
                {
                    _n_estado = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idtipexi;
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

        private string _c_deslocal;
        public string c_deslocal
        {
            get
            {
                return _c_deslocal;
            }

            set
            {
                if (value != _c_deslocal)
                {
                    _c_deslocal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desemp;
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
        #endregion

        #region metodos publicos

        public static List<Almacen> FetchList()
        {
            List<Almacen> m_listentidad = new List<Almacen>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_almacenes_listar";
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Almacen m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static Almacen Fetch(int id)
        {
            Almacen m_entidad = new Almacen();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_almacenes_traerregistro";
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
                            command.CommandText = "alm_almacenes_insertar";
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
                command.CommandText = "alm_almacenes_insertar";
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
                            command.CommandText = "alm_almacenes_actualizar";
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
                command.CommandText = "alm_almacenes_actualizar";
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
                            command.CommandText = "alm_almacenes_eliminar";
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

        private static Almacen SetObject(MySqlDataReader reader)
        {
            return new Almacen
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idlocal = reader.GetInt32("n_idlocal"),
                c_des = reader.GetString("c_des"),
                n_estado = reader.GetInt32("n_estado"),
                n_idtipexi = reader.GetInt32("n_idtipexi"),
                c_deslocal = reader.GetString("c_deslocal"),
                c_desemp = reader.GetString("c_desemp")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idlocal", n_idlocal));
            command.Parameters.Add(new MySqlParameter("@c_des", c_des));
            command.Parameters.Add(new MySqlParameter("@n_estado", n_estado));
            command.Parameters.Add(new MySqlParameter("@n_idtipexi", n_idtipexi));
        }

        #endregion
    }
}
