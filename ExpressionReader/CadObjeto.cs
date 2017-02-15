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

        void Limpa_Campos()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            if (cbbGrupo.Items.Count > 0)
            {
                cbbGrupo.SelectedIndex = 0;
            }
            cbbGrupo.Enabled = true;
            txtNome.Focus();
        }

        void Carrega_Objetos()
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

                //Limpa as lista
                lstObjetos.Items.Clear();

                sql = "SELECT * FROM grupo ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbGrupo.Items.Clear();
                ListViewGroup grupo;

                /*Carrega os Grupos na ListView*/
                while (reader.Read()) {
                    cbbGrupo.Items.Add(reader["nome"]);
                    grupo = new ListViewGroup((reader["nome"]).ToString(), HorizontalAlignment.Left);
                    lstObjetos.Groups.Add(grupo);

                    //Adiciona SubItens
                    sql = "SELECT * FROM objeto WHERE id_grupo = "+ reader["id_grupo"] + " ORDER BY nome";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader readObj = command.ExecuteReader();

                    //Adiciona todas as unidades encontradas no banco
                    while (readObj.Read())
                    {                        
                        lstObjetos.Items.Add(new ListViewItem(new string[] { readObj["nome"].ToString() }, grupo));
                    }

                }
                                
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
            }
        }

        private void CadObjeto_Load(object sender, EventArgs e)
        {
            Carrega_Objetos();
            cbbGrupo.SelectedIndex = 0;
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
            string grupo = cbbGrupo.SelectedItem.ToString();
            string id_grupo = "";

            if (txtNome.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Digite um nome ou identificador para cadastrar um Objeto de Estudo!", "Atenção");
                return;
            }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                #region Select grupo
                sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    id_grupo = (reader["id_grupo"]).ToString();
                #endregion

                #region Select, Update and Insert of Objeto
                int n = 0;
                sql = "SELECT * FROM objeto WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                
                //Conta quantos encontrou
                while (reader.Read())
                    n++;

                if (n > 0)
                {
                    sql = "UPDATE objeto SET descricao = '" + descricao + "', id_grupo = " + id_grupo + " WHERE nome='" + nome + "'";
                }
                else
                {
                    sql = "INSERT INTO objeto (id_objeto, nome, descricao, id_grupo) VALUES (NULL, '" + nome + "' , '" + descricao + "', " + id_grupo + ")";
                }

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                #endregion

                m_dbConnection.Close();

                Carrega_Objetos();

                cbbGrupo.Text = grupo;

                if (n > 0)
                {
                    MessageBox.Show("Objeto atualizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Objeto cadastrado com sucesso!");
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
            string descricao = "";
            string id_grupo = "";
            string grupo = "";

            if (lstObjetos.SelectedItems.Count > 0)
            {
                ListViewItem item = lstObjetos.SelectedItems[0];
                nome = item.Text;
                grupo = item.Group.Header;
            }
            else {
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

                #region Select Objeto (to change)
                /* Busca o Objeto e o id_grupo  */
                sql = "SELECT * FROM objeto WHERE nome='" + nome + "' AND id_grupo = "+ id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read()) { 
                    descricao = (reader["descricao"]).ToString();
                }
                #endregion

                m_dbConnection.Close();

                txtNome.Text = nome;
                txtDescricao.Text = descricao;
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
            string id_objeto = "";
            string grupo = "";
            string id_grupo = "";
            int nEntradas = 0;

            if (lstObjetos.SelectedItems.Count > 0)
            {
                ListViewItem item = lstObjetos.SelectedItems[0];
                nome = item.Text;
                grupo = item.Group.Header;
            }
            else
            {
                MessageBox.Show("Selecione um item para excluir!");
                return;
            }

            DialogResult dialog = MessageBox.Show("Você tem certeza que quer excluir o objeto " + nome + "?", "Exclusão", MessageBoxButtons.YesNo);
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

                #region Select Objeto (id_objeto)
                /* Busca o id do objeto para verificar se há entradas para serem excluidas também*/
                sql = "SELECT * FROM objeto WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_objeto = (reader["id_objeto"]).ToString();
                #endregion

                #region Seleciona todas as entradas referente aos objetos que serão excluídos
                List<string> id_entradas = new List<string>();
                sql = "SELECT * FROM entrada WHERE id_objeto = " + id_objeto;
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
                    dialog = MessageBox.Show("Existe(m): " + quantidades + "\nAssociada(s) ao Objeto " + nome + ".\nVocê tem certeza que deseja excluir?", "Exclusão", MessageBoxButtons.YesNo);
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
                sql = "DELETE FROM entrada WHERE id_objeto=" + id_objeto;
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                //Por último deleta o grupo
                sql = "DELETE FROM objeto WHERE nome='" + nome + "' AND id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                m_dbConnection.Close();

                Carrega_Objetos();

                cbbGrupo.Text = grupo;

                MessageBox.Show("Objeto de Estudo " + nome + " excluído com sucesso!");
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível acessar a base de dados!");
            }
        }

        private void lstObjetos_DoubleClick(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedItems.Count > 0)
            {
                btnAlterar.PerformClick();
            }
        }        
    }
}
