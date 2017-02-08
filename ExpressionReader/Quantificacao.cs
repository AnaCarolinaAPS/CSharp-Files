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

            //try {
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

            //        /*Tabela Objeto de Estudo*/
            //        sql = "CREATE TABLE IF NOT EXISTS objeto (id_objeto INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(50), PRIMARY KEY(id_objeto))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();

            //        /*Tabela Participante*/
            //        sql = "CREATE TABLE IF NOT EXISTS participante (id_participante INTEGER NOT NULL, id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, curso VARCHAR(30), PRIMARY KEY(id_participante), FOREIGN KEY(id_grupo) REFERENCES grupo(id_grupo))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();

            //        /*Tabela Grupo*/
            //        sql = "CREATE TABLE IF NOT EXISTS grupo (id_grupo INTEGER NOT NULL, nome VARCHAR(30) NOT NULL, descricao VARCHAR(30), PRIMARY KEY(id_grupo))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();

            //        /*Tabela Entrada*/
            //        sql = "CREATE TABLE IF NOT EXISTS entrada (id_entrada INTEGER NOT NULL, id_participante INTEGER NOT NULL, descricao TEXT, PRIMARY KEY(id_entrada), FOREIGN KEY(id_participante) REFERENCES participante(id_participante))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();

            //        /*Tabela Entrada_UNIDADE*/
            //        sql = "CREATE TABLE IF NOT EXISTS entrada_unidade (id_entrada INTEGER NOT NULL, id_unidade INTEGER NOT NULL, descricao TEXT, PRIMARY KEY(id_entrada, id_unidade), FOREIGN KEY(id_entrada) REFERENCES entrada(id_entrada), FOREIGN KEY(id_unidade) REFERENCES unidade(id_unidade))";
            //        command = new SQLiteCommand(sql, m_dbConnection);
            //        command.ExecuteNonQuery();
            //        #endregion

            //        try {
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
            //        catch {
            //            MessageBox.Show("Não foi possivel inserir dados na tabela!!");
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Não foi possivel criar as tabelas!!");
            //    }
            //}
            //catch {
            //    MessageBox.Show("Não foi criado o banco!!");
            //    this.Close();
            //}
        }
    }
}
/*
             // These examples assume a "C:\Users\Public\TestFolder" folder on your machine.
            // You can modify the path if necessary.


            // Example #1: Write an array of strings to a file.
            // Create a string array that consists of three lines.
            string[] lines = { "First line", "Second line", "Third line" };
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllLines(@"C:\Users\usuario\Desktop\Quantificacao Pesquisa\WriteLines.txt", lines);


            // Example #2: Write one string to a text file.
            string text = "A class is the most powerful data type in C#. Like a structure, " +
                           "a class defines the data and behavior of the data type. ";
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllText(@"C:\Users\usuario\Desktop\Quantificacao Pesquisa\WriteText.txt", text);

            // Example #3: Write only some strings in an array to a file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            // NOTE: do not use FileStream for text files because it writes bytes, but StreamWriter
            // encodes the output as text.
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\usuario\Desktop\Quantificacao Pesquisa\WriteLines2.txt"))
            {
                foreach (string line in lines)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }
                }
            }

            // Example #4: Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\usuario\Desktop\Quantificacao Pesquisa\WriteLines2.txt", true))
            {
                file.WriteLine("Fourth line");
            }
*/
