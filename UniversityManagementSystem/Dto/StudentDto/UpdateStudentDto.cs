namespace UniversityManagementSystem.Dto.StudentDto
{
    public class UpdateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PaymentStatus { get; set; } //true = done...false = pending 
        public string Major { get; set; }
    }
}
