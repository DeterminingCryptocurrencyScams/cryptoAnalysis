﻿using cryptoAnalysisScraper.core.crawler.models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptoAnalysisScraper.core.crawler
{
    public class UserCrawler
    {
        private const string BASE_URL = "https://bitcointalk.org/index.php?action=profile;";

        public List<UserPageModel> Scrape(int start, int end)
        {
            var list = new List<UserPageModel>();

            
            HtmlWeb web = new HtmlWeb();

            for (int i = start; i < end; i++)
            {
               
             var doc = web.Load(MakeUrl(i));
                var result = this.Parse(i, doc);
                if (result != null)
                {
                    list.Add(result);
                }
            }
            return list;
        }
        private UserPageModel Parse(int id,HtmlDocument doc)
        {
            if (doc.DocumentNode.InnerHtml.Contains("An Error Has Occurred!")){
                return null;
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
