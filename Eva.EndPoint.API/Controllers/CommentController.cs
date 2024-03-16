using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class CommentController : EvaControllerBase<Comment>
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService) : base(commentService)
        {
            _commentService = commentService;
        }
        [NonAction]
        public override Task<ActionResultViewModel<Comment>> InsertAsync(Comment entity)
        {
            return base.InsertAsync(entity);
        }

        [HttpPost]
        public async Task<ActionResultViewModel<Comment>> CreateComment(CommentCreationViewModel commentCreationViewModel)
        {
            try
            {
                return await _commentService.CreateComment(commentCreationViewModel);
            }
            catch (EvaNotFoundException ex)
            {
                return new ActionResultViewModel<Comment>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
            catch (EvaInvalidException ex)
            {
                return new ActionResultViewModel<Comment>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<Comment>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }
    }
}
