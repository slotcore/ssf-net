using SIAC_Datos.Classes;
using SIAC_DATOS.Classes.Contabilidad;
using SIAC_DATOS.Models.Contabilidad;
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
    public partial class FrmCostoProduccionInsumoDetalle : Form
    {
        public FrmCostoProduccionInsumoDetalle(List<CostoProduccionInsumoDetalle> CostoProduccionInsumoDetalles)
        {
            InitializeComponent();
            costoProduccionInsumoDetalleBindingSource.DataSource = CostoProduccionInsumoDetalles;
        }

        private void BtnExportarExcel_Click(object sender, EventArgs e)
        {

        }

        private void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                CostoProduccionInsumoDetalle costoProduccionInsumoDetalle = (CostoProduccionInsumoDetalle)costoProduccionInsumoDetalleBindingSource.Current;

                var costoProduccionInsumoDetalles = CostoProduccion.ObtieneCostoDetalleInsumo(costoProduccionInsumoDetalle.n_idemp,
                    costoProduccionInsumoDetalle.n_idite,
                    costoProduccionInsumoDetalle.Cantidad,
                    costoProduccionInsumoDetalle.FechMov);

                if (costoProduccionInsumoDetalles.Count > 0)
                {
                    using (FrmCostoProduccionInsumoDetalle xForm = new FrmCostoProduccionInsumoDetalle(costoProduccionInsumoDetalles))
                    {
                        xForm.ShowDialog(this);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles en el insumo"
                        , "Costo Insumo Detalle"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrio un error al ver detalle, error: {0}", ex.Message)
                    , "Procesar MP"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
        }
    }
}
