namespace Blogging_Project.Models.ViewModels
{
    public class TodoAddRequest
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public TimeSpan DeadlineTime { get; set; }

    }
}
