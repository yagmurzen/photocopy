using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.DataAccess.Repository;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Service.Services
{
    public class BlogNodeService : IBlogNodeService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BlogNodeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BlogNodeDto> GetBlogNodeAll()
        {
            IEnumerable<BlogNode> node =_unitOfWork.Blogs.GetAll(x=>x.ContentPageType == ContentPageType.Blog && !x.IsDeleted).ToList();
            return _mapper.Map<IEnumerable<BlogNodeDto>>(node);
        }

        public BlogNodeDto GetBlogNodeDetail(int BlogNodeId)
        {
            BlogNode node= _unitOfWork.Blogs.GetByIdAsync(x => x.ContentPageType == ContentPageType.Blog && x.Id == BlogNodeId&& !x.IsDeleted);

            BlogNodeDto outModel = new BlogNodeDto();

            if (node != null)
            {
                IList<BlogPage> pages = _unitOfWork.Blogs.GetBlogPageByIdAsync(x => x.ContentNodeId == node.Id).ToList();
                //IList<Galery> galery = Db.Galeries.Where(x => x.BlogNodeId == node.Id).ToList();

                outModel = new BlogNodeDto
                {
                    Id = node.Id,
                    Name = node.Name,
                    Pages = _mapper.Map<IList<BlogPageDto>>(pages)
                };

            }

            return outModel;

        }

        public BlogNodeDto SaveOrUpdateBlogNode(BlogNodeDto BlogNode)
        {
            BlogNode nodeModel = _mapper.Map<BlogNode>(BlogNode);
            if (nodeModel.Id != 0)
                _unitOfWork.Blogs.Update(nodeModel);
            else
                _unitOfWork.Blogs.AddAsync(nodeModel);

            _unitOfWork.CommitAsync();

            return _mapper.Map<BlogNodeDto>(nodeModel);
        }

        public void DeleteBlogNode(int BlogNodeId)
        {
            _unitOfWork.Blogs.Remove(new BlogNode { Id = BlogNodeId });
        }

        public BlogPageDto GetBlogPageDetail(int BlogPageId)
        {
            IList<BlogPage> page = _unitOfWork.Blogs.GetBlogPageByIdAsync(x => x.Id == BlogPageId && !x.IsDeleted).ToList();

            BlogPageDto outModel = _mapper.Map<BlogPageDto>(page[0]);
            return outModel;
        }


        public async Task<BlogPageDto> SaveOrUpdateBlogPageAsync(BlogPageDto BlogPage)
        {
            BlogPage pageModel = _mapper.Map<BlogPage>(BlogPage);
            pageModel.ContentPageType = ContentPageType.Blog;
            if (pageModel.Id == 0)
                _ = _unitOfWork.Blogs.AddBlogPageAsync(pageModel);
			else
                _ = _unitOfWork.Blogs.UpdateBlogPage(pageModel);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<BlogPageDto>(pageModel);
        }


        public void DeleteBlogPage(int BlogPageId)
        {
            _unitOfWork.Blogs.RemoveBlogPage(new BlogPage { Id = BlogPageId });
        }

    }
}
