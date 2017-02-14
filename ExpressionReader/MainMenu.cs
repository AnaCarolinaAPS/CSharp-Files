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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void participanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadParticipante cad = new CadParticipante();
            cad.MdiParent = this;
            cad.Show();
        }

        private void objetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadObjeto cad = new CadObjeto();
            cad.MdiParent = this;
            cad.Show();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadGrupo cad = new CadGrupo();
            cad.MdiParent = this;
            cad.Show();
        }

        private void unidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadUnidade cad = new CadUnidade();
            cad.MdiParent = this;
            cad.Show();
        }

        private void entradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadEntrada cad = new CadEntrada();
            cad.MdiParent = this;
            cad.Show();
        }

        private void resultadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelObjetos rel = new RelObjetos();
            rel.MdiParent = this;
            rel.Show();
        }

        private void resultadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RelParticipante rel = new RelParticipante();
            rel.MdiParent = this;
            rel.Show();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql;
            SQLiteCommand command;

            try
            {
                SQLiteConnection.CreateFile("apsa.sqlite");
                SQLiteConnection m_dbConnection;
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();

                try
                {
                    #region tabelas
                    /*Tabela Unidade de Analise*/
                    sql = "CREATE TABLE IF NOT EXISTS unidade (id_unidade INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(50), PRIMARY KEY(id_unidade))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Criada tabela Unidade");

                    /*Tabela Grupo*/
                    sql = "CREATE TABLE IF NOT EXISTS grupo (id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(30), PRIMARY KEY(id_grupo))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Criada tabela Grupo");

                    /*Tabela Participante*/
                    sql = "CREATE TABLE IF NOT EXISTS participante (id_participante INTEGER NOT NULL, id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, curso VARCHAR(30), PRIMARY KEY(id_participante), FOREIGN KEY(id_grupo) REFERENCES grupo(id_grupo))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Criada tabela Participante");

                    /*Tabela Objeto de Estudo*/
                    sql = "CREATE TABLE IF NOT EXISTS objeto (id_objeto INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(50), id_grupo INTEGER NOT NULL, PRIMARY KEY(id_objeto), FOREIGN KEY(id_grupo) REFERENCES grupo(id_grupo))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Criada tabela Objeto");

                    /*Tabela Entrada*/
                    sql = "CREATE TABLE IF NOT EXISTS entrada (id_entrada INTEGER NOT NULL, id_participante INTEGER NOT NULL, descricao TEXT, id_objeto INTEGER NOT NULL, PRIMARY KEY(id_entrada), FOREIGN KEY(id_participante) REFERENCES participante(id_participante), FOREIGN KEY(id_objeto) REFERENCES objeto(id_objeto))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Criada tabela Entrada");

                    /*Tabela Entrada_UNIDADE*/
                    sql = "CREATE TABLE IF NOT EXISTS entrada_unidade (id_entrada INTEGER NOT NULL, id_unidade INTEGER NOT NULL, descricao TEXT, PRIMARY KEY(id_entrada, id_unidade), FOREIGN KEY(id_entrada) REFERENCES entrada(id_entrada), FOREIGN KEY(id_unidade) REFERENCES unidade(id_unidade))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Criada tabela Entrada_Unidade");
                    #endregion

                    List<string> unidades = new List<string>();

                    unidades.Add("Presença");
                    unidades.Add("Não-Presença");
                    unidades.Add("Copresença");
                    unidades.Add("Telepresença");
                    unidades.Add("Espaço de Relação");
                    unidades.Add("Espaço de Convivência");
                    unidades.Add("Interação");
                    unidades.Add("Perturbação");
                    unidades.Add("Legitimação");
                    unidades.Add("Emocionar");
                    unidades.Add("Congruência");
                    unidades.Add("Significado no Cotidiano");


                    try
                    {
                        foreach (string unidade in unidades) {
                            sql = "INSERT INTO unidade (id_unidade, nome) values (NULL, '"+ unidade +"')";
                            command = new SQLiteCommand(sql, m_dbConnection);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Banco Criado");
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possivel inserir dados na tabela!!");
                    }
                }
                catch
                {
                    MessageBox.Show("Não foi possivel criar as tabelas!!");
                }
            }
            catch
            {
                MessageBox.Show("Não foi criado o banco!!");
                this.Close();
            }
        }
    }
}
