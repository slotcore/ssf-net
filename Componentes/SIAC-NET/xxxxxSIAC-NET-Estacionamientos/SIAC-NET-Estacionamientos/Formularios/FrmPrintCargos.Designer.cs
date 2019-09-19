namespace SIAC_NET_Estacionamientos.Formularios
{
    partial class FrmPrintCargos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintCargos));
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtNumReg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FgReg = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TooNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolPdf = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgReg)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.Controls.Add(this.FgReg);
            this.c1Sizer1.GridDefinition = "91.0602910602911:False:False;7.9002079002079:False:False;\t99.5402298850575:False:" +
    "False;";
            this.c1Sizer1.Location = new System.Drawing.Point(2, 39);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Size = new System.Drawing.Size(870, 481);
            this.c1Sizer1.SplitterWidth = 1;
            this.c1Sizer1.TabIndex = 0;
            this.c1Sizer1.Text = "c1Sizer1";
            this.c1Sizer1.Click += new System.EventHandler(this.c1Sizer1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TxtFecha);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtNumReg);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 441);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 38);
            this.panel1.TabIndex = 5;
            // 
            // TxtFecha
            // 
            this.TxtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFecha.Location = new System.Drawing.Point(360, 10);
            this.TxtFecha.Name = "TxtFecha";
            this.TxtFecha.Size = new System.Drawing.Size(94, 20);
            this.TxtFecha.TabIndex = 6;
            this.TxtFecha.ValueChanged += new System.EventHandler(this.TxtFecha_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(306, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha";
            // 
            // TxtNumReg
            // 
            this.TxtNumReg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNumReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.TxtNumReg.Location = new System.Drawing.Point(103, 8);
            this.TxtNumReg.Name = "TxtNumReg";
            this.TxtNumReg.Size = new System.Drawing.Size(82, 23);
            this.TxtNumReg.TabIndex = 1;
            this.TxtNumReg.Text = "TxtNumReg";
            this.TxtNumReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº Registros";
            // 
            // FgReg
            // 
            this.FgReg.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgReg.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:12;}\t";
            this.FgReg.Location = new System.Drawing.Point(2, 2);
            this.FgReg.Name = "FgReg";
            this.FgReg.Rows.DefaultSize = 17;
            this.FgReg.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.FgReg.Size = new System.Drawing.Size(866, 438);
            this.FgReg.StyleInfo = resources.GetString("FgReg.StyleInfo");
            this.FgReg.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.ForeColor = System.Drawing.Color.Black;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TooNuevo,
            this.ToolPdf,
            this.toolStripSeparator2,
            this.ToolSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(874, 39);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "Exportar PDF";
            // 
            // TooNuevo
            // 
            this.TooNuevo.Image = ((System.Drawing.Image)(resources.GetObject("TooNuevo.Image")));
            this.TooNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TooNuevo.Name = "TooNuevo";
            this.TooNuevo.Size = new System.Drawing.Size(155, 36);
            this.TooNuevo.Text = "Imprimir Documento";
            this.TooNuevo.Click += new System.EventHandler(this.TooNuevo_Click);
            // 
            // ToolPdf
            // 
            this.ToolPdf.Image = ((System.Drawing.Image)(resources.GetObject("ToolPdf.Image")));
            this.ToolPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolPdf.Name = "ToolPdf";
            this.ToolPdf.Size = new System.Drawing.Size(110, 36);
            this.ToolPdf.Text = "Exportar PDF";
            this.ToolPdf.Click += new System.EventHandler(this.ToolPdf_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolSalir
            // 
            this.ToolSalir.Image = ((System.Drawing.Image)(resources.GetObject("ToolSalir.Image")));
            this.ToolSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolSalir.Name = "ToolSalir";
            this.ToolSalir.Size = new System.Drawing.Size(65, 36);
            this.ToolSalir.Text = "Salir";
            this.ToolSalir.ToolTipText = "Salir";
            this.ToolSalir.Click += new System.EventHandler(this.ToolSalir_Click);
            // 
            // FrmPrintCargos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 522);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.c1Sizer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrintCargos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPrintCargos";
            this.Load += new System.EventHandler(this.FrmPrintCargos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgReg)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TooNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private C1.Win.C1FlexGrid.C1FlexGrid FgReg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TxtNumReg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker TxtFecha;
        private System.Windows.Forms.ToolStripButton ToolPdf;
    }
}