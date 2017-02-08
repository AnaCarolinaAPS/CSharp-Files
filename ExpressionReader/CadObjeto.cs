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
    public partial class CadObjeto : Form
    {
        public CadObjeto()
        {
            InitializeComponent();
        }

        void Carrega_Objetos()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM objeto ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Limpa as unidades já carregadas
                lstObjetos.Items.Clear();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read())
                    lstObjetos.Items.Add(reader["nome"]);

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void CadObjeto_Load(object sender, EventArgs e)
        {
            Carrega_Objetos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
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
                sql = "SELECT * FROM objeto where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Conta quantos encontrou
                while (reader.Read())
                    n++;

                if (n > 0)
                {
                    sql = "UPDATE objeto set descricao = '" + descricao + "' where nome='" + nome + "'";
                }
                else
                {
                    sql = "INSERT INTO objeto (id_objeto, nome, descricao) values (NULL, '" + nome + "' , '" + descricao + "')";
                }

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                Carrega_Objetos();
                btnCancelar.PerformClick();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível inserir na base de dados!");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string nome = lstObjetos.SelectedItem.ToString();
            string descricao = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM objeto where nome='" + nome + "'";
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
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string nome = lstObjetos.SelectedItem.ToString();

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "DELETE FROM objeto where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                m_dbConnection.Close();

                Carrega_Objetos();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi excluir o objeto!");
            }
        }

        private void lstObjetos_DoubleClick(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedItem != null)
            {
                btnAlterar.PerformClick();
            }
        }
    }
}
