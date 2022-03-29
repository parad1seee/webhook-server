using Bogus;
using Microsoft.EntityFrameworkCore;
using WebhookServer.DAL.Entities;
using WebhookServer.Models.Emuns;

namespace WebhookServer.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var ids = 1;
            var stock = new Faker<User>()
                .RuleFor(m => m.Id, f => ids++)
                .RuleFor(m => m.Name, f => f.Person.FirstName)
                .RuleFor(m => m.LastName, f => f.Person.LastName)
                .RuleFor(m => m.Email, f => $"danil.zhmutskiy+{ids}@gmail.com")
                .RuleFor(x => x.Status, f => f.PickRandom<UserStatus>());

            builder
                .Entity<User>()
                .HasData(stock.GenerateBetween(1000, 1000));

            base.OnModelCreating(builder);
        }
    }
}
