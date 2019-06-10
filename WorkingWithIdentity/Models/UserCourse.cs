namespace WorkingWithIdentity.Models
{
    public class UserCourse : BaseEntity
    {
        public MyUser User { get; set; }
        public string UserId { get; set; }
        public Course Course { get; set; }
        public string CourseId { get; set; }

    }
}