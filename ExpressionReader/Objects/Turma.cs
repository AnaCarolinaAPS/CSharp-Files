using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionReader.Objects
{
    class Turma
    {
        private string sNomeArquivo;
        private List<Discussao> lDiscussao;

        public string SNomeArquivo
        {
            get
            {
                return sNomeArquivo;
            }

            set
            {
                sNomeArquivo = value;
            }
        }

        public List<Discussao> LDiscussao
        {
            get
            {
                return lDiscussao;
            }

            set
            {
                lDiscussao = value;
            }
        }
    }
}
