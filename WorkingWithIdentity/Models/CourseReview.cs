namespace WorkingWithIdentity.Models
{
    public class CourseReview
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string ReviewId { get; set; }
        public Review Review { get; set; }
    }
}