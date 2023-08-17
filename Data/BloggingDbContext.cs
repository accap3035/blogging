using Blogging_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Project.Data
{
    public class BloggingDbContext: DbContext
    {
        public BloggingDbContext(DbContextOptions options) : base(options) {
            
        }
        public DbSet<BlogPost> BlogPosts { get; set; } // will create table named BlogPosts
        public DbSet<Tag> Tags { get; set; } // will create table named Tags

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; } // will create users table
    }
}
