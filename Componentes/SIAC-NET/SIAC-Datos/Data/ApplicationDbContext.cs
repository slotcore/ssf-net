using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using SIAC_Datos.Models.Maestros;
using SIAC_Datos.Models.Planillas;
using System.Configuration;

namespace SIAC_DATOS.Data
{
    public class ApplicationDbContext
    {
        public static ObservableListSource<PeriodoLaboral> PeriodoLaborals(int idEmpleado)
        {
            ObservableListSource<PeriodoLaboral> _PeriodoLaborals = new ObservableListSource<PeriodoLaboral>();
            
            using (MySqlConnection connection 
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = Properties.Resources.q_pla_periodolaboral_get;
                    command.Parameters.Add(new MySqlParameter("@n_idempleado", idEmpleado));
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PeriodoLaboral periodoLaboral = new PeriodoLaboral
                            {
                                n_idperiodolaboral = reader.GetString("n_idperiodolaboral"),
                                n_idempleado = reader.GetInt32("n_idempleado"),
                                n_idcategoria = reader.GetInt32("n_idcategoria"),
                                n_idfinperiodo = reader.GetInt32("n_idfinperiodo"),
                                n_corr = reader.GetInt32("n_corr"),
                                d_fchini = reader.GetDateTime("d_fchini"),
                                d_fchfin = reader.GetDateTime("d_fchfin"),
                                categoria = reader.GetString("categoria"),
                                finperiodo = reader.GetString("finperiodo")
                            };
                            periodoLaboral.IsNew = false;
                            _PeriodoLaborals.Add(periodoLaboral);
                        }
                    }
                }
            }
            return _PeriodoLaborals;
        }

        public static ObservableListSource<Categoria> Categorias()
        {
            ObservableListSource<Categoria> _Categorias = new ObservableListSource<Categoria>();
            string selectQuery = "SELECT * FROM mae_categoria";
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria categoria = new Categoria
                            {
                                n_idcategoria = reader.GetInt32("n_idcategoria"),
                                c_descripcion = reader.GetString("c_descripcion"),
                                c_codsun = reader.GetValue(2).ToString(),
                                c_abrev = reader.GetString("c_abrev"),
                                n_activo = reader.GetBoolean("n_activo")
                            };
                            categoria.IsNew = false;
                            _Categorias.Add(categoria);
                        }
                    }
                }
            }
            return _Categorias;
        }

        public static ObservableListSource<FinPeriodo> FinPeriodos()
        {
            ObservableListSource<FinPeriodo> _FinPeriodos = new ObservableListSource<FinPeriodo>();
            string selectQuery = "SELECT * FROM mae_finperiodo";
            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FinPeriodo finPeriodo = new FinPeriodo
                            {
                                n_idfinperiodo = reader.GetInt32("n_idfinperiodo"),
                                c_descripcion = reader.GetString("c_descripcion"),
                                c_codsun = reader.GetString("c_codsun")
                            };
                            finPeriodo.IsNew = false;
                            _FinPeriodos.Add(finPeriodo);
                        }
                    }
                }
            }
            return _FinPeriodos;
        }

        public static double ObtenerCostoPorTarea(int n_idpro, int n_idtar)
        {
            double _Costo = 0;

            using (MySqlConnection connection
                = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = Properties.Resources.q_ObtenerCostoPorTarea;
                    command.Parameters.Add(new MySqlParameter("@n_idpro", n_idpro));
                    command.Parameters.Add(new MySqlParameter("@n_idtar", n_idtar));
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _Costo = reader.GetInt32("n_idfinperiodo");
                        }
                    }
                }
            }
            return _Costo;
        }
    }
}
