using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionReader.Objects
{
    class Interacao
    {
        private string sInteracao;
        private string sUnidadesInt;

        public string SInteracao
        {
            get
            {
                return sInteracao;
            }

            set
            {
                sInteracao = value;
            }
        }

        public string SUnidadesInt
        {
            get
            {
                return sUnidadesInt;
            }

            set
            {
                sUnidadesInt = value;
            }
        }
    }
}
