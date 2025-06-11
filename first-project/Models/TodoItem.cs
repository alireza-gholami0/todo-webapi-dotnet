namespace first_project.Models
{
    public class TodoItem
    {
        int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
    }
}
