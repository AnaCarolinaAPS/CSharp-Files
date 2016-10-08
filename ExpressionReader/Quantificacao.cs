using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
