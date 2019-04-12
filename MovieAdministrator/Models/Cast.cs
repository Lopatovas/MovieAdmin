using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAdministrator.Models
{
    public class Cast
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Actor Name")]
        [Remote("IsExists", "Casts", AdditionalFields = "ActorID, MovieID", ErrorMessage = "Actor is already enrolled in the movie")]
        public int ActorID { get; set; }
        [Display(Name = "Movie Title")]
        public int MovieID { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}
