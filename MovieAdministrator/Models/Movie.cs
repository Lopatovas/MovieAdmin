using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAdministrator.Models
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Movie Title")]
        [StringLength(50)]
        [Remote("IsExists", "Movies", AdditionalFields = "Title, ReleaseDate", ErrorMessage = "Movie already exists")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Genre")]
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Cast> Casts { get; set; }
    }
}
