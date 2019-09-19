namespace Helper.Formularios
{
    partial class FrmVerGrafico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerGrafico));
            this.Graph1 = new C1.Win.C1Chart.C1Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Graph1)).BeginInit();
            this.SuspendLayout();
            // 
            // Graph1
            // 
            this.Graph1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.Graph1.Location = new System.Drawing.Point(2, 4);
            this.Graph1.Name = "Graph1";
            this.Graph1.PropBag = resources.GetString("Graph1.PropBag");
            this.Graph1.Size = new System.Drawing.Size(851, 492);
            this.Graph1.TabIndex = 1;
            this.Graph1.Load += new System.EventHandler(this.Graph1_Load);
            // 
            // FrmVerGrafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 499);
            this.Controls.Add(this.Graph1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVerGrafico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HELPER - Motor Estadistico";
            this.Load += new System.EventHandler(this.FrmVerGrafico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Graph1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Chart.C1Chart Graph1;


    }
}