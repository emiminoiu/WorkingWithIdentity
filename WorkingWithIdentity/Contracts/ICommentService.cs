using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithIdentity.Contracts
{
    public interface ICommentService
    {
         Task<IActionResult> CreateComment(string CommentContent);
         Task<IActionResult> DeleteComment(string Username, string Comment);
         Task<IActionResult> GoToProfilePage(string Username);
         Task<IActionResult> ViewComments(string CourseId);
    }
}
