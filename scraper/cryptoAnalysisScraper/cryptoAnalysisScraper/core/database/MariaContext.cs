﻿using cryptoAnalysisScraper.core.crawler.models;
using cryptoAnalysisScraper.core.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            optionsBuilder.UseMySql(@"Server=45.79.58.138;User Id=atotalnoob;Password=boonlatota;Database=scrapedData");
            return new MariaContext();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder o)
        {
            o.UseMySql(@"Server=45.79.58.138;User Id=atotalnoob;Password=boonlatota;Database=scrapedData");
        }
        public UserProfileScrapingStatus NextProfile()
        {
            bool isFailed = true;
            UserProfileScrapingStatus status = null;
            Task<int> task;
            while (isFailed)
            {
                if (this.ProfileScrapingStatuses.Count() == 0 && this.Users.Count() == 0)
                {
                    task = null;
                    try
                    {
                    status = new UserProfileScrapingStatus(1, ProfileStatus.Working);
                    this.ProfileScrapingStatuses.Add(status);
                    this.SaveChanges();
                    return status;

                    }
                    catch (DbUpdateException)
                    {
                        // retry
                    }
                    catch (InvalidOperationException)
                    {
                        //retry
                    }
                }
                else if (this.ProfileScrapingStatuses.Count() == 0)
                {
                    
                    task = this.Users.MaxAsync(f => f.Id);
                }
                else
                {
                    task = this.ProfileScrapingStatuses.MaxAsync(f => f.Id);
                }
                task.Wait();
                status = new UserProfileScrapingStatus(task.Result + 1, ProfileStatus.Working);
                try
                {
                this.ProfileScrapingStatuses.Add(status);
                    this.SaveChanges();
                    isFailed = false;
                    
                }
                catch (DbUpdateException)
                {
                    // retry
                }
                catch(InvalidOperationException)
                {
                    //retry
                }
               
            }
            return status;

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
