using HMS.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsPostsRepository newsPostsRepository;

        public NewsController(INewsPostsRepository newsPostsRepository)
        {
            this.newsPostsRepository = newsPostsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var newsPost = await newsPostsRepository.GetByUrlHandleAsync(urlHandle);
            return View(newsPost);
        }
    }
}
