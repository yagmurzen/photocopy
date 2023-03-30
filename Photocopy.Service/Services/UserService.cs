using AutoMapper;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Service.Services
{
    public class UserService : IUserService
    {
		ICryptoHelper _cryptoHelper;

		private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public UserService(ICryptoHelper cryptoHelper, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_cryptoHelper = cryptoHelper;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

        public UserDto GetUserById(int userId)
        {
            User? user = _unitOfWork.Users.GetByIdAsync(x => !x.IsDeleted && x.Id == userId);

            var userRoles = _unitOfWork.Users.FindUserRolesAsync(ur => ur.UserId == user.Id && !ur.IsDeleted);

            IList<Role> roles = _unitOfWork.Users.FindRolesAsync(ro => userRoles.Any(ur => ur.RoleId == ro.Id && !ur.IsDeleted) && !ro.IsDeleted).ToList();

            IEnumerable<Role> allRoles = (IEnumerable<Role>)_unitOfWork.Users.GetAllRolesAsync(x => !x.IsDeleted).Result.ToList(); ;

            var role = user.UserRoles;

            UserDto userModel = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Gsm = user.Gsm,
                Password = _cryptoHelper.DecryptString(user.Password),
                UserRoles = _mapper.Map<IList<RoleDto>>(roles),
                Roles = _mapper.Map<IList<RoleDto>>(allRoles)

            };

            return userModel;
        } 

        public IList<UserDto> GetAllUser()
        {
            var users = _unitOfWork.Users.GetAllAsync(x=>!x.IsDeleted).Result;

            return _mapper.Map<IList<UserDto>>(users);
        }

        public async Task<UserDto> SaveOrUpdateAsync(UserDto user)
        {
            user.Password = _cryptoHelper.EncryptString(user.Password);
            User userDbModel= _mapper.Map<User>(user);
            if (user.Id == 0)
                await _unitOfWork.Users.AddAsync(userDbModel);
            else
                await _unitOfWork.Users.Update(userDbModel);

			await _unitOfWork.CommitAsync();

			return _mapper.Map<UserDto>(userDbModel);
        }

        public async void DeleteUser(int userId)
        {
            User user = _unitOfWork.Users.GetByIdAsync(x => !x.IsDeleted && x.Id == userId);
            user.IsDeleted = true;
            await _unitOfWork.Users.Update(user);
             _unitOfWork.CommitAsync();

        }

        public async void DeleteUserRoleAsync(int userId,int userRoleId)
        {
            var userRole =  _unitOfWork.Users.FindUserRolesAsync(ur => ur.UserId == userId && !ur.IsDeleted && ur.RoleId == userRoleId).ToList().Take(1);
            foreach (var item in userRole)
            {
                item.IsDeleted = true;
                 await _unitOfWork.Users.UpdateRole(item);
                _unitOfWork.CommitAsync();

            }

        }

        public UserRoleDto AddRole(UserRoleDto userRole)
        {
            UserRole userDbModel = _mapper.Map<UserRole>(userRole);
            if (userRole.Id!=null)
				_unitOfWork.Users.UpdateRole(userDbModel);
            else 
			    _unitOfWork.Users.AddRoleAsync(userDbModel);

            _unitOfWork.CommitAsync();

            return _mapper.Map<UserRoleDto>(userDbModel);
        }

        public UserDto? GetUserByUsername(string username, string password)
        {
           var  _password = _cryptoHelper.EncryptString(password);
            User? user = _unitOfWork.Users.Find(x => x.UserName == username && !x.IsDeleted ).Take(1).SingleOrDefault() as User;
            if (user!=null)
            {
                var pass = _cryptoHelper.DecryptString(user.Password);
                if (pass == pass)
                    return GetUserById(user.Id);
                return null;
            }

            return null;
		}
	}
}
