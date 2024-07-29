using lab8.Domain;
using System;

namespace lab8.Domain
{
    public class Rezervare : Entity<long>
    {
        private long excursie;
        private string numeClient;
        private string nrTelefon;
        private int nrLocuri;

        public Rezervare(long id, long excursie, string numeClient, string nrTelefon, int nrLocuri):  base(id)
        {
            this.excursie = excursie;
            this.numeClient = numeClient;
            this.nrTelefon = nrTelefon;
            this.nrLocuri = nrLocuri;
        }

        public long Excursie
        {
            get { return excursie; }
            set { excursie = value; }
        }

        public string NumeClient
        {
            get { return numeClient; }
            set { numeClient = value; }
        }

        public string NrTelefon
        {
            get { return nrTelefon; }
            set { nrTelefon = value; }
        }

        public int NrLocuri
        {
            get { return nrLocuri; }
            set { nrLocuri = value; }
        }
    }
}