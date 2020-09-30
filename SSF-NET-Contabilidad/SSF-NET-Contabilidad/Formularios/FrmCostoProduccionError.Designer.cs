namespace SSF_NET_Contabilidad.Formularios
{
    partial class FrmCostoProduccionError
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
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.BtnExportarExcel = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.costoProduccionErrorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.costoProduccionErrorKryptonDataGridView = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionErrorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionErrorKryptonDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox1.Location = new System.Drawing.Point(2, 1);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(925, 325);
            this.kryptonGroupBox1.StateNormal.Back.Color1 = System.Drawing.Color.Transparent;
            this.kryptonGroupBox1.TabIndex = 131;
            this.kryptonGroupBox1.Values.Heading = "Listado de Errores en Proceso de Costeo";
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.BtnExportarExcel});
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.AutoScroll = true;
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.costoProduccionErrorKryptonDataGridView);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(921, 301);
            this.kryptonHeaderGroup1.StateNormal.HeaderPrimary.Back.Color1 = System.Drawing.SystemColors.Control;
            this.kryptonHeaderGroup1.StateNormal.HeaderPrimary.Back.Color2 = System.Drawing.SystemColors.Control;
            this.kryptonHeaderGroup1.TabIndex = 132;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = null;
            // 
            // BtnExportarExcel
            // 
            this.BtnExportarExcel.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.BtnExportarExcel.Image = global::SSF_NET_Contabilidad.Properties.Resources.exporttoxls_16x16;
            this.BtnExportarExcel.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnExportarExcel.Text = "Exportar a Excel";
            this.BtnExportarExcel.UniqueName = "732F8FBA093C4C7A4FA518682797C959";
            // 
            // costoProduccionErrorBindingSource
            // 
            this.costoProduccionErrorBindingSource.DataSource = typeof(SIAC_DATOS.Classes.Contabilidad.CostoProduccionError);
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
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.costoProduccionErrorKryptonDataGridView.DataSource = this.costoProduccionErrorBindingSource;
            this.costoProduccionErrorKryptonDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.costoProduccionErrorKryptonDataGridView.Location = new System.Drawing.Point(0, 0);
            this.costoProduccionErrorKryptonDataGridView.Name = "costoProduccionErrorKryptonDataGridView";
            this.costoProduccionErrorKryptonDataGridView.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.costoProduccionErrorKryptonDataGridView.ReadOnly = true;
            this.costoProduccionErrorKryptonDataGridView.RowHeadersWidth = 15;
            this.costoProduccionErrorKryptonDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.costoProduccionErrorKryptonDataGridView.Size = new System.Drawing.Size(919, 270);
            this.costoProduccionErrorKryptonDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DesFechMov";
            this.dataGridViewTextBoxColumn5.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 67;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DesAlm";
            this.dataGridViewTextBoxColumn4.HeaderText = "Almacén";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 83;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "DesMov";
            this.dataGridViewTextBoxColumn6.HeaderText = "# Movimiento";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 111;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CodItem";
            this.dataGridViewTextBoxColumn1.HeaderText = "Cod. Item";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 88;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DesItem";
            this.dataGridViewTextBoxColumn2.HeaderText = "Item";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Error";
            this.dataGridViewTextBoxColumn3.HeaderText = "Error";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 61;
            // 
            // FrmCostoProduccionError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 329);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Name = "FrmCostoProduccionError";
            this.Text = "Costo de Produccion - Visor de Errores";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionErrorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costoProduccionErrorKryptonDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnExportarExcel;
        private System.Windows.Forms.BindingSource costoProduccionErrorBindingSource;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView costoProduccionErrorKryptonDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}