namespace Domain.Students;
public interface IStudentRepository
{
    void Add(Student student);
    Task<Student> GetByIdAsync(StudentId id);
    Task<List<Student>> GetStudentsWithCoursesInDateRange(DateTime startDate, DateTime endDate);
}
