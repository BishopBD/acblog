﻿using AcBlog.Data.Repositories;

namespace AcBlog.SDK
{
    public interface ICategoryService : ICategoryRepository
    {
        IBlogService Blog { get; }
    }
}
