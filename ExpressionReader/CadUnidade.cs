using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ExpressionReader
{
    public partial class CadUnidade : Form
    {
        public CadUnidade()
        {
            InitializeComponent();
        }

        void Limpa_Campos()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtNome.Focus();
        }

        void Carrega_Unidades()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM unidade ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Limpa as unidades já carregadas
                lstUnidades.Items.Clear();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read())
                    lstUnidades.Items.Add(reader["nome"]);

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
            }
        }

        private void CadUnidade_Load(object sender, EventArgs e)
        {
            Carrega_Unidades();
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpa_Campos();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string nome = txtNome.Text.ToString();
            string descricao = txtDescricao.Text.ToString();

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                int n = 0;
                sql = "SELECT * FROM unidade where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Conta quantos encontrou
                while (reader.Read())
                    n++;

                if (n > 0) {
                    sql = "UPDATE unidade set descricao = '" + descricao + "' where nome='" + nome + "'";
                }
                else {
                    sql = "INSERT INTO unidade (id_unidade, nome, descricao) values (NULL, '" + nome + "' , '" + descricao + "')";
                }

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                Carrega_Unidades();
                Limpa_Campos();

                if (n > 0)
                {
                    MessageBox.Show("Unidade atualizada com sucesso!");
                }
                else
                {                    
                    MessageBox.Show("Unidade cadastrada com sucesso!");
                }
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string nome = lstUnidades.SelectedItem.ToString();
            string descricao = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM unidade where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read())
                    descricao = (reader["descricao"]).ToString();

                m_dbConnection.Close();

                txtNome.Text = nome;
                txtDescricao.Text = descricao;
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string nome = lstUnidades.SelectedItem.ToString();

            DialogResult dialog = MessageBox.Show("Você tem certeza que quer excluir a unidade " + nome + "?", "Exclusão", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No) {
                return;
            } // Se sim, continua a função

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "DELETE FROM unidade where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();
              
                m_dbConnection.Close();

                Carrega_Unidades();

                MessageBox.Show("Unidade " + nome + " excluída com sucesso!");
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
            }
        }

        private void lstUnidades_DoubleClick(object sender, EventArgs e)
        {
            if (lstUnidades.SelectedItem != null)
            {
                btnAlterar.PerformClick();
            }
        }
    }
}
