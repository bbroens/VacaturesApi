namespace VacaturesApi.Features.Vacatures;

/// <summary>
/// Data Transfer Object for a Vacature.
/// By using a record we can compare actual values instead of object references.
/// This is more performant and avoids unnecessary allocations and equals checks.
/// </summary>

public record VacatureDto(
    Guid Id,
    string UrlSlug,
    string Function,  
    string Availability,  
    string Location,  
    string ContactPerson,  
	string Description,  
	string WhatToExpect,  
	string TaskDescription,  
	string Offer,  
	string Requirements,  
	string SalaryRange,  
    string Industry,  
    int ListPriority,
    bool Hidden,
    DateTime CreatedAt, 
    DateTime? UpdatedAt 
);