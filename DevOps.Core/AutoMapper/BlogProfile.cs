using AutoMapper;
using DevOps.Core.Domain;
using DevOps.Core.Models.Blogs;

namespace DevOps.Core.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile() 
        {
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>();
        }
    }
}