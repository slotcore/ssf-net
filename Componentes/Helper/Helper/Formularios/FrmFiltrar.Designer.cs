namespace Helper.Formularios
{
    partial class FrmFiltrar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFiltrar));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.ChkAll = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LblOrden = new System.Windows.Forms.Label();
            this.CmdEsc = new System.Windows.Forms.Button();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Button2 = new System.Windows.Forms.Button();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.FgFiltro = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.FgFix = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.C1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgFiltro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgFix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer2)).BeginInit();
            this.C1Sizer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.ChkAll);
            this.Panel2.Controls.Add(this.label1);
            this.Panel2.Controls.Add(this.LblOrden);
            this.Panel2.Controls.Add(this.CmdEsc);
            this.Panel2.Controls.Add(this.LblNumReg);
            this.Panel2.Controls.Add(this.Label18);
            this.Panel2.Location = new System.Drawing.Point(2, 2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(584, 42);
            this.Panel2.TabIndex = 1;
            // 
            // ChkAll
            // 
            this.ChkAll.AutoSize = true;
            this.ChkAll.Location = new System.Drawing.Point(232, 4);
            this.ChkAll.Name = "ChkAll";
            this.ChkAll.Size = new System.Drawing.Size(72, 17);
            this.ChkAll.TabIndex = 45;
            this.ChkAll.Text = "Sel. Todo";
            this.ChkAll.UseVisualStyleBackColor = true;
            this.ChkAll.CheckedChanged += new System.EventHandler(this.ChkAll_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Ordenado Por :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblOrden
            // 
            this.LblOrden.AutoSize = true;
            this.LblOrden.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOrden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblOrden.Location = new System.Drawing.Point(107, 22);
            this.LblOrden.Name = "LblOrden";
            this.LblOrden.Size = new System.Drawing.Size(65, 13);
            this.LblOrden.TabIndex = 43;
            this.LblOrden.Text = "LblOrden";
            this.LblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmdEsc
            // 
            this.CmdEsc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CmdEsc.Location = new System.Drawing.Point(412, 7);
            this.CmdEsc.Name = "CmdEsc";
            this.CmdEsc.Size = new System.Drawing.Size(40, 22);
            this.CmdEsc.TabIndex = 42;
            this.CmdEsc.Text = "ESC";
            this.CmdEsc.UseVisualStyleBackColor = true;
            // 
            // LblNumReg
            // 
            this.LblNumReg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblNumReg.Location = new System.Drawing.Point(107, 3);
            this.LblNumReg.Name = "LblNumReg";
            this.LblNumReg.Size = new System.Drawing.Size(111, 15);
            this.LblNumReg.TabIndex = 41;
            this.LblNumReg.Text = "Label1";
            this.LblNumReg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(7, 3);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(95, 13);
            this.Label18.TabIndex = 40;
            this.Label18.Text = "Nº Registros   :";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label18.Click += new System.EventHandler(this.Label18_Click);
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Button2);
            this.Panel1.Controls.Add(this.CmdAceptar);
            this.Panel1.Location = new System.Drawing.Point(587, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(251, 42);
            this.Panel1.TabIndex = 0;
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Image = ((System.Drawing.Image)(resources.GetObject("Button2.Image")));
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button2.Location = new System.Drawing.Point(127, 0);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(111, 39);
            this.Button2.TabIndex = 3;
            this.Button2.Text = "Cancelar  ";
            this.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdAceptar.Image = ((System.Drawing.Image)(resources.GetObject("CmdAceptar.Image")));
            this.CmdAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdAceptar.Location = new System.Drawing.Point(13, 0);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.Size = new System.Drawing.Size(111, 39);
            this.CmdAceptar.TabIndex = 2;
            this.CmdAceptar.Text = "Aceptar  ";
            this.CmdAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdAceptar.UseVisualStyleBackColor = true;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // FgFiltro
            // 
            this.FgFiltro.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgFiltro.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:11;}\t1{AllowFiltering:ByCondition;}\t2{AllowFilterin" +
    "g:ByCondition;}\t3{AllowFiltering:ByCondition;}\t4{AllowFiltering:ByCondition;}\t5{" +
    "AllowFiltering:ByCondition;}\t";
            this.FgFiltro.Location = new System.Drawing.Point(1, 62);
            this.FgFiltro.Name = "FgFiltro";
            this.FgFiltro.Rows.DefaultSize = 17;
            this.FgFiltro.Size = new System.Drawing.Size(840, 368);
            this.FgFiltro.TabIndex = 1;
            this.FgFiltro.EnterCell += new System.EventHandler(this.FgFiltro_EnterCell);
            this.FgFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FgFiltro_KeyUp);
            // 
            // FgFix
            // 
            this.FgFix.AllowFiltering = true;
            this.FgFix.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgFix.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:11;}\t1{AllowFiltering:ByCondition;}\t2{AllowFilterin" +
    "g:ByCondition;}\t3{AllowFiltering:ByCondition;}\t4{AllowFiltering:ByCondition;}\t5{" +
    "AllowFiltering:ByCondition;}\t";
            this.FgFix.Location = new System.Drawing.Point(1, 1);
            this.FgFix.Name = "FgFix";
            this.FgFix.Rows.DefaultSize = 17;
            this.FgFix.Size = new System.Drawing.Size(840, 60);
            this.FgFix.TabIndex = 0;
            this.FgFix.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgFix_KeyPressEdit);
            this.FgFix.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgFix_CellChanged);
            this.FgFix.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FgFix_KeyUp);
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.C1Sizer2);
            this.c1Sizer1.Controls.Add(this.FgFiltro);
            this.c1Sizer1.Controls.Add(this.FgFix);
            this.c1Sizer1.GridDefinition = "12.5523012552301:False:True;76.9874476987448:False:False;9.6234309623431:False:Tr" +
    "ue;\t99.7624703087886:False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(0, 1);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Size = new System.Drawing.Size(842, 478);
            this.c1Sizer1.SplitterWidth = 1;
            this.c1Sizer1.TabIndex = 12;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // C1Sizer2
            // 
            this.C1Sizer2.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.C1Sizer2.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.C1Sizer2.Controls.Add(this.Panel1);
            this.C1Sizer2.Controls.Add(this.Panel2);
            this.C1Sizer2.GridDefinition = "91.304347826087:False:False;\t69.5238095238095:False:False;29.8809523809524:False:" +
    "True;";
            this.C1Sizer2.Location = new System.Drawing.Point(1, 431);
            this.C1Sizer2.Margin = new System.Windows.Forms.Padding(1);
            this.C1Sizer2.Name = "C1Sizer2";
            this.C1Sizer2.Padding = new System.Windows.Forms.Padding(1);
            this.C1Sizer2.Size = new System.Drawing.Size(840, 46);
            this.C1Sizer2.SplitterWidth = 1;
            this.C1Sizer2.TabIndex = 12;
            this.C1Sizer2.Text = "C1Sizer2";
            // 
            // FrmFiltrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(843, 479);
            this.Controls.Add(this.c1Sizer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFiltrar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmFiltrar";
            this.Load += new System.EventHandler(this.FrmFiltrar_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgFiltro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgFix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer2)).EndInit();
            this.C1Sizer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label LblOrden;
        internal System.Windows.Forms.Button CmdEsc;
        internal System.Windows.Forms.Label LblNumReg;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button CmdAceptar;
        private C1.Win.C1FlexGrid.C1FlexGrid FgFiltro;
        private C1.Win.C1FlexGrid.C1FlexGrid FgFix;
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        internal C1.Win.C1Sizer.C1Sizer C1Sizer2;
        private System.Windows.Forms.CheckBox ChkAll;

    }
}