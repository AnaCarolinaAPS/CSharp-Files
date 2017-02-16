namespace ExpressionReader
{
    partial class RelParticipante
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Presença",
            "1"}, -1);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstUnidades = new System.Windows.Forms.ListView();
            this.clmUnidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmQtd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblQtdParticipantes = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbGrupo = new System.Windows.Forms.ComboBox();
            this.lblQtdEntradas = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbParticipante = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lstUnidades);
            this.groupBox2.Location = new System.Drawing.Point(12, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 203);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unidades de Análise";
            // 
            // lstUnidades
            // 
            this.lstUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUnidades.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmUnidade,
            this.clmQtd});
            this.lstUnidades.FullRowSelect = true;
            this.lstUnidades.GridLines = true;
            this.lstUnidades.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstUnidades.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lstUnidades.Location = new System.Drawing.Point(19, 19);
            this.lstUnidades.MultiSelect = false;
            this.lstUnidades.Name = "lstUnidades";
            this.lstUnidades.Size = new System.Drawing.Size(405, 170);
            this.lstUnidades.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstUnidades.TabIndex = 2;
            this.lstUnidades.UseCompatibleStateImageBehavior = false;
            this.lstUnidades.View = System.Windows.Forms.View.Details;
            this.lstUnidades.Resize += new System.EventHandler(this.lstUnidades_Resize);
            // 
            // clmUnidade
            // 
            this.clmUnidade.Text = "Unidade";
            this.clmUnidade.Width = 300;
            // 
            // clmQtd
            // 
            this.clmQtd.Text = "Quantidade";
            this.clmQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmQtd.Width = 100;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.lblQtdParticipantes);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cbbGrupo);
            this.groupBox4.Controls.Add(this.lblQtdEntradas);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cbbParticipante);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(438, 137);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Participante";
            // 
            // lblQtdParticipantes
            // 
            this.lblQtdParticipantes.AutoSize = true;
            this.lblQtdParticipantes.Location = new System.Drawing.Point(161, 49);
            this.lblQtdParticipantes.Name = "lblQtdParticipantes";
            this.lblQtdParticipantes.Size = new System.Drawing.Size(13, 13);
            this.lblQtdParticipantes.TabIndex = 8;
            this.lblQtdParticipantes.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Quantidade de Participantes:";
            // 
            // cbbGrupo
            // 
            this.cbbGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGrupo.FormattingEnabled = true;
            this.cbbGrupo.Location = new System.Drawing.Point(161, 19);
            this.cbbGrupo.Name = "cbbGrupo";
            this.cbbGrupo.Size = new System.Drawing.Size(217, 21);
            this.cbbGrupo.TabIndex = 0;
            this.cbbGrupo.SelectedIndexChanged += new System.EventHandler(this.cbbGrupo_SelectedIndexChanged);
            // 
            // lblQtdEntradas
            // 
            this.lblQtdEntradas.AutoSize = true;
            this.lblQtdEntradas.Location = new System.Drawing.Point(161, 101);
            this.lblQtdEntradas.Name = "lblQtdEntradas";
            this.lblQtdEntradas.Size = new System.Drawing.Size(13, 13);
            this.lblQtdEntradas.TabIndex = 5;
            this.lblQtdEntradas.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Quantidade de Entradas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Grupo de Participantes:";
            // 
            // cbbParticipante
            // 
            this.cbbParticipante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbParticipante.FormattingEnabled = true;
            this.cbbParticipante.Location = new System.Drawing.Point(161, 71);
            this.cbbParticipante.Name = "cbbParticipante";
            this.cbbParticipante.Size = new System.Drawing.Size(217, 21);
            this.cbbParticipante.TabIndex = 1;
            this.cbbParticipante.SelectedIndexChanged += new System.EventHandler(this.cbbParticipante_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Participante:";
            // 
            // RelParticipante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 370);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.MinimumSize = new System.Drawing.Size(478, 409);
            this.Name = "RelParticipante";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unidades de Análise x Participantes - Relatório";
            this.Load += new System.EventHandler(this.RelUnidade_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstUnidades;
        private System.Windows.Forms.ColumnHeader clmUnidade;
        private System.Windows.Forms.ColumnHeader clmQtd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblQtdParticipantes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbGrupo;
        private System.Windows.Forms.Label lblQtdEntradas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbParticipante;
        private System.Windows.Forms.Label label3;
    }
}