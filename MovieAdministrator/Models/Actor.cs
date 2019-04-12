using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAdministrator.Models
{
    public class Actor
    {
        public enum GenderOptions
        {
            [Description("Male")]
            Male,
            [Description("Female")]
            Female
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Surname")]
        [Remote("IsExists", "Actors", AdditionalFields = "FirstName, LastName, Age", ErrorMessage = "Actor already exists.")]
        public string LastName { get; set; }

        [Required]
        public GenderOptions Gender { get; set; }

        [Required]
        public int Age { get; set; }

        public ICollection<Cast> Casts { get; set; }


        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

    }
}
