namespace SSF_NET_Gestion.Formularios
{
    partial class FrmPlanVentasUni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlanVentasUni));
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolAbastecimiento = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.Tab1 = new C1.Win.C1Command.C1DockingTab();
            this.TabEmpTP1 = new C1.Win.C1Command.C1DockingTabPage();
            this.FgProEmp1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ToolHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tab1)).BeginInit();
            this.Tab1.SuspendLayout();
            this.TabEmpTP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgProEmp1)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.ForeColor = System.Drawing.Color.Black;
            this.ToolHerramientas.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolHerramientas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolAbastecimiento,
            this.toolStripSeparator2,
            this.ToolImprimir,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(1000, 39);
            this.ToolHerramientas.TabIndex = 40;
            this.ToolHerramientas.Text = "toolStrip1";
            // 
            // ToolAbastecimiento
            // 
            this.ToolAbastecimiento.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolAbastecimiento.Image = ((System.Drawing.Image)(resources.GetObject("ToolAbastecimiento.Image")));
            this.ToolAbastecimiento.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolAbastecimiento.Name = "ToolAbastecimiento";
            this.ToolAbastecimiento.Size = new System.Drawing.Size(36, 36);
            this.ToolAbastecimiento.Text = "toolStripButton1";
            this.ToolAbastecimiento.Visible = false;
            this.ToolAbastecimiento.Click += new System.EventHandler(this.ToolAbastecimiento_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolImprimir
            // 
            this.ToolImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolImprimir.Image")));
            this.ToolImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolImprimir.Name = "ToolImprimir";
            this.ToolImprimir.Size = new System.Drawing.Size(36, 36);
            this.ToolImprimir.Text = "toolStripButton6";
            this.ToolImprimir.ToolTipText = "Imprimir";
            // 
            // ToolSalir
            // 
            this.ToolSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolSalir.Image = ((System.Drawing.Image)(resources.GetObject("ToolSalir.Image")));
            this.ToolSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolSalir.Name = "ToolSalir";
            this.ToolSalir.Size = new System.Drawing.Size(36, 36);
            this.ToolSalir.Text = "toolStripButton8";
            this.ToolSalir.ToolTipText = "Salir";
            this.ToolSalir.Click += new System.EventHandler(this.ToolSalir_Click);
            // 
            // Tab1
            // 
            this.Tab1.Controls.Add(this.TabEmpTP1);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.Location = new System.Drawing.Point(3, 41);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 2;
            this.Tab1.Size = new System.Drawing.Size(969, 510);
            this.Tab1.TabAreaSpacing = 1;
            this.Tab1.TabIndex = 39;
            this.Tab1.TabsSpacing = -10;
            this.Tab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Sloping;
            this.Tab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // TabEmpTP1
            // 
            this.TabEmpTP1.BackColor = System.Drawing.SystemColors.Control;
            this.TabEmpTP1.Controls.Add(this.FgProEmp1);
            this.TabEmpTP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.TabEmpTP1.ForeColor = System.Drawing.Color.Black;
            this.TabEmpTP1.Location = new System.Drawing.Point(1, 23);
            this.TabEmpTP1.Name = "TabEmpTP1";
            this.TabEmpTP1.Size = new System.Drawing.Size(967, 486);
            this.TabEmpTP1.TabIndex = 0;
            this.TabEmpTP1.Text = "UNIFICADO";
            // 
            // FgProEmp1
            // 
            this.FgProEmp1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.FgProEmp1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgProEmp1.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgProEmp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FgProEmp1.ForeColor = System.Drawing.Color.Black;
            this.FgProEmp1.Location = new System.Drawing.Point(0, 0);
            this.FgProEmp1.Name = "FgProEmp1";
            this.FgProEmp1.Rows.DefaultSize = 17;
            this.FgProEmp1.Size = new System.Drawing.Size(967, 486);
            this.FgProEmp1.StyleInfo = resources.GetString("FgProEmp1.StyleInfo");
            this.FgProEmp1.TabIndex = 13;
            // 
            // FrmPlanVentasUni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 552);
            this.Controls.Add(this.ToolHerramientas);
            this.Controls.Add(this.Tab1);
            this.Name = "FrmPlanVentasUni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPlanVentasUni";
            this.Activated += new System.EventHandler(this.FrmPlanVentasUni_Activated);
            this.Load += new System.EventHandler(this.FrmPlanVentasUni_Load);
            this.Resize += new System.EventHandler(this.FrmPlanVentasUni_Resize);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tab1)).EndInit();
            this.Tab1.ResumeLayout(false);
            this.TabEmpTP1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgProEmp1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolAbastecimiento;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private C1.Win.C1Command.C1DockingTab Tab1;
        private C1.Win.C1Command.C1DockingTabPage TabEmpTP1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgProEmp1;
    }
}