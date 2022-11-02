using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private const int MaxCountOfStudents = 30;
    private IsuService _isuService = new IsuService();

    [Fact]
    public void MyTest()
    {
        var group1 = _isuService.AddGroup(new GroupName("M1101"));
        var group2 = _isuService.AddGroup(new GroupName("M1102"));
        var student1 = _isuService.AddStudent(group1, "Sveta");
        var student2 = _isuService.AddStudent(group2, "Andrey");
        var findStudentCourse = _isuService.FindStudents(new CourseNumber(group1.NameOfGroup));
        Assert.Equal("Andrey", findStudentCourse[1].NameOfStudent);
        var findStudents = _isuService.FindStudents(group2.NameOfGroup).ToList();
        Assert.Equal("Andrey", findStudents[0].NameOfStudent);
        Assert.Equal("Andrey", _isuService.FindStudent(2).NameOfStudent);
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var group = _isuService.AddGroup(new GroupName("M1101"));
        var student = _isuService.AddStudent(group, "Sveta");
        Assert.Contains(student, group.ListStudents);
        Assert.Equal(group, student.Group);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var group = _isuService.AddGroup(new GroupName("M1101"));
        for (int i = 0; i <= MaxCountOfStudents; i++)
        {
            var student = _isuService.AddStudent(group, "student" + i.ToString());
        }

        Assert.Throws<MaxStudentExeption>(() => _isuService.AddStudent(group, "studentlast"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<GroupNameExceptions>(() => new GroupName("ErrorGroup1"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var groupWithStudent1 = _isuService.AddGroup(new GroupName("M1101"));
        var groupWithStudent2 = _isuService.AddGroup(new GroupName("M2202"));
        var student1 = _isuService.AddStudent(groupWithStudent1, "Vlad");
        var student2 = _isuService.AddStudent(groupWithStudent2, "Artem");
        _isuService.ChangeStudentGroup(student1, groupWithStudent2);

        Assert.Equal(2, groupWithStudent2.ListStudents.Count());
        Assert.Empty(groupWithStudent1.ListStudents);
    }

    [Fact]
    public void FindStudentId()
    {
        var group = _isuService.AddGroup(new GroupName("M1101"));
        var student1 = _isuService.AddStudent(group, "Sveta");
        var student2 = _isuService.AddStudent(group, "Andrey");
        var student3 = _isuService.AddStudent(group, "Artem");
        var findStudent = _isuService.FindStudent(2);
        Assert.Equal("Andrey", findStudent.NameOfStudent);
    }
}