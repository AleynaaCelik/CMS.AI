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
                    "Artificial Intelligence (AI) is the simulation of human intelligence processes by machines, especially computer systems. These processes include learning (the acquisition of information and rules for using the information), reasoning (using rules to reach approximate or definite conclusions) and self-correction.\n\nKey applications of AI include expert systems, natural language processing, speech recognition and machine vision.",
                    "system"
                ),
                new Content(
                    "The Evolution of Web Development",
                    "Web development has come a long way since the early days of the internet. Initially, websites were simple static pages with basic HTML. Today, web development encompasses complex applications that run on various platforms and devices.\n\nModern web development often involves front-end frameworks like React, Angular, or Vue, combined with powerful back-end technologies and APIs. The future of web development is likely to include more AI-powered tools, immersive experiences, and progressive web applications.",
                    "system"
                ),
                new Content(
                    "Microservices Architecture",
                    "Microservices architecture is an architectural style that structures an application as a collection of small, loosely coupled services. Each service is focused on a specific business capability and can be developed, deployed, and scaled independently.\n\nThe benefits of microservices include improved scalability, better fault isolation, and the ability to use different technologies for different services. However, they also introduce challenges related to distributed systems, such as network latency, message serialization, and maintaining data consistency across services.",
                    "system"
                )
            };

            // Add metadata to each content
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