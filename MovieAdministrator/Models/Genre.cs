using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAdministrator.Models
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Genre Name")]
        [Remote("IsExists", "Genres", ErrorMessage = "Genre already exists")]
        public string Name { get; set; }
    }
}
