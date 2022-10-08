using System.Collections;
using Isu.exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEnumerable<Student>
{
    private const int MinGroupLenght = 5;
    private const int MaxGroupLenght = 5;
    private readonly List<Student> _listStudents = new List<Student>();
    public Group(GroupName group)
    {
        if (group.NameGroup.Length < MinGroupLenght ^ group.NameGroup.Length > MaxGroupLenght)
        {
            throw new GroupNameExceptions("Error\n incorrect group name", group.NameGroup);
        }

        NameOfGroup = group;
    }

    public List<Student> ListStudents
    {
        get
        {
            return _listStudents;
        }
    }

    public GroupName NameOfGroup { get; }

    public bool EquallyGroup(Group temp)
    {
        return this.NameOfGroup.NameGroup == temp.NameOfGroup.NameGroup;
    }

    /*
    public static bool operator ==(Group one, Group two)
    {
        return one.NameOfGroup.NameGroup == two.NameOfGroup.NameGroup;
    }

    public static bool operator !=(Group one, Group two)
    {
        return !(one == two);
    }
    Error: 'Isu.Entities.Group' defines operator '==' or operator '!=' but does not override 'Object.Equals(object o)' and 'Object.GetHashCode()'
    */

    public void Add(Student student)
    {
        this.ListStudents.Add(student);
    }

    public CourseNumber GetCourseGroup()
    {
        return new CourseNumber(this.NameOfGroup);
    }

    public IEnumerator<Student> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}