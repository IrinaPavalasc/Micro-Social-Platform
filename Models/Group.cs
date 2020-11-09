using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Micro_Social_Platform.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        
        public virtual ICollection<Post> Posts { get; set; }





    }
}