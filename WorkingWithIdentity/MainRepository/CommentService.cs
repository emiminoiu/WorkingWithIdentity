using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkingWithIdentity.Contracts;
using WorkingWithIdentity.Models;
using WorkingWithIdentity.ViewModels;

namespace WorkingWithIdentity.MainRepository
{
    //public class CommentService : ICommentService
    //{
    //    public IRepository<Comment> commentRepository;
    //    public IRepository<Course> courseRepository;
    //    public CommentService(IRepository<Comment> commentRepository, IRepository<Course> courseRepository)
    //    {
    //        this.commentRepository = commentRepository;
    //        this.courseRepository = courseRepository;
    //    }
    //    //public Task<IActionResult> CreateComment(string CommentContent)
    //    //{
    //    //    UserCommentViewModel model = new UserCommentViewModel();
    //    //    Comment comment = new Comment();
    //    //    var course = courseRepository.Collection().FirstOrDefault(c => c.Id.Equals(Global_CourseId));
    //    //    comment.CourseId = course.Id;
    //    //    comment.CommentContent = CommentContent;
    //    //    comment.UserId = userManager.GetUserId(HttpContext.User);
    //    //    IdentityUser user = await userManager.FindByIdAsync(comment.UserId);
    //    //    model.Username = user.UserName;
    //    //    model.Comment = comment.CommentContent;
    //    //    Global_models.Add(model);
    //    //    await _context.AddAsync(comment);
    //    //    await _context.SaveChangesAsync();
    //    //    return View("ViewComments", Global_models);
    //    //}

    //    public Task<IActionResult> DeleteComment(string Username, string Comment)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IActionResult> GoToProfilePage(string Username)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IActionResult> ViewComments(string CourseId)
    //    {
    //        throw new NotImplementedException();
    //    }
    
}
