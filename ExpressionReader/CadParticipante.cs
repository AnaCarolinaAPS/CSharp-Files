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
    public partial class CadParticipante : Form
    {
        public CadParticipante()
        {
            InitializeComponent();
        }

        void Carrega_Participantes()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM participante GROUP BY id_grupo ORDER BY nome ";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Limpa as unidades já carregadas
                lstParticipantes.Items.Clear();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read())
                    lstParticipantes.Items.Add(reader["nome"]);

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        void Carrega_Grupos()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM grupo ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Adiciona todas os grupos encontrados no banco
                while (reader.Read()) { 
                    cbbGrupo.Items.Add((reader["nome"]).ToString());
                }

                if (cbbGrupo.Items.Count == 0)
                {
                    cbbGrupo.Items.Add("Sem grupo");
                }

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void CadParticipante_Load(object sender, EventArgs e)
        {
            Carrega_Grupos();
            Carrega_Participantes();
            cbbGrupo.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtCurso.Text = "";
            cbbGrupo.SelectedIndex = 0;
            cbbGrupo.Enabled = true;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string nome = txtNome.Text.ToString();
            string curso = txtCurso.Text.ToString();
            string grupo = cbbGrupo.SelectedItem.ToString();
            int id_grupo = 0;

            cbbGrupo.Enabled = true;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                int n = 0;
                sql = "SELECT * FROM participante where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Conta quantos encontrou
                while (reader.Read())
                    n++;

                if (n > 0)
                {
                    sql = "UPDATE participante set curso = '" + curso + "' where nome='" + nome + "'";
                }
                else
                {
                    sql = "SELECT * FROM grupo where nome='" + grupo + "'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    n = 0;
                    while (reader.Read()) { 
                        id_grupo = Int32.Parse(reader["id_grupo"].ToString());
                        n++;
                    }

                    if (n == 0) {
                        DialogResult dialogResult = MessageBox.Show("Não existem grupos criados. /n Você deseja criar o grupo 'Sem grupo' ", "Grupos", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            sql = "INSERT INTO grupo (id_grupo, nome, descricao) values (NULL, 'Sem grupo' , 'Criado Automaticamente')";
                            command = new SQLiteCommand(sql, m_dbConnection);
                            command.ExecuteNonQuery();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    sql = "INSERT INTO participante (id_participante, id_grupo, nome, curso) values (NULL, " + id_grupo + ",'" + nome + "' , '" + curso + "')";
                }

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                Carrega_Participantes();
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

            string nome = lstParticipantes.SelectedItem.ToString();
            string curso = "";
            string grupo = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM participante where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read()) { 
                    curso = (reader["curso"]).ToString();

                    sql = "SELECT * FROM grupo where id_grupo=" + Int32.Parse((reader["id_grupo"]).ToString()) + "";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader rd = command.ExecuteReader();
                    while (rd.Read()) {
                        grupo = (rd["nome"]).ToString();
                    }
                }
                m_dbConnection.Close();

                txtNome.Text = nome;
                txtCurso.Text = curso;
                cbbGrupo.SelectedIndex = cbbGrupo.Items.IndexOf(grupo);
                MessageBox.Show("Grupo: " + grupo);
                cbbGrupo.Enabled = false;
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

            string nome = lstParticipantes.SelectedItem.ToString();

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "DELETE FROM participante where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                m_dbConnection.Close();

                Carrega_Participantes();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi excluir a unidade!");
            }
        }

        private void lstParticipantes_DoubleClick(object sender, EventArgs e)
        {
            if (lstParticipantes.SelectedItem != null)
            {
                btnAlterar.PerformClick();
            }
        }
    }
}
