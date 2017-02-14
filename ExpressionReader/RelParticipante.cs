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
    public partial class RelParticipante : Form
    {
        public RelParticipante()
        {
            InitializeComponent();
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
                SQLiteDataReader reader;

                #region Select Objetos
                sql = "SELECT * FROM grupo ORDER BY nome";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbGrupo.Items.Clear();
                cbbGrupo.Items.Add("Todos");

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    cbbGrupo.Items.Add(reader["nome"]);
                }
                #endregion

                if (cbbGrupo.Items.Count > 1)
                {
                    cbbGrupo.SelectedIndex = 0;
                }
                else
                {
                    cbbGrupo.Items.Clear();
                }

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

            string grupo = cbbGrupo.Text;
            string id_grupo = "";
            string where = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                if (String.Compare(grupo, "Todos") == 0)
                {
                    where = " ORDER BY nome";
                }
                else
                {
                    #region
                    sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_grupo = (reader["id_grupo"]).ToString();
                    }
                    #endregion

                    where = " WHERE id_grupo = " + id_grupo + " ORDER BY nome";
                }

                #region Select Participante
                sql = "SELECT * FROM participante" + where;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbParticipante.Items.Clear();
                cbbParticipante.Items.Add("Todos");

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    cbbParticipante.Items.Add(reader["nome"]);
                }
                #endregion

                if (cbbParticipante.Items.Count > 1)
                {
                    cbbParticipante.SelectedIndex = 0;
                }
                else
                {
                    cbbParticipante.Items.Clear();
                }
                     
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        void Carrega_Quantificacao()
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string grupo = cbbGrupo.Text;
            string id_grupo = "";
            string participante = cbbParticipante.Text;
            string where = "";

            Dictionary<string, int> id_unidades = new Dictionary<string, int>();

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                if (String.Compare(grupo, "Todos") == 0)
                {
                    #region Select Entrada_Unidade (COUNT)
                    sql = "SELECT id_unidade, COUNT(*) FROM entrada_unidade GROUP BY id_unidade";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_unidades.Add((reader["id_unidade"]).ToString(), Convert.ToInt32(reader["COUNT(*)"]));
                    }
                    #endregion
                }
                else
                {
                    #region Select grupo (id_grupo)
                    sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_grupo = (reader["id_grupo"]).ToString();
                    }
                    #endregion
                                        
                    #region Select participantes (id_participante)
                    List<string> id_participantes = new List<string>();

                    if (String.Compare(participante, "Todos") == 0)
                    {
                        where = "WHERE id_grupo=" + id_grupo;
                    }
                    else
                    {
                        where = "WHERE id_grupo=" + id_grupo + " AND nome = '" + participante + "'";
                    }

                    sql = "SELECT * FROM participante " + where;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_participantes.Add((reader["id_participante"]).ToString());
                    }
                    #endregion

                    List<string> id_entradas = new List<string>();
                    foreach (string id_participante in id_participantes)
                    {
                        #region Select entrada (id_entrada)
                        sql = "SELECT * FROM entrada WHERE id_participante=" + id_participante;
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            id_entradas.Add((reader["id_entrada"]).ToString());
                        }
                        #endregion
                        MessageBox.Show("id: " + id_participante);
                    }

                    string busca = "";

                    foreach (string id_entrada in id_entradas)
                    {
                        #region Select Unidade (id_unidade)
                        sql = "SELECT id_unidade FROM entrada_unidade WHERE id_entrada=" + id_entrada;
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            busca = (reader["id_unidade"]).ToString();
                            //Soma no Dictionary 
                            if (id_unidades.ContainsKey(busca))
                            {
                                id_unidades[busca] += 1;
                            }
                            else
                            {
                                id_unidades.Add(busca, 1);
                            }
                        }
                        #endregion
                    }
                }

                lstUnidades.Items.Clear();

                if (id_unidades.Count > 0)
                {
                    foreach (KeyValuePair<string, int> item in id_unidades)
                    {
                        #region Select Unidades (nome)
                        sql = "SELECT * FROM unidade WHERE id_unidade='" + item.Key + "'";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            lstUnidades.Items.Add(new ListViewItem(new[] { (reader["nome"]).ToString(), (item.Value).ToString() }));
                        }
                        #endregion
                    }
                }
                else
                {
                    lstUnidades.Items.Add(new ListViewItem(new[] { "Nenhuma entrada qualificada.", "" }));
                }


                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

        }

        private void RelUnidade_Load(object sender, EventArgs e)
        {
            Carrega_Grupos();
        }

        private void cbbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string grupo = cbbGrupo.Text;
            string id_grupo = "";
            string where = "";

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                if (String.Compare(grupo, "Todos") == 0)
                {
                    where = ""; //count sem where, ou seja, todos
                }
                else
                {
                    #region Select grupo (id_grupo)
                    sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_grupo = (reader["id_grupo"]).ToString();
                    }
                    #endregion

                    where = " WHERE id_grupo = " + id_grupo + " GROUP BY id_grupo";
                }

                #region Select Quantidade de Participantes de um grupo
                sql = "SELECT COUNT(*) FROM participante" + where;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                lblQtdParticipantes.Text = "0";

                /*Carrega a Quantidade de Participantes*/
                while (reader.Read())
                {
                    lblQtdParticipantes.Text = (reader["COUNT(*)"]).ToString();
                }
                #endregion

                m_dbConnection.Close();

                if (String.Compare(lblQtdParticipantes.Text, "0") == 0)
                {
                    cbbParticipante.Items.Clear();
                    cbbParticipante.Items.Add("Não existem participantes");
                    cbbParticipante.Text = "Não existem participantes";
                    cbbParticipante.Enabled = false;
                    lblQtdEntradas.Text = "0";
                }
                else
                {
                    Carrega_Participantes();
                    if (String.Compare(grupo, "Todos") == 0)
                    {
                        cbbParticipante.Enabled = false;
                    }
                    else
                    {
                        cbbParticipante.Enabled = true;
                        cbbParticipante_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

        }

        private void cbbParticipante_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string participante = cbbParticipante.Text;
            string grupo = cbbGrupo.Text;
            string id_grupo = "";
            string where = "";
            int qtdEntrada = 0;

            if (cbbParticipante.Items.Count > 0)
            {
                try
                {
                    m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                    m_dbConnection.Open();
                    SQLiteDataReader reader;

                    if (String.Compare(grupo, "Todos") == 0) //Todos as entradas de todos os participantes
                    {
                        #region Select Entrada_Unidade (COUNT)
                        sql = "SELECT * FROM entrada";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            qtdEntrada++;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Select grupo (id_grupo)
                        sql = "SELECT * FROM grupo WHERE nome='" + grupo + "'";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            id_grupo = (reader["id_grupo"]).ToString();
                        }
                        #endregion

                        #region Select participantes (id_participante)
                        List<string> id_participantes = new List<string>();

                        if (String.Compare(participante, "Todos") == 0)
                        {
                            where = "WHERE id_grupo=" + id_grupo;
                        }
                        else
                        {
                            where = "WHERE id_grupo=" + id_grupo + " AND nome = '" + participante + "'";
                        }
                        sql = "SELECT * FROM participante " + where;
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            id_participantes.Add((reader["id_participante"]).ToString());
                        }
                        #endregion

                        //MessageBox.Show("Qtd Participantes: " + id_participantes.Count);

                        qtdEntrada = 0;

                        List<string> id_entradas = new List<string>();
                        foreach (string id_participante in id_participantes)
                        {
                            #region Select entrada (id_entrada)
                            sql = "SELECT COUNT(*) FROM entrada WHERE id_participante=" + id_participante;
                            command = new SQLiteCommand(sql, m_dbConnection);
                            reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                qtdEntrada = qtdEntrada + Convert.ToInt32(reader["COUNT(*)"]);
                            }
                            #endregion
                        }
                    }
                    m_dbConnection.Close();

                    lblQtdEntradas.Text = qtdEntrada.ToString();
                    Carrega_Quantificacao();
                }
                catch
                {
                    MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
                }
            }
            else
            {
                lblQtdEntradas.Text = "0 Nenhum participante nesse grupo possui entradas";
                lstUnidades.Items.Clear();
            }
        }
    }
}
