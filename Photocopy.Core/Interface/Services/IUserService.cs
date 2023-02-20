using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Services
{
    public interface IUserService
    {
        UserDto GetUserById(int userId);
        UserDto GetUserByUsername(string username, string password);

        Task<UserDto> SaveOrUpdateAsync(UserDto user);
        IList<UserDto> GetAllUser();
        void DeleteUser(int userId);
        void DeleteUserRole(int userId,int userRoleId);

        UserRoleDto AddRole(UserRoleDto userRole);

    }
}
