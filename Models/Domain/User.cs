using System.Reflection.Metadata.Ecma335;

namespace Blogging_Project.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public ICollection<BlogPost>? blogPosts { get; set; }
        public ICollection<Todo>? Tasks{ get; set; }
    }
}
