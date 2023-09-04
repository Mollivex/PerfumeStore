using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities
{
    public class Perfume
    {
        [HiddenInput(DisplayValue = false)]
        public int PerfumeId { get; set; }

        [Display(Name = "House name")]
        [Required(ErrorMessage="Please, enter perfume house name")]
        public string HouseName { get; set; }

        [Display(Name = "Perfume name")]
        [Required(ErrorMessage="Please, enter perfume name")]
        public string PerfumeName { get; set; }

        [Required(ErrorMessage="Please, enter perfume concentration")]
        public string Concentration { get; set; }

        [Display(Name = "Pyramid Top Notes")]
        [Required(ErrorMessage="Please, enter perfume pyramid TOP notes")]
        public string PyramidTopNotes { get; set; }

        [Display(Name = "Pyramid Middle Notes")]
        [Required(ErrorMessage="Please, enter perfume pyramid MIDDLE notes")]
        public string PyramidMiddleNotes { get; set; }

        [Display(Name = "Pyramid Heart Notes")]
        [Required(ErrorMessage="Please, enter perfume pyramid BASE notes")]
        public string PyramidBaseNotes { get; set; }

        [Required(ErrorMessage = "Please, enter perfume longevity")]
        public string Longevity { get; set; }

        [Required(ErrorMessage = "Please, enter perfume sillage")]
        public string Sillage { get; set; }

        [Required(ErrorMessage = "Please, enter perfume gender")]
        public string Gender { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name="Description")]
        [Required(ErrorMessage = "Please, enter perfume description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please, enter perfume category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please, enter perfume country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please, enter perfume volume")]
        public string Volume { get; set; }

        [Display(Name = "Price($)")]
        [Required]
        [Range (0.01, double.MaxValue, ErrorMessage = "Please, enter positive price value")]
        public decimal Price { get; set; }
    }
}
