namespace HMS.Web.Models.Domain
{
    public class NewsPost
    {
        public Guid Id { get; set; }

        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
