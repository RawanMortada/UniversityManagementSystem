namespace UniversityManagementSystem.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //foreign key
        public ICollection<Course> Courses { get; set; }
    }
}
