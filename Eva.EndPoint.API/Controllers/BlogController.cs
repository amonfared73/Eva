using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class BlogController : EvaControllerBase<Blog, BlogViewModel>
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
            try
            {
                return await _blogService.CreateBlog(blogTitle);
            }
            catch (EvaRequiredPropertyException ex)
            {
                return new ActionResultViewModel<Blog>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<Blog>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }
    }
}
