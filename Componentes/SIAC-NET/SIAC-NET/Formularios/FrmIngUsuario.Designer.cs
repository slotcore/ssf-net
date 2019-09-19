namespace SIAC_NET.Formularios
{
    partial class FrmIngUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIngUsuario));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.TxtPass = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.TxtUsuario = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.CmdAcepta = new DevComponents.DotNetBar.ButtonX();
            this.CmdCancela = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.White;
            this.PictureBox1.ForeColor = System.Drawing.Color.Black;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(6, 10);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(122, 132);
            this.PictureBox1.TabIndex = 8;
            this.PictureBox1.TabStop = false;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.White;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.TxtPass);
            this.groupPanel1.Controls.Add(this.TxtUsuario);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(135, 10);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(259, 87);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 9;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(24, 44);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(71, 21);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "Password";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(24, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(71, 21);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "Usuario";
            // 
            // TxtPass
            // 
            this.TxtPass.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.TxtPass.Border.Class = "TextBoxBorder";
            this.TxtPass.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TxtPass.DisabledBackColor = System.Drawing.Color.White;
            this.TxtPass.ForeColor = System.Drawing.Color.Black;
            this.TxtPass.Location = new System.Drawing.Point(106, 43);
            this.TxtPass.Name = "TxtPass";
            this.TxtPass.PreventEnterBeep = true;
            this.TxtPass.Size = new System.Drawing.Size(120, 22);
            this.TxtPass.TabIndex = 1;
            this.TxtPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPass_KeyPress);
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.TxtUsuario.Border.Class = "TextBoxBorder";
            this.TxtUsuario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TxtUsuario.DisabledBackColor = System.Drawing.Color.White;
            this.TxtUsuario.ForeColor = System.Drawing.Color.Black;
            this.TxtUsuario.Location = new System.Drawing.Point(106, 14);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.PreventEnterBeep = true;
            this.TxtUsuario.Size = new System.Drawing.Size(120, 22);
            this.TxtUsuario.TabIndex = 0;
            this.TxtUsuario.TextChanged += new System.EventHandler(this.TxtUsuario_TextChanged);
            this.TxtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUsuario_KeyPress);
            // 
            // CmdAcepta
            // 
            this.CmdAcepta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CmdAcepta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.CmdAcepta.Image = ((System.Drawing.Image)(resources.GetObject("CmdAcepta.Image")));
            this.CmdAcepta.Location = new System.Drawing.Point(135, 103);
            this.CmdAcepta.Name = "CmdAcepta";
            this.CmdAcepta.Size = new System.Drawing.Size(125, 39);
            this.CmdAcepta.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014;
            this.CmdAcepta.TabIndex = 2;
            this.CmdAcepta.Text = "Aceptar";
            this.CmdAcepta.Click += new System.EventHandler(this.CmdAcepta_Click);
            // 
            // CmdCancela
            // 
            this.CmdCancela.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CmdCancela.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.CmdCancela.Image = ((System.Drawing.Image)(resources.GetObject("CmdCancela.Image")));
            this.CmdCancela.Location = new System.Drawing.Point(269, 103);
            this.CmdCancela.Name = "CmdCancela";
            this.CmdCancela.Size = new System.Drawing.Size(125, 39);
            this.CmdCancela.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014;
            this.CmdCancela.TabIndex = 3;
            this.CmdCancela.Text = "Cancelar";
            this.CmdCancela.Click += new System.EventHandler(this.CmdCancela_Click);
            // 
            // FrmIngUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 150);
            this.Controls.Add(this.CmdCancela);
            this.Controls.Add(this.CmdAcepta);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.PictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmIngUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIAC Soft - Acceso Usuarios";
            this.Activated += new System.EventHandler(this.FrmIngUsuario_Activated);
            this.Load += new System.EventHandler(this.FrmIngUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX TxtPass;
        private DevComponents.DotNetBar.Controls.TextBoxX TxtUsuario;
        private DevComponents.DotNetBar.ButtonX CmdAcepta;
        private DevComponents.DotNetBar.ButtonX CmdCancela;

    }
}