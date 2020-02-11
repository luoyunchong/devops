using AutoMapper;
using DevOps.Core.Domain;
using DevOps.Core.Models.Posts;

namespace DevOps.Core.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto,Post>();
        }
    }
}