using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Micro_Social_Platform.Models
{
    public class Comment
    {

        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}