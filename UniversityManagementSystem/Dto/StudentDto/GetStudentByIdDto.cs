namespace UniversityManagementSystem.Dto.StudentDto
{
    public class GetStudentByIdDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PaymentStatus { get; set; } //true = done...false = pending 
        public int EnrollementYear { get; set; }
        public string Major { get; set; }
    }
}
