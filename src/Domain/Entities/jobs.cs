using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Job
{
    public int JobId { get; set; }

    public string JobName { get; set; } = null!;

    public DateTime JobUpdateAt { get; set; }

    public DateTime JobCreateAt { get; set; }

    public int JobMembers { get; set; }

    public DateTime JobDateEnd { get; set; }

    public DateTime JobDateStart { get; set; }

    public DateTime? JobDeleteAt { get; set; }

    public DateTime? JobRemainingTime { get; set; }

    public string JobStatus { get; set; } = null!;

    public int JobFlag { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
