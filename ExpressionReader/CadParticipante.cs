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

        void Limpa_Campos() {
            txtCurso.Text = "";
            txtNome.Text = "";
            if (cbbGrupo.Items.Count > 0) { 
                cbbGrupo.SelectedIndex = 0;
            }
            cbbGrupo.Enabled = true;
        }

        void Carrega_Participantes()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            Limpa_Campos();

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT * FROM grupo ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbGrupo.Items.Clear();

                //Limpa as lista
                lstParticipantes.Items.Clear();

                ListViewGroup grupo;

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    cbbGrupo.Items.Add(reader["nome"]);
                    grupo = new ListViewGroup((reader["nome"]).ToString(), HorizontalAlignment.Left);
                    lstParticipantes.Groups.Add(grupo);

                    //Adiciona SubItens
                    sql = "SELECT * FROM participante WHERE id_grupo=" + reader["id_grupo"] + " ORDER BY nome";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader readObj = command.ExecuteReader();

                    //Adiciona todas as unidades encontradas no banco
                    while (readObj.Read())
                    {
                        //MessageBox.Show("Participante: " + readObj["nome"].ToString());
                        lstParticipantes.Items.Add(new ListViewItem(new string[] { readObj["nome"].ToString() }, grupo));
                    }

                }

                cbbGrupo.SelectedIndex = 0;
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }      

        private void CadParticipante_Load(object sender, EventArgs e)
        {
            Carrega_Participantes();
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
            string curso = txtCurso.Text.ToString();
            string grupo = cbbGrupo.SelectedItem.ToString();
            string id_grupo = "";

            cbbGrupo.Enabled = true;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                int n = 0;
                sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                n = 0;
                while (reader.Read())
                {
                    id_grupo = reader["id_grupo"].ToString();
                    n++;
                }

                if (n == 0)
                {
                    MessageBox.Show("Não existem grupos criados.\nFavor Criar um grupo!");
                    return;
                }


                sql = "SELECT * FROM participante WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                n = 0;

                //Conta quantos encontrou
                while (reader.Read())
                    n++;

                if (n > 0)
                {
                    sql = "UPDATE participante SET curso = '" + curso + "' WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                }
                else
                {
                    sql = "INSERT INTO participante (id_participante, id_grupo, nome, curso) VALUES (NULL, " + id_grupo + ",'" + nome + "' , '" + curso + "')";
                }

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                Carrega_Participantes();
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

            string nome = "";
            string curso = "";
            string grupo = "";
            string id_grupo = "";

            if (lstParticipantes.SelectedItems.Count > 0)
            {
                ListViewItem item = lstParticipantes.SelectedItems[0];
                nome = item.Text;
                grupo = item.Group.Header;
            }
            else
            {
                MessageBox.Show("Selecione um item para alterar!");
                return;
            }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                #region Select Grupo (id_grupo)
                /* Busca o nome do grupo para colocar no combobox*/
                sql = "SELECT * FROM grupo WHERE nome = '" + grupo + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_grupo = (reader["id_grupo"]).ToString();
                #endregion

                #region Select Participante
                sql = "SELECT * FROM participante WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read()) { 
                    curso = (reader["curso"]).ToString();
                }
                #endregion

                m_dbConnection.Close();

                txtNome.Text = nome;
                txtCurso.Text = curso;
                cbbGrupo.Text = grupo;
                //MessageBox.Show("Grupo: " + grupo);
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

            string nome = "";//lstParticipantes.SelectedItem.ToString();
            string grupo = "";
            string id_grupo = "";

            if (lstParticipantes.SelectedItems.Count > 0)
            {
                ListViewItem item = lstParticipantes.SelectedItems[0];
                nome = item.Text;
                grupo = item.Group.Header;
            }
            else
            {
                MessageBox.Show("Selecione um item para excluir!");
                return;
            }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                #region Select Grupo (id_grupo)
                /* Busca o nome do grupo para colocar no combobox*/
                sql = "SELECT * FROM grupo WHERE nome = '" + grupo + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_grupo = (reader["id_grupo"]).ToString();

                #endregion

                sql = "DELETE FROM participante WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                m_dbConnection.Close();

                Carrega_Participantes();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível excluir!");
            }
        }

        private void lstParticipantes_DoubleClick(object sender, EventArgs e)
        {
            if (lstParticipantes.SelectedItems.Count > 0)
            {
                btnAlterar.PerformClick();
            }
        }
    }
}
