using System.Collections.Generic;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Abstract;
using PerfumeStore.Domain.Concrete;

namespace PerfumeStore.Domain.Concrete
{
    public class EFPerfumeRepository : IPerfumeRepository
    {
         EFDbContext context = new EFDbContext();

        public IEnumerable<Perfume> Perfumes
        {
            get { return context.Perfumes; }
        }
    }
}
