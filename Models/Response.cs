using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perusedit.Models
{
    public class Response
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [ForeignKey("Father")]
        public int FatherId { get; set; }
        public Response Father { get; set; }


    }
}
