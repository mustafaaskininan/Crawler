using Microsoft.EntityFrameworkCore;
using SeriElemanCrawler.Models.Entities;

namespace SeriElemanCrawler.DbContext
{
    public class SeriElemanContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<CrawlerJobs> CrawlerJobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=db;uid=root;password=abc123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CrawlerJobs>(entity => { entity.HasKey(e => e.Id); });
        }
    }
}