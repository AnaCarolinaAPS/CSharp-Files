using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressionReader
{
    public partial class Form1 : Form
    {
        Dictionary<string, int> dUnidades = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnvia_Click(object sender, EventArgs e)
        {
            string sTexto = "";
            sTexto = txtText.Text.ToLower();

            //Para ajudar a separar por Titulo
            string[] titulos = sTexto.Split('\n');

            List<String> lTitulos = new List<string>();

            foreach (string sTitulo in titulos)
            {
                if (sTitulo.Contains("unidade de análise"))
                {
                    int index = sTitulo.IndexOf("unidade de análise");
                    if (index >= 0)
                    {
                        string sCapitalCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((sTitulo).ToLower());
                        //txtResults.Text += sCapitalCase.Substring(0, index-1) + "\r\n";

                        lTitulos.Add(sCapitalCase.Substring(0, index - 1));
                    }
                }
            }

            //Para ajudar a separar as Discussoes
            string sTextoFull = "";
            sTextoFull = txtText.Text;

            string[] discussoes = sTextoFull.Split('\n');

            string[] aTitulos = new string[lTitulos.Count];
            int iTitulos = 0;

            //Fazer do lTitulos, um array de strings e ir cortando cada texto em outro... (talvez usar dictionary lTitulo, conteudo);
            foreach (string sTitulo in lTitulos)
            {
                aTitulos[iTitulos] = sTitulo;
                iTitulos++;
            }

            //Separando as dicussões por titulos
            Dictionary<string, string> dDiscussao = new Dictionary<string, string>();

            for (iTitulos = 0; iTitulos < lTitulos.Count; iTitulos++)
            {
                string sTitulo = aTitulos[iTitulos];
                if (sTextoFull.Contains(sTitulo))
                {
                    int index = sTextoFull.IndexOf(sTitulo);
                    if (index >= 0)
                    {
                        if ((iTitulos + 1) < lTitulos.Count)
                        {
                            int iFinal = sTextoFull.IndexOf(aTitulos[iTitulos + 1]);

                            index = index + (aTitulos[iTitulos]).Length + (",Unidade de Análise:").Length + 2; //+2 pelo enter +1 pela aspa
                            if (iFinal >= 0)
                            {
                                //txtResults.Text += "---------------------------------------\r\n";
                                //txtResults.Text += aTitulos[iTitulos] + "\r\n";
                                //txtResults.Text += "---------------------------------------\r\n";
                                //txtResults.Text += sTextoFull.Substring(index, iFinal - index - 5) + "\r\n";
                                //txtResults.Text += "---------------------------------------\r\n";
                                dDiscussao.Add(aTitulos[iTitulos], sTextoFull.Substring(index, iFinal - index - 5));
                            }
                        }
                        else
                        {
                            //txtResults.Text += "---------------------------------------\r\n";
                            //txtResults.Text += aTitulos[iTitulos] + "\r\n";
                            //txtResults.Text += "---------------------------------------\r\n";
                            //txtResults.Text += sTextoFull.Substring(index) + "\r\n";
                            //txtResults.Text += "---------------------------------------\r\n";
                            dDiscussao.Add(aTitulos[iTitulos], sTextoFull.Substring(index));
                        }
                    }
                }
            }

            string[] comentarios = dDiscussao["Cognição"].Split('"');

            txtResults.Text += "---------------------------------------\r\n";
            foreach (string pedaco in comentarios)
            {
                //Separação por PONTO 
                if (String.Compare(pedaco, ".") != 0 && String.Compare(pedaco, "\r\n") != 0)
                {
                    txtResults.Text += pedaco + "\r\n";
                    txtResults.Text += "---------------------------------------\r\n";
                }
            }

            //txtResults.Text = dDiscussao["Cognição"];

            /*
            foreach (KeyValuePair<string, string> pair in dDiscussao)
            {
                string sNewText = dDiscussao["Cognição"];//pair.Value;//dDiscussao["Cognição"];


                int valorPos = sNewText.IndexOf(@"""") + 1;

                if (valorPos > 0)
                {

                    string valorEntreAspas = sNewText.Substring(valorPos, sNewText.IndexOf(@"""", valorPos) - valorPos);

                    //Para retirar o que já foi encontrado.
                    sNewText = sNewText.Substring(sNewText.IndexOf(@"""", valorPos) + 2); //para retirar as aspas finais e a virgula

                    valorPos = sNewText.IndexOf(@"""") + 1;
                    string sUnidades = sNewText.Substring(valorPos, sNewText.IndexOf(@"""", valorPos) - valorPos);

                    try
                    {
                        sNewText = sNewText.Substring(sNewText.IndexOf(@"""", valorPos) + 3); //para retirar as aspas finais e a virgula
                    }
                    catch
                    {
                        sNewText = sNewText.Substring(sNewText.IndexOf(@"""", valorPos)); //para retirar as aspas finais e a virgula
                    }


                    txtResults.Text += "---------------------------------------\r\n";
                    txtResults.Text += pair.Key + "\r\n";
                    txtResults.Text += "---------------------------------------\r\n";
                    txtResults.Text += valorEntreAspas + "\r\n";
                    txtResults.Text += "---------------------------------------\r\n";
                    txtResults.Text += sUnidades + "\r\n";
                    txtResults.Text += "---------------------------------------\r\n";
                    //txtResults.Text += sNewText + "\r\n";
                }
                else {
                    break;

                }
            }*/



            /*
            string sUnidades = "";
            sUnidades = txtUnidades.Text.ToLower();

            //Separa as unidades pelo caractere "."
            string[] tokens = sUnidades.Split('.');

            //Calculando a quantidade
            //Dictionary<string, int> dUnidades = new Dictionary<string, int>();

            foreach (string sTUnidade in tokens)
            {
                if (dUnidades.ContainsKey(sTUnidade))
                {                   
                    dUnidades[sTUnidade] += 1;
                }
                else {
                    if (String.Compare(sTUnidade, "", true)>0) {
                        dUnidades.Add(sTUnidade, 1);
                    }                    
                }                
            }

            string sResults = "";
            foreach (var item in dUnidades)
            {
                string sKey = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((item.Key).ToLower());
                sResults += sKey + " - " + item.Value + "\n";                
            }

            //txtResults.Text = sResults; 
            
    */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Quantificacao frm = new Quantificacao();
            frm.Show();

        }
    }
}
