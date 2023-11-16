using Mc2.CrudTest.Query;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace Mc2.CrudTest.Presentation.Server.Seed
{
    public class ContextSeed
    {
        public async Task SeedMigrationAsync(CustomerQueryDbContext context)
        {
            var policy = CreatePolicy();

            await policy.ExecuteAsync(async () =>
            {
                await context.Database.MigrateAsync();
            });
        }

        private AsyncRetryPolicy CreatePolicy(int retries = 1)
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(
                retries,
                sleepDurationProvider => TimeSpan.FromSeconds(15),

                (exception, retry) =>
                {
                    Console.WriteLine(exception.InnerException);
                }
            );
        }
    }
}