namespace Domain.Courses.ValueObjects;
public record CourseDuration
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    private CourseDuration(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    public static CourseDuration? Create(DateTime startDate, DateTime endDate)
    {
        if (startDate < endDate)
        {
            return new CourseDuration(startDate, endDate);
        }

        return null;
    }
}
