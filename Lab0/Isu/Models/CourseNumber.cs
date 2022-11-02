using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private const int CoursePossitionInNameGroup = 2;
    private const int MaxCourse = 4;
    private const int MinCourse = 1;
    public CourseNumber(GroupName name)
    {
        var courseNumber = name.NameGroup[CoursePossitionInNameGroup] - '0';
        if (courseNumber >= MaxCourse | courseNumber < MinCourse)
        {
            throw new CourseInvalidException("Error \nCourseNumber");
        }

        CourseOfNumber = name.NameGroup[CoursePossitionInNameGroup] - '0';
    }

    public int CourseOfNumber { get; }
}