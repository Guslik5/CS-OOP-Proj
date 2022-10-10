using System.Collections;
using Isu.exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEnumerable<Student>, IEquatable<Group>
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

    public IEnumerable<Student> ListStudents => _listStudents;

    public GroupName NameOfGroup { get; }

    public void Add(Student student)
    {
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

    public IEnumerator<Student> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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