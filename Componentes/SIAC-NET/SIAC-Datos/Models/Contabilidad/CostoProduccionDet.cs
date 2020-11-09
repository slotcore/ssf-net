using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using SIAC_Entidades.Produccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Contabilidad
{
    public class CostoProduccionDet : ObjectBase
    {
        #region constructor
        public CostoProduccionDet()
        {
            _IsNew = true;
            _IsValid = false;
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idcostoprod;

        private int _n_idparteprod;

        private int _n_idmov;

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

        public int n_idparteprod
        {
            get
            {
                return _n_idparteprod;
            }

            set
            {
                if (value != _n_idparteprod)
                {
                    _n_idparteprod = value;
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

        private double _n_factdist;
        public double n_factdist
        {
            get
            {
                return _n_factdist;
            }

            set
            {
                if (value != _n_factdist)
                {
                    _n_factdist = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _n_costomp;
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

        public double n_costoprimo
        {
            get
            {
                return n_costomp + n_costomod;
            }
        }

        private double _n_costomod;
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

        private double _n_costocif;
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

        public double n_costototal
        {
            get
            {
                return n_costoprimo + n_costocif;
            }
        }

        private double _n_can;
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

        private int _n_idpro;
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

        private string _c_desparteprod;
        public string c_desparteprod
        {
            get
            {
                return _c_desparteprod;
            }

            set
            {
                if (value != _c_desparteprod)
                {
                    _c_desparteprod = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idrec;
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

        private string _c_descodprod;
        public string c_descodprod
        {
            get
            {
                return _c_descodprod;
            }

            set
            {
                if (value != _c_descodprod)
                {
                    _c_descodprod = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desprod;
        public string c_desprod
        {
            get
            {
                return _c_desprod;
            }

            set
            {
                if (value != _c_desprod)
                {
                    _c_desprod = value;
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

        private ObservableListSource<CostoProduccionDetIns> _CostoProduccionDetInss;
        public ObservableListSource<CostoProduccionDetIns> CostoProduccionDetInss
        {
            get
            {
                if (_CostoProduccionDetInss == null)
                {
                    if (IsValid)
                        _CostoProduccionDetInss = CostoProduccionDetIns.FetchList(_n_id);
                }
                return _CostoProduccionDetInss;
            }

            set
            {
                if (value != _CostoProduccionDetInss)
                {
                    _CostoProduccionDetInss = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<CostoProduccionDetMod> _CostoProduccionDetMods;
        public ObservableListSource<CostoProduccionDetMod> CostoProduccionDetMods
        {
            get
            {
                if (_CostoProduccionDetMods == null)
                {
                    if (IsValid)
                        _CostoProduccionDetMods = CostoProduccionDetMod.FetchList(_n_id);
                }
                return _CostoProduccionDetMods;
            }

            set
            {
                if (value != _CostoProduccionDetMods)
                {
                    _CostoProduccionDetMods = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableListSource<CostoProduccionDetCif> _CostoProduccionDetCifs;
        public ObservableListSource<CostoProduccionDetCif> CostoProduccionDetCifs
        {
            get
            {
                if (_CostoProduccionDetCifs == null)
                {
                    if (IsValid)
                        _CostoProduccionDetCifs = CostoProduccionDetCif.FetchList(_n_id);
                }
                return _CostoProduccionDetCifs;
            }

            set
            {
                if (value != _CostoProduccionDetCifs)
                {
                    _CostoProduccionDetCifs = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region metodos publicos

        public static ObservableListSource<CostoProduccionDet> FetchList(int n_idcostoprod)
        {
            ObservableListSource<CostoProduccionDet> m_listentidad = new ObservableListSource<CostoProduccionDet>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddet_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idcostoprod", n_idcostoprod));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionDet m_entidad = SetObject(reader);
                            m_entidad.IsNew = false;
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static CostoProduccionDet Fetch(int id)
        {
            CostoProduccionDet m_entidad = new CostoProduccionDet();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddet_obtenerregistro";
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
                            command.CommandText = "con_costoproddet_insertar";
                            AddParameters(command);
                            command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                            int rows = command.ExecuteNonQuery();
                            n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
                        }
                        SaveChildren(connection, transaction);
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
                command.CommandText = "con_costoproddet_insertar";
                AddParameters(command);
                command.Parameters["@n_id"].Direction = System.Data.ParameterDirection.Output;
                int rows = command.ExecuteNonQuery();
                n_id = Convert.ToInt32(command.Parameters["@n_id"].Value);
            }
            SaveChildren(connection, transaction);
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
                        if (IsOld)
                        {
                            using (MySqlCommand command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.CommandText = "con_costoproddet_actualizar";
                                AddParameters(command);
                                int rows = command.ExecuteNonQuery();
                            }
                        }
                        if (HasOldOrNewChildren)
                        {
                            SaveChildren(connection, transaction);
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
            if (IsOld)
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddet_actualizar";
                    AddParameters(command);
                    int rows = command.ExecuteNonQuery();
                }
            }
            if (HasOldOrNewChildren)
            {
                SaveChildren(connection, transaction);
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
                            command.CommandText = "con_costoproddet_eliminar";
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
            //Se eliminan en primer lugar los hijos
            //_CostoProduccionDetInss
            CostoProduccionDetIns.DeleteAll(n_id, connection, transaction);

            //_CostoProduccionDetMods
            CostoProduccionDetMod.DeleteAll(n_id, connection, transaction);

            //_CostoProduccionDetCifs
            CostoProduccionDetCif.DeleteAll(n_id, connection, transaction);
            //
            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddet_eliminar";
                    command.Parameters.Add(new MySqlParameter("@n_id", n_id));
                    int rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveChildren(MySqlConnection connection, MySqlTransaction transaction)
        {
            //CostoProduccionDetInss
            foreach (var hijo in CostoProduccionDetInss)
            {
                if (hijo.IsNew)
                    hijo.n_idcostoproddet = n_id;

                hijo.Save(connection, transaction);
            }
            foreach (var hijo in CostoProduccionDetInss.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
            }

            //CostoProduccionDetMods
            foreach (var hijo in CostoProduccionDetMods)
            {
                if (hijo.IsNew)
                    hijo.n_idcostoproddet = n_id;

                hijo.Save(connection, transaction);
            }
            foreach (var hijo in CostoProduccionDetMods.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
            }

            //CostoProduccionDetCifs
            foreach (var hijo in CostoProduccionDetCifs)
            {
                if (hijo.IsNew)
                    hijo.n_idcostoproddet = n_id;

                hijo.Save(connection, transaction);
            }
            foreach (var hijo in CostoProduccionDetCifs.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
            }
        }

        public void ListarInsumosParteProduccion()
        {
            if (_CostoProduccionDetInss == null)
                _CostoProduccionDetInss = new ObservableListSource<CostoProduccionDetIns>();

            _CostoProduccionDetInss.Clear();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddet_listarinsumosparte";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", _n_idparteprod));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionDetIns m_entidad = CostoProduccionDetIns.SetObject(reader);
                            _CostoProduccionDetInss.Add(m_entidad);
                        }
                    }
                }
            }
        }

        public void ListarModParteProduccion()
        {
            if (_CostoProduccionDetMods == null)
                _CostoProduccionDetMods = new ObservableListSource<CostoProduccionDetMod>();

            _CostoProduccionDetMods.Clear();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddet_listarmodparte";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", _n_idparteprod));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionDetMod m_entidad = CostoProduccionDetMod.SetObject(reader);
                            _CostoProduccionDetMods.Add(m_entidad);
                        }
                    }
                }
            }
        }

        #endregion

        #region metodos privados

        public static CostoProduccionDet SetObject(MySqlDataReader reader)
        {
            CostoProduccionDet costoProduccionDet = new CostoProduccionDet
            {
                n_id = reader.GetInt32("n_id"),
                n_idcostoprod = reader.GetInt32("n_idcostoprod"),
                n_idparteprod = reader.GetInt32("n_idparteprod"),
                n_factdist = reader.GetDouble("n_factdist"),
                n_costomp = reader.GetDouble("n_costomp"),
                n_costomod = reader.GetDouble("n_costomod"),
                n_costocif = reader.GetDouble("n_costocif"),
                n_can = reader.GetDouble("n_can"),
                n_idpro = reader.GetInt32("n_idpro"),
                c_desparteprod = reader.GetString("c_desparteprod"),
                n_idrec = reader.GetInt32("n_idrec"),
                c_descodprod = reader.GetString("c_descodprod"),
                c_desprod = reader.GetString("c_desprod"),
                c_desrec = reader.GetString("c_desrec"),
                c_desunimed = reader.GetString("c_desunimed")
            };
            if (!reader.IsDBNull(reader.GetOrdinal("n_idmov")))
            {
                costoProduccionDet.n_idmov = reader.GetInt32("n_idmov");
            }

            return costoProduccionDet;
        }

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idcostoprod", n_idcostoprod));
            command.Parameters.Add(new MySqlParameter("@n_idparteprod", n_idparteprod));
            command.Parameters.Add(new MySqlParameter("@n_idmov", n_idmov));
            command.Parameters.Add(new MySqlParameter("@n_factdist", n_factdist));
            command.Parameters.Add(new MySqlParameter("@n_costomp", n_costomp));
            command.Parameters.Add(new MySqlParameter("@n_costomod", n_costomod));
            command.Parameters.Add(new MySqlParameter("@n_costocif", n_costocif));
            command.Parameters.Add(new MySqlParameter("@n_can", n_can));
        }

        #endregion
    }
}
