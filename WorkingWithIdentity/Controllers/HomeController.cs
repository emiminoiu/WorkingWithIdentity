using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;


        public HomeController(ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
         
        }
        public async Task<IActionResult> ShowAllTeachers()
        {
            List<MyUser> teachers = new List<MyUser>();
            var alLUsers = context.Users.ToList();
            foreach (var user in alLUsers)
            {
               if(await userManager.IsInRoleAsync(user,"Teacher"))
               {
                    teachers.Add(user as MyUser);
               }
            }
            return View(teachers);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateInvoice()
        {
            
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public async Task<IActionResult> ViewTeacherProfile(string CourseId)
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id.Equals(CourseId));
            if (course.AuthorName != null)
            {
                var user = await userManager.FindByNameAsync(course.AuthorName) as MyUser;
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                return View("ProfilePage", user);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> ProfilePage()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if(userId == null)
            {
                return RedirectToAction("Index");
            }

            IdentityUser theUser = await userManager.FindByIdAsync(userId);
            return View(theUser);
        }
        public async Task<IActionResult> UpdateProfilePage(MyUser user, IFormFile Image)
        {       
            var theUser =  await userManager.FindByNameAsync(user.UserName) as MyUser;
            theUser.UserName = user.UserName;
            theUser.Email = user.Email;
            theUser.ConcurrencyStamp = user.ConcurrencyStamp;
            theUser.SecurityStamp = user.SecurityStamp;
            theUser.LastName = user.LastName;
            if (Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    theUser.Image = ms.ToArray();
                }
            }
            IdentityResult result = await userManager.UpdateAsync(theUser);
            if (result.Succeeded)
            {
                await context.SaveChangesAsync();
            }
        
            return RedirectToAction("Index", "Students");
        }

        
        public IActionResult ShowInvoice()
        {
            var invoices = context.Invoices.Where(x => x.ClientId.Equals(userManager.GetUserId(HttpContext.User)));
            return View(invoices);
        }
        public IActionResult SideBar()
        {
            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
    }
}
