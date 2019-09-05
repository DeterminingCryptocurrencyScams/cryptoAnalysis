using cryptoAnalysisScraper.core.crawler.models;
using cryptoAnalysisScraper.core.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cryptoAnalysisScraper.core.database
{
   public class MariaContext : DbContext
    {
        public DbSet<UserPageModel> Users { get; set; }
        public DbSet<UserProfileScrapingStatus> ProfileScrapingStatuses { get; set; }
        public MariaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MariaContext>();
            optionsBuilder.UseMySql(@"Server=database-1.c0srsxgmo39w.us-east-2.rds.amazonaws.com;User Id=scraper;Database=innodb");
            return new MariaContext();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder o)
        {
            o.UseMySql(@"Server=database-1.c0srsxgmo39w.us-east-2.rds.amazonaws.com;User Id=scraper;Database=innodb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfileScrapingStatus>().Property(f => f.Id).ValueGeneratedOnAdd();
        }
        public UserProfileScrapingStatus NextProfile()
        {
            var s = new UserProfileScrapingStatus();
            this.ProfileScrapingStatuses.Add(s);
            this.SaveChanges();
            return s;
        }
        public bool SetStatusForId(int id, ProfileStatus status)
        {
            return SetStatusForId(new UserProfileScrapingStatus(id, status));
        }
        public bool SetStatusForId(UserProfileScrapingStatus status)
        {
            try
            {
                this.Entry(status).State = ProfileStatusExists(status.Id) ? EntityState.Modified : EntityState.Added;
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw e;
            }
        }
        private bool ProfileStatusExists(int id)
        {
            return this.ProfileScrapingStatuses.Any(e => e.Id == id);
        }
    }
    
}
