using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCoreScaffoldLab.Models.Annotations;

public partial class User
{
    [Key]
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
