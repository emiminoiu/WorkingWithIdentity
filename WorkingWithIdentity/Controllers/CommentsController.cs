using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;
using WorkingWithIdentity.ViewModels;

namespace WorkingWithIdentity.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        static List<UserCommentViewModel> Global_models = new List<UserCommentViewModel>();
        static string Global_CourseId;
        public CommentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> CreateComment(string CommentContent)
        {
            UserCommentViewModel model = new UserCommentViewModel();
            Comment comment = new Comment();
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id.Equals(Global_CourseId));
            comment.CourseId = course.Id;
            comment.CommentContent = CommentContent;
            comment.UserId = userManager.GetUserId(HttpContext.User);
            IdentityUser user = await userManager.FindByIdAsync(comment.UserId);
            model.Username = user.UserName;
            model.Comment = comment.CommentContent;
            Global_models.Add(model);
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            return View("ViewComments",Global_models);

        }
        public async Task<IActionResult> DeleteComment(string Username,string Comment)
        {
            UserCommentViewModel model = new UserCommentViewModel();
            var theComment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentContent.Equals(Comment));
            IdentityUser user = await userManager.FindByIdAsync(theComment.UserId);
            model.Username = user.UserName;
            model.Comment = theComment.CommentContent;
           
            foreach (var searchedModel in Global_models)
            {
                if ((searchedModel.Username.Equals(model.Username)) && (searchedModel.Comment.Equals(model.Comment))) 
                {
                    Global_models.Remove(searchedModel);
                    break;
                }
            }
            _context.Remove(theComment);
            await _context.SaveChangesAsync();
            return View("ViewComments", Global_models);
        }

        public async Task<IActionResult> GoToProfilePage(string Username)
        {
            var user = await userManager.FindByNameAsync(Username);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View("ProfilePage", user);
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments.Include(c => c.Course).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Course)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        public async Task<IActionResult> ViewComments(string CourseId)
        {
            List<UserCommentViewModel> models = new List<UserCommentViewModel>();
            UserCommentViewModel model;
            Global_CourseId = CourseId;
            var comments = _context.Comments.Where(c => c.CourseId.Equals(CourseId)).ToList();
            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    model = new UserCommentViewModel();
                    var user = await userManager.FindByIdAsync(comment.UserId);
                    model.Username = user.UserName.ToString();
                    model.Comment = comment.CommentContent.ToString();
                    models.Add(model);
                   
                }
                Global_models = models;
                return View(models);
            }
            return View("NoComments");
          
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CourseId,CommentContent,Id")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", comment.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", comment.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,CourseId,CommentContent,Id")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", comment.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Course)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(string id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
