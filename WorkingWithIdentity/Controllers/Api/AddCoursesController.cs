using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;
using WorkingWithIdentity.ViewModels;

namespace WorkingWithIdentity.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCoursesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public AddCoursesController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        [HttpPost]
        public StatusCodeResult CreateNewRentals(StudentCoursesViewModel enrollCourses)
        {
            //var student = _context.Students.Single(
            //     c => c.Id.Equals(enrollCourses.StudentId));
            //if (student == null)
            //    return BadRequest();
            //CourseStudent cs = new CourseStudent();
            //var courses = _context.Courses.Where(
            //    m => enrollCourses.CoursesIds.Contains(m.CourseId)).ToList();          
            //foreach (var course in courses)
            //{
            //    if (course != null)
            //    {
            //        cs.Course = course;
            //        cs.CourseId = course.CourseId;
            //        cs.Student = student;
            //        cs.StudentId = Convert.ToString(student.Id);
            //        _context.SaveChanges();
            //    }
            //}
            return Ok();
        }

    }
}