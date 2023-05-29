namespace CardExchange.Dto
{
    public class UserAuthDTO 
    { 
        public string? Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
