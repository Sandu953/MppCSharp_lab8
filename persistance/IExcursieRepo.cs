using lab8.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab8.Domain;

namespace lab8.Repository
{
    internal interface IExcursieRepo : IRepository<long, Excursie>
    {
        List<Excursie> findBeetwenHours(string ora1, string ora2, string obiectiv);

        int findLocuriLibere(int id);
    }
}
