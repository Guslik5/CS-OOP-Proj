using Isu.Entities;

namespace Isu.Models;

public class CourseNumber
{
    public CourseNumber(GroupName name)
    {
        CourseOfNumber = name.NameGroup[2];
    }

    public int CourseOfNumber { get; }
}