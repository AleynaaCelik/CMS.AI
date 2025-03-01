// CMS.AI.Infrastructure/Persistence/ApplicationDbContextSeed.cs
using CMS.AI.Domain.Entities;
using CMS.AI.Infrastructure.Persistance;
using Microsoft.Extensions.Logging;

namespace CMS.AI.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context, ILogger<ApplicationDbContextSeed> logger)
        {
            if (!context.Contents.Any())
            {
                logger.LogInformation("Seeding database");

                await SeedContentsAsync(context);

                logger.LogInformation("Seed completed");
            }
        }

        private static async Task SeedContentsAsync(ApplicationDbContext context)
        {
            var contents = new List<Content>
            {
                new Content(
                    "Introduction to Artificial Intelligence",
                    "Artificial Intelligence (AI) is the simulation of human intelligence processes by machines...",
                    "system"
                ),
                new Content(
                    "The Evolution of Web Development",
                    "Web development has come a long way since the early days of the internet...",
                    "system"
                ),
                new Content(
                    "Microservices Architecture",
                    "Microservices architecture is an architectural style that structures an application...",
                    "system"
                )
            };

            // Her entity için LastModifiedBy alanını doldur
            foreach (var content in contents)
            {
                content.Update(content.Title, content.Body, "system"); // LastModifiedBy alanını doldurur
            }

            contents[0].AddMetaData("en", "category", "Technology", "system");
            contents[0].AddMetaData("en", "keyword", "AI", "system");
            contents[0].AddMetaData("en", "keyword", "Machine Learning", "system");

            contents[1].AddMetaData("en", "category", "Technology", "system");
            contents[1].AddMetaData("en", "keyword", "Web Development", "system");
            contents[1].AddMetaData("en", "keyword", "Frontend", "system");

            contents[2].AddMetaData("en", "category", "Technology", "system");
            contents[2].AddMetaData("en", "keyword", "Architecture", "system");
            contents[2].AddMetaData("en", "keyword", "Microservices", "system");

            await context.Contents.AddRangeAsync(contents);
            await context.SaveChangesAsync();
        }
    }
}
