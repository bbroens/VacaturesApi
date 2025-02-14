using System.ComponentModel.DataAnnotations;
using MediatR;

namespace VacaturesApi.Features.Vacatures.Create;

/// <summary>
/// Command record describing a request to create a VacatureDto and return it.
/// Uses Mediatr to pass the request fields to the handler listening.
/// </summary>

public record CreateVacatureCommand(
    Guid VacatureId,
    [Required] string UrlSlug,
    [Required] string FunctionTitle,  
    [Required] string Availability,  
    [Required] string Location,  
    [Required] string ContactPerson,  
    [Required] string Description,  
    [Required] string WhatToExpect,  
    [Required] string Responsibilities,  
    [Required] string Offer,  
    [Required] string Requirements,  
    [Required] string SalaryRange,  
    [Required] string Industry,  
    [Required] int ListPriority,
    [Required] bool Hidden,
    [Required] DateTime CreatedAt, 
    DateTime UpdatedAt
) 
    : IRequest<VacatureDto>;