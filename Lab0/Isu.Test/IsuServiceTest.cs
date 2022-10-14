using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private const int MaxCountOfStudents = 30;
    private IsuService _isuServise = new IsuService();

    [Fact]
    public void MyTest()
    {
        var group1 = _isuServise.AddGroup(new GroupName("M1101"));
        var group2 = _isuServise.AddGroup(new GroupName("M1102"));
        var student1 = _isuServise.AddStudent(group1, "Sveta");
        var student2 = _isuServise.AddStudent(group2, "Andrey");
        var findStudentCourse = _isuServise.FindStudents(new CourseNumber(group1.NameOfGroup));
        Assert.True(findStudentCourse[1].NameOfStudent == "Andrey");
        var findStudents = _isuServise.FindStudents(group2.NameOfGroup).ToList();
        Assert.True(findStudents[0].NameOfStudent == "Andrey");
        Assert.True(_isuServise.FindStudent(2).NameOfStudent == "Andrey");
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var group = _isuServise.AddGroup(new GroupName("M1101"));
        var student = _isuServise.AddStudent(group, "Sveta");
        Assert.Contains(student, group.ListStudents);
        Assert.Equal(group, student.Group);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var group = _isuServise.AddGroup(new GroupName("M1101"));
        for (int i = 0; i <= MaxCountOfStudents; i++)
        {
            var student = _isuServise.AddStudent(group, "student" + i.ToString());
        }

        Assert.Throws<MaxStudentExeption>(() => _isuServise.AddStudent(group, "studentlast"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<GroupNameExceptions>(() => new GroupName("ErrorGroup1"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var groupWithStudent1 = _isuServise.AddGroup(new GroupName("M1101"));
        var groupWithStudent2 = _isuServise.AddGroup(new GroupName("M2202"));
        var student1 = _isuServise.AddStudent(groupWithStudent1, "Vlad");
        var student2 = _isuServise.AddStudent(groupWithStudent2, "Artem");
        _isuServise.ChangeStudentGroup(student1, groupWithStudent2);

        Assert.Equal(2, groupWithStudent2.ListStudents.Count());
        Assert.True(!groupWithStudent1.ListStudents.Any());
    }

    [Fact]
    public void FindStudentId()
    {
        var group = _isuServise.AddGroup(new GroupName("M1101"));
        var student1 = _isuServise.AddStudent(group, "Sveta");
        var student2 = _isuServise.AddStudent(group, "Andrey");
        var student3 = _isuServise.AddStudent(group, "Artem");
        var findStudent = _isuServise.FindStudent(2);
        Assert.Equal("Andrey", findStudent.NameOfStudent);
    }
}