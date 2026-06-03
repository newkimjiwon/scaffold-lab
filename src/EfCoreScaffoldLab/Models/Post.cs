using System;
using System.Collections.Generic;

namespace EfCoreScaffoldLab.Models;

public partial class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateOnly CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
