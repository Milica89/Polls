using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polls.Models
{
    public class Vote
    {        
        [Key, Column(Order=0)]
        public int UserID { get; set; }
        [Key, Column(Order=1)]
        public int AnswerID { get; set; }

        public virtual User User { get; set; }        
        public virtual Answer Answer { get; set; }
    }
}