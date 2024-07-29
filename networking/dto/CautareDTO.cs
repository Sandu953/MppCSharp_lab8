using System;

namespace network 
{
    [Serializable]
    public class CautareDTO
    {
        private string obiectiv;
        private string ora1;
        private string ora2;

        public CautareDTO(string obiectiv, string ora1, string ora2)
        {
            this.obiectiv = obiectiv;
            this.ora1 = ora1;
            this.ora2 = ora2;
        }

        public string Obiectiv
        {
            get { return obiectiv; }
            set { obiectiv = value; }
        }

        public string Ora1
        {
            get { return ora1; }
            set { ora1 = value; }
        }

        public string Ora2
        {
            get { return ora2; }
            set { ora2 = value; }
        }

        public override string ToString()
        {
            return $"CautareDTO[{obiectiv},{ora1},{ora2}]";
        }
    }
}
