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

    public class ItemMovimientoDetalle : ObjectBase
    {
        #region constructor
        public ItemMovimientoDetalle()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idcostoprod;
        private int _n_idmov;
        private double _n_can;
        private DateTime _d_fechmov;
        private double _n_costounit;
        private double _n_costounitprom;
        private double _n_costoprom;
        private double _n_costomp;
        private double _n_costomod;
        private double _n_costocif;
        private string _desmov;
        private string _c_destipmov;
        private string _c_desunimed;
        private int _n_idtipdocref;
        private string _c_destipdocref;
        private int _n_iddocref;
        private string _c_desdocref;

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

        public int n_idmov
        {
            get
            {
                return _n_idmov;
            }

            set
            {
                if (value != _n_idmov)
                {
                    _n_idmov = value;
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

        public DateTime d_fechmov
        {
            get
            {
                return _d_fechmov;
            }

            set
            {
                if (value != _d_fechmov)
                {
                    _d_fechmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costounit
        {
            get
            {
                return _n_costounit;
            }

            set
            {
                if (value != _n_costounit)
                {
                    _n_costounit = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costounitprom
        {
            get
            {
                return _n_costounitprom;
            }

            set
            {
                if (value != _n_costounitprom)
                {
                    _n_costounitprom = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costoprom
        {
            get
            {
                return _n_costoprom;
            }

            set
            {
                if (value != _n_costoprom)
                {
                    _n_costoprom = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costomp
        {
            get
            {
                return _n_costomp;
            }

            set
            {
                if (value != _n_costomp)
                {
                    _n_costomp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costomod
        {
            get
            {
                return _n_costomod;
            }

            set
            {
                if (value != _n_costomod)
                {
                    _n_costomod = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costocif
        {
            get
            {
                return _n_costocif;
            }

            set
            {
                if (value != _n_costocif)
                {
                    _n_costocif = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_costo
        {
            get
            {
                return n_costomp + n_costomod + n_costocif;
            }
        }

        public string desmov
        {
            get
            {
                return _desmov;
            }

            set
            {
                if (value != _desmov)
                {
                    _desmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_destipmov
        {
            get
            {
                return _c_destipmov;
            }

            set
            {
                if (value != _c_destipmov)
                {
                    _c_destipmov = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        public int n_idtipdocref
        {
            get
            {
                return _n_idtipdocref;
            }

            set
            {
                if (value != _n_idtipdocref)
                {
                    _n_idtipdocref = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_destipdocref
        {
            get
            {
                return _c_destipdocref;
            }

            set
            {
                if (value != _c_destipdocref)
                {
                    _c_destipdocref = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_iddocref
        {
            get
            {
                return _n_iddocref;
            }

            set
            {
                if (value != _n_iddocref)
                {
                    _n_iddocref = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_desdocref
        {
            get
            {
                return _c_desdocref;
            }

            set
            {
                if (value != _c_desdocref)
                {
                    _c_desdocref = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static List<ItemMovimientoDetalle> FetchList(int n_idemp, int n_anotra)
        {
            List<ItemMovimientoDetalle> m_listentidad = new List<ItemMovimientoDetalle>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_movimientoitemdetalle_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemMovimientoDetalle m_entidad = SetObject(reader);
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static ItemMovimientoDetalle Fetch(int id)
        {
            ItemMovimientoDetalle m_entidad = new ItemMovimientoDetalle();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_movimientoitemdetalle_traerregistro";
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
                            command.CommandText = "con_movimientoitemdetalle_insertar";
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
                command.CommandText = "con_movimientoitemdetalle_insertar";
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
                            command.CommandText = "con_movimientoitemdetalle_actualizar";
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
                command.CommandText = "con_movimientoitemdetalle_actualizar";
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
                            command.CommandText = "con_movimientoitemdetalle_eliminar";
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

        private static ItemMovimientoDetalle SetObject(MySqlDataReader reader)
        {
            return new ItemMovimientoDetalle
            {
                n_id = reader.GetInt32("n_id"),
                n_idcostoprod = reader.GetInt32("n_idcostoprod"),
                n_idmov = reader.GetInt32("n_idmov"),
                n_can = reader.GetDouble("n_can"),
                n_costounit = reader.GetDouble("n_costounit"),
                n_costoprom = reader.GetDouble("n_costoprom"),
                n_costomp = reader.GetDouble("n_costomp"),
                n_costomod = reader.GetDouble("n_costomod"),
                n_costocif = reader.GetDouble("n_costocif")
            };
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idcostoprod", n_idcostoprod));
            command.Parameters.Add(new MySqlParameter("@n_idmov", n_idmov));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
            command.Parameters.Add(new MySqlParameter("@n_costounit", n_costounit));
            command.Parameters.Add(new MySqlParameter("@n_costoprom", n_costoprom));
            command.Parameters.Add(new MySqlParameter("@n_costomp", n_costomp));
            command.Parameters.Add(new MySqlParameter("@n_costomod", n_costomod));
            command.Parameters.Add(new MySqlParameter("@n_costocif", n_costocif));
        }

        #endregion
    }
}
