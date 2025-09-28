using System;
using System.Collections.Generic;

namespace Application.DTOs;

public partial class JobsDTO
{
    public int JobId { get; set; }
    public string JobName { get; set; } = null!;

    public DateTime JobCreateAt { get; set; }

    public int JobMembers { get; set; }

    public DateTime JobDateEnd { get; set; }

    public DateTime JobDateStart { get; set; }

    public DateTime? JobRemainingTime { get; set; }

    public string JobStatus { get; set; } = null!;

    public int UserId { get; set; }

}
