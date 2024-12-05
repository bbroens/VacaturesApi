using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacaturesApi.Domain;

/// <summary>
/// Model class representing a single Vacature.
/// This class is used by Entity Framework to create and manipulate this table in the DB.
/// By modifying this class you will modify the database using migrations.
/// </summary>

public class Vacature
{
	[Column("VacatureId")]
    public Guid Id { get; set; }
    
	[Required(ErrorMessage = "UrlSlug is a required field")]
	[MaxLength(256, ErrorMessage = "UrlSlug cannot be longer than 256 characters")]
    public string UrlSlug { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "Function is a required field")]
	[MaxLength(256, ErrorMessage = "Function cannot be longer than 256 characters")]
    public string Function { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "Availability is a required field")]
	[MaxLength(64, ErrorMessage = "Availability cannot be longer than 64 characters")]
    public string Availability { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "Location is a required field")]
	[MaxLength(128, ErrorMessage = "Location cannot be longer than 128 characters")]
    public string Location { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "ContactPerson is a required field")]
	[MaxLength(64, ErrorMessage = "ContactPerson cannot be longer than 64 characters")]
    public string ContactPerson { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "Description is a required field")]
	public string Description { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "WhatToExpect is a required field")]
	public string WhatToExpect { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "TaskDescription is a required field")]
	public string TaskDescription { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "Offer is a required field")]
	public string Offer { get; set; } = string.Empty;
    
	[Required(ErrorMessage = "Requirements is a required field")]
	public string Requirements { get; set; } = string.Empty;
    
	[MaxLength(32, ErrorMessage = "SalaryRange cannot be longer than 32 characters")]
	public string? SalaryRange { get; set; } = string.Empty;
    
	[MaxLength(128, ErrorMessage = "Industry cannot be longer than 128 characters")]
    public string? Industry { get; set; } = string.Empty;
    
	[MaxLength(3, ErrorMessage = "ListPriority cannot be longer than 3 digits")]
    public int? ListPriority { get; set; } = 0;
    
    public bool? Hidden { get; set; } = false;
    
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}