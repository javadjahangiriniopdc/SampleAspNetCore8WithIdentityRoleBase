using System.Collections.Generic;

namespace SampleAspNetCore8WithIdentityRoleBase.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<UserRoleModel> Roles { get; set; }
    }

    public class UserRoleModel
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}