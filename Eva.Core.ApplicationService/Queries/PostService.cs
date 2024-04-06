using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Infra.Tools.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class PostService : BaseService<Post, PostViewModel>, IPostService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public PostService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ActionResultViewModel<Post>> CreatePost(PostCreationViewModel postCreationViewModel)
        {
            using (EvaDbContext context = _dbContextFactory.CreateDbContext())
            {
                if (!postCreationViewModel.IsValid())
                    throw new EvaInvalidException("Provided properties are invalid");

                var blog = await context.Blogs.FirstOrDefaultAsync(b => b.Id == postCreationViewModel.BlogId);
                if (blog is null)
                    throw new EvaNotFoundException("No blog found with the given blogId", typeof(Blog));

                var post = new Post()
                {
                    Title = postCreationViewModel.Title,
                    Content = postCreationViewModel.Content,
                    BlogId = postCreationViewModel.BlogId
                };
                await context.Posts.AddAsync(post);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<Post>()
                {
                    Entity = post,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Post created successfully")
                };
            }
        }
    }
}
