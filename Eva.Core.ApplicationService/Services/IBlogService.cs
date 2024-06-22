﻿using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IBlogService : IEvaBaseService<Blog, BlogViewModel>
    {
        Task<ActionResultViewModel<Blog>> CreateBlog(string blogTitle);
    }
}
