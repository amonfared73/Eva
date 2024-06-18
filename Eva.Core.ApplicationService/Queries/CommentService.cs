using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Core.Domain.Enums;
using Eva.Infra.Tools.Extensions;
using Microsoft.EntityFrameworkCore;
using Eva.Core.ApplicationService.Validators;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class CommentService : BaseService<Comment, CommentViewModel>, ICommentService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        private readonly CommentValidator _commentValidator;
        public CommentService(IEvaDbContextFactory dbContextFactory, CommentValidator commentValidator) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _commentValidator = commentValidator;
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
