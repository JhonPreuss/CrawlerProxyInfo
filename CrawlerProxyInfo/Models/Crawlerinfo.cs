using System;
using System.Collections.Generic;

namespace CrawlerProxyInfo.Models
{
    public partial class Crawlerinfo
    {
        public uint Id { get; set; }
        public DateTime? StartCrawlerPages { get; set; }
        public DateTime? EndCrawlerPages { get; set; }
        public int? TotalRowsPages { get; set; }
        public int TotalPages { get; set; }
        public string? JsonProxysInfo { get; set; }
    }
}
