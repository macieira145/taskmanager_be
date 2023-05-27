using System;
using System.Collections.Generic;

namespace CardExchange.Entities;

public partial class Task
{
    public int Id { get; set; }

    public int UsersId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? Completed { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual User Users { get; set; } = null!;
}
