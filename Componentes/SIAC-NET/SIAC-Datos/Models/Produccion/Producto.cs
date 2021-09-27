using Helper;
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

    public class Producto : ObjectBase
    {
        #region constructor
        public Producto()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private string _c_cod;

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

        public string c_cod
        {
            get
            {
                return _c_cod;
            }

            set
            {
                if (value != _c_cod)
                {
                    _c_cod = value;
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

        private int _n_idunimed;
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

        private int _n_idfam;
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

        private int _n_idcla;
        public int n_idcla
        {
            get
            {
                return _n_idcla;
            }

            set
            {
                if (value != _n_idcla)
                {
                    _n_idcla = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idsubcla;
        public int n_idsubcla
        {
            get
            {
                return _n_idsubcla;
            }

            set
            {
                if (value != _n_idsubcla)
                {
                    _n_idsubcla = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idtip;
        public int n_idtip
        {
            get
            {
                return _n_idtip;
            }

            set
            {
                if (value != _n_idtip)
                {
                    _n_idtip = value;
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

        private int _n_act;
        public int n_act
        {
            get
            {
                return _n_act;
            }

            set
            {
                if (value != _n_act)
                {
                    _n_act = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_kilhor;
        public double n_kilhor
        {
            get
            {
                return _n_kilhor;
            }

            set
            {
                if (value != _n_kilhor)
                {
                    _n_kilhor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idmatpri;
        public int n_idmatpri
        {
            get
            {
                return _n_idmatpri;
            }

            set
            {
                if (value != _n_idmatpri)
                {
                    _n_idmatpri = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<ProductoReceta> _ProductoRecetas;
        public ObservableListSource<ProductoReceta> ProductoRecetas
        {
            get
            {
                if (_ProductoRecetas == null)
                {
                    _ProductoRecetas = ProductoReceta.FetchList(_n_id);
                }
                return _ProductoRecetas;
            }

            set
            {
                if (value != _ProductoRecetas)
                {
                    _ProductoRecetas = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<Producto> FetchList(int n_idemp, int n_anotra)
        {
            List<Producto> m_listentidad = new List<Producto>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productos_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static Producto Fetch(int id)
        {
            Producto m_entidad = null;

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pro_productos_traerregistro";
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
                            command.CommandText = "pro_productos_insertar";
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
                command.CommandText = "pro_productos_insertar";
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
                            command.CommandText = "pro_productos_actualizar";
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
                command.CommandText = "pro_productos_actualizar";
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
                            command.CommandText = "pro_productos_eliminar";
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

        private static Producto SetObject(MySqlDataReader reader)
        {
            return new Producto
            {
                n_id = Genericas.GetInt(reader, "n_id"),
                n_idemp = Genericas.GetInt(reader, "n_idemp"),
                c_cod = Genericas.GetString(reader, "c_cod"),
                c_despro = Genericas.GetString(reader, "c_despro"),
                n_idunimed = Genericas.GetInt(reader, "n_idunimed"),
                n_idfam = Genericas.GetInt(reader, "n_idfam"),
                n_idcla = Genericas.GetInt(reader, "n_idcla"),
                n_idsubcla = Genericas.GetInt(reader, "n_idsubcla"),
                n_idtip = Genericas.GetInt(reader, "n_idtip"),
                c_obs = Genericas.GetString(reader, "c_obs"),
                n_act = Genericas.GetInt(reader, "n_act"),
                n_kilhor = reader.GetDouble("n_kilhor"),
                n_idmatpri = Genericas.GetInt(reader, "n_idmatpri")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@c_cod", c_cod));
            command.Parameters.Add(new MySqlParameter("@c_despro", c_despro));
            command.Parameters.Add(new MySqlParameter("@n_idunimed", n_idunimed));
            command.Parameters.Add(new MySqlParameter("@n_idfam", n_idfam));
            command.Parameters.Add(new MySqlParameter("@n_idcla", n_idcla));
            command.Parameters.Add(new MySqlParameter("@n_idsubcla", n_idsubcla));
            command.Parameters.Add(new MySqlParameter("@n_idtip", n_idtip));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_act", n_act));
            command.Parameters.Add(new MySqlParameter("@n_kilhor", n_kilhor));
            command.Parameters.Add(new MySqlParameter("@n_idmatpri", n_idmatpri));
        }

        #endregion
    }
}
