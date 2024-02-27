namespace todoAPI
{
    public class Todo
    {
        public int Id { get; set; } = default;
        public string Label { get; set; } = default;
        public bool IsDone { get; set; } = default;
        public DateTime CreatedDateTime { get; set; } = default;
        public DateTime UpdatedDate { get; set; } = default;
    }
}
