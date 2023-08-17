using System.ComponentModel.DataAnnotations;

namespace Blogging_Project.Models.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DeadlineDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan DeadlineTime { get; set; }

        public DateTime Deadline => DeadlineDate + DeadlineTime;
        public bool State { get; set; }
        public User Owner { get; set; }
    }
}
