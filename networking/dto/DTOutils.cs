using System;
using lab8.Domain;

namespace network 
{
    public static class DTOUtils
    {
        public static Agentie GetFromDTO(AgentieDTO agentieDTO)
        {
            long id = agentieDTO.Id;
            string user = agentieDTO.Username;
            return new Agentie(id,user);
        }

        public static AgentieDTO GetDTO(Agentie agentie)
        {
            string user = agentie.Username;
            long id = agentie.Id;
            return new AgentieDTO(id,user, "");
        }

        public static Excursie GetFromDTO(ExcursieDTO excursieDTO)
        {
            string obiectiv = excursieDTO.Obiectiv;
            string firmaTransport = excursieDTO.FirmaTransport;
            string oraPlecare = excursieDTO.OraPlecare;
            int nrLocuri = excursieDTO.NrLocuri;
            int pret = excursieDTO.Pret;
            int locuriLibere = excursieDTO.LocuriLibere;
            long id = excursieDTO.Id;
            return new Excursie(id,obiectiv, firmaTransport, oraPlecare, pret, nrLocuri, locuriLibere);
        }

        public static ExcursieDTO GetDTO(Excursie excursie)
        {
            string obiectiv = excursie.ObiectivTuristic;
            string firmaTransport = excursie.NumeTransport;
            string oraPlecare = excursie.OraPlecare;
            int nrLocuri = excursie.NrLocuri;
            int pret = excursie.Pret;
            int locuriLibere = excursie.LocuriLibere;
            long id = excursie.Id;
            return new ExcursieDTO(id,obiectiv, firmaTransport, oraPlecare, nrLocuri, pret, locuriLibere);
        }

        public static RezervareDTO GetDTO(Rezervare rezervare)
        {
            long idE = rezervare.Excursie;
            string numeClient = rezervare.NumeClient;
            string nrTelefon = rezervare.NrTelefon;
            int nrBilete = rezervare.NrLocuri;
            return new RezervareDTO(idE, numeClient, nrTelefon, nrBilete);
        }

        public static Rezervare GetFromDTO(RezervareDTO rezervareDTO)
        {
            long id = rezervareDTO.Id;
            long idE = rezervareDTO.IdExcursie;
            string numeClient = rezervareDTO.NumeClient;
            string nrTelefon = rezervareDTO.NrTelefon;
            int nrBilete = rezervareDTO.NrBilete;
            return new Rezervare(id,idE, numeClient, nrTelefon, nrBilete);
        }

        public static AgentieDTO[] GetDTO(Agentie[] agenties)
        {
            AgentieDTO[] frDTO = new AgentieDTO[agenties.Length];
            for (int i = 0; i < agenties.Length; i++)
            {
                frDTO[i] = GetDTO(agenties[i]);
            }
            return frDTO;
        }

        public static Agentie[] GetFromDTO(AgentieDTO[] agentieDTOS)
        {
            Agentie[] friends = new Agentie[agentieDTOS.Length];
            for (int i = 0; i < agentieDTOS.Length; i++)
            {
                friends[i] = GetFromDTO(agentieDTOS[i]);
            }
            return friends;
        }
    }
}
