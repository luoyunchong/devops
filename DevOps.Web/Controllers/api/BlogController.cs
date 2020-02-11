using System;
using System.Collections.Generic;
using AutoMapper;
using DevOps.Core.Domain;
using DevOps.Core.Models.Blogs;
using DevOps.Core.Web;
using Microsoft.AspNetCore.Mvc;

namespace DevOps.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        // GET api/Blog

        private readonly IFreeSql _fsql;
        private readonly IMapper _mapper;
        public BlogController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        /// <summary>
        /// �����б�ҳ 
        /// </summary>
        /// <param name="pageDto">��ҳ����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<PagedResultDto<Blog>> Get([FromQuery]PageDto pageDto)
        {
            List<Blog> blogs = _fsql.Select<Blog>().OrderByDescending(r => r.CreateTime).Page(pageDto.PageNumber, pageDto.PageSize).ToList();
            long count = _fsql.Select<Blog>().Count();
            return new PagedResultDto<Blog>(count, blogs);
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public ActionResult<Blog> Get(int id)
        {
            // eg.1 return _fsql.Select<Blog>().Where(a => a.Id == id).ToOne();
            // eg.2
            return _fsql.Select<Blog>(id).ToOne();
        }

        // POST api/blog
        [HttpPost]
        public void Post([FromBody] CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            _fsql.Insert<Blog>(blog).ExecuteAffrows();
        }

        // PUT api/blog
        [HttpPut]
        public void Put([FromBody] UpdateBlogDto updateBlogDto)
        {

            //eg.1 ����ָ����
            //_fsql.Update<Blog>(updateBlogDto.BlogId).Set(a => new Blog()
            //{
            //    Title = updateBlogDto.Title,
            //    Content = updateBlogDto.Content
            //}).ExecuteAffrows();

            //eg.2�����ʵ����µ����ݿ��С�������ʱ����������е�ֵ����CreateTimeҲ���µ���
            //ʹ��IgnoreColumns�ɺ���ĳһЩ�С�

            Blog blog = _mapper.Map<Blog>(updateBlogDto);
            _fsql.Update<Blog>().SetSource(blog).IgnoreColumns(r => r.CreateTime).ExecuteAffrows();
        }

        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _fsql.Transaction(() =>
            {
                _fsql.Delete<Blog>(new { BlogId = id }).ExecuteAffrows();
                _fsql.Delete<Post>(new { BlogId = id }).ExecuteAffrows();

            });
        }

        private void Test()
        {
            CreateBlogDto createBlogDto = new CreateBlogDto()
            {
                Title = "����title",
                Content = "����content"
            };

            Blog newBlog = new Blog()
            {
                Title = createBlogDto.Title,
                Content = createBlogDto.Content
            };

        }
    }
}