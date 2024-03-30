using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class BlogService : BaseService<Blog, BlogViewModel>, IBlogService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public BlogService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<ActionResultViewModel<Blog>> CreateBlog(string blogTitle)
        {
            using (EvaDbContext context = _dbContextFactory.CreateDbContext())
            {
                if (string.IsNullOrEmpty(blogTitle))
                    throw new EvaRequiredPropertyException("Blog title is required");

                var blog = new Blog()
                {
                    Title = blogTitle
                };
                await context.Blogs.AddAsync(blog);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<Blog>()
                {
                    Entity = blog,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Blog created successfully")
                };
            }
        }
    }
}
