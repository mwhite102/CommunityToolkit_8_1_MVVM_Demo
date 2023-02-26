namespace CommunityToolkitDemo.WPF.Models
{
    public class ToDo
    {
        public int ToDoId { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsDone { get; set; }
    }
}
