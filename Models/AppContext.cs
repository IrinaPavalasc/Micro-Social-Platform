using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Micro_Social_Platform.Models
{
    public class AppContext: DbContext
    {
        public AppContext() : base("DBConnectionString") { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}