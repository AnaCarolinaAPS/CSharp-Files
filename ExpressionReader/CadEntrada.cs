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
    public partial class CadEntrada : Form
    {
        public CadEntrada()
        {
            InitializeComponent();
        }

        void Limpa_Campos()
        {
            txtDescricao.Text = "";
            if (cbbParticipante.Items.Count > 0)
            {
                cbbParticipante.SelectedIndex = 0;
            }
            foreach (int i in clbUnidades.CheckedIndices)
            {
                clbUnidades.SetItemCheckState(i, CheckState.Unchecked);
            }
            cbbParticipante.Enabled = true;
        }

        void Carrega_Grupos()
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

                #region Select Objetos
                sql = "SELECT * FROM grupo ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbGrupo.Items.Clear();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    cbbGrupo.Items.Add(reader["nome"]);
                }
                #endregion

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        void Carrega_Objetos()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            Limpa_Campos();

            string grupo = cbbGrupo.Text;
            string id_grupo = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();
                                
                while (reader.Read())
                {
                    id_grupo = (reader["id_grupo"]).ToString();
                }

                #region Select Objetos
                sql = "SELECT * FROM objeto WHERE id_grupo = "+ id_grupo +" ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbObjeto.Items.Clear();
                
                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    cbbObjeto.Items.Add(reader["nome"]);                  
                }
                #endregion

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        void Carrega_Participantes()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            Limpa_Campos();

            string grupo = cbbGrupo.Text;
            string id_grupo = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id_grupo = (reader["id_grupo"]).ToString();
                }

                #region Select Participante
                sql = "SELECT * FROM participante WHERE id_grupo = " + id_grupo + " ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbParticipante.Items.Clear();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    cbbParticipante.Items.Add(reader["nome"]);
                }
                #endregion

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
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
                SQLiteDataReader reader;

                #region Select Unidade
                sql = "SELECT * FROM unidade ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                clbUnidades.Items.Clear();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    clbUnidades.Items.Add(reader["nome"]);
                }
                #endregion

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        void Carrega_Entradas() {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string objeto = cbbObjeto.Text;
            string id_objeto = "";
            string descricao = "";
            string entrada = "";
            string participante = "";
            string id_participante = "";

            Limpa_Campos();

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;
                SQLiteDataReader rdParticipante;

                #region Select Objetos
                sql = "SELECT * FROM objeto WHERE nome='" + objeto + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id_objeto = (reader["id_objeto"]).ToString();
                }
                #endregion

                #region Select Entrada
                sql = "SELECT * FROM entrada WHERE id_objeto=" + id_objeto;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                lstEntradas.Items.Clear();

                int max = 0;
                int n = 0;

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    #region Select Participantes
                    id_participante = (reader["id_participante"]).ToString();

                    sql = "SELECT * FROM participante WHERE id_participante=" + id_participante;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    rdParticipante = command.ExecuteReader();

                    n = 0;
                    while (rdParticipante.Read())
                    {
                        participante = (rdParticipante["nome"]).ToString();
                        n++;                                               
                    }

                    if (n == 0) {
                        MessageBox.Show("Erro, não foi encontrado o participante que fez a entrada!");
                        return;                       
                    }
                    #endregion

                    entrada = "(" + participante + ")";
                    entrada = entrada + "\"";
                    max = 18;

                    max = max - entrada.Length;

                    descricao = (reader["descricao"]).ToString();
                    if (descricao.Length < max)
                    {
                        entrada = entrada + descricao.Substring(0, descricao.Length);
                    }
                    else
                    {
                        entrada = entrada + descricao.Substring(0, max);
                    }
                    entrada = entrada + "...\"";
                    lstEntradas.Items.Add(entrada);
                }
                #endregion

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void cbbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbGrupo.Items.Count > 0)
            {
                Carrega_Objetos();
                if (cbbObjeto.Items.Count > 0)
                {
                    cbbObjeto.SelectedIndex = 0;
                }
                else
                {
                    cbbObjeto.Text = "";                    
                }            
                cbbObjeto.Enabled = true;
                cbbObjeto_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void cbbObjeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            Limpa_Campos();

            string nome = cbbObjeto.Text;
            string id_grupo = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                #region Select Objetos
                sql = "SELECT * FROM objeto WHERE nome = '" + nome + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                lblDescObj.Text = "";

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    lblDescObj.Text = (reader["descricao"]).ToString();
                    //id_grupo = (reader["id_grupo"]).ToString();
                }
                #endregion

                m_dbConnection.Close();

                if (cbbObjeto.Items.Count > 0)
                {
                    gpbEntrada.Enabled = true;
                    gpbUnidades.Enabled = true;
                    gpbLista.Enabled = true;
                    Carrega_Participantes();
                    Carrega_Entradas();
                }
                else
                {
                    gpbEntrada.Enabled = false;
                    gpbUnidades.Enabled = false;
                    gpbLista.Enabled = false;
                }

            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void CadEntrada_Load(object sender, EventArgs e)
        {
            Carrega_Grupos();
            Carrega_Unidades();

            lblDescObj.Text = "";
            cbbObjeto.Enabled = false;
            if (cbbGrupo.Items.Count > 0)
            {
                cbbGrupo.SelectedIndex = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpa_Campos();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                int n = 0;
                string unidade = "";
                foreach (int i in clbUnidades.CheckedIndices)
                {
                    n++;
                    unidade = clbUnidades.Text;
                    #region Select Descricao das Unidades
                    sql = "SELECT * FROM unidade WHERE nome='" + unidade + "'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    /*Carrega os Grupos na ListView*/
                    while (reader.Read())
                    {
                        MessageBox.Show("Unidade: " + (reader["nome"]).ToString() + "\nDescrição: " + (reader["descricao"]).ToString());
                    }
                    #endregion
                }

                if (n == 0) {
                    MessageBox.Show("Selecione uma unidade para saber mais sobre.");
                }

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }            
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string participante = cbbParticipante.Text;
            string id_participante = "";
            string descricao = txtDescricao.Text;
            string objeto = cbbObjeto.Text;
            string id_objeto = "";
            string unidade = "";
            string id_unidade = "";
            string id_entrada = "";

            int x = 0;
            int check = 0;
            Dictionary<string, int> unidades = new Dictionary<string, int>();

            //Cria um dicionário com as unidades, dessa forma, podemos saber se o usuário excluiu ou adicionou uma unidade
            foreach (object listBoxItem in clbUnidades.Items)
            {
                unidade = listBoxItem.ToString();
                if (clbUnidades.CheckedItems.Contains(listBoxItem)) //se está marcado como check 1
                {
                    check = 1;
                }
                else
                {
                    check = 0;
                }
                unidades.Add(unidade, check);
                x++;
            }

            if (x == 0) {
                MessageBox.Show("Selecione pelo menos uma unidade de análise.");
                return;
            }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                #region Select Participante (id_participante)
                sql = "SELECT * FROM participante WHERE nome='" + participante + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    id_participante = (reader["id_participante"]).ToString();
                #endregion

                #region Select Objeto (id_objeto)
                sql = "SELECT * FROM objeto WHERE nome='" + objeto + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_objeto = (reader["id_objeto"]).ToString();
                #endregion


                #region Select, Update and Insert of Entrada
                int n = 0;
                sql = "SELECT * FROM entrada WHERE id_participante=" + id_participante + " AND id_objeto = " + id_objeto;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Conta quantos encontrou
                while (reader.Read())
                    n++;

                if (n > 0)
                {
                    sql = "UPDATE entrada SET descricao = '" + descricao + "' WHERE id_participante=" + id_participante + " AND id_objeto = " + id_objeto;
                }
                else
                {
                    sql = "INSERT INTO entrada (id_entrada, id_participante, descricao, id_objeto) VALUES (NULL, '" + id_participante + "' , '" + descricao + "', " + id_objeto + ")";
                }

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                #endregion

                #region Select Entrada (id_entrada)                
                sql = "SELECT * FROM entrada WHERE id_participante=" + id_participante + " AND id_objeto = " + id_objeto;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Conta quantos encontrou
                while (reader.Read())
                    id_entrada = (reader["id_entrada"]).ToString(); ;
                #endregion

                #region Insert Update de Entrada_Unidade
                foreach (KeyValuePair<string, int> item in unidades)
                {
                    unidade = item.Key;

                    #region Select Unidade (id_unidade)                
                    sql = "SELECT * FROM unidade WHERE nome='" + unidade + "'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    //Conta quantos encontrou
                    while (reader.Read())
                        id_unidade = (reader["id_unidade"]).ToString(); ;
                    #endregion

                    n = 0;
                    sql = "SELECT * FROM entrada_unidade WHERE id_entrada=" + id_entrada + " AND id_unidade = " + id_unidade;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    //Conta quantos encontrou
                    while (reader.Read())
                        n++;

                    if (n == 0 && item.Value == 1)
                    {
                        sql = "INSERT INTO entrada_unidade (id_entrada, id_unidade, descricao) VALUES (" + id_entrada + " , " + id_unidade + ", 'teste')";
                    }
                    else
                    {
                        if (item.Value == 0) //Se existe e está com o valor zero, o usuário remeveu essa unidade
                            sql = "DELETE FROM entrada_unidade WHERE id_entrada = " + id_entrada + " AND id_unidade = " + id_unidade;
                    }

                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                #endregion

                m_dbConnection.Close();

                Carrega_Entradas();
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

            string entrada = lstEntradas.SelectedItem.ToString();
            string id_entrada = "";
            string objeto = cbbObjeto.Text;
            string id_objeto = "";
            string participante = "";
            string id_participante = "";
            string descricao = "";
            string unidade = "";
            string id_unidade = "";

            //separa o nome do participante
            int pFrom = entrada.IndexOf("(") + "(".Length;
            int pTo = entrada.IndexOf(")");

            participante = entrada.Substring(pFrom, pTo - pFrom);

            Limpa_Campos();

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                #region Select Participante (id_participante)
                sql = "SELECT * FROM participante WHERE nome='" + participante + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_participante = (reader["id_participante"]).ToString();
                #endregion

                #region Select Objeto (id_objeto)
                sql = "SELECT * FROM objeto WHERE nome='" + objeto + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                    id_objeto = (reader["id_objeto"]).ToString();
                #endregion

                #region Select Entrada
                sql = "SELECT * FROM entrada WHERE id_participante=" + id_participante + " AND id_objeto = " + id_objeto;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Conta quantos encontrou
                while (reader.Read()) {
                    id_entrada = (reader["id_entrada"]).ToString();
                    descricao = (reader["descricao"]).ToString();
                }
                #endregion

                List<int> indices = new List<int>();

                #region Select Unidades da Entrada
                sql = "SELECT * FROM entrada_unidade WHERE id_entrada=" + id_entrada;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                indices.Clear();

                //Conta quantos encontrou
                while (reader.Read())
                {
                    id_unidade = (reader["id_unidade"]).ToString();

                    #region Select Unidade
                    sql = "SELECT * FROM unidade WHERE id_unidade=" + id_unidade;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader rdUnidade = command.ExecuteReader();
                    
                    while (rdUnidade.Read())
                    {
                        unidade = (rdUnidade["nome"]).ToString();
                        //MessageBox.Show(unidade);                        
                    }
                    #endregion
                    int i = 0;
                    foreach (object listBoxItem in clbUnidades.Items)
                    {
                        if (!clbUnidades.CheckedItems.Contains(listBoxItem) && (String.Compare(listBoxItem.ToString(), unidade) == 0))
                        {
                            //clbUnidades.SetItemCheckState(i, CheckState.Checked);
                            indices.Add(i);
                        }
                        i++;
                    }
                }

                foreach (int i in indices) {
                    clbUnidades.SetItemCheckState(i, CheckState.Checked);
                }
                #endregion
                
                m_dbConnection.Close();

                txtDescricao.Text = descricao;
                cbbParticipante.Text = participante;
                cbbParticipante.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void lstEntradas_DoubleClick(object sender, EventArgs e)
        {
            if (lstEntradas.SelectedItem != null)
            {
                btnAlterar.PerformClick();
            }
        }
    }
}
