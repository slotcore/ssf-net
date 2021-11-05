using Helper;
using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using SIAC_DATOS.Classes.Planilla;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Datos.Models.Planillas
{
    public class Empleado : ObjectBase
    {

        private int _n_id;

        private int _n_idemp;

        private int _n_idtipdocide;

        private string _c_numdocide;

        private string _c_ape1;

        private string _c_ape2;

        private string _c_nom1;

        private string _c_nom2;

        private DateTime _d_fchnac;

        private int _n_idsex;

        private string _c_numtel;

        private string _c_numesa;

        private DateTime _d_fching;

        private int _n_asigfam;

        private double _n_suebas;

        private int _n_bon;

        private double _n_imphornor;

        private double _n_imphorext;

        private int _n_activo;

        private int _n_destajo;

        private string _c_dir;

        private string _c_email;

        private int _n_idnacpro;

        private int _n_idnacdep;

        private int _n_idnacdis;

        private int _n_idrespro;

        private int _n_idresdep;

        private int _n_idresdis;

        private int _n_destacado;

        private DateTime _d_fchbaj;

        private ObservableListSource<PeriodoLaboral> _PeriodoLaborals = new ObservableListSource<PeriodoLaboral>();

        public virtual ObservableListSource<PeriodoLaboral> PeriodoLaborals
        {
            get
            {
                return _PeriodoLaborals;
            }
            set
            {
                if (value != _PeriodoLaborals)
                {
                    _PeriodoLaborals = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime d_fchbaj
        {
            get
            {
                return _d_fchbaj;
            }

            set
            {
                if (value != _d_fchbaj)
                {
                    _d_fchbaj = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_destacado
        {
            get
            {
                return _n_destacado;
            }

            set
            {
                if (value != _n_destacado)
                {
                    _n_destacado = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idresdis
        {
            get
            {
                return _n_idresdis;
            }

            set
            {
                if (value != _n_idresdis)
                {
                    _n_idresdis = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idresdep
        {
            get
            {
                return _n_idresdep;
            }

            set
            {
                if (value != _n_idresdep)
                {
                    _n_idresdep = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idrespro
        {
            get
            {
                return _n_idrespro;
            }

            set
            {
                if (value != _n_idrespro)
                {
                    _n_idrespro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idnacdis
        {
            get
            {
                return _n_idnacdis;
            }

            set
            {
                if (value != _n_idnacdis)
                {
                    _n_idnacdis = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idnacdep
        {
            get
            {
                return _n_idnacdep;
            }

            set
            {
                if (value != _n_idnacdep)
                {
                    _n_idnacdep = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idnacpro
        {
            get
            {
                return _n_idnacpro;
            }

            set
            {
                if (value != _n_idnacpro)
                {
                    _n_idnacpro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_email
        {
            get
            {
                return _c_email;
            }

            set
            {
                if (value != _c_email)
                {
                    _c_email = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_dir
        {
            get
            {
                return _c_dir;
            }

            set
            {
                if (value != _c_dir)
                {
                    _c_dir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_destajo
        {
            get
            {
                return _n_destajo;
            }

            set
            {
                if (value != _n_destajo)
                {
                    _n_destajo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_activo
        {
            get
            {
                return _n_activo;
            }

            set
            {
                if (value != _n_activo)
                {
                    _n_activo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_imphorext
        {
            get
            {
                return _n_imphorext;
            }

            set
            {
                if (value != _n_imphorext)
                {
                    _n_imphorext = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_imphornor
        {
            get
            {
                return _n_imphornor;
            }

            set
            {
                if (value != _n_imphornor)
                {
                    _n_imphornor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_bon
        {
            get
            {
                return _n_bon;
            }

            set
            {
                if (value != _n_bon)
                {
                    _n_bon = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double n_suebas
        {
            get
            {
                return _n_suebas;
            }

            set
            {
                if (value != _n_suebas)
                {
                    _n_suebas = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_asigfam
        {
            get
            {
                return _n_asigfam;
            }

            set
            {
                if (value != _n_asigfam)
                {
                    _n_asigfam = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime d_fching
        {
            get
            {
                return _d_fching;
            }

            set
            {
                if (value != _d_fching)
                {
                    _d_fching = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_numesa
        {
            get
            {
                return _c_numesa;
            }

            set
            {
                if (value != _c_numesa)
                {
                    _c_numesa = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_numtel
        {
            get
            {
                return _c_numtel;
            }

            set
            {
                if (value != _c_numtel)
                {
                    _c_numtel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idsex
        {
            get
            {
                return _n_idsex;
            }

            set
            {
                if (value != _n_idsex)
                {
                    _n_idsex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime d_fchnac
        {
            get
            {
                return _d_fchnac;
            }

            set
            {
                if (value != _d_fchnac)
                {
                    _d_fchnac = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_nom2
        {
            get
            {
                return _c_nom2;
            }

            set
            {
                if (value != _c_nom2)
                {
                    _c_nom2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_nom1
        {
            get
            {
                return _c_nom1;
            }

            set
            {
                if (value != _c_nom1)
                {
                    _c_nom1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_ape2
        {
            get
            {
                return _c_ape2;
            }

            set
            {
                if (value != _c_ape2)
                {
                    _c_ape2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_ape1
        {
            get
            {
                return _c_ape1;
            }

            set
            {
                if (value != _c_ape1)
                {
                    _c_ape1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_numdocide
        {
            get
            {
                return _c_numdocide;
            }

            set
            {
                if (value != _c_numdocide)
                {
                    _c_numdocide = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idtipdocide
        {
            get
            {
                return _n_idtipdocide;
            }

            set
            {
                if (value != _n_idtipdocide)
                {
                    _n_idtipdocide = value;
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

        public static Empleado Fetch(int id)
        {
            Empleado m_entidad = new Empleado();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pla_empleados_obtenerregistro";
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

        public static List<EmpleadoAvance> FetchEmpleadosAvance(int n_idpro
            , int n_idemp
            , string n_idtraIn
            , int n_idtar
            , DateTime d_fchini
            , DateTime d_fchfin)
        {
            string c_fchini = d_fchini.ToString("yyyy-MM-dd");
            string c_fchfin = d_fchfin.ToString("yyyy-MM-dd");
            List<EmpleadoAvance> m_listaentidad = new List<EmpleadoAvance>();

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pla_empleados_listaravance";
                    command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
                    command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
                    command.Parameters.Add(new MySqlParameter("@n_idtraIn", n_idtraIn));
                    command.Parameters.Add(new MySqlParameter("@n_idtar", n_idtar));
                    command.Parameters.Add(new MySqlParameter("@c_fchini", c_fchini));
                    command.Parameters.Add(new MySqlParameter("@c_fchfin", c_fchfin));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m_listaentidad.Add(new EmpleadoAvance
                            {
                                n_idper = Genericas.GetInt(reader, "n_idper"),
                                c_apenom = Genericas.GetString(reader, "c_apenom"),
                                d_fchtra = Genericas.GetDateTime(reader, "d_fchtra"),
                                n_can = Genericas.GetDouble(reader, "n_can"),
                                c_horini = Genericas.GetString(reader, "c_horini"),
                                c_horter = Genericas.GetString(reader, "c_horter")
                            });
                        }
                    }
                }
            }
            return m_listaentidad;
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
                        command.CommandText = SIAC_DATOS.Properties.Resources.q_pla_empleado_insert;
                        AddParameters(command);
                        int rows = command.ExecuteNonQuery();
                        //
                        foreach (PeriodoLaboral periodoLaboral in PeriodoLaborals)
                        {
                            periodoLaboral.Save(connection, transaction);
                        }
                    }
                }
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
                        command.Transaction = transaction;
                        command.CommandText = SIAC_DATOS.Properties.Resources.q_pla_empleado_update;
                        AddParameters(command);
                        int rows = command.ExecuteNonQuery();
                        //
                        foreach (PeriodoLaboral periodoLaboral in PeriodoLaborals)
                        {
                            periodoLaboral.Save(connection, transaction);
                        }
                    }
                }
            }
        }

        private static Empleado SetObject(MySqlDataReader reader)
        {
            return new Empleado
            {
                n_id = reader.GetInt32("n_id"),
                n_idemp = reader.GetInt32("n_idemp"),
                n_idtipdocide = reader.GetInt32("n_idtipdocide"),
                c_numdocide = reader.GetString("c_numdocide"),
                c_ape1 = Genericas.GetString(reader, "c_ape1"),
                c_ape2 = Genericas.GetString(reader, "c_ape2"),
                c_nom1 = Genericas.GetString(reader, "c_nom1"),
                c_nom2 = Genericas.GetString(reader, "c_nom2"),
                d_fchnac = Genericas.GetDateTime(reader, "d_fchnac"),
                n_idsex = Genericas.GetInt(reader, "n_idsex"),
                c_numtel = Genericas.GetString(reader, "c_numtel"),
                c_numesa = Genericas.GetString(reader, "c_numesa"),
                d_fching = Genericas.GetDateTime(reader, "d_fching"),
                n_asigfam = Genericas.GetInt(reader, "n_asigfam"),
                n_suebas = Genericas.GetDouble(reader, "n_suebas"),
                n_bon = Genericas.GetInt(reader, "n_bon"),
                n_imphornor = Genericas.GetDouble(reader, "n_imphornor"),
                n_imphorext = Genericas.GetDouble(reader, "n_imphorext"),
                n_activo = Genericas.GetInt(reader, "n_activo"),
                n_destajo = Genericas.GetInt(reader, "n_destajo"),
                c_dir = Genericas.GetString(reader, "c_dir"),
                c_email = Genericas.GetString(reader, "c_email"),
                n_idnacpro = Genericas.GetInt(reader, "n_idnacpro"),
                n_idnacdep = Genericas.GetInt(reader, "n_idnacdep"),
                n_idnacdis = Genericas.GetInt(reader, "n_idnacdis"),
                n_idrespro = Genericas.GetInt(reader, "n_idrespro"),
                n_idresdep = Genericas.GetInt(reader, "n_idresdep"),
                n_idresdis = Genericas.GetInt(reader, "n_idresdis"),
                n_destacado = Genericas.GetInt(reader, "n_destacado"),
                d_fchbaj = Genericas.GetDateTime(reader, "d_fchbaj")
            };
        }

        public void AddParameters(MySqlCommand command)
        {
            command.Parameters.Add(new MySqlParameter("@n_idemp", n_idemp));
            command.Parameters.Add(new MySqlParameter("@n_id", n_id));
            command.Parameters.Add(new MySqlParameter("@n_idtipdocide", n_idtipdocide));
            command.Parameters.Add(new MySqlParameter("@c_numdocide", c_numdocide));
            command.Parameters.Add(new MySqlParameter("@c_ape1", c_ape1));
            command.Parameters.Add(new MySqlParameter("@c_ape2", c_ape2));
            command.Parameters.Add(new MySqlParameter("@c_nom1", c_nom1));
            command.Parameters.Add(new MySqlParameter("@c_nom2", c_nom2));
            command.Parameters.Add(new MySqlParameter("@d_fchnac", d_fchnac));
            command.Parameters.Add(new MySqlParameter("@n_idsex", n_idsex));
            command.Parameters.Add(new MySqlParameter("@c_numtel", c_numtel));
            command.Parameters.Add(new MySqlParameter("@c_numesa", c_numesa));
            command.Parameters.Add(new MySqlParameter("@d_fching", d_fching));
            command.Parameters.Add(new MySqlParameter("@n_asigfam", n_asigfam));
            command.Parameters.Add(new MySqlParameter("@n_suebas", n_suebas));
            command.Parameters.Add(new MySqlParameter("@n_bon", n_bon));
            command.Parameters.Add(new MySqlParameter("@n_imphornor", n_imphornor));
            command.Parameters.Add(new MySqlParameter("@n_imphorext", n_imphorext));
            command.Parameters.Add(new MySqlParameter("@n_activo", n_activo));
            command.Parameters.Add(new MySqlParameter("@n_destajo", n_destajo));
            command.Parameters.Add(new MySqlParameter("@c_dir", c_dir));
            command.Parameters.Add(new MySqlParameter("@c_email", c_email));
            command.Parameters.Add(new MySqlParameter("@n_idnacpro", n_idnacpro));
            command.Parameters.Add(new MySqlParameter("@n_idnacdep", n_idnacdep));
            command.Parameters.Add(new MySqlParameter("@n_idnacdis", n_idnacdis));
            command.Parameters.Add(new MySqlParameter("@n_idrespro", n_idrespro));
            command.Parameters.Add(new MySqlParameter("@n_idresdep", n_idresdep));
            command.Parameters.Add(new MySqlParameter("@n_idresdis", n_idresdis));
            command.Parameters.Add(new MySqlParameter("@n_destacado", n_destacado));
            command.Parameters.Add(new MySqlParameter("@d_fchbaj", d_fchbaj));
        }
    }
}
