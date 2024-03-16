using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface ICommentService : IBaseService<Comment>
    {
        Task<CustomResultViewModel<string>> CreateComment(CommentCreationViewModel commentCreationViewModel);
    }
}
