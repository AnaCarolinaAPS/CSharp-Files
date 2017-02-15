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
    public partial class CadGrupo : Form
    {
        public CadGrupo()
        {
            InitializeComponent();
        }

        void Limpa_Campos() {
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

                sql = "SELECT * FROM grupo ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Limpa as unidades já carregadas
                lstGrupos.Items.Clear();

                //Adiciona todas as unidades encontradas no banco
                while (reader.Read())
                    lstGrupos.Items.Add(reader["nome"]);

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void CadGrupo_Load(object sender, EventArgs e)
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

            if (txtNome.Text.Trim().Length <= 0) {
                MessageBox.Show("Digite um nome ou identificador para cadastrar um Grupo!", "Atenção");
                return;
            }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                int n = 0;
                sql = "SELECT * FROM grupo where nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                //Conta quantos encontrou
                while (reader.Read())
                    n++;

                if (n > 0)
                {
                    sql = "UPDATE grupo set descricao = '" + descricao + "' where nome='" + nome + "'";
                }
                else
                {
                    sql = "INSERT INTO grupo (id_grupo, nome, descricao) values (NULL, '" + nome + "' , '" + descricao + "')";
                }

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();

                Carrega_Unidades();
                Limpa_Campos();

                if (n > 0)
                {
                    MessageBox.Show("Grupo atualizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Grupo cadastrado com sucesso!");
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

            if (lstGrupos.SelectedItems.Count > 0)
            {
                nome = lstGrupos.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Selecione um item para alterar!", "Atenção");
                return;
            }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                sql = "SELECT * FROM grupo where nome='" + nome + "'";
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

            string nome = "";
            string id_grupo = "";
            int nParticipantes = 0, nObjetos = 0, nEntradas = 0;

            if (lstGrupos.SelectedItems.Count > 0)
            {
                nome = lstGrupos.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Selecione um item para excluir!", "Atenção");
                return;
            }

            DialogResult dialog = MessageBox.Show("Você tem certeza que quer excluir o grupo " + nome + "?", "Exclusão", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
            {
                return;
            } // Se sim, continua a função

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                //Verifica se não existem outras entradas associadas.

                #region Select Grupo (id_grupo)
                sql = "SELECT * FROM grupo WHERE nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_grupo = (reader["id_grupo"]).ToString();
                #endregion

                //Verifica quantos PARTICIPANTES serão excluídos
                #region Select Participante (COUNT)
                sql = "SELECT COUNT(*) FROM participante WHERE id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    nParticipantes = Convert.ToInt32(reader["COUNT(*)"]);
                #endregion

                //Verifica quantos OBJETOS serão excluídos
                #region Select Participante (COUNT)
                sql = "SELECT * FROM objeto WHERE id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                List<string> id_objetos = new List<string>();
                while (reader.Read()) {
                    nObjetos++;
                    id_objetos.Add(reader["id_objeto"].ToString());
                }
                #endregion

                #region Seleciona todas as entradas referente aos objetos que serão excluídos
                List<string> id_entradas = new List<string>();
                foreach (string id_objeto in id_objetos) {
                    sql = "SELECT * FROM entrada WHERE id_objeto = " + id_objeto;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        nEntradas++;
                        id_entradas.Add(reader["id_entrada"].ToString());
                    }
                }
                #endregion

                if (nParticipantes + nObjetos + nEntradas > 0) {
                    string quantidades = "";
                    quantidades = "\n   + Participante(s): " + nParticipantes.ToString();
                    quantidades = quantidades + "\n   + Objeto(s):" + nObjetos.ToString();
                    quantidades = quantidades + "\n   + Entrada(s): " + nEntradas.ToString();

                    dialog = MessageBox.Show("Existe(m): " + quantidades + "\nAssociada(s) ao Grupo " + nome + ".\nVocê tem certeza que deseja excluir?", "Exclusão", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                    {
                        return;
                    } // Se sim, continua a função
                }

                //Primeiro se retira as entrada_unidade
                foreach (string id_entrada in id_entradas) {
                    sql = "DELETE FROM entrada_unidade WHERE id_entrada=" + id_entrada;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteReader();
                }

                //Segundo se retira as entradas
                foreach (string id_objeto in id_objetos)
                {
                    sql = "DELETE FROM entrada WHERE id_objeto=" + id_objeto;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteReader();
                }

                //Se retiram os objetos
                sql = "DELETE FROM objeto WHERE id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                //Se retiram os participantes
                sql = "DELETE FROM participante WHERE id_grupo = " + id_grupo;
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                //Por último deleta o grupo
                sql = "DELETE FROM grupo WHERE nome='" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteReader();

                m_dbConnection.Close();

                Carrega_Unidades();

                MessageBox.Show("Grupo de Participantes " + nome + " excluído com sucesso!");
            }
            catch
            {
                MessageBox.Show("Não foi possível acessar a base de dados!", "Erro");
            }
        }

        private void lstGrupos_DoubleClick(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem != null)
            {
                btnAlterar.PerformClick();
            }
        }      
    }
}
