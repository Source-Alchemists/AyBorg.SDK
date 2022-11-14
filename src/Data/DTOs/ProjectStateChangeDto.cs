using System.ComponentModel.DataAnnotations;
using AyBorg.SDK.Projects;

namespace AyBorg.SDK.Data.DTOs;

public record ProjectStateChangeDto
{
    [Required]
    public ProjectState State { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? VersionName { get; set; }

    [StringLength(200)]
    public string? Comment { get; set; }
}
