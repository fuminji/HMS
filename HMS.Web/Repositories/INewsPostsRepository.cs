using HMS.Web.Models.Domain;

namespace HMS.Web.Repositories
{
    public interface INewsPostsRepository
    {
        Task<IEnumerable<NewsPost>> GetAllAsync();

        Task<NewsPost?> GetAsync(Guid id);

        Task<NewsPost?> GetByUrlHandleAsync(string urlHandle);

        Task<NewsPost> AddAsync(NewsPost newsPost);

        Task<NewsPost?> UpdateAsync(NewsPost newsPost);
        Task<NewsPost?> DeleteAsync(Guid id);
    }
}
