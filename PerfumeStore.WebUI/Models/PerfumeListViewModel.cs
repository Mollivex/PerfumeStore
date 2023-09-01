using System;
using System.Collections.Generic;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.WebUI.Models
{
    public class PerfumeListViewModel
    {
        public IEnumerable<Perfume> Perfumes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentHouseName { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentGender { get; set; }
        public string CurrentConcentration { get; set; }
        public string CurrentCountry { get; set; }
        public string CurrentVolume { get; set; }
    }
}