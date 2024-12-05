using VacaturesApi.Domain;
using VacaturesApi.Persistence.Data;

namespace VacaturesApi.Persistence.Seeding;

/// <summary>
/// Contains methods and dummy data for seeding the database in development.
/// </summary>

public static class VacatureSeedData
{
    public static void Seed(VacatureDbContext context)
    {
        // Only seed if no vacatures exist
        if (!context.Vacatures.Any())
        {
            var seedVacatures = new List<Vacature>
            {
                new Vacature()
                {
                    VacatureId = Guid.NewGuid(),
                    UrlSlug = "software-developer-amsterdam-5512",
                    FunctionTitle = "Software developer",
                    Availability = "Part-time",
                    Location = "Amsterdam",
                    ContactPerson = "John Doe",
                    Description = "We are looking for a talented software developer to join our team.",
                    WhatToExpect = "You will be responsible for designing and implementing new features for our software.",
                    Responsibilities = "You will be responsible for designing and implementing new features for our software.",
                    Offer = "You will be responsible for designing and implementing new features for our software.",
                    Requirements = "You will be responsible for designing and implementing new features for our software.",
                    SalaryRange = "€ 10.000 - € 15.000",
                    Industry = "IT",
                    ListPriority = 0,
                    Hidden = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
                new Vacature()
                {
                    VacatureId = Guid.NewGuid(),
                    UrlSlug = "software-developer-rotterdam-5513",
                    FunctionTitle = "Software developer",
                    Availability = "Full-time",
                    Location = "Rotterdam",
                    ContactPerson = "John Doe",
                    Description = "We are looking for a talented software developer to join our team.",
                    WhatToExpect = "You will be responsible for designing and implementing new features for our software.",
                    Responsibilities = "You will be responsible for designing and implementing new features for our software.",
                    Offer = "You will be responsible for designing and implementing new features for our software.",
                    Requirements = "You will be responsible for designing and implementing new features for our software.",
                    SalaryRange = "€ 10.000 - € 15.000",
                    Industry = "IT",
                    ListPriority = 0,
                    Hidden = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            };

            context.Vacatures.AddRange(seedVacatures);
            context.SaveChanges();
        }
    }
}