using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milo.Models
{
    public class Movie
    {

        public Movie()
        {
            Actors = new HashSet<Actor>();
        }


        public int MovieId { get; set; }

        [StringLength(30,MinimumLength=3)]
        [Required]
        public string Title { get; set; }

        //Navigation Properties
        [Display(Name = "Onoma skinotheti")]
        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }
        public virtual ICollection<Actor> Actors { get; set; } 
    }
}