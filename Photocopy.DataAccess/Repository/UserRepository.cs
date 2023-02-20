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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(Expression<Func<Role, bool>> predicate)
        {
            return await Context.Set<Role>().Where(predicate).ToListAsync();
        }

        public IEnumerable<UserRole> FindUserRolesAsync(Expression<Func<UserRole, bool>> predicate)
        {
            return Context.Set<UserRole>().Where(predicate);
        }

        public IEnumerable<Role> FindRolesAsync(Expression<Func<Role, bool>> predicate)
        {
            return Context.Set<Role>().Where(predicate);
        }

        public void Remove(UserRole entity)
        {
            UserRole userRole= Context.Set<UserRole>().Where(x=>x.UserId==entity.UserId && x.RoleId==entity.RoleId).Single();
            userRole.IsDeleted= true;
            Context.Set<UserRole>().Remove(userRole);
            Context.SaveChanges();
        }

        public async Task<UserRole> AddRoleAsync(UserRole entity)
        {
			await Context.Set<UserRole>().AddAsync(entity);
			await Context.SaveChangesAsync();
			return entity;
        }
		public UserRole UpdateRole(UserRole entity)
		{
			Context.Attach(entity);
			Context.Entry(entity).State = EntityState.Modified;
			return entity;
		}

    }
}
