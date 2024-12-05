namespace VacaturesApi.Domain;

/// <summary>
/// Model class representing a single Vacature.
/// This class is used by Entity Framework to create and manipulate this table in the DB.
/// Field requirement config is set at the persistence level to keep the model pure.
/// </summary>

public class Vacature
{
    public Guid VacatureId { get; init; }
    
    public string UrlSlug { get; init; } = string.Empty;
    public string FunctionTitle { get; init; } = string.Empty;
    public string Availability { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string ContactPerson { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public string WhatToExpect { get; init; } = string.Empty;
	public string Responsibilities { get; init; } = string.Empty;
	public string Offer { get; init; } = string.Empty;
	public string Requirements { get; init; } = string.Empty;
	public string? SalaryRange { get; init; } = string.Empty;
    public string? Industry { get; init; } = string.Empty;
    public int? ListPriority { get; init; } = 0;
    public bool? Hidden { get; init; } = false;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
}