using SIAC_Datos.Classes;
using SIAC_DATOS.Classes.Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmCostoProduccionError : Form
    {
        public FrmCostoProduccionError(ObservableListSource<CostoProduccionError> CostoProduccionErrors)
        {
            InitializeComponent();
            costoProduccionErrorBindingSource.DataSource = CostoProduccionErrors;
        }
    }
}
