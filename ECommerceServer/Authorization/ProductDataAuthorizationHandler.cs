using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ECommerceServer.Authorization
{
    public class ProductDataAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Product>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Product resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name == "Read")
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == "Create" || 
                requirement.Name == "Update" ||
                requirement.Name == "Delete")
            {
                if (resource.UserId.ToString() == context.User.FindFirst("user-id").Value)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
