using SIAC_DATOS.Data;
using SIAC_Datos.Classes;
using SIAC_Datos.Models.Maestros;
using SIAC_Datos.Models.Planillas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmManEmpleadoPeriodo : Form
    {
        private PeriodoLaboral ViewModel = new PeriodoLaboral();
        private ObservableListSource<PeriodoLaboral> ViewModelList = new ObservableListSource<PeriodoLaboral>();

        public FrmManEmpleadoPeriodo(PeriodoLaboral viewModel
            , ObservableListSource<PeriodoLaboral> viewModelList)
        {
            InitializeComponent();
            ViewModel.CopyOf(viewModel);
            ViewModelList = viewModelList;
            //
            categoriaBindingSource.DataSource = ApplicationDbContext.Categorias();
            finPeriodoBindingSource.DataSource = ApplicationDbContext.FinPeriodos();
            periodoLaboralBindingSource.DataSource = ViewModel;
        }

        public FrmManEmpleadoPeriodo(ObservableListSource<PeriodoLaboral> viewModelList)
        {
            InitializeComponent();
            ViewModel = new PeriodoLaboral();
            ViewModelList = viewModelList;
            //
            categoriaBindingSource.DataSource = ApplicationDbContext.Categorias();
            finPeriodoBindingSource.DataSource = ApplicationDbContext.FinPeriodos();
            periodoLaboralBindingSource.DataSource = ViewModel;
        }

        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            periodoLaboralBindingSource.CancelEdit();
            this.Close();

        }

        private void Grabar()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                periodoLaboralBindingSource.EndEdit();

                var categoria = (Categoria)categoriaBindingSource.Current;
                var finPeriodo = (FinPeriodo)finPeriodoBindingSource.Current;

                if (categoria == null) throw new Exception("Debe de seleccionar una categoría");
                if (finPeriodo == null) throw new Exception("Debe se seleccionar un tipo de extinción de contrato");

                ViewModel.n_idcategoria = categoria.n_idcategoria;
                ViewModel.categoria = categoria.c_descripcion;

                ViewModel.n_idfinperiodo = finPeriodo.n_idfinperiodo;
                ViewModel.finperiodo = finPeriodo.c_descripcion;

                if (ViewModel.IsNew)
                {
                    ViewModel.n_corr = ViewModelList.Count + 1;
                    ViewModelList.Add(ViewModel);
                }
                else
                {
                    var viewModel = ViewModelList
                        .Where(o => o.n_idempleado == ViewModel.n_idempleado)
                        .FirstOrDefault();
                    viewModel.CopyOf(ViewModel);
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al grabar, mensaje de error: {0}", ex.Message)
                    , "Grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
