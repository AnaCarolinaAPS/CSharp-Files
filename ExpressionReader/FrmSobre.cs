using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressionReader
{
    public partial class FrmSobre : Form
    {
        public FrmSobre()
        {
            InitializeComponent();
        }

        string textoNormal = "";
        string textoExtendido = "";

        private void FrmSobre_Load(object sender, EventArgs e)
        {

            textoNormal = "O SAAPC (Sistema de Apoio e Análise para Pesquisas Científicas) tem\n";
            textoNormal = textoNormal + " por objetivo auxiliar na organização, qualificação e quantificação de\n";
            textoNormal = textoNormal + " dados obtidos atráves de projetos de pesquisa científica.\n";
            textoNormal = textoNormal + "\n";
            textoNormal = textoNormal + "\n";
            textoNormal = textoNormal + "\n";
            textoNormal = textoNormal + "Este software é livre e se encontra na versão 1.0.\n";
            textoNormal = textoNormal + "\n";
            textoNormal = textoNormal + "Desenvolvido por Ana Carolina dos Anjos Pereira da Silva em 2017.\n";

            textoExtendido = "O SAAPC (Sistema de Apoio e Análise para Pesquisas Científicas) tem\n";
            textoExtendido = textoExtendido + " por objetivo auxiliar na organização, qualificação e quantificação de\n";
            textoExtendido = textoExtendido + " dados obtidos atráves de projetos de pesquisa científica.\n";
            textoExtendido = textoExtendido + "\n";
            textoExtendido = textoExtendido + "Nele você poderá cadastrar dados obtidos através de questionários,\n";
            textoExtendido = textoExtendido + " entrevistas, materiais didáticos, etc. Para isso, você precisa: \n";
            textoExtendido = textoExtendido + "\n";
            textoExtendido = textoExtendido + " - Participantes (pessoas que contribuiram para os resultados)\n";
            textoExtendido = textoExtendido + " - Grupos de Participantes (ex.: pessoas com conhecimento específico)\n";
            textoExtendido = textoExtendido + " - Objetos de Estudo (ex.: textos, questionários, entrevistas...)\n";
            textoExtendido = textoExtendido + " - Unidades de Análise (como você pretende qualificar os dados)\n";
            textoExtendido = textoExtendido + " - Entradas de Dados (o que o participante respondeu no objeto \n";
            textoExtendido = textoExtendido + "   e como qualificar/categorizar)\n";
            textoExtendido = textoExtendido + "\n";
            textoExtendido = textoExtendido + "\n";
            textoExtendido = textoExtendido + "Este software é livre e se encontra na versão 1.0.\n";
            textoExtendido = textoExtendido + "\n";
            textoExtendido = textoExtendido + "Desenvolvido por Ana Carolina dos Anjos Pereira da Silva em 2017.\n";

            lblText.Text = textoNormal;
        }

        private void btnDicas_Click(object sender, EventArgs e)
        {
            lblText.Text = textoExtendido;
            btnDicas.Hide();
            this.Size = this.MaximumSize;
        }
    }
}
