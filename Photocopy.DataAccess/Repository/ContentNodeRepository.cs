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
    public class ContentNodeRepository : Repository<ContentNode>, IContentNodeRepository
    {
        public ContentNodeRepository(ApplicationContext context) : base(context)
        {
        }


        public async Task<ContentPage> AddContentPageAsync(ContentPage entity)
        {
			await Context.Set<ContentPage>().AddAsync(entity);
			await Context.SaveChangesAsync();

			return entity;
        }
        public IEnumerable<ContentNode> GetAllContentNodeAsync(Expression<Func<ContentNode, bool>> predicate)
        {
            return Context.Set<ContentNode>().Where(predicate);
        }

        public ContentNode GetContentNodeAsync(Expression<Func<ContentNode, bool>> predicate)
        {
            return Context.Set<ContentNode>().Where(predicate).SingleOrDefault();
        }

        public IEnumerable<ContentPage> GetContentPageByIdAsync(Expression<Func<ContentPage, bool>> predicate)
        {
            return Context.Set<ContentPage>().Where(predicate);
        }

        public IEnumerable<Slider> GetSliderAsync(Expression<Func<Slider, bool>> predicate)
        {
            return Context.Set<Slider>().Where(predicate);
        }

        public void RemoveContentNode(Expression<Func<ContentNode, bool>> predicate)
        {
            ContentNode contentNode = Context.Set<ContentNode>().Where(predicate).Single();
            Context.Set<ContentNode>().Remove(contentNode);
            Context.SaveChanges();
        }

        public void RemoveContentPage(ContentPage entity)
        {
            Context.Set<ContentPage>().Remove(entity);
            Context.SaveChanges();
        }

     
    }
}
