namespace first_project.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
    }
}
