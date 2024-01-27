using Domain.Courses.ValueObjects;

namespace Domain.Courses;
public interface ICourseRepository
{
    void Add(Course course);
    Task<Course> GetByIdAsync(CourseId id);
}