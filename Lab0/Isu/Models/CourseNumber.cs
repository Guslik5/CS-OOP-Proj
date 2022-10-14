using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private const int CoursePossitionInNameGroup = 2;
    private const int MaxCourse = 4;
    private const int MinCourse = 1;
    public CourseNumber(GroupName name)
    {
        if (name.NameGroup[CoursePossitionInNameGroup] > MaxCourse || name.NameGroup[CoursePossitionInNameGroup] < MinCourse)
        {
            throw new CourseException("Error \nCourseNumver");
        }

        CourseOfNumber = name.NameGroup[CoursePossitionInNameGroup];
    }

    public int CourseOfNumber { get; }
}