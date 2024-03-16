using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IPostService : IBaseService<Post>
    {
        Task<ActionResultViewModel<Post>> CreatePost(PostCreationViewModel postCreationViewModel);
    }
}
