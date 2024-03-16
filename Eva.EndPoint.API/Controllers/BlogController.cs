using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class BlogController : EvaControllerBase<Blog>
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService) : base(blogService)
        {
            _blogService = blogService;
        }
        [NonAction]
        public override Task<ActionResultViewModel<Blog>> InsertAsync(Blog entity)
        {
            return base.InsertAsync(entity);
        }
        [HttpPost]
        public async Task<ActionResultViewModel<Blog>> CreateBlogAsync(string blogTitle)
        {
            return await _blogService.CreateBlog(blogTitle);
        }
    }
}
