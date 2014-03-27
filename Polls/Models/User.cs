using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Polls.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }        
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}