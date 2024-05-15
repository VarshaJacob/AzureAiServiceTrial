using IronWebScraper;

namespace ChatUiTrial
{
    public class IronWebScraperTrial : WebScraper
    {
        public override void Init()
        {
            LoggingLevel = LogLevel.All;
            Request("https://www.recipetineats.com/apple-pie-recipe/", Parse);
        }

        public override void Parse(Response response)
        {
            foreach (HtmlNode title_link in response.Css(".oxy-post-title"))
            {
                string strTitle = title_link.TextContentClean;
                Scrape(new ScrapedData() { { "Title", strTitle } });
            }
            if (response.CssExists("div.oxy-easy-posts-pages > a[href]"))
            {
                string next_page = response.Css("div.oxy-easy-posts-pages > a[href]")[0].Attributes["href"];
                Request(next_page, Parse);
            }
        }
    }
}
