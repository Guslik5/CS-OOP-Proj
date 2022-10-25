using Isu.Entities;
using Isu.Exceptions;
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
        if (group.ListStudents.Count() > MaxCountOfStudents)
        {
            throw new MaxStudentExeption();
        }

        _id++;
        if (_groups.Contains(group))
        {
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

        var allStudents = _groups.SelectMany(d => d.ListStudents).ToList();
        var findStudent = allStudents.Where(p => p.ID.Equals(id));
        if (!findStudent.Any())
        {
            throw new StudentException("Student not found");
        }

        return findStudent.First();
    }

    public IEnumerable<Student> FindStudents(GroupName groupName)
    {
        var desiredStudents = _groups.SelectMany(s => s.ListStudents).
            Where(s => s.Group.NameOfGroup.Equals(groupName));
        return desiredStudents;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var groups = FindGroups(courseNumber);
        var students = groups.SelectMany(p => p.ListStudents);

        if (students.Count() == 0)
        {
            throw new CourseNumberStudentExeption("There are no students of such a course");
        }

        return students.ToList();
    }

    public List<Group> FindGroup(GroupName groupName)
    {
        var group = _groups.Where(g => g.NameOfGroup.NameGroup.Equals(groupName.NameGroup));
        if (group.Count() == 0)
        {
            throw new GroupNotFoundException("Group is not found: ", groupName.NameGroup);
        }

        return group.ToList();
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber.CourseOfNumber > 4 | courseNumber.CourseOfNumber < 0)
        {
            throw new CourseException("Invalid coursenumber");
        }

        var groups = _groups.Where(g => int.Parse(g.NameOfGroup.NameGroup[2].ToString()).Equals(courseNumber.CourseOfNumber));

        if (!groups.Any())
        {
            throw new CourseNumberGroupException();
        }

        return groups.ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        Group oldGroup = student.Group;
        if (!_groups.Contains(newGroup))
        {
            throw new GroupNotFoundException("Error\n Group for student not found: ", newGroup.NameOfGroup.NameGroup);
        }

        var copyStudent = new Student(newGroup, student.NameOfStudent, student.ID);
        newGroup.Add(copyStudent);
        oldGroup.Remove(student);
    }
}