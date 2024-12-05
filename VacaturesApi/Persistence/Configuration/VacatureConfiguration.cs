using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacaturesApi.Domain;

namespace VacaturesApi.Persistence.Configuration;

/// <summary>
/// Configuration of the database schema for the Vacature entity.
/// </summary>

public class VacatureConfiguration : IEntityTypeConfiguration<Vacature>
{
    public void Configure(EntityTypeBuilder<Vacature> builder)
    {
        builder.HasKey(vacature => vacature.VacatureId);
        
        builder.Property(vacature => vacature.UrlSlug)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(vacature => vacature.FunctionTitle)
	        .IsRequired()
	        .HasMaxLength(128);
        
        builder.Property(vacature => vacature.Availability)
	        .IsRequired()
	        .HasMaxLength(32);
        
        builder.Property(vacature => vacature.Location)
	        .IsRequired()
	        .HasMaxLength(128);
        
        builder.Property(vacature => vacature.ContactPerson)
	        .IsRequired()
	        .HasMaxLength(64);
        
        builder.Property(vacature => vacature.SalaryRange)
	        .IsRequired(false)
	        .HasMaxLength(32);
        
        builder.Property(vacature => vacature.Industry)
	        .IsRequired(false)
	        .HasMaxLength(128);

        builder.Property(vacature => vacature.ListPriority)
	        .IsRequired(false);
        
        builder.Property(vacature => vacature.Description)
	        .IsRequired();

        builder.Property(vacature => vacature.WhatToExpect)
	        .IsRequired();

        builder.Property(vacature => vacature.Responsibilities)
	        .IsRequired();

        builder.Property(vacature => vacature.Offer)
	        .IsRequired();

        builder.Property(vacature => vacature.Requirements)
	        .IsRequired();

        builder.Property(vacature => vacature.Hidden)
	        .IsRequired(false);

        builder.Property(vacature => vacature.UpdatedAt)
	        .IsRequired(false);
        
        builder.Property(vacature => vacature.CreatedAt)
	        .IsRequired();
    }
}