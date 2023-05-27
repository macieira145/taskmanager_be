using System;
using System.Collections.Generic;

namespace CardExchange.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }
}
