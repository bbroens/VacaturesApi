namespace VacaturesApi.Domain;

/// <summary>
/// Model representing a single Vacature.
/// Field requirements are configured in the VacatureConfiguration class.
/// </summary>

public class Vacature
{
    public Guid VacatureId { get; set; }
    public string UrlSlug { get; set; } = string.Empty;
    public string FunctionTitle { get; set; } = string.Empty;
    public string Availability { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string WhatToExpect { get; set; } = string.Empty;
    public string Responsibilities { get; set; } = string.Empty;
    public string Offer { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public string? SalaryRange { get; set; } = string.Empty;
    public string? Industry { get; set; } = string.Empty;
    public int? ListPriority { get; set; } = 0;
    public bool? Hidden { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}