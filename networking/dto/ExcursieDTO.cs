using System;
using System.Runtime.Serialization;

namespace network { 
    [Serializable]
    public class ExcursieDTO : ISerializable
    {
        private long id;
        private string obiectiv;
        private string firmaTransport;
        private string oraPlecare;
        private int nrLocuri;
        private int pret;
        private int locuriLibere;

        public ExcursieDTO(long id, string obiectiv, string firmaTransport, string oraPlecare, int nrLocuriDisponibile, int pret, int locuriLibere)
        {
            this.id = id;
            this.obiectiv = obiectiv;
            this.firmaTransport = firmaTransport;
            this.oraPlecare = oraPlecare;
            this.nrLocuri = nrLocuriDisponibile;
            this.pret = pret;
            this.locuriLibere = locuriLibere;
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Obiectiv
        {
            get { return obiectiv; }
            set { obiectiv = value; }
        }

        public string FirmaTransport
        {
            get { return firmaTransport; }
            set { firmaTransport = value; }
        }

        public string OraPlecare
        {
            get { return oraPlecare; }
            set { oraPlecare = value; }
        }

        public int NrLocuri
        {
            get { return nrLocuri; }
            set { nrLocuri = value; }
        }

        public int Pret
        {
            get { return pret; }
            set { pret = value; }
        }

        public int LocuriLibere
        {
            get { return locuriLibere; }
            set { locuriLibere = value; }
        }

        public override string ToString()
        {
            return $"ExcursieDTO[{obiectiv} {firmaTransport} {oraPlecare.ToString()} {nrLocuri.ToString()} {pret.ToString()} {locuriLibere.ToString()}]";
        }

        // Required for serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", id);
            info.AddValue("Obiectiv", obiectiv);
            info.AddValue("FirmaTransport", firmaTransport);
            info.AddValue("OraPlecare", oraPlecare);
            info.AddValue("NrLocuri", nrLocuri);
            info.AddValue("Pret", pret);
            info.AddValue("LocuriLibere", locuriLibere);
        }

        // Required for deserialization
        public ExcursieDTO(SerializationInfo info, StreamingContext context)
        {
            obiectiv = (string)info.GetValue("Obiectiv", typeof(string));
            firmaTransport = (string)info.GetValue("FirmaTransport", typeof(string));
            oraPlecare = (string)info.GetValue("OraPlecare", typeof(string));
            nrLocuri = (int)info.GetValue("NrLocuri", typeof(int));
            pret = (int)info.GetValue("Pret", typeof(int));
            locuriLibere = (int)info.GetValue("LocuriLibere", typeof(int));
        }
    }
}