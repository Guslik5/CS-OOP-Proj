using Isu.Entities;
using Isu.exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private const int CoursePossitionInNameGroup = 2;
    private const int MaxCountOfStudents = 30;
    private List<Group> _groups = new List<Group>();
    private int _id = 0;

    public Group AddGroup(GroupName name)
    {
        var group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        _id++;
        foreach (var igroup in _groups)
        {
            if (igroup.NameOfGroup.NameGroup == group.NameOfGroup.NameGroup)
            {
                if (igroup.ListStudents.Count > MaxCountOfStudents)
                {
                    throw new MaxStudentExeption();
                }

                var student = new Student(group, name, _id);
                igroup.ListStudents.Add(student);
                return student;
            }
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
        foreach (var group in _groups)
        {
            foreach (var student in group.ListStudents)
            {
                if (student.ID == id)
                {
                    return student;
                }
            }
        }

        return null;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        var flag = 0;
        var students = new List<Student>();
        foreach (var group in _groups)
        {
            if (group.NameOfGroup.NameGroup == groupName.NameGroup)
            {
                students = group.ListStudents;
                flag = 1;
            }
        }

        if (flag == 0)
        {
            throw new GroupNotFoundException("Error\n Students is not found: ", groupName.NameGroup);
        }

        return students;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var students = new List<Student>();
        var groups = FindGroups(courseNumber);
        foreach (var group in groups)
        {
            foreach (var student in group.ListStudents)
            {
                students.Add(student);
            }
        }

        if (students.Count == 0)
        {
            throw new CourseNumberStudentExeption();
        }

        return students;
    }

    public Group? FindGroup(GroupName groupName)
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

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        var groups = new List<Group>();
        var flag = 0;
        foreach (Group group in _groups)
        {
            if (group.NameOfGroup.NameGroup[CoursePossitionInNameGroup] == courseNumber.CourseOfNumber)
            {
                flag = 1;
                groups.Add(group);
            }
        }

        if (flag == 0)
        {
            throw new CourseNumberGroupException();
        }

        return groups;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        var oldGroup = student.Group.NameGroup;
        var flag = 0;
        foreach (Group group in _groups)
        {
            if (group.NameOfGroup.NameGroup == newGroup.NameOfGroup.NameGroup)
            {
                // var newStudent = new Student(newGroup, student.NameOfStudent, student.ID);
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
}