namespace WorkingWithIdentity.Models
{
    public class StudentAddress : BaseEntity
    {
        public string Address { get; set; }
        public Student Student { get; set; }
        public string StudentId { get; set; }
    }
}