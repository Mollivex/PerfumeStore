using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Perfume> Perfumes { get; set; }

    }
}
