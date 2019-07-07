using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;
using WorkingWithIdentity.ViewModels;

namespace WorkingWithIdentity.ViewCompenents
{
    public class GetBudgetViewComponent : ViewComponent
    {
        public ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        public static BudgetViewModel model = new BudgetViewModel();
        public GetBudgetViewComponent(ApplicationDbContext _context, UserManager<IdentityUser> userManager)
        {
            this._context = _context;
            this.userManager = userManager;
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var user = await userManager.FindByIdAsync(userId) as MyUser;
            if (user != null)
            {
                model.Budget = user.Budget;
                model.NoOfCourses = user.NoOfCourses;
            }
            return View(model);
        }
    }
}   
