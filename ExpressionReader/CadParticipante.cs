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
            txtNome.Focus();
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
                        lstParticipantes.Items.Add(new ListViewItem(new string[] { readObj["nome"].ToString() }, grupo));
                    }

                }

                cbbGrupo.SelectedIndex = 0;
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
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

            if (txtNome.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Digite um nome ou identificador para cadastrar um Participante!", "Atenção");
                return;
            }

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

                cbbGrupo.Text = grupo;

                if (n > 0)
                {
                    MessageBox.Show("Participante atualizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Participante cadastrado com sucesso!");
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
                cbbGrupo.Enabled = false;
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

            string nome = "";
            string id_participante = "";
            string grupo = "";
            string id_grupo = "";
            int nEntradas = 0;

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

            DialogResult dialog = MessageBox.Show("Você tem certeza que quer excluir o participante " + nome + "?", "Exclusão", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
            {
                return;
            } // Se sim, continua a função

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                #region Select Grupo (id_grupo)
                /* Busca o nome do grupo que está no combobox*/
                sql = "SELECT * FROM grupo WHERE nome = '" + grupo + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_grupo = (reader["id_grupo"]).ToString();
                #endregion

                #region Select Participante (id_participante)
                /* Busca o id do objeto para verificar se há entradas para serem excluidas também*/
                sql = "SELECT * FROM participante WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_participante = (reader["id_participante"]).ToString();
                #endregion

                #region Seleciona todas as entradas referente aos objetos que serão excluídos
                List<string> id_entradas = new List<string>();
                sql = "SELECT * FROM entrada WHERE id_participante = " + id_participante;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    nEntradas++;
                    id_entradas.Add(reader["id_entrada"].ToString());
                }
                #endregion

                if (nEntradas > 0) {
                    string quantidades = "";
                    quantidades = "\n   + Entrada(s): " + nEntradas.ToString();
                    dialog = MessageBox.Show("Existe(m): " + quantidades + "\nAssociada(s) ao Participante " + nome + ".\nVocê tem certeza que deseja excluir?", "Exclusão", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                    {
                        return;
                    } // Se sim, continua a função
                }

                //Primeiro se retira as entrada_unidade
                foreach (string id_entrada in id_entradas)
                {
                    sql = "DELETE FROM entrada_unidade WHERE id_entrada=" + id_entrada;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteReader();
                }

                //Segundo se retira as entradas
                sql = "DELETE FROM entrada WHERE id_participante=" + id_participante;
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                //Por último deleta o grupo
                sql = "DELETE FROM participante WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                m_dbConnection.Close();

                cbbGrupo.Text = grupo;

                Carrega_Participantes();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
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
