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
    public partial class RelObjetos : Form
    {
        public RelObjetos()
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
                else {
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

                #region Select Objetos
                sql = "SELECT * FROM objeto" + where;
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                //Limpa combobox
                cbbObjeto.Items.Clear();
                cbbObjeto.Items.Add("Todos");

                while (reader.Read())
                {
                    cbbObjeto.Items.Add(reader["nome"]);
                }
                #endregion

                if (cbbObjeto.Items.Count <= 1)
                {
                    cbbObjeto.Items.Clear();
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

            string objeto = cbbObjeto.Text;
            //string id_objeto = "";
            string grupo = cbbGrupo.Text;
            string id_grupo = "";
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

                    List<string> id_objetos = new List<string>();
                    #region Select objeto (id_objeto)

                    if (String.Compare(objeto, "Todos") == 0)
                    {
                        where = " WHERE id_grupo = " + id_grupo;
                    }
                    else
                    {
                        where = "WHERE id_grupo = " + id_grupo + " AND nome = '" + objeto + "'";
                    }
                    sql = "SELECT * FROM objeto " + where;
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        id_objetos.Add((reader["id_objeto"]).ToString());
                    }
                    #endregion

                    //MessageBox.Show("Quantidade de Objetos: " + id_objetos.Count);

                    List<string> id_entradas = new List<string>();
                    foreach (string id_objeto in id_objetos) {
                        #region Select entrada (id_entrada)
                        sql = "SELECT * FROM entrada WHERE id_objeto=" + id_objeto;
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        //MessageBox.Show("Id: " + id_objeto);

                        while (reader.Read())
                        {
                            id_entradas.Add((reader["id_entrada"]).ToString());
                            //MessageBox.Show("Achou");
                            //MessageBox.Show("id_entrada " + (reader["id_entrada"]).ToString() + " participante: " + (reader["id_participante"]).ToString() + " objeto: " + (reader["id_objeto"]).ToString());
                        }
                        #endregion
                    }

                    string busca = "";

                    foreach (string id_entrada in id_entradas) {
                        #region Select Unidade (id_unidade)
                        sql = "SELECT id_unidade FROM entrada_unidade WHERE id_entrada="+ id_entrada;
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
                    lstUnidades.Items.Add(new ListViewItem(new[] { "Nenhuma entrada qualificada.", ""}));
                }


                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

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

                Carrega_Objetos();
                if (cbbObjeto.Items.Count > 0) 
                    cbbObjeto.SelectedIndex = 0;

                if (String.Compare(grupo, "Todos") == 0)
                {
                    cbbObjeto.Enabled = false;
                }
                else {
                    cbbObjeto.Enabled = true;
                    cbbObjeto_SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }
        }

        private void cbbObjeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            string objeto = cbbObjeto.Text;

            if (cbbObjeto.Items.Count > 0)
            {
                try
                {
                    m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                    m_dbConnection.Open();
                    SQLiteDataReader reader;

                    if (String.Compare(objeto, "Todos") == 0)
                    {
                        lblDescObj.Text = "Quantificação de Todos os Objetos";
                    }
                    else
                    {
                        #region Select Objetos
                        sql = "SELECT * FROM objeto WHERE nome = '" + objeto + "'";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();

                        lblDescObj.Text = "";

                        while (reader.Read())
                        {
                            lblDescObj.Text = (reader["descricao"]).ToString();
                        }
                        #endregion
                    }

                    m_dbConnection.Close();

                    Carrega_Quantificacao();
                }
                catch
                {
                    MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
                }
            }
            else {
                lblDescObj.Text = "Não existem objetos cadastrados nesse grupo";
                lstUnidades.Items.Clear();
            }
        }

        private void RelObjetos_Load(object sender, EventArgs e)
        {
            Carrega_Grupos();
            cbbGrupo.SelectedIndex = 0;
        }
    }
}
