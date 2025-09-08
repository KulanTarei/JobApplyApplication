using System;
using System.Collections.Generic;

namespace JobApplication.Models;

public partial class Jobapplication
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public int UserId { get; set; }

    public sbyte Status { get; set; }

    public string? CoverLetter { get; set; }

    public string? CustomResumeFilePath { get; set; }

    public DateTime AppliedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Notes { get; set; }

    public int? ExpectedSalary { get; set; }

    public string? AdditionalDocuments { get; set; }

    public DateTime? InterviewDate { get; set; }

    public string? InterviewFeedback { get; set; }

    public sbyte? Rating { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
