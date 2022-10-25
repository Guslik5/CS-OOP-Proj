using System.Collections;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int MinGroupLength = 5;
    private const int MaxGroupLength = 6;
    private const int MaxStudentInGroup = 30;
    private readonly List<Student> _listStudents = new List<Student>();
    public Group(GroupName group)
    {
        if (group.NameGroup.Length > MaxGroupLength || group.NameGroup.Length < MinGroupLength)
        {
            throw new GroupNameExceptions("Error\n incorrect group name", group.NameGroup);
        }

        NameOfGroup = group;
    }

    public IEnumerable<Student> ListStudents => _listStudents;

    public GroupName NameOfGroup { get; }

    public void Add(Student student)
    {
        if (_listStudents.Count() > MaxStudentInGroup)
        {
            throw new MaxStudentExeption();
        }

        this._listStudents.Add(student);
    }

    public void Remove(Student student)
    {
        this._listStudents.Remove(student);
    }

    public CourseNumber GetCourseGroup()
    {
        return new CourseNumber(this.NameOfGroup);
    }

    public bool Equals(Group other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _listStudents.Equals(other._listStudents) && NameOfGroup.Equals(other.NameOfGroup);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Group)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_listStudents, NameOfGroup);
    }
}