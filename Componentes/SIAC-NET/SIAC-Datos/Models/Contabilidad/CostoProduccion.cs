using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using SIAC_DATOS.Classes.Contabilidad;
using SIAC_DATOS.Models.Almacen;
using SIAC_DATOS.Models.Logistica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Models.Contabilidad
{
    public class CostoProduccion : ObjectBase
    {
        #region constructor

        public CostoProduccion()
        {
            _IsNew = true;
            _IsValid = true;
        }

        #endregion

        #region propiedades

        private int _n_id;
        private int _n_idemp;
        private int _n_anotra;
        private int _n_idmes;
        private ObservableListSource<CostoProduccionDet> _CostoProduccionDets;
        private ObservableListSource<CostoProduccionMovimiento> _CostoProduccionMovimientos;
        private ObservableListSource<CostoProduccionError> _CostoProduccionErrors;
        private ObservableListSource<CostoProduccionCue> _CostoProduccionCues;

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

        public int n_idmes
        {
            get
            {
                return _n_idmes;
            }

            set
            {
                if (value != _n_idmes)
                {
                    _n_idmes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idconfigval;
        public int n_idconfigval
        {
            get
            {
                return _n_idconfigval;
            }

            set
            {
                if (value != _n_idconfigval)
                {
                    _n_idconfigval = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _n_idresp;
        public int n_idresp
        {
            get
            {
                return _n_idresp;
            }

            set
            {
                if (value != _n_idresp)
                {
                    _n_idresp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_numser;
        public string c_numser
        {
            get
            {
                return _c_numser;
            }

            set
            {
                if (value != _c_numser)
                {
                    _c_numser = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_numdoc;
        public string c_numdoc
        {
            get
            {
                return _c_numdoc;
            }

            set
            {
                if (value != _c_numdoc)
                {
                    _c_numdoc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_des;
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

        public DateTime d_fchini
        {
            get
            {
                DateTime fchIni = new DateTime(n_anotra, n_idmes, 1);
                return fchIni;
            }
        }

        public DateTime d_fchfin
        {
            get
            {
                int m_mesactual = n_idmes;
                int m_anhoactual = n_anotra;
                if (m_mesactual == 12)
                {
                    m_anhoactual += 1;
                    m_mesactual = 0;
                }
                    
                DateTime fchIni = new DateTime(m_anhoactual, m_mesactual + 1, 1);
                return fchIni;
            }
        }

        private string _c_desmes;
        public string c_desmes
        {
            get
            {
                return _c_desmes;
            }

            set
            {
                if (value != _c_desmes)
                {
                    _c_desmes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desconfigval;
        public string c_desconfigval
        {
            get
            {
                return _c_desconfigval;
            }

            set
            {
                if (value != _c_desconfigval)
                {
                    _c_desconfigval = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_numdocvis;
        public string c_numdocvis
        {
            get
            {
                return _c_numdocvis;
            }

            set
            {
                if (value != _c_numdocvis)
                {
                    _c_numdocvis = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _c_desresp;
        public string c_desresp
        {
            get
            {
                return _c_desresp;
            }

            set
            {
                if (value != _c_desresp)
                {
                    _c_desresp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableListSource<CostoProduccionDet> CostoProduccionDets
        {
            get
            {
                if (_CostoProduccionDets == null)
                {
                    if (IsValid)
                        _CostoProduccionDets = CostoProduccionDet.FetchList(_n_id);
                }
                return _CostoProduccionDets;
            }

            set
            {
                if (value != _CostoProduccionDets)
                {
                    _CostoProduccionDets = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableListSource<CostoProduccionMovimiento> CostoProduccionMovimientos
        {
            get
            {
                return _CostoProduccionMovimientos;
            }

            set
            {
                if (value != _CostoProduccionMovimientos)
                {
                    _CostoProduccionMovimientos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableListSource<CostoProduccionError> CostoProduccionErrors
        {
            get
            {
                return _CostoProduccionErrors;
            }

            set
            {
                if (value != _CostoProduccionErrors)
                {
                    _CostoProduccionErrors = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableListSource<CostoProduccionCue> CostoProduccionCues
        {
            get
            {
                if (_CostoProduccionCues == null)
                {
                    if (IsValid)
                        _CostoProduccionCues = CostoProduccionCue.FetchList(_n_id);
                }
                return _CostoProduccionCues;
            }

            set
            {
                if (value != _CostoProduccionCues)
                {
                    _CostoProduccionCues = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region metodos publicos

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
                            command.CommandText = "con_costoprod_insertar";
                            AddParameters(command);
                            int rows = command.ExecuteNonQuery();
                        }
                        //
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
                command.CommandText = "con_costoprod_insertar";
                AddParameters(command);
                int rows = command.ExecuteNonQuery();
            }
            //
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
                                command.CommandText = "con_costoprod_actualizar";
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
                    command.CommandText = "con_costoprod_actualizar";
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
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        try
                        {
                            command.Transaction = transaction;
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_costoprod_delete";
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

        protected override bool CheckHasOldOrNewChildren()
        {
            bool isChildOld = false;

            //CostoProduccionDets
            var childsDetOld = CostoProduccionDets.Where(o => o.IsNew == true || o.IsOld == true);
            if (childsDetOld != null)
            {
                if (childsDetOld.Count() > 0)
                    isChildOld = true;
            }
            if (!isChildOld)
            {
                if (CostoProduccionDets.GetRemoveItems().Count > 0)
                {
                    isChildOld = true;
                }
            }

            //CostoProduccionCues
            if (!isChildOld)
            {
                var childsCueOld = CostoProduccionCues.Where(o => o.IsNew == true || o.IsOld == true);
                if (childsCueOld != null)
                {
                    if (childsCueOld.Count() > 0)
                        isChildOld = true;
                }
                if (!isChildOld)
                {
                    if (CostoProduccionCues.GetRemoveItems().Count > 0)
                    {
                        isChildOld = true;
                    }
                }
            }

            return isChildOld;
        }

        private void SaveChildren(MySqlConnection connection, MySqlTransaction transaction)
        {
            //CostoProduccionDets
            foreach (var hijo in CostoProduccionDets)
            {
                if (hijo.IsNew)
                    hijo.n_idcostoprod = n_id;

                hijo.Save(connection, transaction);
            }
            foreach (var hijo in CostoProduccionDets.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
            }

            //CostoProduccionDets
            foreach (var hijo in CostoProduccionCues)
            {
                if (hijo.IsNew)
                    hijo.n_idcostoprod = n_id;

                hijo.Save(connection, transaction);
            }
            foreach (var hijo in CostoProduccionCues.GetRemoveItems())
            {
                if (!hijo.IsNew)
                    hijo.Delete(connection, transaction);
            }
        }

        #endregion

        #region Metodos Estaticos

        public static List<CostoProduccion> FetchList(int n_idemp, int n_anotra)
        {
            List<CostoProduccion> m_listentidad = new List<CostoProduccion>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprod_listar";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccion m_entidad = SetObject(reader);
                            m_entidad.IsNew = false;
                            m_listentidad.Add(m_entidad);
                        }
                    }
                }
            }
            return m_listentidad;
        }

        public static CostoProduccion Fetch(int id)
        {
            CostoProduccion m_entidad = new CostoProduccion();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprod_traerregistro";
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

        public void ListarPartesdeProduccion(int n_idemp, int n_anotra, int n_idmes)
        {
            if (_CostoProduccionDets == null)
                _CostoProduccionDets = new ObservableListSource<CostoProduccionDet>();

            _CostoProduccionDets?.RemoveAll();
            _CostoProduccionCues?.RemoveAll();
            _CostoProduccionErrors?.RemoveAll();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoproddet_listarpartes";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
                    command.Parameters.Add(new MySqlParameter("@n_idmes", n_idmes));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CostoProduccionDet m_entidad = CostoProduccionDet.SetObject(reader);
                            //insumos
                            m_entidad.ListarInsumosParteProduccion();
                            //mano de obra
                            m_entidad.ListarModParteProduccion();
                            //
                            CostoProduccionDets.Add(m_entidad);
                        }
                    }
                }
            }
        }

        private void CosteaMateriales(int n_idemp, DateTime d_fchini, DateTime d_fchfin)
        {
            // Validar movimientos anteriores sin costear
            // Se busca materiales en el rango de fechas
            List<ItemProceso> itemProcesos = new List<ItemProceso>();
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprod_listarmateriales_rangofecha";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@c_fchini", d_fchini.ToString("dd/MM/yyyy")));
                    command.Parameters.Add(new MySqlParameter("@c_fchfin", d_fchfin.ToString("dd/MM/yyyy")));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itemProcesos.Add(new ItemProceso(reader));
                        }
                    }
                }
            }
            //Se limpia la lista de errores
            if (CostoProduccionErrors == null)
            {
                CostoProduccionErrors = new ObservableListSource<CostoProduccionError>();
            }
            CostoProduccionErrors.Clear();

            //Se procesan los materiales encontrados
            foreach (ItemProceso itemProceso in itemProcesos)
            {
                try
                {
                    CosteaItem(n_idemp, itemProceso.n_idite, itemProceso.n_idalm, d_fchini, d_fchfin);
                }
                catch (CosteoProdException ex)
                {
                    CostoProduccionErrors.Add(new CostoProduccionError()
                    {
                        CodItem = ex.CodItem,
                        DesItem = ex.DesItem,
                        DesFechMov = ex.Fecha.ToString("dd/MM/yyyy"),
                        DesAlm = ex.Almacen,
                        DesMov = ex.NumeroMovimiento,
                        Error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void CosteaProductosIntermedios(int n_idemp, DateTime d_fchini, DateTime d_fchfin)
        {
            // Validar movimientos anteriores sin costear
            // Se busca materiales en el rango de fechas
            List<ItemProceso> itemProcesos = new List<ItemProceso>();
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprod_listarmateriales_rangofecha";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@c_fchini", d_fchini.ToString("dd/MM/yyyy")));
                    command.Parameters.Add(new MySqlParameter("@c_fchfin", d_fchfin.ToString("dd/MM/yyyy")));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itemProcesos.Add(new ItemProceso(reader));
                        }
                    }
                }
            }
            //Se limpia la lista de errores
            if (CostoProduccionErrors == null)
            {
                CostoProduccionErrors = new ObservableListSource<CostoProduccionError>();
            }
            CostoProduccionErrors.Clear();

            //Se procesan los materiales encontrados
            foreach (ItemProceso itemProceso in itemProcesos)
            {
                try
                {
                    CosteaItem(n_idemp, itemProceso.n_idite, itemProceso.n_idalm, d_fchini, d_fchfin);
                }
                catch (CosteoProdException ex)
                {
                    CostoProduccionErrors.Add(new CostoProduccionError()
                    {
                        CodItem = ex.CodItem,
                        DesItem = ex.DesItem,
                        DesFechMov = ex.Fecha.ToString("dd/MM/yyyy"),
                        DesAlm = ex.Almacen,
                        DesMov = ex.NumeroMovimiento,
                        Error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void CosteaProductosTerminados(int n_idemp, DateTime d_fchini, DateTime d_fchfin)
        {
            // Validar movimientos anteriores sin costear
            // Se busca materiales en el rango de fechas
            List<ItemProceso> itemProcesos = new List<ItemProceso>();
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "con_costoprod_listarmateriales_rangofecha";
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@c_fchini", d_fchini.ToString("dd/MM/yyyy")));
                    command.Parameters.Add(new MySqlParameter("@c_fchfin", d_fchfin.ToString("dd/MM/yyyy")));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itemProcesos.Add(new ItemProceso(reader));
                        }
                    }
                }
            }
            //Se limpia la lista de errores
            if (CostoProduccionErrors == null)
            {
                CostoProduccionErrors = new ObservableListSource<CostoProduccionError>();
            }
            CostoProduccionErrors.Clear();

            //Se procesan los materiales encontrados
            foreach (ItemProceso itemProceso in itemProcesos)
            {
                try
                {
                    CosteaItem(n_idemp, itemProceso.n_idite, itemProceso.n_idalm, d_fchini, d_fchfin);
                }
                catch (CosteoProdException ex)
                {
                    CostoProduccionErrors.Add(new CostoProduccionError()
                    {
                        CodItem = ex.CodItem,
                        DesItem = ex.DesItem,
                        DesFechMov = ex.Fecha.ToString("dd/MM/yyyy"),
                        DesAlm = ex.Almacen,
                        DesMov = ex.NumeroMovimiento,
                        Error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ProcesarMp()
        {
            CostoProduccionErrors.Clear();

            CosteaMateriales(n_idemp, d_fchini, d_fchfin);

            if (CostoProduccionErrors.Count == 0)
            {
                CosteaProductosIntermedios(n_idemp, d_fchini, d_fchfin);

                if (CostoProduccionErrors.Count == 0)
                {
                    CosteaProductosTerminados(n_idemp, d_fchini, d_fchfin);
                }
            }
        }

        public void ProcesarMod()
        {
            foreach (var costoProduccionDet in CostoProduccionDets)
            {
                if (costoProduccionDet.CostoProduccionDetMods.Count > 0)
                {
                    var costo = costoProduccionDet.CostoProduccionDetMods.Sum(o => o.n_costo);
                    costoProduccionDet.n_costomod = costo;
                }
            }
        }

        public void ProcesarCif()
        {
            //Se llena las cuentas
            CostoProduccionCues = CostoProduccionCue
                .TraerListaPorConfiguracion(n_idemp, n_anotra, n_idmes, n_idconfigval);

            //Se realiza el prorrateo segun configuracion
            ConfigVal configVal = ConfigVal.Fetch(n_idconfigval);
            switch (configVal.c_factdist)
            {
                case ConfigVal.FactorDistribucion.Cantidad:
                    double cantidadTotal = CostoProduccionDets.Sum(o => o.n_can);
                    foreach (var costoProduccionDet in CostoProduccionDets)
                    {
                        if (costoProduccionDet.n_can > 0)
                        {
                            double indiceProrr = costoProduccionDet.n_can / cantidadTotal;
                            double impTot = CostoProduccionCues.Sum(o => o.n_impt);
                            costoProduccionDet.n_factdist = indiceProrr;
                            costoProduccionDet.n_costocif = impTot * indiceProrr;
                            // Se prorratean los cifs
                            costoProduccionDet.CostoProduccionDetCifs 
                                = new ObservableListSource<CostoProduccionDetCif>();
                            foreach (var costoProduccionCue in CostoProduccionCues)
                            {
                                CostoProduccionDetCif costoProduccionDetCif = new CostoProduccionDetCif();
                                costoProduccionDetCif.n_idcue = costoProduccionCue.n_idcue;
                                costoProduccionDetCif.n_impt = costoProduccionCue.n_impt * indiceProrr;
                                costoProduccionDetCif.c_descue = costoProduccionCue.c_descue;
                                costoProduccionDet.CostoProduccionDetCifs.Add(costoProduccionDetCif);
                            }
                        }
                    }
                    break;
            }
        }

        public void GrabaCostoMovimiento(int n_idite, ItemMovimientoDetalle MovimientoItem)
        {
            CostoProduccionMovimiento costoProduccionMovimiento 
                = CostoProduccionMovimientos
                .Where(o => o.n_idite == n_idite && o.n_idmov == MovimientoItem.n_idmov)
                .FirstOrDefault();

            if (costoProduccionMovimiento == null)
            {
                costoProduccionMovimiento = new CostoProduccionMovimiento();
                costoProduccionMovimiento.n_idmov = MovimientoItem.n_idmov;
                costoProduccionMovimiento.n_idite = n_idite;
                CostoProduccionMovimientos.Add(costoProduccionMovimiento);
            }
            //Se actualizan los valores actuales
            costoProduccionMovimiento.n_can = MovimientoItem.n_can;
            costoProduccionMovimiento.n_costounit = MovimientoItem.n_costounit;
            costoProduccionMovimiento.n_costounitprom = MovimientoItem.n_costounitprom;
            costoProduccionMovimiento.n_costomp = MovimientoItem.n_costomp;
            costoProduccionMovimiento.n_costomod = MovimientoItem.n_costomod;
            costoProduccionMovimiento.n_costocif = MovimientoItem.n_costocif;
        }

        public double ObtenerCostoMovimiento(int n_idite, int n_idmov)
        {
            double m_costoMovimiento = 0;

            if (CostoProduccionMovimientos == null)
            {
                CostoProduccionMovimientos = new ObservableListSource<CostoProduccionMovimiento>();
            }

            CostoProduccionMovimiento costoProduccionMovimiento
                = CostoProduccionMovimientos
                .Where(o => o.n_idite == n_idite && o.n_idmov == n_idmov)
                .FirstOrDefault();

            if (costoProduccionMovimiento != null)
            {
                m_costoMovimiento 
                    = costoProduccionMovimiento.n_can * costoProduccionMovimiento.n_costounitprom;
            }

            return m_costoMovimiento;
        }

        public void CosteaItem(int n_idemp, int n_idite, int n_idalm, DateTime d_fchini, DateTime d_fchfin)
        {
            if (n_idalm == 0)
            {
                Inventario inventario = Inventario.Fetch(n_idite);

                throw new CosteoProdException(inventario.c_codpro
                    , inventario.c_despro
                    , d_fchini
                    , ""
                    , string.Empty
                    , "Movimiento registrado sin almacén. ");
            }

            ItemMovimiento itemMovimiento 
                = ItemMovimiento.TraerMovimientoPorFecha(n_idemp, n_idite, n_idalm, d_fchini, d_fchfin);


            if (itemMovimiento.n_saldoini < 0)
            {
                throw new CosteoProdException(itemMovimiento.c_codite
                    , itemMovimiento.c_desite
                    , d_fchini
                    , itemMovimiento.c_desalm
                    , string.Empty
                    , "Saldo inicial menor o igual a cero. ");
            }

            if (itemMovimiento.n_costoini < 0)
            {
                throw new CosteoProdException(itemMovimiento.c_codite
                    , itemMovimiento.c_desite
                    , d_fchini
                    , itemMovimiento.c_desalm
                    , string.Empty
                    , "Costo inicial menor o igual a cero. ");
            }

            double mCantidadAcumulada = itemMovimiento.n_saldoini;
            double mCantidadAcumuladaEntrada = itemMovimiento.n_saldoini;
            double mCostoAcumulado = itemMovimiento.n_costoini;
            double mCostoUnitarioPromedio = itemMovimiento.n_costounipromini;

            double mCostoMovimiento = 0;
            double mCostoUnitarioMovimiento = 0;
            double mCantidadAcumuladaSalida = 0;

            foreach (ItemMovimientoDetalle itemMovimientoDet
                        in itemMovimiento.ItemMovimientoDetalles)
            {
                // Se busca errores de busqueda de items
                if (itemMovimientoDet.n_can <= 0)
                {
                    throw new CosteoProdException(itemMovimiento.c_codite
                        , itemMovimiento.c_desite
                        , itemMovimientoDet.d_fechmov
                        , itemMovimiento.c_desalm
                        , itemMovimientoDet.desmov
                        , "Movimiento con cantidad menor o igual a cero. ");
                }
                //**********
                // INGRESOS
                //**********
                if (itemMovimientoDet.c_destipmov == "E")
                {
                    mCantidadAcumulada = mCantidadAcumulada + itemMovimientoDet.n_can;
                    mCantidadAcumuladaEntrada = mCantidadAcumuladaEntrada + itemMovimientoDet.n_can;
                    // MOVIMIENTO SIN COSTEAR
                    if (ObtenerCostoMovimiento(n_idite, itemMovimientoDet.n_idmov) == 0)
                    {
                        ValidarDocumentoReferencia(itemMovimientoDet.n_idtipdocref
                                , itemMovimiento.c_codite
                                , itemMovimiento.c_desite
                                , itemMovimientoDet.d_fechmov
                                , itemMovimiento.c_desalm
                                , itemMovimientoDet.desmov);

                        mCostoMovimiento = CosteaMovimientoDetalle(n_idemp
                            , itemMovimiento.n_idite
                            , itemMovimiento.n_idalm
                            , d_fchini
                            , itemMovimientoDet);
                        mCostoUnitarioMovimiento = mCostoMovimiento / itemMovimientoDet.n_can;


                        // Validamos el costo unitario del movimiento
                        if (mCostoUnitarioMovimiento <= 0)
                        {
                            throw new CosteoProdException(itemMovimiento.c_codite
                                , itemMovimiento.c_desite
                                , itemMovimientoDet.d_fechmov
                                , itemMovimiento.c_desalm
                                , itemMovimientoDet.desmov
                                , "Costo Unitario de movimiento igual (o menor) a cero.");
                        }

                        mCostoAcumulado = mCostoAcumulado + mCostoMovimiento;
                        mCostoUnitarioPromedio = mCostoAcumulado / mCantidadAcumulada;
                        // Costeamos el movimiento
                        itemMovimientoDet.n_costomp = mCostoMovimiento;
                        itemMovimientoDet.n_costounit = mCostoUnitarioMovimiento;
                        itemMovimientoDet.n_costounitprom = mCostoUnitarioPromedio;

                        GrabaCostoMovimiento(itemMovimiento.n_idite, itemMovimientoDet);
                    }
                    // MOVIMIENTO COSTEADO
                    else
                    {
                        // Buscamos el importe del movimiento
                        mCostoAcumulado = mCostoAcumulado + itemMovimientoDet.n_costo;
                        mCostoMovimiento = itemMovimientoDet.n_costo;
                        mCostoUnitarioMovimiento = itemMovimientoDet.n_costounit;
                        mCostoUnitarioPromedio = mCostoAcumulado / mCantidadAcumulada;


                        // Se valida que este bien costeado
                        if (mCostoUnitarioPromedio != itemMovimientoDet.n_costounitprom) // Si esta mal costeado
                        {
                            // Actualizamos el costo unitario promedio
                            itemMovimientoDet.n_costounitprom = mCostoUnitarioPromedio;
                            GrabaCostoMovimiento(itemMovimiento.n_idite, itemMovimientoDet);
                        }
                    }
                }
                //**********
                // SALIDAS
                //**********
                else
                {
                    mCostoMovimiento = mCostoUnitarioPromedio * itemMovimientoDet.n_can;
                    mCantidadAcumulada = mCantidadAcumulada - itemMovimientoDet.n_can;
                    mCantidadAcumuladaSalida = mCantidadAcumuladaSalida + itemMovimientoDet.n_can;
                    mCostoUnitarioMovimiento = mCostoUnitarioPromedio;

                    // Se valida que no hayan saldos negativos
                    if (Math.Round(mCantidadAcumulada, 2) < 0)
                    {
                        throw new CosteoProdException(itemMovimiento.c_codite
                            , itemMovimiento.c_desite
                            , itemMovimientoDet.d_fechmov
                                , itemMovimiento.c_desalm
                                , itemMovimientoDet.desmov
                            , "Cantidad acumulada menor a cero.");
                    }

                    if (mCostoUnitarioPromedio <= 0)
                    {
                        throw new CosteoProdException(itemMovimiento.c_codite
                            , itemMovimiento.c_desite
                            , itemMovimientoDet.d_fechmov
                                , itemMovimiento.c_desalm
                                , itemMovimientoDet.desmov
                            , "Costo promedio igual (o menor) a cero.");
                    }

                    // MOVIMIENTO NO COSTEADO
                    if (ObtenerCostoMovimiento(n_idite, itemMovimientoDet.n_idmov) == 0)
                    {
                        // Costeamos el movimiento
                        itemMovimientoDet.n_costomp = mCostoMovimiento;
                        itemMovimientoDet.n_costounit = mCostoUnitarioMovimiento;
                        itemMovimientoDet.n_costounitprom = mCostoUnitarioPromedio;

                        GrabaCostoMovimiento(itemMovimiento.n_idite, itemMovimientoDet);
                    // MOVIMIENTO COSTEADO
                    }
                    else
                    {
                        // Si ha cambiado el costo unitario promedio
                        if (mCostoUnitarioPromedio != itemMovimientoDet.n_costounitprom)
                        {
                            // Costeamos el movimiento
                            itemMovimientoDet.n_costomp = mCostoMovimiento;
                            itemMovimientoDet.n_costounit = mCostoUnitarioMovimiento;
                            itemMovimientoDet.n_costounitprom = mCostoUnitarioPromedio;

                            GrabaCostoMovimiento(itemMovimiento.n_idite, itemMovimientoDet);
                        }
                    }


                    // Actualizamos el costo acumulado
                    mCostoAcumulado = mCostoAcumulado - mCostoMovimiento;
                }
            }
        }

        private void ValidarDocumentoReferencia(int n_idtipdocref, string c_codite, string c_desite, DateTime d_fechmov, string c_desalm, string desmov)
        {
            if (n_idtipdocref == 0)
            {
                throw new CosteoProdException(c_codite
                    , c_desite
                    , d_fechmov
                    , c_desalm
                    , desmov
                    , "Movimiento no cuenta con documento de referencia");
            }

            switch (n_idtipdocref)
            {
                case 10:
                    throw new CosteoProdException(c_codite
                        , c_desite
                        , d_fechmov
                        , c_desalm
                        , desmov
                        , "Tipo de documento referenciado no válido");
            }
        }

        public double CosteaParteProduccion(int n_idemp, int n_idprod)
        {
            double costoParte = 0;
            List<ItemMovimiento> itemMovimientos
                = ItemMovimiento.TraerMovimientoPorParte(n_idprod);

            foreach (ItemMovimiento itemMovimiento in itemMovimientos)
            {
                foreach (ItemMovimientoDetalle itemMovimientoDetalle in itemMovimiento.ItemMovimientoDetalles)
                {
                    double costoMovimiento = ObtenerCostoMovimiento(itemMovimiento.n_idite, itemMovimientoDetalle.n_idmov);
                    if (costoMovimiento == 0)
                    {
                        costoParte += CosteaMovimientoDetalle(n_idemp, itemMovimiento.n_idite
                            , itemMovimiento.n_idalm
                            , d_fchini
                            , itemMovimientoDetalle);
                    }
                    else
                    {
                        costoParte += costoMovimiento;
                    }
                }
            }

            return costoParte;
        }

        public double CosteaInventarioInicial(int n_idinvini, int n_idite)
        {
            double n_costoInventario = 0;
            InventarioInicial inventarioInicial = InventarioInicial.Fetch(n_idinvini);
            if (inventarioInicial != null)
            {
                InventarioInicialDet inventarioInicialDet = inventarioInicial.InventarioInicialDets
                    .Where(o => o.n_idite == n_idite)
                    .FirstOrDefault();

                if (inventarioInicialDet != null)
                {
                    n_costoInventario = inventarioInicialDet.n_can * inventarioInicialDet.n_costounit;
                }
            }
            return n_costoInventario;
        }

        public double CosteaAjusteInventario(ItemMovimientoDetalle MovimientoItem)
        {
            return 0;
        }

        public double ObtenerCostoUnitarioUltimaCompra(ItemMovimientoDetalle MovimientoItem)
        {
            return 0;
        }

        public double CosteaMovimientoDetalle(int n_idemp, int n_idite, int n_idalm, DateTime d_fchini, ItemMovimientoDetalle MovimientoItem)
        {
            double mCostoMovimientoDetalle = 0;
            // INGRESOS
            if (MovimientoItem.c_destipmov == "E")
            {
                switch (MovimientoItem.n_idtipdocref)
                {
                    //ORDEN DE COMPRA
                    case 84:
                        mCostoMovimientoDetalle = CosteaOrdenCompra(MovimientoItem.n_iddocref, n_idite);
                        break;

                    // PARTE DE PRODUCCION
                    case 1:
                        mCostoMovimientoDetalle = CosteaParteProduccion(n_idemp, MovimientoItem.n_iddocref);
                        break;


                    // SOLICITUD DE MATERIALES
                    case 2:
                        //Validamos el costo promedio
                        if (MovimientoItem.n_costounitprom == 0)
                        {
                            throw new Exception("Costo Unitario Promedio igual a cero al intentar costear Factura.");
                        }
                        mCostoMovimientoDetalle = MovimientoItem.n_costounitprom * MovimientoItem.n_can;
                        break;


                    // INVENTARIO INICIAL
                    case 97:
                        mCostoMovimientoDetalle = CosteaInventarioInicial(MovimientoItem.n_iddocref, n_idite);
                        break;


                    // AJUSTE DE INVENTARIO
                    case 4:
                        mCostoMovimientoDetalle = CosteaAjusteInventario(MovimientoItem);
                        break;


                    //NOTAS DE CREDITO
                    case 5:
                        // Validamos el costo promedio
                        if (MovimientoItem.n_costounitprom == 0)
                        {
                            throw new Exception("Costo Unitario Promedio igual a cero al intentar costear Factura.");
                        }
                        mCostoMovimientoDetalle = MovimientoItem.n_costounitprom * MovimientoItem.n_can;
                        break;


                    default:

                        mCostoMovimientoDetalle = ObtenerCostoUnitarioUltimaCompra(MovimientoItem) * MovimientoItem.n_can;
                        break;
                }
            }
            // SALIDAS
            else
            {
                // Se calculan los costos unitarios hasta la fecha del movimiento
                CosteaItem(n_idemp, n_idite, n_idalm, d_fchini, MovimientoItem.d_fechmov);
                //                
                mCostoMovimientoDetalle = ObtenerCostoMovimiento(n_idite, MovimientoItem.n_idmov);
            }


            return mCostoMovimientoDetalle;
        }

        private double CosteaOrdenCompra(int n_idordcom, int n_idite)
        {
            double n_costoOrdenCompra = 0;
            OrdenCompra ordenCompra = OrdenCompra.Fetch(n_idordcom);
            if (ordenCompra != null)
            {
                OrdenCompraDetalle ordenCompraDetalle = ordenCompra.OrdenCompraDetalles
                    .Where(o => o.n_idite == n_idite)
                    .FirstOrDefault();

                if (ordenCompraDetalle != null)
                {
                    n_costoOrdenCompra = ordenCompraDetalle.n_imptot;
                }
            }
            return n_costoOrdenCompra;
        }

        public static bool DocumentoExiste(int idemp, string numser, string numdoc)
        {
            return false;
        }

        #endregion

        #region metodos privados

        private void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_anotra", n_anotra));
            command.Parameters.Add(new MySqlParameter("@n_idmes", n_idmes));
            command.Parameters.Add(new MySqlParameter("@n_idconfigval", n_idconfigval));
            command.Parameters.Add(new MySqlParameter("@n_idresp", n_idresp));
            command.Parameters.Add(new MySqlParameter("@c_numser", c_numser));
            command.Parameters.Add(new MySqlParameter("@c_numdoc", c_numdoc));
            command.Parameters.Add(new MySqlParameter("@c_des", c_des));
            command.Parameters.Add(new MySqlParameter("@c_obs", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_costomod", c_obs));
            command.Parameters.Add(new MySqlParameter("@n_costocif", c_obs));
        }

        private static CostoProduccion SetObject(MySqlDataReader reader)
        {
            return new CostoProduccion
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_anotra = reader.GetInt32("n_anotra"),
                n_idmes = reader.GetInt32("n_idmes"),
                n_idconfigval = reader.GetInt32("n_idconfigval"),
                n_idresp = reader.GetInt32("n_idresp"),
                c_numser = reader.GetString("c_numser"),
                c_numdoc = reader.GetString("c_numdoc"),
                c_des = reader.GetString("c_des"),
                c_obs = reader.GetString("c_obs"),
                n_costomod = reader.GetDouble("n_costomod"),
                n_costocif = reader.GetDouble("n_costocif"),
                c_desmes = reader.GetString("c_desmes"),
                c_numdocvis = reader.GetString("c_numdocvis"),
                c_desconfigval = reader.GetString("c_desconfigval"),
                c_desresp = reader.GetString("c_desresp")
            };
        }

        #endregion
    }
}
