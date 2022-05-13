using System;
using System.Threading.Tasks;
using AzureCosmosEFCoreCRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureCosmosEFCoreCRUD.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderDB>()
                 .HasNoDiscriminator()
                 .ToContainer(nameof(OrderDB))
                 .HasPartitionKey(da => da.Id)
                 .HasKey(da => new { da.Id });
            modelBuilder.ApplyConfiguration(new VideogameEntityConfiguration());

        }
        public DbSet<OrderDB> OrderDB { get; set; }

        internal Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        internal void Update(object p)
        {
            throw new NotImplementedException();
        }
    }
    public class VideogameEntityConfiguration : IEntityTypeConfiguration<OrderDB>
    {
        public void Configure(EntityTypeBuilder<OrderDB> builder)
        {
            builder.OwnsOne(x => x.Company);
        }
    }

}