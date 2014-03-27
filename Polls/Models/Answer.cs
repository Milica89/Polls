using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Polls.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        [Required(ErrorMessage="this field is required")] 
        public string Text { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}