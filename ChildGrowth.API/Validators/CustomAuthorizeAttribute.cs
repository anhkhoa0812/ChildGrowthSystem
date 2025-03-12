using ChildGrowth.API.Enums;
using ChildGrowth.API.Utils;
using Microsoft.AspNetCore.Authorization;

namespace ChildGrowth.API.Validators;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(params RoleEnum[] roleEnums)
    {
        var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
        Roles = string.Join(",", allowedRolesAsString);
    }
}