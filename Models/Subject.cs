using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perusedit.Models
{
    public class Subject
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
