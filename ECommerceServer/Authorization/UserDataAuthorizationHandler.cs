using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceServer.Database;
using ECommerceServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ECommerceServer.Authorization
{
    public class UserDataAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, User resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != "Create"&&
                requirement.Name != "Read" &&
                requirement.Name != "Update" &&
                requirement.Name != "Delete")
            {
                return Task.CompletedTask;
            }

            if (resource.UserId.ToString() == context.User.FindFirst("user-id").Value)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
