namespace ExpressionReader
{
    partial class CadEntrada
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
            this.gpbLista = new System.Windows.Forms.GroupBox();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.lstEntradas = new System.Windows.Forms.ListBox();
            this.gpbEntrada = new System.Windows.Forms.GroupBox();
            this.cbbParticipante = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gpbUnidades = new System.Windows.Forms.GroupBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.clbUnidades = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbbGrupo = new System.Windows.Forms.ComboBox();
            this.lblDescObj = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbObjeto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gpbLista.SuspendLayout();
            this.gpbEntrada.SuspendLayout();
            this.gpbUnidades.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbLista
            // 
            this.gpbLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbLista.Controls.Add(this.btnAlterar);
            this.gpbLista.Controls.Add(this.btnExcluir);
            this.gpbLista.Controls.Add(this.lstEntradas);
            this.gpbLista.Location = new System.Drawing.Point(505, 85);
            this.gpbLista.Name = "gpbLista";
            this.gpbLista.Size = new System.Drawing.Size(196, 263);
            this.gpbLista.TabIndex = 7;
            this.gpbLista.TabStop = false;
            this.gpbLista.Text = "Entradas";
            // 
            // btnAlterar
            // 
            this.btnAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlterar.Location = new System.Drawing.Point(19, 224);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(75, 23);
            this.btnAlterar.TabIndex = 9;
            this.btnAlterar.Text = "<< Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluir.Location = new System.Drawing.Point(102, 224);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnExcluir.TabIndex = 10;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // lstEntradas
            // 
            this.lstEntradas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEntradas.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.lstEntradas.FormattingEnabled = true;
            this.lstEntradas.HorizontalScrollbar = true;
            this.lstEntradas.ItemHeight = 14;
            this.lstEntradas.Location = new System.Drawing.Point(19, 19);
            this.lstEntradas.Name = "lstEntradas";
            this.lstEntradas.Size = new System.Drawing.Size(158, 186);
            this.lstEntradas.TabIndex = 8;
            this.lstEntradas.DoubleClick += new System.EventHandler(this.lstEntradas_DoubleClick);
            // 
            // gpbEntrada
            // 
            this.gpbEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbEntrada.Controls.Add(this.cbbParticipante);
            this.gpbEntrada.Controls.Add(this.btnCancelar);
            this.gpbEntrada.Controls.Add(this.btnCadastrar);
            this.gpbEntrada.Controls.Add(this.txtDescricao);
            this.gpbEntrada.Controls.Add(this.label1);
            this.gpbEntrada.Controls.Add(this.label2);
            this.gpbEntrada.Location = new System.Drawing.Point(12, 85);
            this.gpbEntrada.Name = "gpbEntrada";
            this.gpbEntrada.Size = new System.Drawing.Size(286, 263);
            this.gpbEntrada.TabIndex = 6;
            this.gpbEntrada.TabStop = false;
            this.gpbEntrada.Text = "Cadastro de Entrada";
            // 
            // cbbParticipante
            // 
            this.cbbParticipante.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbParticipante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbParticipante.FormattingEnabled = true;
            this.cbbParticipante.Location = new System.Drawing.Point(78, 19);
            this.cbbParticipante.Name = "cbbParticipante";
            this.cbbParticipante.Size = new System.Drawing.Size(202, 21);
            this.cbbParticipante.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Location = new System.Drawing.Point(9, 224);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCadastrar.Location = new System.Drawing.Point(205, 224);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(75, 23);
            this.btnCadastrar.TabIndex = 5;
            this.btnCadastrar.Text = "Salvar >>";
            this.btnCadastrar.UseVisualStyleBackColor = true;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.Location = new System.Drawing.Point(9, 59);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(271, 159);
            this.txtDescricao.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Participante:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Descrição:";
            // 
            // gpbUnidades
            // 
            this.gpbUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbUnidades.Controls.Add(this.btnInfo);
            this.gpbUnidades.Controls.Add(this.clbUnidades);
            this.gpbUnidades.Location = new System.Drawing.Point(304, 85);
            this.gpbUnidades.Name = "gpbUnidades";
            this.gpbUnidades.Size = new System.Drawing.Size(195, 263);
            this.gpbUnidades.TabIndex = 8;
            this.gpbUnidades.TabStop = false;
            this.gpbUnidades.Text = "Unidades de Análise";
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnInfo.Location = new System.Drawing.Point(60, 224);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(75, 23);
            this.btnInfo.TabIndex = 7;
            this.btnInfo.Text = "Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // clbUnidades
            // 
            this.clbUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbUnidades.FormattingEnabled = true;
            this.clbUnidades.Location = new System.Drawing.Point(19, 19);
            this.clbUnidades.Name = "clbUnidades";
            this.clbUnidades.Size = new System.Drawing.Size(158, 199);
            this.clbUnidades.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.cbbGrupo);
            this.groupBox4.Controls.Add(this.lblDescObj);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cbbObjeto);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(12, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(689, 75);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Objeto de Estudo";
            // 
            // cbbGrupo
            // 
            this.cbbGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGrupo.FormattingEnabled = true;
            this.cbbGrupo.Location = new System.Drawing.Point(133, 19);
            this.cbbGrupo.Name = "cbbGrupo";
            this.cbbGrupo.Size = new System.Drawing.Size(217, 21);
            this.cbbGrupo.TabIndex = 0;
            this.cbbGrupo.SelectedIndexChanged += new System.EventHandler(this.cbbGrupo_SelectedIndexChanged);
            // 
            // lblDescObj
            // 
            this.lblDescObj.AutoSize = true;
            this.lblDescObj.Location = new System.Drawing.Point(70, 50);
            this.lblDescObj.Name = "lblDescObj";
            this.lblDescObj.Size = new System.Drawing.Size(55, 13);
            this.lblDescObj.TabIndex = 5;
            this.lblDescObj.Text = "Descricao";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Descrição:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Grupo de Participantes: ";
            // 
            // cbbObjeto
            // 
            this.cbbObjeto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbObjeto.FormattingEnabled = true;
            this.cbbObjeto.Location = new System.Drawing.Point(453, 19);
            this.cbbObjeto.Name = "cbbObjeto";
            this.cbbObjeto.Size = new System.Drawing.Size(217, 21);
            this.cbbObjeto.TabIndex = 1;
            this.cbbObjeto.SelectedIndexChanged += new System.EventHandler(this.cbbObjeto_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Objeto:";
            // 
            // CadEntrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 357);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gpbUnidades);
            this.Controls.Add(this.gpbLista);
            this.Controls.Add(this.gpbEntrada);
            this.MinimumSize = new System.Drawing.Size(730, 396);
            this.Name = "CadEntrada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrada de Dados - Cadastro";
            this.Load += new System.EventHandler(this.CadEntrada_Load);
            this.gpbLista.ResumeLayout(false);
            this.gpbEntrada.ResumeLayout(false);
            this.gpbEntrada.PerformLayout();
            this.gpbUnidades.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbLista;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.ListBox lstEntradas;
        private System.Windows.Forms.GroupBox gpbEntrada;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gpbUnidades;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbObjeto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDescObj;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.CheckedListBox clbUnidades;
        private System.Windows.Forms.ComboBox cbbParticipante;
        private System.Windows.Forms.ComboBox cbbGrupo;
    }
}