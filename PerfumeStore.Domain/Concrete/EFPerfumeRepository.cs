using System.Collections.Generic;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Abstract;

namespace PerfumeStore.Domain.Concrete
{
    public class EFPerfumeRepository : IPerfumeRepository
    {
        readonly EFDbContext context = new EFDbContext();

        public IEnumerable<Perfume> Perfumes
        {
            get { return context.Perfumes; }
        }

        public void SavePerfume(Perfume perfume)
        {
            if (perfume.PerfumeId == 0)
                context.Perfumes.Add(perfume);
            else
            {
                Perfume dbEntry = context.Perfumes.Find(perfume.PerfumeId);
                if (dbEntry != null)
                {
                    dbEntry.HouseName = perfume.HouseName;
                    dbEntry.PerfumeName = perfume.PerfumeName;
                    dbEntry.Concentration = perfume.Concentration;
                    dbEntry.PyramidTopNotes = perfume.PyramidTopNotes;
                    dbEntry.PyramidMiddleNotes = perfume.PyramidMiddleNotes;
                    dbEntry.PyramidBaseNotes = perfume.PyramidBaseNotes;
                    dbEntry.Longevity = perfume.Longevity;
                    dbEntry.Sillage = perfume.Sillage;
                    dbEntry.Gender = perfume.Gender;
                    dbEntry.Description = perfume.Description;
                    dbEntry.Category = perfume.Category;
                    dbEntry.Country = perfume.Country;
                    dbEntry.Volume = perfume.Volume;
                    dbEntry.Price = perfume.Price;
                }
            }
            context.SaveChanges();
        }

        public Perfume DeletePerfume(int PerfumeId)
        {
            Perfume dbEntry = context.Perfumes.Find(PerfumeId);
            if (dbEntry != null)
            {
                context.Perfumes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
