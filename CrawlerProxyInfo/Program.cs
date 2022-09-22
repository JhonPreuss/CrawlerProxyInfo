using System.Text;
using CrawlerProxyInfo;
using CrawlerProxyInfo.Models;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;

internal class Program
{
    readonly static object Block = new object();
    private static async Task Main(string[] args)
    {
        var startCrawlerPages = DateTime.Now;
        var urlsToCrawlerList = new List<UrlsToCrawler>();
        var totalRowsPages = new int();

        var totalOfPages = await GetTotalOfPages();

       if (totalOfPages > 0){
        //Constroi uma lista de objetos de URL's
          BuildUrlList(urlsToCrawlerList, totalOfPages);

          var options = new ParallelOptions(){
            MaxDegreeOfParallelism = 3
          };

          Parallel.ForEach(urlsToCrawlerList, options, item=>{
                var html = GetDataPages(item.Url);
                var lst = ParseDataPages(html, item.Id);
                totalRowsPages += lst.Count();

                JsonWrite(lst);
          });

       }
       else{
        Console.WriteLine("Não foram encontradas Url's para analise");
       }

        Console.WriteLine("Gravar em banco ");
        string json = File.ReadAllText("ProxysInfo.json");

        using (var db = new ProxyDbContext())
        {
            var crawlerinfo = new Crawlerinfo
            {
              StartCrawlerPages = startCrawlerPages,
              EndCrawlerPages = DateTime.Now,
              TotalPages = totalOfPages,
              TotalRowsPages= totalRowsPages,
              JsonProxysInfo= json,
            };
            db.Crawlerinfos.Add(crawlerinfo);
            db.SaveChanges();
        }

    }

    /*Método que realiza a construção de uma lista de objetos de Urls*/
    private static void BuildUrlList(List<UrlsToCrawler> listUrlToCrawler, int totalOfPages)
    {
        try
        {
          for (int i = 1; i < totalOfPages+1; i++)
          {
            listUrlToCrawler.Add( new UrlsToCrawler(i, "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/"+i, null));
          };
        }
        catch (System.Exception)
        {
          Console.WriteLine("Falha ao construir a lista de Url's"); 
        }
    }

    /*Metodo que retorna o número total de páginas para serem analisádas e coletadas*/
    private static async Task<int> GetTotalOfPages()
    {
        try
        {
            var urlBase = "https://proxyservers.pro/";
            //execuda a requisição HTTP e armazena a respota.
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(urlBase);

            //tranforma a resposta da requisição HTTP em um objeto estruturado
            var data = new HtmlDocument();
            data.LoadHtml(response);

            var repositories = data
                .DocumentNode
                .SelectNodes("//div[@class='card-footer']/nav/ul[@class='pagination justify-content-end']/li/a").ToList();

            var totalOfPages = repositories.Last().GetDirectInnerText();
            Console.WriteLine(totalOfPages);
            return Int32.Parse(totalOfPages);
            
        }
        catch (System.Exception)
        {
          Console.WriteLine("Falha ao construir a lista de Url's");
          throw; 
        }
    }

    /*Método que realiza a requisição HTTP para uma Url informada*/
    private static string GetDataPages(string url)
    {
        var chromeOptions = new ChromeOptions();
        
        //Deve inserir o caminho relatico do seu navegador Chrome
        chromeOptions.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        chromeOptions.AddArguments("--headless");

        var chrome = new ChromeDriver(chromeOptions);

        chrome.Navigate().GoToUrl(url);
        
        chrome.CloseDevToolsSession();

        return chrome.PageSource;
    }

    /*Método que realiza a coleta das informções IpAddress, Port, Country, Protocol de uma página*/
    private static List<ProxysInfo> ParseDataPages(string html, int pageNumber)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        htmlDoc.Save(@"PagesCrawled/proxy_page_"+pageNumber+".html", Encoding.UTF8);

        var repositories = htmlDoc
       .DocumentNode
       .SelectNodes("//div[@class='card-body']/div[@class='table-responsive']/table[@class='table table-hover']/tbody");

       var lstProxysInfo = new List<ProxysInfo>();

        foreach (var item in repositories)
        {
            var row = item.SelectNodes("tr");

            foreach (var itemData in row)
            {
                var nodes = itemData.SelectNodes("td").ToList();
                if (nodes.Count() > 1)
                {
                    lstProxysInfo.Add(new ProxysInfo()
                    {
                        IpAddress = nodes[1].InnerText.Trim(),
                        Port = nodes[2].InnerText.Trim(),
                        Country = nodes[3].InnerText.Trim(),
                        Protocol = nodes[6].InnerText.Trim(),
                    });
                }
            }
        }
        return lstProxysInfo;
    }

    /*Método que realiza a escrita de uma lista de objetos em um arquivo Json*/
    private static void JsonWrite(List<ProxysInfo> lst)
    {
        lock (Block){
            try
            {
                string jsonpath = "ProxysInfo.json";

                string json = File.ReadAllText("ProxysInfo.json");
                string updatedJson = Newtonsoft.Json.JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented);
                json += updatedJson;
                File.WriteAllText(jsonpath, updatedJson);
            }
            catch (System.Exception)
            {

                throw;
            }

        };
        
    }
}