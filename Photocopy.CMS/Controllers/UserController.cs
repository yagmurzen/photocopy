using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.FixType;
using Photocopy.Entities.Dto;
using System.Xml.Linq;

namespace Photocopy.CMS.Controllers
{
    //[Authorize("Admin")]
    public class UserController : Controller
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(ILogger<AuthController> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index(int? userId)
        {
            var user = new UserDto();
            if (userId != null) user = _userService.GetUserById(userId.Value);
            return View("/Views/User/Index.cshtml", user);
        }
        [HttpGet]

        public IActionResult UserList()
        {
            IList<UserDto> users = _userService.GetAllUser();
           
            return View("/Views/User/UserList.cshtml",users);

        }
        [HttpPost]
        public async Task<IActionResult> UserAsync(UserDto user)
        {
            UserDto userModel =await _userService.SaveOrUpdateAsync(user);

            return RedirectToAction("Index", new { userId=userModel.Id });

        }
        [HttpPost]

        public IActionResult UserRole(UserRoleDto user)
        {
            UserRoleDto userInModel =_userService.AddRole(user);
            #region Update İşlemleri
            //if (user.Id != null)
            //{ 
            //    userInModel = Db.UserRoles.Where(x => x.Id==user.Id).Single();
            //    userInModel.UserId = user.UserId;
            //    userInModel.RoleId = user.RoleId;
            //    Db.Update(userInModel);

            //}
            //else
            //{
            //    userInModel = _mapper.Map<UserRole>(user);
            //    Db.Add(userInModel);

            //}
            //Db.SaveChanges();

            #endregion

            return RedirectToAction("Index", new { userId = userInModel.UserId });

        }
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);

            return RedirectToAction("UserList");

        }
        [HttpPost]
        public IActionResult DeleteUserRole(int roleId,int userId)
        {
            //#region Delete İşlemleri

            //var entity = Db.UserRoles.Where(x=>x.RoleId==roleId && x.UserId==userId).Take(1).Single();
            //Db.UserRoles.Remove(entity);
            //Db.SaveChanges();

            //#endregion
            _userService.DeleteUserRoleAsync(userId, roleId);

            return RedirectToAction("Index", new { userId = userId });


        }
    }
}
