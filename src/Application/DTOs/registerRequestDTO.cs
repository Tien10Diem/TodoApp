using System;
using System.Collections.Generic;

namespace Application.DTOs;

public partial class registerRequestDTO 
{
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string UserPasswordHash { get; set; } = null!;
}
