namespace VacaturesApi.Features.Vacatures;

/// <summary>
/// DTO describing a fully detailed vacature.
/// </summary>

public record VacatureDto(
    Guid VacatureId,
    string UrlSlug,
    string FunctionTitle,  
    string Availability,  
    string Location,  
    string ContactPerson,  
    string Description,  
    string WhatToExpect,  
    string Responsibilities,  
    string Offer,  
    string Requirements,  
    string SalaryRange,  
    string Industry,  
    int ListPriority,
    bool Hidden,
    DateTime CreatedAt, 
    DateTime? UpdatedAt
    );