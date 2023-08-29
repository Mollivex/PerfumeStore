using PerfumeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Domain.Abstract
{
    public interface IPerfumeRepository
    {
        IEnumerable<Perfume> Perfumes { get; }
    }
}
