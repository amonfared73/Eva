using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;

namespace Eva.Core.ApplicationService.Services
{
    public interface IBlogService : IBaseService<Blog>
    {
        Task<ActionResultViewModel<Blog>> CreateBlog(string blogTitle);
    }
}
