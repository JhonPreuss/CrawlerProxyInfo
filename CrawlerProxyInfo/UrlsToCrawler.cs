namespace CrawlerProxyInfo
{
    public class UrlsToCrawler
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? TotalRows { get; set; }


        public UrlsToCrawler(int id, string url, int? totalRows)
        {
            this.Id = id;
            this.Url = url;
            this.TotalRows = totalRows;
        }    
    }
}