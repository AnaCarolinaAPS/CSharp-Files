using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ExpressionReader
{
    public partial class MainMenu : Form
    {
        private Splash splashScreen;
        private bool done = false;

        public MainMenu()
        {
            InitializeComponent();
            this.Load += new EventHandler(HandleFormLoad);
            this.splashScreen = new Splash();

            //System.Threading.Thread.Sleep(5000);

            //Splash.CloseSplash();
        }
        private void HandleFormLoad(object sender, EventArgs e)
        {
            this.Hide();
            Thread thread = new Thread(new ThreadStart(this.ShowSplashScreen));
            thread.Start();
            Hardworker worker = new Hardworker();
            worker.ProgressChanged += (o, ex) =>
            {
                this.splashScreen.UpdateProgress(ex.Progress);
            };
            worker.HardWorkDone += (o, ex) =>
            {
                done = true;
                this.Show();
            };
            worker.DoHardWork();
        }

        private void ShowSplashScreen()
        {
            splashScreen.Show();
            while (!done)
            {
                Application.DoEvents();
            }
            splashScreen.Close();
            this.splashScreen.Dispose();
        }

        void Cria_Banco() {
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
                    //MessageBox.Show("Criada tabela Unidade");

                    /*Tabela Grupo*/
                    sql = "CREATE TABLE IF NOT EXISTS grupo (id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(30), PRIMARY KEY(id_grupo))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Criada tabela Grupo");

                    /*Tabela Participante*/
                    sql = "CREATE TABLE IF NOT EXISTS participante (id_participante INTEGER NOT NULL, id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, curso VARCHAR(30), PRIMARY KEY(id_participante), FOREIGN KEY(id_grupo) REFERENCES grupo(id_grupo))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Criada tabela Participante");

                    /*Tabela Objeto de Estudo*/
                    sql = "CREATE TABLE IF NOT EXISTS objeto (id_objeto INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(50), id_grupo INTEGER NOT NULL, PRIMARY KEY(id_objeto), FOREIGN KEY(id_grupo) REFERENCES grupo(id_grupo))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Criada tabela Objeto");

                    /*Tabela Entrada*/
                    sql = "CREATE TABLE IF NOT EXISTS entrada (id_entrada INTEGER NOT NULL, id_participante INTEGER NOT NULL, descricao TEXT, id_objeto INTEGER NOT NULL, PRIMARY KEY(id_entrada), FOREIGN KEY(id_participante) REFERENCES participante(id_participante), FOREIGN KEY(id_objeto) REFERENCES objeto(id_objeto))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Criada tabela Entrada");

                    /*Tabela Entrada_UNIDADE*/
                    sql = "CREATE TABLE IF NOT EXISTS entrada_unidade (id_entrada INTEGER NOT NULL, id_unidade INTEGER NOT NULL, descricao TEXT, PRIMARY KEY(id_entrada, id_unidade), FOREIGN KEY(id_entrada) REFERENCES entrada(id_entrada), FOREIGN KEY(id_unidade) REFERENCES unidade(id_unidade))";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Criada tabela Entrada_Unidade");
                    #endregion

                    /*List<string> unidades = new List<string>();

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
                        foreach (string unidade in unidades)
                        {
                            sql = "INSERT INTO unidade (id_unidade, nome) values (NULL, '" + unidade + "')";
                            command = new SQLiteCommand(sql, m_dbConnection);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possivel inserir dados no Banco!");
                    }*/

                    MessageBox.Show("Banco de Dados criado com sucesso!");
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

        int qtdUnidades()
        {
            int nUnidade = 0;
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT COUNT(*) FROM unidade";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    nUnidade = Convert.ToInt32(reader["COUNT(*)"]);
                }
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

            return nUnidade;
        }

        int qtdGrupos() {
            int nGrupo = 0;
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT COUNT(*) FROM grupo";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    nGrupo = Convert.ToInt32(reader["COUNT(*)"]);
                }
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

            return nGrupo;
        }

        int qtdParticipantes()
        {
            int nParticipante = 0;
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT COUNT(*) FROM participante";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    nParticipante = Convert.ToInt32(reader["COUNT(*)"]);
                }
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

            return nParticipante;
        }

        int qtdObjetos()
        {
            int nObjeto = 0;
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT COUNT(*) FROM objeto";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    nObjeto = Convert.ToInt32(reader["COUNT(*)"]);
                }
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

            return nObjeto;
        }

        int qtdEntradas()
        {
            int nEntrada = 0;
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT COUNT(*) FROM entrada";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    nEntrada = Convert.ToInt32(reader["COUNT(*)"]);
                }
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

            return nEntrada;
        }

        int qtdEntradasUnidade()
        {
            int nEntrada = 0;
            string sql;
            SQLiteCommand command;
            SQLiteConnection m_dbConnection;

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteDataReader reader;

                sql = "SELECT COUNT(*) FROM entrada_unidade";
                command = new SQLiteCommand(sql, m_dbConnection);
                reader = command.ExecuteReader();

                /*Carrega os Grupos na ListView*/
                while (reader.Read())
                {
                    nEntrada = Convert.ToInt32(reader["COUNT(*)"]);
                }
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Erro! Não foi possível abrir a base de dados!");
            }

            return nEntrada;
        }

        private void participanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (qtdGrupos() <= 0)
            {
                MessageBox.Show("Não existem grupos cadastrados.\nPor favor, cadastre um grupo primeiro.", "Atenção");
                CadGrupo cad = new CadGrupo();
                cad.MdiParent = this;
                cad.Show();
            }
            else {
                CadParticipante cad = new CadParticipante();
                cad.MdiParent = this;
                cad.Show();
            }
        }

        private void objetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (qtdGrupos() <= 0)
            {
                MessageBox.Show("Não existem grupos cadastrados.\nPor favor, cadastre um grupo primeiro.", "Atenção");
                CadGrupo cad = new CadGrupo();
                cad.MdiParent = this;
                cad.Show();
            }
            else
            {
                CadObjeto cad = new CadObjeto();
                cad.MdiParent = this;
                cad.Show();
            }
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
            if (qtdGrupos() <= 0)
            {
                MessageBox.Show("Não existem grupos cadastrados.\nPor favor, cadastre um grupo primeiro.", "Atenção");
                CadGrupo cad = new CadGrupo();
                cad.MdiParent = this;
                cad.Show();
            }
            else
            {
                if (qtdParticipantes() <= 0)
                {
                    MessageBox.Show("Não existem participantes cadastrados.\nPor favor, cadastre um participante primeiro.", "Atenção");
                    CadParticipante cad = new CadParticipante();
                    cad.MdiParent = this;
                    cad.Show();
                }
                else
                {
                    if (qtdObjetos() <= 0)
                    {
                        MessageBox.Show("Não existem objetos cadastrados.\nPor favor, cadastre um objeto primeiro.", "Atenção");
                        CadObjeto cad = new CadObjeto();
                        cad.MdiParent = this;
                        cad.Show();
                    }
                    else
                    {
                        CadEntrada cad = new CadEntrada();
                        cad.MdiParent = this;
                        cad.Show();
                    }
                }
            }
        }

        private void resultadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (qtdEntradas() <= 0)
            {
                MessageBox.Show("Não existem entradas cadastradas.\nPor favor, cadastre uma entrada primeiro.", "Atenção");
                entradasToolStripMenuItem.PerformClick();
            }
            else
            {
                if (qtdEntradasUnidade() <= 0)
                {
                    MessageBox.Show("Não existem entradas qualificadas.\nPor favor, qualifique uma entrada primeiro.", "Atenção");
                    entradasToolStripMenuItem.PerformClick();
                }
                else
                {
                    RelObjetos rel = new RelObjetos();
                    rel.MdiParent = this;
                    rel.Show();
                }
            }
        }

        private void resultadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (qtdEntradas() <= 0)
            {
                MessageBox.Show("Não existem entradas cadastradas.\nPor favor, cadastre uma entrada primeiro.", "Atenção");
                entradasToolStripMenuItem.PerformClick();
            }
            else
            {
                if (qtdEntradasUnidade() <= 0)
                {
                    MessageBox.Show("Não existem entradas qualificadas.\nPor favor, qualifique uma entrada primeiro.", "Atenção");
                    entradasToolStripMenuItem.PerformClick();
                }
                else
                {
                    RelParticipante rel = new RelParticipante();
                    rel.MdiParent = this;
                    rel.Show();
                }
            }            
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSobre sobre = new FrmSobre();
            sobre.MdiParent = this;
            sobre.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            /*if (System.IO.File.Exists(@"apsa.sqlite"))
            {
                MessageBox.Show("EXISTE!");
            }
            else
            {
                MessageBox.Show("O Banco de dados necessário para funcionamento da aplicação será criado.\nPor favor, aguarde alguns segundos.", "Atenção");
                Cria_Banco();
            }*/
        }
    }
}
