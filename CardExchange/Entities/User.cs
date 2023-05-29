using CardExchange.Dto;

namespace CardExchange.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public User(int id)
    {
        this.Id = id;
    }

    public User(int id, string name, string email, string passwordHash, string passwordSalt)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.PasswordHash = passwordHash;   
        this.PasswordSalt = passwordSalt;
    }

    public User(UserAuthDTO userAuthDTO) 
    {
        this.Email = userAuthDTO.Email;
        this.Name = userAuthDTO.Name;
    }

    public User(int id, string name, string email, string passwordHash, string passwordSalt, DateTime created, DateTime updated)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.PasswordHash = passwordHash;
        this.PasswordSalt = passwordSalt;
        this.Created = created; 
        this.Updated = updated;
    }
}
