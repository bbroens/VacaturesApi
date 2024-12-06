using Serilog;
using VacaturesApi.Domain;
using VacaturesApi.Persistence.Data;

namespace VacaturesApi.Persistence.Seeding;

/// <summary>
/// Provides seed method with data for initial database population.
/// This method checks for empty Db at Program startup, but ONLY in Development environment.
/// </summary>

public static class DataSeeder
{
    public static void Seed(VacatureDbContext context)
    {
        Log.Information("Checking if development database is empty...");
        if (context.Vacatures.Any()) return;
        
        Log.Information("No Vacatures in database. Seeding data...");
        
        context.Vacatures.AddRange(
            new Vacature
            {
                VacatureId = Guid.NewGuid(),
                UrlSlug = "senior-software-engineer-net",
                FunctionTitle = "Senior Software Engineer (.NET)",
                Availability = "Full-time",
                Location = "Amsterdam, Netherlands",
                ContactPerson = "Jane Doe",
                Description = "We are looking for an experienced .NET developer to join our team.",
                WhatToExpect = "Challenging projects, modern tech stack, great team culture",
                Responsibilities = "Develop and maintain .NET applications, mentor junior developers",
                Offer = "Competitive salary, professional growth, flexible working hours",
                Requirements = "5+ years of .NET development, strong C# skills, microservices experience",
                SalaryRange = "€70,000 - €90,000",
                Industry = "Technology",
                ListPriority = 1,
                Hidden = false,
                CreatedAt = DateTime.UtcNow
            },
            new Vacature
            {
                VacatureId = Guid.NewGuid(),
                UrlSlug = "data-analyst-marketing",
                FunctionTitle = "Data Analyst - Marketing Insights",
                Availability = "Part-time",
                Location = "Rotterdam, Netherlands",
                ContactPerson = "John Smith",
                Description = "Seeking a data-driven analyst to provide marketing insights.",
                WhatToExpect = "Work with cross-functional teams, use advanced analytics tools",
                Responsibilities = "Analyze marketing data, create dashboards, provide actionable insights",
                Offer = "Competitive compensation, learning opportunities, modern work environment",
                Requirements = "Proficiency in SQL, Excel, and data visualization tools",
                SalaryRange = "€45,000 - €65,000",
                Industry = "Marketing",
                ListPriority = 2,
                Hidden = false,
                CreatedAt = DateTime.UtcNow
            }
        );

        context.SaveChanges();
    }
}
