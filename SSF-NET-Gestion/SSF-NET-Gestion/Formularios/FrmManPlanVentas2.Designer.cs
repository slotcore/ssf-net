﻿using System;

namespace SSF_NET_Gestion.Formularios
{
    partial class FrmManPlanVentas2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManPlanVentas2));
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripSplitButton();
            this.modificarRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolEliminar2 = new System.Windows.Forms.ToolStripSplitButton();
            this.eliminarRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.desactivarPlanVentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.Tab1 = new System.Windows.Forms.TabControl();
            this.c1DockingTabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LblNumRegistro = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CmdDelTod = new System.Windows.Forms.Button();
            this.CmdDelItem = new System.Windows.Forms.Button();
            this.CmdAddItem = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmdRetItem = new System.Windows.Forms.Button();
            this.CmdAllItem = new System.Windows.Forms.Button();
            this.Cmd_VerGra = new System.Windows.Forms.Button();
            this.CboMesIni = new System.Windows.Forms.ComboBox();
            this.CmdAddValores = new System.Windows.Forms.Button();
            this.FgHisAno = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtFchCre = new System.Windows.Forms.DateTimePicker();
            this.TxtDes = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblTitulo2 = new System.Windows.Forms.Label();
            this.ToolHerramientas.SuspendLayout();
            this.Tab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgHisAno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolHerramientas.ForeColor = System.Drawing.Color.Black;
            this.ToolHerramientas.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolHerramientas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolNuevo,
            this.ToolModificar,
            this.ToolEliminar2,
            this.ToolEliminar,
            this.toolStripSeparator1,
            this.ToolGrabar,
            this.ToolCancelar,
            this.toolStripSeparator2,
            this.ToolExportar,
            this.ToolImprimir,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(1293, 39);
            this.ToolHerramientas.TabIndex = 17;
            this.ToolHerramientas.Text = "toolStrip1";
            this.ToolHerramientas.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ToolHerramientas_ItemClicked);
            // 
            // ToolNuevo
            // 
            this.ToolNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolNuevo.Image = ((System.Drawing.Image)(resources.GetObject("ToolNuevo.Image")));
            this.ToolNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolNuevo.Name = "ToolNuevo";
            this.ToolNuevo.Size = new System.Drawing.Size(36, 36);
            this.ToolNuevo.Text = "toolStripButton1";
            this.ToolNuevo.ToolTipText = "Agregar registro";
            this.ToolNuevo.Click += new System.EventHandler(this.ToolNuevo_Click);
            // 
            // ToolModificar
            // 
            this.ToolModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolModificar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarRegistroToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem2});
            this.ToolModificar.Image = global::SSF_NET_Gestion.Properties.Resources.editar32x32;
            this.ToolModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolModificar.Name = "ToolModificar";
            this.ToolModificar.Size = new System.Drawing.Size(51, 36);
            this.ToolModificar.Text = "toolStripSplitButton1";
            // 
            // modificarRegistroToolStripMenuItem
            // 
            this.modificarRegistroToolStripMenuItem.Name = "modificarRegistroToolStripMenuItem";
            this.modificarRegistroToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.modificarRegistroToolStripMenuItem.Text = "Modificar Registro";
            this.modificarRegistroToolStripMenuItem.Click += new System.EventHandler(this.modificarRegistroToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(235, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(238, 26);
            this.toolStripMenuItem2.Text = "Activar Plan de Ventas";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // ToolEliminar2
            // 
            this.ToolEliminar2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolEliminar2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarRegistroToolStripMenuItem,
            this.toolStripMenuItem1,
            this.desactivarPlanVentasToolStripMenuItem});
            this.ToolEliminar2.Image = ((System.Drawing.Image)(resources.GetObject("ToolEliminar2.Image")));
            this.ToolEliminar2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolEliminar2.Name = "ToolEliminar2";
            this.ToolEliminar2.Size = new System.Drawing.Size(51, 36);
            this.ToolEliminar2.Text = "toolStripSplitButton1";
            this.ToolEliminar2.ToolTipText = "Eliminar registro";
            // 
            // eliminarRegistroToolStripMenuItem
            // 
            this.eliminarRegistroToolStripMenuItem.Name = "eliminarRegistroToolStripMenuItem";
            this.eliminarRegistroToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.eliminarRegistroToolStripMenuItem.Text = "Eliminar Registro";
            this.eliminarRegistroToolStripMenuItem.Click += new System.EventHandler(this.eliminarRegistroToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(237, 6);
            // 
            // desactivarPlanVentasToolStripMenuItem
            // 
            this.desactivarPlanVentasToolStripMenuItem.Name = "desactivarPlanVentasToolStripMenuItem";
            this.desactivarPlanVentasToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.desactivarPlanVentasToolStripMenuItem.Text = "Desactivar Plan Ventas";
            this.desactivarPlanVentasToolStripMenuItem.Click += new System.EventHandler(this.desactivarPlanVentasToolStripMenuItem_Click);
            // 
            // ToolEliminar
            // 
            this.ToolEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolEliminar.Image = ((System.Drawing.Image)(resources.GetObject("ToolEliminar.Image")));
            this.ToolEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolEliminar.Name = "ToolEliminar";
            this.ToolEliminar.Size = new System.Drawing.Size(36, 36);
            this.ToolEliminar.Text = "toolStripButton3";
            this.ToolEliminar.ToolTipText = "Eliminar registro";
            this.ToolEliminar.Visible = false;
            this.ToolEliminar.Click += new System.EventHandler(this.ToolEliminar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolGrabar
            // 
            this.ToolGrabar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolGrabar.Enabled = false;
            this.ToolGrabar.Image = ((System.Drawing.Image)(resources.GetObject("ToolGrabar.Image")));
            this.ToolGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolGrabar.Name = "ToolGrabar";
            this.ToolGrabar.Size = new System.Drawing.Size(36, 36);
            this.ToolGrabar.Text = "toolStripButton4";
            this.ToolGrabar.ToolTipText = "Grabar registro";
            this.ToolGrabar.Click += new System.EventHandler(this.ToolGrabar_Click);
            // 
            // ToolCancelar
            // 
            this.ToolCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolCancelar.Enabled = false;
            this.ToolCancelar.Image = ((System.Drawing.Image)(resources.GetObject("ToolCancelar.Image")));
            this.ToolCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolCancelar.Name = "ToolCancelar";
            this.ToolCancelar.Size = new System.Drawing.Size(36, 36);
            this.ToolCancelar.Text = "toolStripButton5";
            this.ToolCancelar.ToolTipText = "Cancelar";
            this.ToolCancelar.Click += new System.EventHandler(this.ToolCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolExportar
            // 
            this.ToolExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolExportar.Image = ((System.Drawing.Image)(resources.GetObject("ToolExportar.Image")));
            this.ToolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExportar.Name = "ToolExportar";
            this.ToolExportar.Size = new System.Drawing.Size(36, 36);
            this.ToolExportar.Text = "toolStripButton1";
            this.ToolExportar.ToolTipText = "Exportra Excel";
            this.ToolExportar.Visible = false;
            this.ToolExportar.Click += new System.EventHandler(this.ToolExportar_Click);
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
            this.ToolImprimir.Visible = false;
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
            this.Tab1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Tab1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.ForeColor = System.Drawing.Color.Black;
            this.Tab1.Location = new System.Drawing.Point(3, 54);
            this.Tab1.Margin = new System.Windows.Forms.Padding(4);
            this.Tab1.Multiline = true;
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(1289, 623);
            this.Tab1.TabIndex = 16;
            this.Tab1.SelectedIndexChanged += new System.EventHandler(this.Tab1_SelectedIndexChanged);
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.BackColor = System.Drawing.Color.White;
            this.c1DockingTabPage1.Controls.Add(this.panel4);
            this.c1DockingTabPage1.Controls.Add(this.panel2);
            this.c1DockingTabPage1.Controls.Add(this.DgLista);
            this.c1DockingTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage1.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage1.Location = new System.Drawing.Point(32, 4);
            this.c1DockingTabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(1253, 615);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Consulta";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.CboMeses);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(5, 574);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1245, 36);
            this.panel4.TabIndex = 2;
            // 
            // CboMeses
            // 
            this.CboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMeses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(881, 4);
            this.CboMeses.Margin = new System.Windows.Forms.Padding(4);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(189, 28);
            this.CboMeses.TabIndex = 3;
            this.CboMeses.Visible = false;
            this.CboMeses.SelectedValueChanged += new System.EventHandler(this.CboMeses_SelectedValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(737, 10);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Mes trabajo :";
            this.label11.Visible = false;
            // 
            // LblNumReg
            // 
            this.LblNumReg.AutoSize = true;
            this.LblNumReg.BackColor = System.Drawing.Color.White;
            this.LblNumReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumReg.ForeColor = System.Drawing.Color.Black;
            this.LblNumReg.Location = new System.Drawing.Point(187, 9);
            this.LblNumReg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNumReg.Name = "LblNumReg";
            this.LblNumReg.Size = new System.Drawing.Size(91, 17);
            this.LblNumReg.TabIndex = 1;
            this.LblNumReg.Text = "LblNumReg";
            this.LblNumReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(12, 9);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(109, 17);
            this.label18.TabIndex = 0;
            this.label18.Text = "Nº Registros :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel2.Controls.Add(this.label17);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1245, 28);
            this.panel2.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1245, 28);
            this.label17.TabIndex = 0;
            this.label17.Text = "CONSULTA";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DgLista
            // 
            this.DgLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgLista.BackColor = System.Drawing.Color.White;
            this.DgLista.CaptionHeight = 19;
            this.DgLista.ForeColor = System.Drawing.Color.Black;
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(5, 37);
            this.DgLista.Margin = new System.Windows.Forms.Padding(4);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.RowHeight = 17;
            this.DgLista.Size = new System.Drawing.Size(1245, 533);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel5);
            this.c1DockingTabPage2.Controls.Add(this.panel1);
            this.c1DockingTabPage2.Controls.Add(this.FgItems);
            this.c1DockingTabPage2.Controls.Add(this.panel3);
            this.c1DockingTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage2.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage2.Location = new System.Drawing.Point(32, 4);
            this.c1DockingTabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(1253, 615);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.LblNumRegistro);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.CmdDelTod);
            this.panel5.Controls.Add(this.CmdDelItem);
            this.panel5.Controls.Add(this.CmdAddItem);
            this.panel5.Location = new System.Drawing.Point(1, 578);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1249, 37);
            this.panel5.TabIndex = 14;
            // 
            // LblNumRegistro
            // 
            this.LblNumRegistro.BackColor = System.Drawing.Color.Transparent;
            this.LblNumRegistro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblNumRegistro.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumRegistro.ForeColor = System.Drawing.Color.Navy;
            this.LblNumRegistro.Location = new System.Drawing.Point(1113, 7);
            this.LblNumRegistro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNumRegistro.Name = "LblNumRegistro";
            this.LblNumRegistro.Size = new System.Drawing.Size(118, 24);
            this.LblNumRegistro.TabIndex = 47;
            this.LblNumRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(991, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 46;
            this.label4.Text = "Nº Productos";
            // 
            // CmdDelTod
            // 
            this.CmdDelTod.BackColor = System.Drawing.Color.White;
            this.CmdDelTod.Enabled = false;
            this.CmdDelTod.ForeColor = System.Drawing.Color.Black;
            this.CmdDelTod.Location = new System.Drawing.Point(252, 2);
            this.CmdDelTod.Margin = new System.Windows.Forms.Padding(4);
            this.CmdDelTod.Name = "CmdDelTod";
            this.CmdDelTod.Size = new System.Drawing.Size(116, 34);
            this.CmdDelTod.TabIndex = 16;
            this.CmdDelTod.Text = "Eliminar Todo";
            this.CmdDelTod.UseVisualStyleBackColor = false;
            this.CmdDelTod.Click += new System.EventHandler(this.CmdDelTod_Click);
            // 
            // CmdDelItem
            // 
            this.CmdDelItem.BackColor = System.Drawing.Color.White;
            this.CmdDelItem.Enabled = false;
            this.CmdDelItem.ForeColor = System.Drawing.Color.Black;
            this.CmdDelItem.Location = new System.Drawing.Point(128, 2);
            this.CmdDelItem.Margin = new System.Windows.Forms.Padding(4);
            this.CmdDelItem.Name = "CmdDelItem";
            this.CmdDelItem.Size = new System.Drawing.Size(116, 34);
            this.CmdDelItem.TabIndex = 15;
            this.CmdDelItem.Text = "Eliminar Item";
            this.CmdDelItem.UseVisualStyleBackColor = false;
            this.CmdDelItem.Click += new System.EventHandler(this.CmdDelItem_Click);
            // 
            // CmdAddItem
            // 
            this.CmdAddItem.BackColor = System.Drawing.Color.White;
            this.CmdAddItem.Enabled = false;
            this.CmdAddItem.ForeColor = System.Drawing.Color.Black;
            this.CmdAddItem.Location = new System.Drawing.Point(9, 2);
            this.CmdAddItem.Margin = new System.Windows.Forms.Padding(4);
            this.CmdAddItem.Name = "CmdAddItem";
            this.CmdAddItem.Size = new System.Drawing.Size(116, 34);
            this.CmdAddItem.TabIndex = 14;
            this.CmdAddItem.Text = "Agregar Items";
            this.CmdAddItem.UseVisualStyleBackColor = false;
            this.CmdAddItem.Click += new System.EventHandler(this.CmdAddItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.CmdRetItem);
            this.panel1.Controls.Add(this.CmdAllItem);
            this.panel1.Controls.Add(this.Cmd_VerGra);
            this.panel1.Controls.Add(this.CboMesIni);
            this.panel1.Controls.Add(this.CmdAddValores);
            this.panel1.Controls.Add(this.FgHisAno);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtFchCre);
            this.panel1.Controls.Add(this.TxtDes);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtFchFin);
            this.panel1.Controls.Add(this.TxtFchIni);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1249, 100);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // CmdRetItem
            // 
            this.CmdRetItem.BackColor = System.Drawing.Color.White;
            this.CmdRetItem.Enabled = false;
            this.CmdRetItem.ForeColor = System.Drawing.Color.Black;
            this.CmdRetItem.Location = new System.Drawing.Point(1071, 5);
            this.CmdRetItem.Margin = new System.Windows.Forms.Padding(4);
            this.CmdRetItem.Name = "CmdRetItem";
            this.CmdRetItem.Size = new System.Drawing.Size(175, 38);
            this.CmdRetItem.TabIndex = 45;
            this.CmdRetItem.Text = "Ver Historico";
            this.CmdRetItem.UseVisualStyleBackColor = false;
            this.CmdRetItem.Click += new System.EventHandler(this.CmdRetItem_Click);
            // 
            // CmdAllItem
            // 
            this.CmdAllItem.BackColor = System.Drawing.Color.White;
            this.CmdAllItem.Enabled = false;
            this.CmdAllItem.ForeColor = System.Drawing.Color.Black;
            this.CmdAllItem.Location = new System.Drawing.Point(1072, 43);
            this.CmdAllItem.Margin = new System.Windows.Forms.Padding(4);
            this.CmdAllItem.Name = "CmdAllItem";
            this.CmdAllItem.Size = new System.Drawing.Size(175, 48);
            this.CmdAllItem.TabIndex = 44;
            this.CmdAllItem.Text = "Eliminar Items sin Movimiento";
            this.CmdAllItem.UseVisualStyleBackColor = false;
            this.CmdAllItem.Visible = false;
            this.CmdAllItem.Click += new System.EventHandler(this.CmdAllItem_Click);
            // 
            // Cmd_VerGra
            // 
            this.Cmd_VerGra.Enabled = false;
            this.Cmd_VerGra.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_VerGra.Image")));
            this.Cmd_VerGra.Location = new System.Drawing.Point(965, 5);
            this.Cmd_VerGra.Margin = new System.Windows.Forms.Padding(4);
            this.Cmd_VerGra.Name = "Cmd_VerGra";
            this.Cmd_VerGra.Size = new System.Drawing.Size(104, 86);
            this.Cmd_VerGra.TabIndex = 43;
            this.Cmd_VerGra.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cmd_VerGra.UseVisualStyleBackColor = true;
            this.Cmd_VerGra.Click += new System.EventHandler(this.Cmd_VerGra_Click);
            this.Cmd_VerGra.MouseHover += new System.EventHandler(this.Cmd_VerGra_MouseHover);
            // 
            // CboMesIni
            // 
            this.CboMesIni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesIni.FormattingEnabled = true;
            this.CboMesIni.Location = new System.Drawing.Point(120, 59);
            this.CboMesIni.Margin = new System.Windows.Forms.Padding(4);
            this.CboMesIni.Name = "CboMesIni";
            this.CboMesIni.Size = new System.Drawing.Size(199, 25);
            this.CboMesIni.TabIndex = 42;
            this.CboMesIni.SelectedIndexChanged += new System.EventHandler(this.CboMesIni_SelectedIndexChanged);
            // 
            // CmdAddValores
            // 
            this.CmdAddValores.Enabled = false;
            this.CmdAddValores.Image = ((System.Drawing.Image)(resources.GetObject("CmdAddValores.Image")));
            this.CmdAddValores.Location = new System.Drawing.Point(860, 5);
            this.CmdAddValores.Margin = new System.Windows.Forms.Padding(4);
            this.CmdAddValores.Name = "CmdAddValores";
            this.CmdAddValores.Size = new System.Drawing.Size(104, 86);
            this.CmdAddValores.TabIndex = 41;
            this.CmdAddValores.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CmdAddValores.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CmdAddValores.UseVisualStyleBackColor = true;
            this.CmdAddValores.Click += new System.EventHandler(this.CmdAddValores_Click);
            this.CmdAddValores.MouseHover += new System.EventHandler(this.CmdAddValores_MouseHover);
            // 
            // FgHisAno
            // 
            this.FgHisAno.BackColor = System.Drawing.Color.White;
            this.FgHisAno.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgHisAno.ColumnInfo = resources.GetString("FgHisAno.ColumnInfo");
            this.FgHisAno.ForeColor = System.Drawing.Color.Black;
            this.FgHisAno.Location = new System.Drawing.Point(711, 5);
            this.FgHisAno.Margin = new System.Windows.Forms.Padding(4);
            this.FgHisAno.Name = "FgHisAno";
            this.FgHisAno.Rows.Count = 1;
            this.FgHisAno.Rows.DefaultSize = 20;
            this.FgHisAno.Size = new System.Drawing.Size(146, 86);
            this.FgHisAno.StyleInfo = resources.GetString("FgHisAno.StyleInfo");
            this.FgHisAno.TabIndex = 40;
            this.FgHisAno.EnterCell += new System.EventHandler(this.FgHisAno_EnterCell);
            this.FgHisAno.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgHisAno_CellChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 39;
            this.label3.Text = "Fecha Creacion";
            // 
            // TxtFchCre
            // 
            this.TxtFchCre.BackColor = System.Drawing.Color.White;
            this.TxtFchCre.Enabled = false;
            this.TxtFchCre.ForeColor = System.Drawing.Color.Black;
            this.TxtFchCre.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchCre.Location = new System.Drawing.Point(120, 4);
            this.TxtFchCre.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFchCre.Name = "TxtFchCre";
            this.TxtFchCre.Size = new System.Drawing.Size(133, 23);
            this.TxtFchCre.TabIndex = 38;
            // 
            // TxtDes
            // 
            this.TxtDes.BackColor = System.Drawing.Color.White;
            this.TxtDes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDes.Enabled = false;
            this.TxtDes.ForeColor = System.Drawing.Color.Black;
            this.TxtDes.Location = new System.Drawing.Point(120, 31);
            this.TxtDes.Margin = new System.Windows.Forms.Padding(4);
            this.TxtDes.MaxLength = 100;
            this.TxtDes.Multiline = true;
            this.TxtDes.Name = "TxtDes";
            this.TxtDes.Size = new System.Drawing.Size(583, 25);
            this.TxtDes.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(5, 33);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 17);
            this.label9.TabIndex = 37;
            this.label9.Text = "Descripcion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(347, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fch. Documento";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mes de Inicio";
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.BackColor = System.Drawing.Color.White;
            this.TxtFchFin.Enabled = false;
            this.TxtFchFin.ForeColor = System.Drawing.Color.Black;
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(469, 60);
            this.TxtFchFin.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(133, 23);
            this.TxtFchFin.TabIndex = 1;
            this.TxtFchFin.Visible = false;
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.BackColor = System.Drawing.Color.White;
            this.TxtFchIni.Enabled = false;
            this.TxtFchIni.ForeColor = System.Drawing.Color.Black;
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(469, 1);
            this.TxtFchIni.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(133, 23);
            this.TxtFchIni.TabIndex = 0;
            this.TxtFchIni.Visible = false;
            // 
            // FgItems
            // 
            this.FgItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgItems.BackColor = System.Drawing.Color.White;
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "3,1,0,0,0,100,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"Text" +
    "Align:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgItems.ForeColor = System.Drawing.Color.Black;
            this.FgItems.Location = new System.Drawing.Point(1, 134);
            this.FgItems.Margin = new System.Windows.Forms.Padding(4);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.Count = 2;
            this.FgItems.Rows.DefaultSize = 20;
            this.FgItems.Size = new System.Drawing.Size(1249, 438);
            this.FgItems.StyleInfo = resources.GetString("FgItems.StyleInfo");
            this.FgItems.TabIndex = 13;
            this.FgItems.EnterCell += new System.EventHandler(this.FgItems_EnterCell);
            this.FgItems.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgItems_KeyPressEdit);
            this.FgItems.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgItems_CellChanged);
            this.FgItems.Click += new System.EventHandler(this.FgItems_Click);
            this.FgItems.DoubleClick += new System.EventHandler(this.FgItems_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1251, 28);
            this.panel3.TabIndex = 8;
            // 
            // LblTitulo2
            // 
            this.LblTitulo2.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo2.ForeColor = System.Drawing.Color.Black;
            this.LblTitulo2.Location = new System.Drawing.Point(0, 0);
            this.LblTitulo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTitulo2.Name = "LblTitulo2";
            this.LblTitulo2.Size = new System.Drawing.Size(1251, 28);
            this.LblTitulo2.TabIndex = 0;
            this.LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmManPlanVentas2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 681);
            this.Controls.Add(this.ToolHerramientas);
            this.Controls.Add(this.Tab1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmManPlanVentas2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmManPlanVentas2";
            this.Activated += new System.EventHandler(this.FrmManPlanVentas2_Activated);
            this.Load += new System.EventHandler(this.FrmManPlanVentas2_Load);
            this.Resize += new System.EventHandler(this.FrmManPlanVentas2_Resize);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.Tab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgHisAno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolNuevo;
        private System.Windows.Forms.ToolStripButton ToolEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        private System.Windows.Forms.ToolStripButton ToolCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.TabControl Tab1;
        private System.Windows.Forms.TabPage c1DockingTabPage1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private System.Windows.Forms.TabPage c1DockingTabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtDes;
        private System.Windows.Forms.Label label9;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.Button CmdDelItem;
        private System.Windows.Forms.Button CmdAddItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTitulo2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker TxtFchCre;
        private C1.Win.C1FlexGrid.C1FlexGrid FgHisAno;
        private System.Windows.Forms.Button CmdAddValores;
        private System.Windows.Forms.ComboBox CboMesIni;
        private System.Windows.Forms.Button Cmd_VerGra;
        private System.Windows.Forms.Button CmdRetItem;
        private System.Windows.Forms.Button CmdAllItem;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button CmdDelTod;
        private System.Windows.Forms.Label LblNumRegistro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSplitButton ToolEliminar2;
        private System.Windows.Forms.ToolStripMenuItem eliminarRegistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem desactivarPlanVentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton ToolModificar;
        private System.Windows.Forms.ToolStripMenuItem modificarRegistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}