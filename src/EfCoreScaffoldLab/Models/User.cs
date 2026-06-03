using System;
using System.Collections.Generic;

namespace EfCoreScaffoldLab.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
