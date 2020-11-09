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

    public class Inventario : ObjectBase
    {
        #region constructor
        public Inventario()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private int _n_idtipexi;

        private int _n_idfam;

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

        public int n_idfam
        {
            get
            {
                return _n_idfam;
            }

            set
            {
                if (value != _n_idfam)
                {
                    _n_idfam = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idclas;
        public int n_idclas
        {
            get
            {
                return _n_idclas;
            }

            set
            {
                if (value != _n_idclas)
                {
                    _n_idclas = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idsubclas;
        public int n_idsubclas
        {
            get
            {
                return _n_idsubclas;
            }

            set
            {
                if (value != _n_idsubclas)
                {
                    _n_idsubclas = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_codpro;
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

        private string _c_despro;
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

        private string _c_destec;
        public string c_destec
        {
            get
            {
                return _c_destec;
            }

            set
            {
                if (value != _c_destec)
                {
                    _c_destec = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_descar;
        public string c_descar
        {
            get
            {
                return _c_descar;
            }

            set
            {
                if (value != _c_descar)
                {
                    _c_descar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<InventarioUnimed> _InventarioUnimeds;
        public ObservableListSource<InventarioUnimed> InventarioUnimeds
        {
            get
            {
                if (_InventarioUnimeds == null)
                {
                    _InventarioUnimeds = InventarioUnimed.FetchList(_n_id);
                }
                return _InventarioUnimeds;
            }

            set
            {
                if (value != _InventarioUnimeds)
                {
                    _InventarioUnimeds = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region metodos publicos

        public static List<Inventario> FetchList(int n_idemp, int n_anotra)
        {
            List<Inventario> m_listentidad = new List<Inventario>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventario_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Inventario m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static Inventario Fetch(int id)
        {
            Inventario m_entidad = new Inventario();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventario_traerregistro";
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

        public static Inventario TraerPorDescripcion(string c_despro)
        {
            Inventario m_entidad = new Inventario();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "alm_inventario_traerregistro_por_descripcion";
                    command.Parameters.Add(new MySqlParameter("@c_despro", c_despro));
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
                            command.CommandText = "alm_inventario_insertar";
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
                command.CommandText = "alm_inventario_insertar";
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
                            command.CommandText = "alm_inventario_actualizar";
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
                command.CommandText = "alm_inventario_actualizar";
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
                            command.CommandText = "alm_inventario_eliminar";
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

        private static Inventario SetObject(MySqlDataReader reader)
        {
            return new Inventario
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idtipexi = reader.GetInt32("n_idtipexi"),
                n_idfam = reader.GetInt32("n_idfam"),
                n_idclas = reader.GetInt32("n_idclas"),
                n_idsubclas = reader.GetInt32("n_idsubclas"),
                c_codpro = reader.GetString("c_codpro"),
                c_despro = reader.GetString("c_despro"),
                c_destec = reader.GetString("c_destec"),
                c_descar = reader.GetString("c_descar")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idtipexi", n_idtipexi));
            command.Parameters.Add(new MySqlParameter("@n_idfam", n_idfam));
            command.Parameters.Add(new MySqlParameter("@n_idclas", n_idclas));
            command.Parameters.Add(new MySqlParameter("@n_idsubclas", n_idsubclas));
            command.Parameters.Add(new MySqlParameter("@c_codpro", c_codpro));
            command.Parameters.Add(new MySqlParameter("@c_despro", c_despro));
            command.Parameters.Add(new MySqlParameter("@c_destec", c_destec));
            command.Parameters.Add(new MySqlParameter("@c_descar", c_descar));
        }

        #endregion
    }
}
