namespace Domain.Courses.ValueObjects;
public record MaxStudents
{
    public int Value { get; }

    private MaxStudents(int value)
    {
        Value = value;
    }

    public static MaxStudents Create(int value)
    {
        if (value < 1)
        {
            return null;
        }
        return new MaxStudents(value);
    }
}