using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace LittleOwl.Parse
{
    public static class HtmlLoader
    {
        public static async Task<IHtmlDocument> GetHtmlDocument(string url)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(url);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);

            return document;
        }
    }
}
