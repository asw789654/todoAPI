namespace todoAPI
{
    public class Todo
    {
        public int Id { get; set; }
        public string Label { get; set; } = default;
        public bool IsDone { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
