using Microsoft.EntityFrameworkCore;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Repository;
using Photocopy.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Photocopy.DataAccess.Repository
{
    public class BlogNodeRepository : Repository<BlogNode>, IBlogNodeRepository
    {
        public BlogNodeRepository(ApplicationContext context) : base(context)
        {
        }

            
        public async Task<BlogPage> AddBlogPageAsync(BlogPage entity)
        {
			await Context.Set<BlogPage>().AddAsync(entity);

			return entity;
		}
		public async Task<BlogPage> UpdateBlogPage(BlogPage entity)
		{
			Context.Set<BlogPage>().Update(entity);

			return entity;
		}

		public IEnumerable<BlogPage> GetBlogPageByIdAsync(Expression<Func<BlogPage, bool>> predicate)
        {
            return Context.Set<BlogPage>().Where(predicate).ToList();
        }
       
        public void RemoveBlogNode(Expression<Func<BlogNode, bool>> predicate)
        {
            BlogNode BlogNode = Context.Set<BlogNode>().Where(predicate).Single();
            Context.Set<BlogNode>().Remove(BlogNode);
        }

        public void RemoveBlogPage(BlogPage entity)
        {
            Context.Set<BlogPage>().Remove(entity);
        }

        public async Task<IEnumerable<BlogPage>> GetAllPageAsync(Expression<Func<BlogPage, bool>> predicate)
        {
            return await Context.Set<BlogPage>().Where(predicate).ToListAsync();
        }
    }
}
