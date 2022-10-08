using Isu.Entities;
using Isu.exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private const int CoursePossitionInNameGroup = 2;
    private const int MaxCountOfStudents = 30;
    private readonly List<Group> _groups = new List<Group>();
    private int _id = 0;

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
            if (group.ListStudents.Count > MaxCountOfStudents)
            {
                throw new MaxStudentExeption();
            }

            var student = new Student(group, name, _id);
            group.ListStudents.Add(student);
            return student;
        }

        throw new GroupNotFoundException("Error\n Group is not found: ", group.NameOfGroup.NameGroup);
    }

    public Student GetStudent(int id)
    {
        var student = FindStudent(id);
        if (student == null)
        {
            throw new StudentException("Student is not found! ");
        }

        return student;
    }

    public Student? FindStudent(int id)
    {
        var desiredStudent = from p in _groups
            from a in p
            where a.ID.Equals(id)
            select a;
        Student student = desiredStudent.First();
        return student;
    }

    public IEnumerable<List<Student>> FindStudents(GroupName groupName)
    {
        var desiredStudents = from p in _groups
            where p.NameOfGroup.Equals(groupName)
            select p.ListStudents;
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

    public Group? FindGroupold(GroupName groupName)
    {
        foreach (var group in _groups)
        {
            if (group.NameOfGroup.NameGroup == groupName.NameGroup)
            {
                return group;
            }
        }

        return null;
    }

    public IEnumerable<Group>? FindGroup(GroupName groupName)
    {
        var group = from p in _groups
            where p.NameOfGroup.NameGroup.Equals(groupName.NameGroup)
            select p;

        return group;
    }

    public IEnumerable<Group> FindGroups(CourseNumber courseNumber)
    {
        var groups = from p in _groups
            where p.GetCourseGroup().Equals(courseNumber)
            select p;

        if (groups.Count() == 0)
        {
            throw new CourseNumberGroupException();
        }

        return groups;
    }

    public void ChangeStudentGroupold(Student student, Group newGroup)
    {
        var oldGroup = student.Group.NameGroup;
        var flag = 0;
        foreach (Group group in _groups)
        {
            if (group.NameOfGroup.NameGroup == newGroup.NameOfGroup.NameGroup)
            {
                var copyStudent = new Student(newGroup, student.NameOfStudent, student.ID);
                newGroup.ListStudents.Add(copyStudent);
                flag = 1;
                break;
            }
        }

        if (flag == 0)
        {
            throw new GroupNotFoundException("Error\n Group for student not found: ", newGroup.NameOfGroup.NameGroup);
        }

        foreach (Group group2 in _groups)
        {
            if (group2.NameOfGroup.NameGroup == oldGroup)
            {
                group2.ListStudents.Remove(student);
                return;
            }
        }

        // throw new GroupNotFoundException("Группа для студента не найдена: ", newGroup.NameOfGroup.NameGroup);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        var oldGroup = student.Group;
        if (_groups.Contains(newGroup) == false)
        {
            throw new GroupNotFoundException("Error\n Group for student not found: ", newGroup.NameOfGroup.NameGroup);
        }

        var copyStudent = new Student(newGroup, student.NameOfStudent, student.ID);
        newGroup.ListStudents.Add(copyStudent);
        var result = _groups.Single(s => s.NameOfGroup == oldGroup);
        result.ListStudents.Remove(student);

        // throw new GroupNotFoundException("Группа для студента не найдена: ", newGroup.NameOfGroup.NameGroup);
    }
}