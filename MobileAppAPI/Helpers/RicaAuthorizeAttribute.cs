using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;

namespace MobileAppAPI.WebApi.Helpers
{
    public class MobileAppAPIAuthorizeAttribute : Attribute, IAuthorizationFilter
    {        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var existingClaims = context.HttpContext.User.Claims;
            var controllerName = context.RouteData.Values["controller"];

            var IsSuperAdmin = existingClaims.FirstOrDefault(r => r.Type == "IsSuperAdmin");
            if(IsSuperAdmin.Value == "YES")
            {
                //bypass everything...
            }
            else
            {
                if (controllerName.ToString() == "User")
                {
                    var allowedCompanies = existingClaims.FirstOrDefault(r => r.Type == "Companies");
                    if (allowedCompanies != null)
                    {
                        var companyId = context.HttpContext.Request.Query["companyid"];
                        var allowedCompanyIds = (allowedCompanies.Value).Split(',').Select(c => c.Trim()).ToList();
                        if (allowedCompanyIds != null && allowedCompanyIds.Any() && allowedCompanyIds.Contains(companyId))
                        {
                            //all good
                        }
                        else
                        {
                            context.Result = new BadRequestObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                            //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        }
                    }
                    else
                    {
                        context.Result = new BadRequestObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
                else
                {
                    var accessLevelClaim = existingClaims.FirstOrDefault(r => r.Type == controllerName.ToString());

                    if (accessLevelClaim != null)
                    {
                        var accessLevel = accessLevelClaim.Value;
                        var requestMethod = context.HttpContext.Request.Method;
                        if (requestMethod == HttpMethod.Get.Method && !accessLevel.Contains("R"))
                        {
                            context.Result = new BadRequestObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                            //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        }
                        if ((requestMethod == HttpMethod.Post.Method || requestMethod == HttpMethod.Put.Method) && !accessLevel.Contains("W"))
                        {
                            //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                            context.Result = new BadRequestObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        }
                        if (requestMethod == HttpMethod.Delete.Method && !accessLevel.Contains("D"))
                        {
                            //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                            context.Result = new BadRequestObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        }
                    }
                    else
                    {
                        context.Result = new BadRequestObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
            }

            

           
        }
    }
}