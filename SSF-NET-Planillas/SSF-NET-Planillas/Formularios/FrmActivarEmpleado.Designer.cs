namespace SSF_NET_Planillas.Formularios
{
    partial class FrmActivarEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmActivarEmpleado));
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.FgEmpleados = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptUnSelTod = new System.Windows.Forms.RadioButton();
            this.OptSelTod = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.CmdSalir = new System.Windows.Forms.Button();
            this.CmdOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgEmpleados)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.FgEmpleados);
            this.c1Sizer1.GridDefinition = "0.216450216450216:False:False;96.1038961038961:False:False;0.216450216450216:Fals" +
    "e:False;\t0.163934426229508:False:False;97.0491803278689:False:False;0.1639344262" +
    "29508:False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(3, 29);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(610, 462);
            this.c1Sizer1.TabIndex = 0;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // FgEmpleados
            // 
            this.FgEmpleados.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgEmpleados.Location = new System.Drawing.Point(9, 9);
            this.FgEmpleados.Name = "FgEmpleados";
            this.FgEmpleados.Rows.DefaultSize = 17;
            this.FgEmpleados.Size = new System.Drawing.Size(592, 444);
            this.FgEmpleados.TabIndex = 0;
            this.FgEmpleados.Click += new System.EventHandler(this.FgEmpleados_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "..::  ACTIVACION DE EMPLEADOS  ::..";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmdSalir);
            this.groupBox1.Controls.Add(this.CmdOk);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.OptUnSelTod);
            this.groupBox1.Controls.Add(this.OptSelTod);
            this.groupBox1.Location = new System.Drawing.Point(7, 487);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 62);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // OptUnSelTod
            // 
            this.OptUnSelTod.AutoSize = true;
            this.OptUnSelTod.Location = new System.Drawing.Point(136, 35);
            this.OptUnSelTod.Name = "OptUnSelTod";
            this.OptUnSelTod.Size = new System.Drawing.Size(117, 17);
            this.OptUnSelTod.TabIndex = 3;
            this.OptUnSelTod.TabStop = true;
            this.OptUnSelTod.Text = "Deseleccionar todo";
            this.OptUnSelTod.UseVisualStyleBackColor = true;
            this.OptUnSelTod.CheckedChanged += new System.EventHandler(this.OptUnSelTod_CheckedChanged);
            // 
            // OptSelTod
            // 
            this.OptSelTod.AutoSize = true;
            this.OptSelTod.Location = new System.Drawing.Point(16, 35);
            this.OptSelTod.Name = "OptSelTod";
            this.OptSelTod.Size = new System.Drawing.Size(109, 17);
            this.OptSelTod.TabIndex = 2;
            this.OptSelTod.TabStop = true;
            this.OptSelTod.Text = "Seleccionar Todo";
            this.OptSelTod.UseVisualStyleBackColor = true;
            this.OptSelTod.CheckedChanged += new System.EventHandler(this.OptSelTod_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(14, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "..:: Opciones ::..";
            // 
            // CmdSalir
            // 
            this.CmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("CmdSalir.Image")));
            this.CmdSalir.Location = new System.Drawing.Point(469, 17);
            this.CmdSalir.Name = "CmdSalir";
            this.CmdSalir.Size = new System.Drawing.Size(125, 39);
            this.CmdSalir.TabIndex = 8;
            this.CmdSalir.Text = "  &Cancelar";
            this.CmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdSalir.UseVisualStyleBackColor = true;
            this.CmdSalir.Click += new System.EventHandler(this.CmdSalir_Click);
            // 
            // CmdOk
            // 
            this.CmdOk.Image = ((System.Drawing.Image)(resources.GetObject("CmdOk.Image")));
            this.CmdOk.Location = new System.Drawing.Point(342, 17);
            this.CmdOk.Name = "CmdOk";
            this.CmdOk.Size = new System.Drawing.Size(125, 39);
            this.CmdOk.TabIndex = 7;
            this.CmdOk.Text = "   &Aceptar";
            this.CmdOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdOk.UseVisualStyleBackColor = true;
            this.CmdOk.Click += new System.EventHandler(this.CmdOk_Click);
            // 
            // FrmActivarEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 553);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.c1Sizer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmActivarEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmActivarEmpleado";
            this.Load += new System.EventHandler(this.FrmActivarEmpleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgEmpleados)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgEmpleados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton OptUnSelTod;
        private System.Windows.Forms.RadioButton OptSelTod;
        internal System.Windows.Forms.Button CmdSalir;
        internal System.Windows.Forms.Button CmdOk;
    }
}