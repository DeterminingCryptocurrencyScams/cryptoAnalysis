using System;
using System.Collections.Generic;
using System.Text;

namespace cryptoAnalysisScraper.core.crawler
{
    public class XpathSelectors
    {
        public static string NameSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[2]";
        public static string PostSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[2]/td[2]";
        public static string ActivitySelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[3]/td[2]";
        public static string MeritSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[4]/td[2]";
        public static string PositionSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[5]/td[2]";
        public static string DateRegisteredSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[6]/td[2]";
        public static string LastActiveSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[7]/td[2]";
        public static string GenderSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[17]/td[2]";
        public static string AgeSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[18]/td[2]";
        public static string LocationSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[19]/td[2]";
        public static string LocalTimeSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[20]/td[2]";
        public static string errorSelector { get; set; } = "/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/div[1]/table[1]/tr[1]/td[1]";
    }
}
