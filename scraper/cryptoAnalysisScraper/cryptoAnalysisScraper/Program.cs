using cryptoAnalysisScraper.core.crawler;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace cryptoAnalysisScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new UserCrawler();
            var dict = new Dictionary<int, int>();
            dict.Add(1, 1000);
            dict.Add(1000, 2000);
            dict.Add(2000, 4000);
            dict.Add(4000, 6000);
            dict.Add(6000, 8000);
            dict.Add(8000, 10000);
            dict.Add(10000, 20000);
            dict.Add(20000, 40000);
            dict.Add(40000, 100000);
            dict.Add(100000, 150000);

            foreach (var item in dict)
            {
                var result = crawler.Scrape(item.Key, item.Value);
                using (var writer = new StreamWriter("output.csv", true))
                {
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords(result);
                    }
                }
            }
           
        }
    }
}
