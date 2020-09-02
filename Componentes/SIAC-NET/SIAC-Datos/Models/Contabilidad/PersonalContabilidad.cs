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

    public class PersonalContabilidad : ObjectBase
    {
        #region constructor
        public PersonalContabilidad()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private int _n_idtra;

        private int _n_idcargo;

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

        public int n_idtra
        {
            get
            {
                return _n_idtra;
            }

            set
            {
                if (value != _n_idtra)
                {
                    _n_idtra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idcargo
        {
            get
            {
                return _n_idcargo;
            }

            set
            {
                if (value != _n_idcargo)
                {
                    _n_idcargo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_destra;
        public string c_destra
        {
            get
            {
                return _c_destra;
            }

            set
            {
                if (value != _c_destra)
                {
                    _c_destra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_descargo;
        public string c_descargo
        {
            get
            {
                return _c_descargo;
            }

            set
            {
                if (value != _c_descargo)
                {
                    _c_descargo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region metodos publicos

        public static List<PersonalContabilidad> FetchList(int n_idemp, int n_idcargo)
        {
            List<PersonalContabilidad> m_listentidad = new List<PersonalContabilidad>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_personal_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_idcargo", n_idcargo));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PersonalContabilidad m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static PersonalContabilidad Fetch(int id)
        {
            PersonalContabilidad m_entidad = new PersonalContabilidad();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_personal_traerregistro";
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
                            command.CommandText = "con_personal_insertar";
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
                command.CommandText = "con_personal_insertar";
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
                            command.CommandText = "con_personal_actualizar";
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
                command.CommandText = "con_personal_actualizar";
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
                            command.CommandText = "con_personal_eliminar";
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

        private static PersonalContabilidad SetObject(MySqlDataReader reader)
        {
            return new PersonalContabilidad
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idtra = reader.GetInt32("n_idtra"),
                n_idcargo = reader.GetInt32("n_idcargo"),
                c_destra = reader.GetString("c_destra"),
                c_descargo = reader.GetString("c_descargo")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_idtra", n_idtra));
            command.Parameters.Add(new MySqlParameter("@n_idcargo", n_idcargo));
        }

        #endregion
    }
}
