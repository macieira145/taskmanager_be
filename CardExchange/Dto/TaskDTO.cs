namespace CardExchange.Dto
{
    public class TaskDTO
    {
        public int? Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Completed { get; set; } = false;
    }
}
