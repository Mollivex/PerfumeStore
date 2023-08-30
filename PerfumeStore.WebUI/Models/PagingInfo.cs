using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.WebUI.Models
{
    public class PagingInfo
    {
        // Items number
        public int TotalItems { get; set; }

        // Items number per page
        public int ItemsPerPage { get; set; }

        // Current page number
        public int CurrentPage { get; set; }

        // Total pages number
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}