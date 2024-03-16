using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class BlogController : EvaControllerBase<Blog>
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService) : base(blogService)
        {
            _blogService = blogService;
        }
    }
}
