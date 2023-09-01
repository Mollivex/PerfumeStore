using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PerfumeStore.Domain.Entities
{
    public class Perfume
    {
        public int PerfumeId { get; set; }
        public string HouseName { get; set; }
        public string PerfumeName { get; set; }
        public string Concentration { get; set; }
        public string PyramidTopNotes { get; set; }
        public string PyramidMiddleNotes { get; set; }
        public string PyramidBaseNotes { get; set; }
        public string Longevity { get; set; }
        public string Sillage { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
        public string Volume { get; set; }
        public decimal Price { get; set; }
    }
}
