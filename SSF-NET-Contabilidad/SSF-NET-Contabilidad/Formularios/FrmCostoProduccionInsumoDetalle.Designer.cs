namespace SSF_NET_Contabilidad.Formularios
{
    partial class FrmCostoProduccionInsumoDetalle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.costoProduccionInsumoDetalleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.costoProduccionErrorKryptonDataGridView = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostoUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnVerDetalle = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionInsumoDetalleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionErrorKryptonDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // costoProduccionInsumoDetalleBindingSource
            // 
            this.costoProduccionInsumoDetalleBindingSource.DataSource = typeof(SIAC_DATOS.Classes.Contabilidad.CostoProduccionInsumoDetalle);
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.BtnVerDetalle});
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.costoProduccionErrorKryptonDataGridView);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(1237, 405);
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Detalle de Insumos";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = null;
            // 
            // costoProduccionErrorKryptonDataGridView
            // 
            this.costoProduccionErrorKryptonDataGridView.AllowUserToAddRows = false;
            this.costoProduccionErrorKryptonDataGridView.AllowUserToDeleteRows = false;
            this.costoProduccionErrorKryptonDataGridView.AllowUserToOrderColumns = true;
            this.costoProduccionErrorKryptonDataGridView.AutoGenerateColumns = false;
            this.costoProduccionErrorKryptonDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.costoProduccionErrorKryptonDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.costoProduccionErrorKryptonDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.costoProduccionErrorKryptonDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Unidad,
            this.Cantidad,
            this.CostoUnitario,
            this.CostoTotal});
            this.costoProduccionErrorKryptonDataGridView.DataSource = this.costoProduccionInsumoDetalleBindingSource;
            this.costoProduccionErrorKryptonDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.Location = new System.Drawing.Point(0, 0);
            this.costoProduccionErrorKryptonDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.costoProduccionErrorKryptonDataGridView.Name = "costoProduccionErrorKryptonDataGridView";
            this.costoProduccionErrorKryptonDataGridView.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.costoProduccionErrorKryptonDataGridView.ReadOnly = true;
            this.costoProduccionErrorKryptonDataGridView.RowHeadersWidth = 15;
            this.costoProduccionErrorKryptonDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.costoProduccionErrorKryptonDataGridView.Size = new System.Drawing.Size(1235, 370);
            this.costoProduccionErrorKryptonDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CodItem";
            this.dataGridViewTextBoxColumn1.HeaderText = "Cod. Item";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 106;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DesItem";
            this.dataGridViewTextBoxColumn2.HeaderText = "Item";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 72;
            // 
            // Unidad
            // 
            this.Unidad.DataPropertyName = "Unidad";
            this.Unidad.HeaderText = "Unidad";
            this.Unidad.MinimumWidth = 6;
            this.Unidad.Name = "Unidad";
            this.Unidad.ReadOnly = true;
            this.Unidad.Width = 90;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            dataGridViewCellStyle4.Format = "N3";
            dataGridViewCellStyle4.NullValue = null;
            this.Cantidad.DefaultCellStyle = dataGridViewCellStyle4;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 102;
            // 
            // CostoUnitario
            // 
            this.CostoUnitario.DataPropertyName = "CostoUnitario";
            dataGridViewCellStyle5.Format = "N3";
            dataGridViewCellStyle5.NullValue = null;
            this.CostoUnitario.DefaultCellStyle = dataGridViewCellStyle5;
            this.CostoUnitario.HeaderText = "CostoUnitario";
            this.CostoUnitario.MinimumWidth = 6;
            this.CostoUnitario.Name = "CostoUnitario";
            this.CostoUnitario.ReadOnly = true;
            this.CostoUnitario.Width = 133;
            // 
            // CostoTotal
            // 
            this.CostoTotal.DataPropertyName = "CostoTotal";
            dataGridViewCellStyle6.Format = "N3";
            dataGridViewCellStyle6.NullValue = null;
            this.CostoTotal.DefaultCellStyle = dataGridViewCellStyle6;
            this.CostoTotal.HeaderText = "CostoTotal";
            this.CostoTotal.MinimumWidth = 6;
            this.CostoTotal.Name = "CostoTotal";
            this.CostoTotal.ReadOnly = true;
            this.CostoTotal.Width = 113;
            // 
            // BtnVerDetalle
            // 
            this.BtnVerDetalle.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnVerDetalle.Text = "Ver Detalle";
            this.BtnVerDetalle.UniqueName = "3EA1B0094D774566D08B4F4ACBC0A71A";
            this.BtnVerDetalle.Click += new System.EventHandler(this.BtnVerDetalle_Click);
            // 
            // FrmCostoProduccionInsumoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 405);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCostoProduccionInsumoDetalle";
            this.Text = "Costo de Produccion - Visor de Errores";
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionInsumoDetalleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionErrorKryptonDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource costoProduccionInsumoDetalleBindingSource;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnVerDetalle;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView costoProduccionErrorKryptonDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostoUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostoTotal;
    }
}