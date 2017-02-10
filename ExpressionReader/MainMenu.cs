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
    }
}
