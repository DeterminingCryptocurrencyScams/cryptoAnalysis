using cryptoAnalysisScraper.core.crawler.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cryptoAnalysisScraper.core.database
{
   public class MariaContext : DbContext
    {
        public DbSet<UserPageModel> Users { get; set; }

        public MariaContext(DbContextOptions<MariaContext> options): base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder o)
        {
            o.UseMySql(@"Server=localhost;User Id=scraper;Database=scrapedData");
        }
        public int NumberToStartAt()
        {
            var task = this.Users.MaxAsync(f => f.Id);
            task.Wait();
            return task.Result;

        }
    }
}
