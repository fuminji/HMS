namespace HMS.Web.Models.ViewModels
{
    public class NewsPostRequest
    {

        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
