using System;

namespace network { 
    [Serializable]
    public class RezervareDTO
    {
        private long id;
        private long idExcursie;
        private string numeClient;
        private string nrTelefon;
        private int nrBilete;

        public RezervareDTO(long idExcursie, string client, string nrTel, int nrBilete)
        {
            this.idExcursie = idExcursie;
            this.numeClient = client;
            this.nrTelefon = nrTel;
            this.nrBilete = nrBilete;
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public long IdExcursie
        {
            get { return idExcursie; }
        }

        public string NumeClient
        {
            get { return numeClient; }
        }

        public int NrBilete
        {
            get { return nrBilete; }
        }

        public string NrTelefon
        {
            get { return nrTelefon; }
        }

        public override string ToString()
        {
            return $"RezervareDTO[{idExcursie} {numeClient} {nrTelefon} {nrBilete}]";
        }
    }
}