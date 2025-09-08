using System;
using System.Collections.Generic;

namespace JobApplication.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public string? Country { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Education { get; set; }

    public string? Experience { get; set; }

    public string? Skills { get; set; }

    public string? ResumeFilePath { get; set; }

    public string? ProfilePicturePath { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? LinkedInProfile { get; set; }

    public string? GitHubProfile { get; set; }

    public string? Portfolio { get; set; }

    public virtual ICollection<Jobapplication> Jobapplications { get; set; } = new List<Jobapplication>();
    public Jobapplication Jobapplication { get; internal set; }
}
