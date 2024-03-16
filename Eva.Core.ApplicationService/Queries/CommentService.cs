using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Extentions;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class CommentService : BaseService<Comment, CommentViewModel>, ICommentService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public CommentService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<CustomResultViewModel<string>> CreateComment(CommentCreationViewModel commentCreationViewModel)
        {
            using(EvaDbContext context = _dbContextFactory.CreateDbContext())
            {
                var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == commentCreationViewModel.PostId);
                if (post is null)
                    throw new EvaNotFoundException("No post fount with the given postId", typeof(Post));

                if (!commentCreationViewModel.IsValid())
                    throw new EvaInvalidException("Comment has invalid properties");

                var comment = new Comment
                {
                    Text = commentCreationViewModel.Text,
                    PostId = commentCreationViewModel.PostId
                };
                await context.Comments.AddAsync(comment);
                await context.SaveChangesAsync();
                return new CustomResultViewModel<string>()
                {
                    Entity = comment.Text,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Comment created successfully")
                };
            }
        }
    }
}
