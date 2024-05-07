using HMS.Web.Data;
using HMS.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HMS.Web.Repositories
{
    public class NewsPostRepository : INewsPostsRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public NewsPostRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<NewsPost> AddAsync(NewsPost newsPost)
        {
           await applicationDbContext.AddAsync(newsPost);
            await applicationDbContext.SaveChangesAsync();
            return newsPost;
        }

        public async Task<NewsPost?> DeleteAsync(Guid id)
        {
            var existingNews = await applicationDbContext.NewPosts.FindAsync(id);

            if (existingNews != null)
            {
                applicationDbContext.NewPosts.Remove(existingNews);
                await applicationDbContext.SaveChangesAsync();
                return existingNews;
            }
            return null;
        }

        public async Task<IEnumerable<NewsPost>> GetAllAsync()
        {
            return await applicationDbContext.NewPosts.ToListAsync();
        }

        public async Task<NewsPost?> GetAsync(Guid id)
        {
            return await applicationDbContext.NewPosts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<NewsPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await applicationDbContext.NewPosts
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

        }

        public async Task<NewsPost?> UpdateAsync(NewsPost newsPost)
        {
           var existingNews = await applicationDbContext.NewPosts.FirstOrDefaultAsync(x => x.Id == newsPost.Id);

           if (existingNews != null)
            {
                existingNews.Id = newsPost.Id;
                existingNews.Heading = newsPost.Heading;
                existingNews.PageTitle = newsPost.PageTitle;
                existingNews.Content = newsPost.Content;
                existingNews.ShortDescription = newsPost.ShortDescription;
                existingNews.FeaturedImageUrl = newsPost.FeaturedImageUrl;
                existingNews.UrlHandle = newsPost.UrlHandle;
                existingNews.PublishedDate = newsPost.PublishedDate;

                await applicationDbContext.SaveChangesAsync();
                return existingNews;
            }
            return null;
        }
    }
}
