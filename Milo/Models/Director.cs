using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milo.Models
{
    public class Director
    {
        [Display(Name = "Onoma skinotheti")]
        public int DirectorId { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}