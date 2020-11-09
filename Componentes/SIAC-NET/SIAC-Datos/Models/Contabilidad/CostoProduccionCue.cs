using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Contabilidad
{

    public class CostoProduccionCue : ObjectBase
    {
        #region constructor
        public CostoProduccionCue()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idcostoprod;
        private int _n_idcue;
        private double _n_impt;
        private string _c_descue;

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

        public int n_idcostoprod
        {
            get
            {
                return _n_idcostoprod;
            }

            set
            {
                if (value != _n_idcostoprod)
                {
                    _n_idcostoprod = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idcue
        {
            get
            {
                return _n_idcue;
            }

            set
            {
                if (value != _n_idcue)
                {
                    _n_idcue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_impt
        {
            get
            {
                return _n_impt;
            }

            set
            {
                if (value != _n_impt)
                {
                    _n_impt = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_descue
        {
            get
            {
                return _c_descue;
            }

            set
            {
                if (value != _c_descue)
                {
                    _c_descue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<CostoProduccionCue> FetchList(int n_idcostoprod)
        {
            ObservableListSource<CostoProduccionCue> m_listentidad = new ObservableListSource<CostoProduccionCue>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprodcue_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idcostoprod", n_idcostoprod));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionCue m_entidad = SetObject(reader);
                            m_entidad.IsNew = false;
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ObservableListSource<CostoProduccionCue> TraerListaPorConfiguracion(int n_idemp
            , int n_anotra
            , int n_mes
            , int n_idconfigval)
        {
            // Se trae la lista de cuenta de la configuracion
            ConfigVal configVal = ConfigVal.Fetch(n_idconfigval);

            //Se trae el listado de cuentas del periodo
            DataTable DtResul = new DataTable();
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_diario_balancecomprobacion_v2";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    command.Parameters.Add(new MySqlParameter("@n_mesini", n_mes));
                    command.Parameters.Add(new MySqlParameter("@n_mesfin", n_mes));
                    command.Parameters.Add(new MySqlParameter("@n_idlib", 0));
                    connection.Open();

                    MySqlDataAdapter adp = new MySqlDataAdapter(command);
                    adp.Fill(DtResul);
                }
            }

            //Se agregan registros
            ObservableListSource<CostoProduccionCue> m_listentidad = new ObservableListSource<CostoProduccionCue>();

            foreach (var configValCue in configVal.ConfigValCues)
            { 
                //Se busca el registro en la tabla
                DataRow row = DtResul.AsEnumerable()
                    .SingleOrDefault(r => r.Field<int>("n_idcue") == configValCue.n_idcue);

                if (row != null)
                {
                    CostoProduccionCue m_entidad = new CostoProduccionCue();
                    m_entidad.n_idcue = Convert.ToInt32(row["n_idcue"]);
                    m_entidad.n_impt = Convert.ToDouble(row["n_movperhab"]);
                    m_entidad.c_descue = configValCue.c_descue;

                    m_listentidad.Add(m_entidad);
                }
            }
            return m_listentidad;
        }

        public static CostoProduccionCue Fetch(int id)
        {
            CostoProduccionCue m_entidad = new CostoProduccionCue();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprodcue_traerregistro";
                    command.Parameters.Add(new MySqlParameter("@n_id", id));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            m_entidad = SetObject(reader);
                            m_entidad.IsNew = false;
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_costoprodcue_insertar";
                            AddParameters(command);
                            command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                            int rows = command.ExecuteNonQuery();
                            n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
                        }
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

        protected override void Insert(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "con_costoprodcue_insertar";
                AddParameters(command);
                command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                int rows = command.ExecuteNonQuery();
                n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_costoprodcue_actualizar";
                            AddParameters(command);
                            int rows = command.ExecuteNonQuery();
                        }
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

        protected override void Update(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "con_costoprodcue_actualizar";
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
                    try
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_costoprodcue_eliminar";
                            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
                            int rows = command.ExecuteNonQuery();
                        }
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

        public override void Delete(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "con_costoprodcue_eliminar";
                command.Parameters.Add(new MySqlParameter("@n_id", n_id));
                int rows = command.ExecuteNonQuery();
            }
        }

        #endregion

        #region metodos privados

        private static CostoProduccionCue SetObject(MySqlDataReader reader)
        {
            return new CostoProduccionCue
            {
                n_id = reader.GetInt32("n_id"),
                n_idcostoprod = reader.GetInt32("n_idcostoprod"),
                n_idcue = reader.GetInt32("n_idcue"),
                n_impt = reader.GetDouble("n_impt"),
                c_descue = reader.GetString("c_descue")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idcostoprod", n_idcostoprod));
            command.Parameters.Add(new MySqlParameter("@n_idcue", n_idcue));
            command.Parameters.Add(new MySqlParameter("@n_impt", n_impt));
        }

        #endregion
    }
}
