using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : IEntityBase
    {
        [Key] 
        public int Id{ get; set; }

       
        [Display(Name = "Profile Picture")]
        public string ProfilePicURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bio is Required")]
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public List<Actor_Movie> Actor_Movies{ get; set; }
    }
}
