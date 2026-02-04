using System.ComponentModel.DataAnnotations;
using ML.PCM.Enums;

namespace ML.PCM.Services.Dtos.Projects;

public class CreateUpdateProjectDto
{
    [StringLength(50)]
    public string ProjectCode { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public ProjectCategory? Category { get; set; }
    public string? MainContent { get; set; }
    public decimal? TotalInvestment { get; set; }

    [StringLength(50)]
    public string? Manager { get; set; }

    [StringLength(50)]
    public string? Operator { get; set; }

    public string? Remarks { get; set; }
}

