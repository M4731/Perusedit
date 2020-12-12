using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perusedit.Models
{
    public class Subject
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*Titlul este obligatoriu.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*Textul este obligatoriu.")]
        public string Text { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
