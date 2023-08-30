using System;
using System.Collections.Generic;
using PerfumeStore.Domain.Entities;




namespace PerfumeStore.WebUI.Models
{
    public class PerfumeListViewModel
    {
        public IEnumerable<Perfume> Perfumes { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}