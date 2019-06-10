using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WorkingWithIdentity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public CoursesController(ApplicationDbContext context, IHostingEnvironment _appEnvironment,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            this._appEnvironment = _appEnvironment;
            this.userManager = userManager;
           
        }
        public async Task<IActionResult> AddUser(string CourseId)
        {
            UserCourse userCourse = new UserCourse();
            var userId = userManager.GetUserId(HttpContext.User);
            userCourse.UserId = userId;
            userCourse.CourseId = CourseId;
            await _context.UserCourses.AddAsync(userCourse);
            await _context.SaveChangesAsync();
            return View("EnrollSuccessfully");
        }
        public async Task<IActionResult> ViewStudents(string CourseId)
        {
            List<MyUser> users = new List<MyUser>();
            var userCourses = await _context.UserCourses
                .Where(u => u.CourseId.Equals(CourseId)).ToListAsync();
            foreach(var usercourse in userCourses)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(usercourse.UserId));
                users.Add(user as MyUser);
            }
            return View(users);
        }
        public async Task<IActionResult> ViewMyCourses()
        {
            List<Course> courses = new List<Course>();
            var userId = userManager.GetUserId(HttpContext.User);
            var userCourses = await _context.UserCourses.ToListAsync();
            foreach (var usercourse in userCourses)
            {
                if(usercourse.UserId.Equals(userId))
                {
                    var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id.Equals(usercourse.CourseId));
                    courses.Add(course);
                    await _context.SaveChangesAsync();
                }              
            }
            return View(courses);

        }
        [Authorize]
        public async Task<IActionResult> CreateComment(string CourseName,string CommentContent)
        {
            Comment comment = new Comment();
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseName.Equals(CourseName));
            comment.CourseId = course.Id;
            comment.CommentContent = CommentContent;
            comment.UserId = userManager.GetUserId(HttpContext.User);
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }
        [Authorize]
        public IActionResult AddComment()
        {
            return View();
        }
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }
       

        [HttpGet]
        public IActionResult GetName(string term)
        {
            //var result = (from N in _context.Students
            //              where N.Name.Contains(term)
            //              select new { value = N.Name });
            var result = _context.Courses.Where(s => s.CourseName.Contains(term))
                         .Select(s => s.CourseName);
            return Json(result);
        }
        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,Image")] Course course)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {

                        var file = Image;
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "img\\courses");

                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse
                                (file.ContentDisposition).FileName.Trim('"');

                            System.Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                course.Image = file.FileName;
                            }


                        }
                    }
                }              
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseId,CourseName,Image")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(string id)
        {
            return _context.Courses.Any(e => e.Id.Equals(id));
        }
    }
}
