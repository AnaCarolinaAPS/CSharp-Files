using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ExpressionReader
{
    public partial class Quantificacao : Form
    {
        Dictionary<string, int> dUnidades = new Dictionary<string, int>();

        public Quantificacao()
        {
            InitializeComponent();
        }

        private void btnEnvia_Click(object sender, EventArgs e)
        {
            txtResults.Clear();
            string sTexto = "";
            sTexto = txtText.Text.ToLower();

            //Para ajudar a separar por Titulo
            sTexto = Regex.Replace(sTexto, System.Environment.NewLine, ",");
            string[] unidades = sTexto.Split(',');

            StringComparison comparison = StringComparison.InvariantCulture;
            foreach (string sUnidade in unidades)
            {
                string sTrim = sUnidade.Trim();

                if (sTrim.StartsWith("\"", comparison))
                {
                    sTrim = sTrim.Substring(1);
                }
                else if (sTrim.EndsWith("\"", comparison))
                {
                    sTrim = sTrim.Substring(0, sTrim.Length - 1);
                }

                //txtResults.Text += sTrim + "\r\n";

                //Adiciona no Dictionary
                if (dUnidades.ContainsKey(sTrim))
                {
                    dUnidades[sTrim] += 1;
                }
                else {
                    dUnidades.Add(sTrim, 1);
                }
            }

            txtResults.Text += "---------------------------------------\r\n";
            foreach (KeyValuePair<string, int> pair in dUnidades)
            {
                txtResults.Text += pair.Key + "\r\n";
                txtResults.Text += "---------------------------------------\r\n";
                txtResults.Text += pair.Value + "\r\n";
                txtResults.Text += "---------------------------------------\r\n";
            }

            txtText.Clear();
            txtText.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Example #2: Write one string to a text file.
            string text = txtResults.Text.ToString();
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllText(@"C:\Users\usuario\Desktop\Quantificacao Pesquisa\_Resultados\Parciais.txt", text);
            txtResults.Clear();
            dUnidades.Clear();
        }

        private void btnBanco_Click(object sender, EventArgs e)
        {
            /*CadUnidade cad = new CadUnidade();
            cad.Show();*/

            /*CadGrupo cad = new CadGrupo();
            cad.Show();*/

            /*CadParticipante cad = new CadParticipante();
            cad.Show();*/

            MainMenu cad = new MainMenu();
            cad.Show();



            //string sql;
            //SQLiteCommand command;

            //try
            //{
            //    SQLiteConnection.CreateFile("apsa.sqlite");
            //    SQLiteConnection m_dbConnection;
            //    m_dbConnection = new SQLiteConnection("Data Source=apsa.sqlite;Version=3;");
            //    m_dbConnection.Open();

            //    try
            //    {
            //        #region tabelas
            //        /*Tabela Unidade de Analise*/
            //        sql = "CREATE TABLE IF NOT EXISTS unidade (id_unidade INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(50), PRIMARY KEY(id_unidade))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Criada tabela Unidade");

            //        /*Tabela Grupo*/
            //        sql = "CREATE TABLE IF NOT EXISTS grupo (id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(30), PRIMARY KEY(id_grupo))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Criada tabela Grupo");

            //        /*Tabela Participante*/
            //        sql = "CREATE TABLE IF NOT EXISTS participante (id_participante INTEGER NOT NULL, id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, curso VARCHAR(30), PRIMARY KEY(id_participante), FOREIGN KEY(id_grupo) REFERENCES grupo(id_grupo))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Criada tabela Participante");

            //        /*Tabela Objeto de Estudo*/
            //        sql = "CREATE TABLE IF NOT EXISTS objeto (id_objeto INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(50), id_grupo INTEGER NOT NULL, PRIMARY KEY(id_objeto), FOREIGN KEY(id_grupo) REFERENCES grupo(id_grupo))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Criada tabela Objeto");

            //        /*Tabela Entrada*/
            //        sql = "CREATE TABLE IF NOT EXISTS entrada (id_entrada INTEGER NOT NULL, id_participante INTEGER NOT NULL, descricao TEXT, id_objeto INTEGER NOT NULL, PRIMARY KEY(id_entrada), FOREIGN KEY(id_participante) REFERENCES participante(id_participante), FOREIGN KEY(id_objeto) REFERENCES objeto(id_objeto))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Criada tabela Entrada");

            //        /*Tabela Entrada_UNIDADE*/
            //        sql = "CREATE TABLE IF NOT EXISTS entrada_unidade (id_entrada INTEGER NOT NULL, id_unidade INTEGER NOT NULL, descricao TEXT, PRIMARY KEY(id_entrada, id_unidade), FOREIGN KEY(id_entrada) REFERENCES entrada(id_entrada), FOREIGN KEY(id_unidade) REFERENCES unidade(id_unidade))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Criada tabela Entrada_Unidade");
            //        #endregion

            //        try
            //        {
            //            sql = "INSERT INTO unidade (id_unidade, nome) values (NULL, 'Presença')";
            //            command = new SQLiteCommand(sql, m_dbConnection);
            //            command.ExecuteNonQuery();

            //            sql = "SELECT * FROM unidade ORDER BY nome";
            //            command = new SQLiteCommand(sql, m_dbConnection);
            //            SQLiteDataReader reader = command.ExecuteReader();

            //            string retorno = "";

            //            while (reader.Read())
            //                retorno = retorno + reader["nome"];

            //            txtResults.Text = retorno;

            //            MessageBox.Show("Banco Criado");

            //        }
            //        catch
            //        {
            //            MessageBox.Show("Não foi possivel inserir dados na tabela!!");
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Não foi possivel criar as tabelas!!");
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Não foi criado o banco!!");
            //    this.Close();
            //}
        }
    }
}