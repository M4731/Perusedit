using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perusedit.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }
}
