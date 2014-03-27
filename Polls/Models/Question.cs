using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Polls.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "must define start date!")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "must define end date!")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "must not be empty!")]
        public string Text { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}