using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perusedit.Models
{
    public class Response
    {
        public Response(Response i)
        {
            Id = i.Id;
            //Text = i.Text;
            //FatherId = i.FatherId;
            //Responses = i.Responses;
            //SubjectId = i.SubjectId;

        }

        public Response()
        {

        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*Textul este obligatoriu.")]
        public string Text { get; set; }

        [ForeignKey("FatherId")]
        public virtual ICollection<Response> Responses { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }


        public int? FatherId { get; set; }
        public virtual Response Father { get; set; }
        public Response I { get; }
    }
}
