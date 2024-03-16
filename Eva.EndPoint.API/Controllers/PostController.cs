using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    public class PostController : EvaControllerBase<Post>
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) : base(postService)
        {
            _postService = postService;
        }
        [NonAction]
        public override Task<ActionResultViewModel<Post>> InsertAsync(Post entity)
        {
            return base.InsertAsync(entity);
        }
        [HttpPost]
        public async Task<ActionResultViewModel<Post>> CreatePost(PostCreationViewModel postCreationViewModel)
        {
            try
            {
                return await _postService.CreatePost(postCreationViewModel);
            }
            catch (EvaInvalidException ex)
            {
                return new ActionResultViewModel<Post>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
            catch (EvaNotFoundException ex)
            {
                return new ActionResultViewModel<Post>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<Post>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }
    }
}
