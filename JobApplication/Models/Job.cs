using System;
using System.Collections.Generic;

namespace JobApplication.Models;

public partial class Job
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Requirements { get; set; } = null!;

    public string? Location { get; set; }

    public decimal? MinSalary { get; set; }

    public decimal? MaxSalary { get; set; }

    public string? JobType { get; set; }

    public string? Experience { get; set; }

    public int CompanyId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? ApplicationDeadline { get; set; }

    public string? Skills { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Jobapplication> Jobapplications { get; set; } = new List<Jobapplication>();
    public Jobapplication Jobapplication { get; internal set; }
}
