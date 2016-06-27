using System.Web.Mvc;

namespace QTSWebUI
{
    public static class VideoTagExtensions
    {
        public static MvcHtmlString Video(this HtmlHelper html, string src)
        {
            var url = UrlHelper.GenerateContentUrl(src, html.ViewContext.HttpContext);
            var tag = new TagBuilder("video") {InnerHtml = "Your browser doesn't support video tags."};

            tag.MergeAttribute("src", url);
            //tag.MergeAttribute("controls", "controls"); // Show Play/Pause buttons
            return MvcHtmlString.Create(tag.ToString());
        }
    }
}