using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class CommentController : EvaControllerBase<Comment>
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService) : base(commentService)
        {
            _commentService = commentService;
        }
    }
}
