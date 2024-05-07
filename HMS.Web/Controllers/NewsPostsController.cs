using HMS.Web.Models.Domain;
using HMS.Web.Models.ViewModels;
using HMS.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Web.Controllers
{
   
    public class NewsPostsController : Controller
    {

        private readonly INewsPostsRepository newsPostsRepository;

        public NewsPostsController(INewsPostsRepository newsPostsRepository)
        
        {
            this.newsPostsRepository = newsPostsRepository;
        }
        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewsPostRequest newsPostRequest)
        {
            // Map view model to domain model
            var newsPostDomainModel = new NewsPost
            {
                Heading = newsPostRequest.Heading,
                PageTitle = newsPostRequest.PageTitle,
                Content = newsPostRequest.Content,
                ShortDescription = newsPostRequest.ShortDescription,
                FeaturedImageUrl = newsPostRequest.FeaturedImageUrl,
                UrlHandle = newsPostRequest.UrlHandle,
                PublishedDate = newsPostRequest.PublishedDate,

            };
            await newsPostsRepository.AddAsync(newsPostDomainModel);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Call the repository
            var newsPosts = await newsPostsRepository.GetAllAsync();

            return View(newsPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // retrieve result from the repos
            var newsPost = await newsPostsRepository.GetAsync(id);

            if (newsPost != null)
            {
                // map the domain model into the view model
                var model = new EditNewsPostRequest
                {
                    Id = newsPost.Id,
                    Heading = newsPost.Heading,
                    PageTitle = newsPost.PageTitle,
                    Content = newsPost.Content,
                    ShortDescription = newsPost.ShortDescription,
                    FeaturedImageUrl = newsPost.FeaturedImageUrl,
                    UrlHandle = newsPost.UrlHandle,
                    PublishedDate = newsPost.PublishedDate,
                };
                return View(model);
            }

            

            // pass data to view

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditNewsPostRequest editNewsPostRequest)
        {
            // map view model to domain model
            var newsPostDomainModel = new NewsPost
            {
                Id = editNewsPostRequest.Id,
                Heading = editNewsPostRequest.Heading,
                PageTitle = editNewsPostRequest.PageTitle,
                Content = editNewsPostRequest.Content,
                ShortDescription = editNewsPostRequest.ShortDescription,
                FeaturedImageUrl = editNewsPostRequest.FeaturedImageUrl,
                UrlHandle = editNewsPostRequest.UrlHandle,
                PublishedDate = editNewsPostRequest.PublishedDate,
            };
            // submit information to repository to update
            var updatedNews = await newsPostsRepository.UpdateAsync(newsPostDomainModel);

            if(updatedNews != null)
            {
                // Show success notification
                return RedirectToAction("Edit");
            }

            // Show error notif
            return RedirectToAction("Edit");
            // redirect to get method
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditNewsPostRequest editNewsPostRequest)
        {
            // talk to repo to delete this blog post
            var deletedNews = await newsPostsRepository.DeleteAsync(editNewsPostRequest.Id);

            if(deletedNews != null)
            {
                // show success notification
                return RedirectToAction("List");
            }

            // show error notif
            return RedirectToAction("Edit", new { id = editNewsPostRequest.Id });
        }
    }
}
