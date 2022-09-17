namespace Isu.exceptions;

public class CourseNumberGroupException : Exception
{
    public CourseNumberGroupException()
    {
        Value = "Error\n Groups of this course were not found";
    }

    public string Value { get; }
}