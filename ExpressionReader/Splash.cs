using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ExpressionReader
{
    public partial class Splash : Form
    {
        private static Thread _splashThread;
        private static Splash _splashForm;

        private delegate void ProgressDelegate(int progress);
        private ProgressDelegate del;

        public Splash()
        {
            InitializeComponent();
            this.pgbSplash.Maximum = 100;
            del = this.UpdateProgressInternal;
        }

        private void UpdateProgressInternal(int progress)
        {
            if (this.Handle == null)
            {
                return;
            }
            this.pgbSplash.Value = progress;
        }

        public void UpdateProgress(int progress)
        {
            this.Invoke(del, progress);
        }

        public static void ShowSplash() {
            if (_splashThread == null) {
                //show form new thread
                _splashThread = new Thread(new ThreadStart(DoShowSplash));
                _splashThread.IsBackground = true;
                _splashThread.Start();
            }
        }

        private static void DoShowSplash() {
            if (_splashForm == null) {
                _splashForm = new Splash();
                Application.Run(_splashForm);
            }
        }

        public static void CloseSplash() {
            if (_splashForm.InvokeRequired)
            {
                _splashForm.Invoke(new MethodInvoker(CloseSplash));
            }
            else
            {
                Application.ExitThread();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            lblSplash.Text = "Carregando Banco de Dados...";
            //BancoTimer.Enabled = true;
            pgbSplash.Value = 10;
        }

        /*
        private void BancoTimer_Tick(object sender, EventArgs e)
        {
            //lblSplash.Text = "Verificando Banco de Dados...";
            pgbSplash.Value += 10;

            if (pgbSplash.Value == 100) {
                BancoTimer.Enabled = false;
            }
        }

        private void UnidadeTimer_Tick(object sender, EventArgs e)
        {
            lblSplash.Text = "Carregando Unidades de Análise...";
            pgbSplash.Value += 15;
            Thread.Sleep(1000);
            UnidadeTimer.Enabled = false;
            GrupoTimer.Enabled = true;
        }

        private void GrupoTimer_Tick(object sender, EventArgs e)
        {
            lblSplash.Text = "Carregando Grupos de Participantes...";
            pgbSplash.Value += 15;
            Thread.Sleep(1000);
            GrupoTimer.Enabled = false;
            ParticipanteTimer.Enabled = true;
        }

        private void ParticipanteTimer_Tick(object sender, EventArgs e)
        {
            lblSplash.Text = "Carregando Participantes...";
            pgbSplash.Value += 15;
            Thread.Sleep(1000);
            ParticipanteTimer.Enabled = false;
            ObjetoTimer.Enabled = true;
        }

        private void ObjetoTimer_Tick(object sender, EventArgs e)
        {
            lblSplash.Text = "Carregando Objetos de Estudo...";
            pgbSplash.Value += 15;
            Thread.Sleep(1000);
            ObjetoTimer.Enabled = false;
            EntradaTimer.Enabled = true;
        }

        private void EntradaTimer_Tick(object sender, EventArgs e)
        {
            lblSplash.Text = "Carregando Entradas de Participantes...";
            pgbSplash.Value += 15;
            Thread.Sleep(1000);
            EntradaTimer.Enabled = false;
            this.Hide();
            MainMenu menu = new MainMenu();
            menu.ShowDialog();
            this.Close();
        }*/
    }
}
