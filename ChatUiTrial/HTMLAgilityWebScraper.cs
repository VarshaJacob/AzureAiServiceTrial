using HtmlAgilityPack;

namespace ChatUiTrial
{
    public class HTMLAgilityWebScraper
    {

        public static HtmlDocument GetDocument(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                return doc;
            }
            catch (Exception ex)
            {
                return new HtmlDocument();
            }
           
        }

        public void StartScrap()
        {
            try
            {
                var doc = GetDocument("https://www.recipetineats.com/apple-pie-recipe/");

                HtmlNodeCollection names = doc.DocumentNode.SelectNodes("//a/h2");
               // HtmlNodeCollection prices = doc.DocumentNode.SelectNodes("//div/main/ul/li/a/span");

                //for (int i = 0; i < names.Count(); i++)
                //{
                //    Console.WriteLine("Name: {0}, Price: {1}", names[i].InnerText, prices[i].InnerText);
                //}
            }
            catch (Exception e)
            {

            }
            

        }

    }
}
