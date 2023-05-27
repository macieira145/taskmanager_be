namespace CardExchange.Dto
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
    }
}
