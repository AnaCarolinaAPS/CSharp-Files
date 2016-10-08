using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionReader.Objects
{
    class Discussao
    {
        private string sTitulo;
        private List<Interacao> lInteracao;

        public string STitulo
        {
            get
            {
                return sTitulo;
            }

            set
            {
                sTitulo = value;
            }
        }

        internal List<Interacao> LInteracao
        {
            get
            {
                return lInteracao;
            }

            set
            {
                lInteracao = value;
            }
        }
    }
}
