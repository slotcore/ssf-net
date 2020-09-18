using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using SIAC_DATOS.Classes.Contabilidad;
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
        }

        #endregion

        #region propiedades

        private int _n_id;

        private int _n_idemp;

        private int _n_anotra;

        private int _n_idmes;

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

        private ObservableListSource<CostoProduccionDet> _CostoProduccionDets;
        public ObservableListSource<CostoProduccionDet> CostoProduccionDets
        {
            get
            {
                if (_CostoProduccionDets == null)
                {
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

        private ObservableListSource<CostoProduccionMovimiento> _CostoProduccionMovimientos;
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
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "con_costroprod_insertar";
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
                command.CommandText = "con_costroprod_insertar";
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
                            command.CommandText = "con_costoprod_actualizar";
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
                command.CommandText = "con_costoprod_actualizar";
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

            _CostoProduccionDets.Clear();

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
                            _CostoProduccionDets.Add(m_entidad);
                        }
                    }
                }
            }
        }

        public void ProcesarMp(int n_idemp, DateTime d_fchini, DateTime d_fchfin)
        {
            // Validar movimientos anteriores sin costear
            // Materiales
            List<ItemProceso> itemProcesos = new List<ItemProceso>();
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
                            itemProcesos.Add(new ItemProceso(reader));
                        }
                    }
                }
            }

            foreach (ItemProceso itemProceso in itemProcesos)
            {
                try
                {
                    CosteaItem(itemProceso.n_idite, itemProceso.n_idalm, d_fchini, d_fchfin);
                }
                catch (Exception ex)
                {
                    throw;
                }
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
                costoProduccionMovimiento.n_can = MovimientoItem.n_can;
                costoProduccionMovimiento.n_costounit = MovimientoItem.n_costounit;
                costoProduccionMovimiento.n_costounitprom = MovimientoItem.n_costounitprom;
                costoProduccionMovimiento.n_costomp = MovimientoItem.n_costomp;
                costoProduccionMovimiento.n_costomod = MovimientoItem.n_costomod;
                costoProduccionMovimiento.n_costocif = MovimientoItem.n_costocif;
            }
        }

        public double ObtenerCostoMovimiento(int n_idite, int n_idmov)
        {
            double m_costoMovimiento = 0;
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

        public void CosteaItem(int n_idite, int n_idalm, DateTime d_fchini, DateTime d_fchfin)
        {
            ItemMovimiento itemMovimiento 
                = ItemMovimiento.TraerMovimientoPorFecha(n_idite, n_idalm, d_fchini, d_fchfin);

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
                    throw new Exception("Movimiento con cantidad igual (o menor) a cero. ");
                }
                //**********
                // INGRESOS
                //**********
                if (itemMovimientoDet.c_destipmov == "I")
                {
                    mCantidadAcumulada = mCantidadAcumulada + itemMovimientoDet.n_can;
                    mCantidadAcumuladaEntrada = mCantidadAcumuladaEntrada + itemMovimientoDet.n_can;
                    // MOVIMIENTO SIN COSTEAR
                    if (ObtenerCostoMovimiento(n_idite, itemMovimientoDet.n_idmov) == 0)
                    {
                        itemMovimientoDet.n_costounitprom = mCostoUnitarioPromedio;
                        mCostoMovimiento = CosteaMovimientoDetalle(itemMovimiento.n_idite,
                                                itemMovimiento.n_idalm,
                                                d_fchini,
                                                itemMovimientoDet);
                        mCostoUnitarioMovimiento = mCostoMovimiento / itemMovimientoDet.n_can;


                        // Validamos el costo unitario del movimiento
                        if (mCostoUnitarioMovimiento <= 0)
                        {
                            throw new Exception("Costo Unitario de movimiento igual (o menor) a cero. ");
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
                        throw new Exception("Cantidad acumulada menor a cero. ");
                    }


                    // MOVIMIENTO NO COSTEADO
                    if (itemMovimientoDet.n_costo == 0)
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

        public double CosteaParteProduccion(int n_idprod)
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
                        costoParte += CosteaMovimientoDetalle(itemMovimiento.n_idite
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

        public double CosteaInventarioInicial(ItemMovimientoDetalle MovimientoItem)
        {
            return 0;
        }

        public double CosteaAjusteInventario(ItemMovimientoDetalle MovimientoItem)
        {
            return 0;
        }

        public double ObtenerCostoUnitarioUltimaCompra(ItemMovimientoDetalle MovimientoItem)
        {
            return 0;
        }

        public double CosteaMovimientoDetalle(int n_idite, int n_idalm, DateTime d_fchini, ItemMovimientoDetalle MovimientoItem)
        {
            double mCostoMovimientoDetalle = 0;
            // INGRESOS
            if (MovimientoItem.c_destipmov == "I")
            {
                switch (MovimientoItem.n_idtipdocref)
                {
                    // PARTE DE PRODUCCION
                    case 1:
                        mCostoMovimientoDetalle = CosteaParteProduccion(MovimientoItem.n_iddocref);
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
                    case 3:
                        mCostoMovimientoDetalle = CosteaInventarioInicial(MovimientoItem);
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
                CosteaItem(n_idite, n_idalm, d_fchini, MovimientoItem.d_fechmov);
                //                
                mCostoMovimientoDetalle = ObtenerCostoMovimiento(n_idite, MovimientoItem.n_idmov);
            }


            return mCostoMovimientoDetalle;
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
