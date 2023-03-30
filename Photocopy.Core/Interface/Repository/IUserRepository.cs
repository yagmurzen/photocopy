using Photocopy.Entities.Domain;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<Role>> GetAllRolesAsync(Expression<Func<Role, bool>> predicate);
        IEnumerable<UserRole> FindUserRolesAsync(Expression<Func<UserRole, bool>> predicate);
        IEnumerable<Role> FindRolesAsync(Expression<Func<Role, bool>> predicate);
        void Remove(UserRole entity);
        Task<UserRole> AddRoleAsync(UserRole entity);
        Task<UserRole> UpdateRole(UserRole entity);
    }
}
