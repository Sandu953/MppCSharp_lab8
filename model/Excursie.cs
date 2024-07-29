using System;

namespace lab8.Domain
{
    public class Excursie : Entity<long>
    {
        private string obiectivTuristic;
        private string numeTransport;
        private string oraPlecare;
        private int pret;
        private int nrLocuri;
        private int locuriLibere;

        public Excursie(long id, string obiectivTuristic, string numeTransport, String oraPlecare,int pret, int nrLocuri, int locuriLibere) : base(id)
        {
            this.obiectivTuristic = obiectivTuristic;
            this.numeTransport = numeTransport;
            this.oraPlecare = oraPlecare;
            this.pret = pret;
            this.nrLocuri = nrLocuri;
            this.locuriLibere = locuriLibere;
            
        }

        public string ObiectivTuristic
        {
            get { return obiectivTuristic; }
            set { obiectivTuristic = value; }
        }

        public string NumeTransport
        {
            get { return numeTransport; }
            set { numeTransport = value; }
        }

        public string OraPlecare
        {
            get { return oraPlecare; }
            set { oraPlecare = value; }
        }

        public int Pret
        {
            get { return pret; }
            set { pret = value; }
        }

        public int NrLocuri
        {
            get { return nrLocuri; }
            set { nrLocuri = value; }
        }

        public int LocuriLibere
        {
            get { return locuriLibere; }
            set { locuriLibere = value; }
        }
    }
}