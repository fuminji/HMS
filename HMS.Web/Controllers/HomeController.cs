using HMS.Web.Models;
using HMS.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsPostsRepository newsPostsRepository;

        public HomeController(ILogger<HomeController> logger, INewsPostsRepository newsPostsRepository)
        {
            _logger = logger;
            this.newsPostsRepository = newsPostsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var newsPosts = await newsPostsRepository.GetAllAsync();
            return View(newsPosts);
        }

        public async Task<IActionResult> HomePage()
        {
            var newsPosts = await newsPostsRepository.GetAllAsync();
            return View(newsPosts);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
