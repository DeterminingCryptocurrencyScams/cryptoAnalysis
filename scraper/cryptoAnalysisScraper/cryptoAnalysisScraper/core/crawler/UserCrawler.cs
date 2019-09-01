using cryptoAnalysisScraper.core.crawler.models;
using HtmlAgilityPack;
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
        public List<UserPageModel> list { get; set; } = new List<UserPageModel>();
        public int Start { get; set; }
        public int End { get; set; }
        public int i { get; set; }
        public bool isRunning { get; set; } = false;
        public List<UserPageModel> Scrape(int start, int end)
        {
            Start = start;
            End = end;

            timer.Interval = 1000; //1 second
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            isRunning = true;

            var random = new Random();

            while (isRunning)
            {
                  Thread.Sleep(random.Next(10000));
            }

            return list;

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (i<End)
            {
                HtmlWeb web = new HtmlWeb();
                var doc = web.Load(MakeUrl(i));
                var result = this.Parse(i, doc);
                if (result != null)
                {
                    list.Add(result);
                }
                i++;
            }
            else
            {
                timer.Stop();
                isRunning = false;
            }
        }

        private UserPageModel Parse(int id,HtmlDocument doc)
        {
            if (doc.DocumentNode.InnerHtml.Contains("An Error Has Occurred!")){
                return null;
            }
            if (doc.DocumentNode.InnerText.Contains("403"))
            {
                i--;
                timer.Stop();
                timer.Interval = 1500;
                timer.Start();
            }

            var item = new UserPageModel(id);
            item.Name = handleItem(doc.DocumentNode.SelectNodes(XpathSelectors.NameSelector));
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
