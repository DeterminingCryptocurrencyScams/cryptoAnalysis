using cryptoAnalysisScraper.core.crawler.models;
using cryptoAnalysisScraper.core.database;
using cryptoAnalysisScraper.core.models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace cryptoAnalysisScraper.core.crawler
{
    public class UserCrawler
    {
        private const string BASE_URL = "https://bitcointalk.org/index.php?action=profile;";
        private System.Timers.Timer timer { get; set; } = new System.Timers.Timer();
        public int End { get; set; } = 3000000;
        public bool isRunning { get; set; } = false;
        private bool isWorking { get; set; } = false;
        public void Scrape()
        {
            timer.Interval = 1000; //1 second
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            isRunning = true;

            var random = new Random();

            while (isRunning)
            {
                  Thread.Sleep(random.Next(10000)); //keeps app alive
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isWorking)
            {
                return;
            }
            var context = new MariaContext(); //reinstaniating like this means its thread safe
            var status = context.NextProfile();
            if (status.Id < End)
            {
                isWorking = true;
                HtmlWeb web = new HtmlWeb();
                var doc = web.Load(MakeUrl(status.Id));
                var result = this.Parse(status.Id, doc, status);
                if (result != null)
                {
                    status.Status = core.models.ProfileStatus.Complete;
                    context.SetStatusForId(status);
                   context.Users.Add(result); 
                    context.SaveChanges();
                }
                else
                {
                    status.Status = core.models.ProfileStatus.ProfileNotPresent;
                    context.SetStatusForId(status);
                }
                isWorking = false;
            }
            else
            {
                timer.Stop();
                isRunning = false;
                isWorking = false;
            }
        }

        private UserPageModel Parse(int id,HtmlDocument doc, UserProfileScrapingStatus userProfileStatus)
        {
            if (doc.DocumentNode.InnerHtml.Contains("An Error Has Occurred!")){
                return null;
            }
            if (doc.DocumentNode.InnerText.Contains("403"))
            {
                var context = new MariaContext();
                userProfileStatus.Status = ProfileStatus.Error;
                context.SetStatusForId(userProfileStatus);
                throw new Exception("Error! getting 403 response. Quitting so we don't get locked out for longer!");
            }

            var item = new UserPageModel(id);
            item.Name = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.NameSelector));
            item.Merit = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.MeritSelector));
            item.Position = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.PositionSelector));
            item.Posts = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.PostSelector));
            item.Activity = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.ActivitySelector));
            item.DateRegistered = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.DateRegisteredSelector));
            item.LastActive = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.LastActiveSelector));
            item.Gender = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.GenderSelector));
            item.Age = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.AgeSelector));
            item.Location = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.LocationSelector));
            item.LocalTime = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.LocalTimeSelector));

            return item;
        }
        private string handleItem(HtmlNodeCollection col)
        {
            if (col == null)
            {
                return "";
            }
            else if (col.FirstOrDefault() != null)
            {
                return col.FirstOrDefault().InnerText;
            }
            else
            {
                return "";
            }
        }
        private string MakeUrl(int id)
        {
            return $"{BASE_URL}u={id}";
        }
    }
}
