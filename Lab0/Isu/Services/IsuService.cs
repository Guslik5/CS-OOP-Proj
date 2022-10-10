using Isu.Entities;
using Isu.exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private const int MaxCountOfStudents = 30;
    private readonly List<Group> _groups = new List<Group>();
    private int _id = 0;

    public IEnumerable<Group> Groups => _groups;
    public Group AddGroup(GroupName name)
    {
        var group = new Group(name);
        if (_groups.Contains(group))
        {
            throw new GroupCreatedException("Error \n Group is created");
        }

        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        _id++;
        if (_groups.Contains(group))
        {
            if (group.ListStudents.Count() > MaxCountOfStudents)
            {
                throw new MaxStudentExeption();
            }

            var student = new Student(group, name, _id);
            group.Add(student);
            return student;
        }

        throw new GroupNotFoundException("Error\n Group is not found: ", group.NameOfGroup.NameGroup);
    }

    public Student GetStudent(int id)
    {
        Student student = FindStudent(id);
        if (student == null)
        {
            throw new StudentException("Student is not found! ");
        }

        return student;
    }

    public Student FindStudent(int id)
    {
        if (id < 1)
        {
            throw new IdStudentException();
        }

        var desiredStudent = from p in _groups
            from a in p
            where a.ID.Equals(id)
            select a;
        Student student = desiredStudent.First();
        return student;
    }

    public IEnumerable<Student> FindStudents(GroupName groupName)
    {
        var desiredStudents = from g in _groups
            where g.NameOfGroup.Equals(groupName)
            from s in g.ListStudents
            select s;
        if (desiredStudents.Count() == 0)
        {
            throw new GroupNotFoundException("Error\n Students is not found: ", groupName.NameGroup);
        }

        return desiredStudents;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var groups = FindGroups(courseNumber);
        var students = from p in groups
            from a in p.ListStudents
            select a;

        if (students.Count() == 0)
        {
            throw new CourseNumberStudentExeption();
        }

        return students.ToList();
    }

    public List<Group> FindGroup(GroupName groupName)
    {
        var group = from p in _groups
            where p.NameOfGroup.NameGroup.Equals(groupName.NameGroup)
            select p;
        if (group.Count() == 0)
        {
            throw new GroupNotFoundException("Group is not found: ", groupName.NameGroup);
        }

        return group.ToList();
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        var groups = from p in _groups
            where p.GetCourseGroup().Equals(courseNumber)
            select p;

        if (groups.Count() == 0)
        {
            throw new CourseNumberGroupException();
        }

        return groups.ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        GroupName oldGroup = student.Group;
        if (_groups.Contains(newGroup) == false)
        {
            throw new GroupNotFoundException("Error\n Group for student not found: ", newGroup.NameOfGroup.NameGroup);
        }

        var copyStudent = new Student(newGroup, student.NameOfStudent, student.ID);
        newGroup.Add(copyStudent);
        Group result = _groups.Single(s => s.NameOfGroup == oldGroup);
        result.Remove(student);
    }
}