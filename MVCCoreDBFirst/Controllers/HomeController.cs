using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCCoreDBFirst.Data;
using MVCCoreDBFirst.Models;


namespace MVCCoreDBFirst.Controllers
{
    public class HomeController : Controller
    {

        protected ApplicationDBContext mContext;
        protected UserManager<ApplicationUser> mUserManager;
        protected SignInManager<ApplicationUser> mSignInManager;

        public HomeController(
            ApplicationDBContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;

        }
        public IActionResult Index()
        {

            mContext.Database.EnsureCreated();

            if (!mContext.Site.Any())
            {
                var site1 = new SiteDataModel()
                {
                    SiteName = "Site 1",
                    SiteDescription = "This is site one"
                };

                mContext.Site.Add(site1);
                //if (!mContext.Users.Any())
                //{
                //    var user1 = new MsysUsersDataModel()
                //    {
                //        UserName = "User 1",
                //        Site = site1
                //    };

                //    mContext.Users.Add(user1);

                //    var user2 = new MsysUsersDataModel()
                //    {
                //        UserName = "User 2",
                //        Site = site1
                //    };
                //    mContext.Users.Add(user2);
                //}

            }



            mContext.SaveChanges();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("create")]
        public async Task<IActionResult> CreateUserAsync ()
        {
            var result = await mUserManager.CreateAsync(new ApplicationUser
            {
                UserName = "Flavien",
                Email = "flavien.deslandres@gmail.com"
            }, "password");


            if (result.Succeeded)
                return Content("User was creeated", "text/html");

            return Content("Unable to create user", "text/html");

        }

        [Authorize]
        [Route("Private")]
        public IActionResult Private()
        {

            return Content($"This is a private area.<br />Welcome {HttpContext.User.Identity.Name}<br />IsAuthenticated : {HttpContext.User.Identity.IsAuthenticated}<br />AuthenticationType : {HttpContext.User.Identity.AuthenticationType}", "text/html");

            //return Content($"This is a private area.<br />Welcome {mUserManager . }<br />IsAuthenticated : {HttpContext.User.Identity.IsAuthenticated}<br />AuthenticationType : {HttpContext.User.Identity.AuthenticationType}", "text/html");
        }

        [Route("Login")]
        public async Task<IActionResult> LoginAsync(string returnURL)
        {

            var result = await mSignInManager.PasswordSignInAsync("Flavien", "password", false, true);
          

            if (result.Succeeded)
                return Content("Login Success");

            return Content("Login failed");
        }


        [Route("Logout")]
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await mSignInManager.SignOutAsync();
            return Content("Logged out");
        }




    }
}
