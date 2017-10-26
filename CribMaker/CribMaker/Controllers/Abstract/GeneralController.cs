#region

using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CribMaker.Core.Data.Entities;
using CribMaker.Services.Services.Factory;

#endregion

namespace CribMaker.Controllers.Abstract
{
    public class GeneralController : Controller
    {
        private ApplicationUser _сurrentUser;
        protected readonly IServiceManager ServiceManager;
        protected GeneralController(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
        }

        protected string CurretUserId
        {
            get
            {
                var userId = User.Identity.GetUserId();
                if (userId == null) throw new ArgumentException("Use this only for authenticated users!");
                return userId;
            }
        }

        protected ApplicationUser CurrentUser
        {
            get
            {
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentException("Use this property only in authorize context!");
                }

                return _сurrentUser ?? (_сurrentUser = ServiceManager.ApplicationUserService.GetByUserName(userName));
            }
        }
    }
}