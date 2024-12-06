namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// DTO for updating a vacature based on an incoming request
/// </summary>

public class UpdateVacatureDto
{
    public Guid VacatureId { get; set; }
    public string? UrlSlug { get; set; }
    public string? FunctionTitle { get; set; }
    public string? Availability { get; set; }
    public string? Location { get; set; }
    public string? ContactPerson { get; set; }
    public string? Description { get; set; }
    public string? WhatToExpect { get; set; }
    public string? Responsibilities { get; set; }
    public string? Offer { get; set; }
    public string? Requirements { get; set; }
    public string? SalaryRange { get; set; }
    public string? Industry { get; set; }
    public int? ListPriority { get; set; }
    public bool? Hidden { get; set; }
}