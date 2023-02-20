using Photocopy.Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email adresi Giriniz")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Geçersiz Email Adresi.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefon Numarası Giriniz")]
        [Display(Name = "Gsm")]
        [DataType(DataType.PhoneNumber)]
        public string Gsm { get; set; }
        public string Password { get; set; }
        public IList<RoleDto> Roles { get; set; }

        public IList<RoleDto> UserRoles { get; set; }

    }

    public class RoleDto {
        public string Id { get; set; }
        public string Name { get; set; }

    }

    public class UserRoleDto
    {

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? Id { get; set; }
    }
}
