using Isu.Entities;
using Isu.exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var isuServise = new IsuService();
        var group = isuServise.AddGroup(new GroupName("M1101"));
        var student = isuServise.AddStudent(group, "Sveta");
        Assert.Contains(student, group.ListStudents);
        Assert.True(group.NameOfGroup.NameGroup == student.Group.NameGroup);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isuServise = new IsuService();
        var group = isuServise.AddGroup(new GroupName("M1101"));
        for (int i = 0; i <= 30; i++)
        {
            var student = isuServise.AddStudent(group, "student" + i.ToString());
        }

        Assert.Throws<MaxStudentExeption>(() => isuServise.AddStudent(group, "studentlast"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<GroupNameExceptions>(() => new GroupName("ErrorGroup1"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var isuService = new IsuService();
        var groupWithStudent1 = isuService.AddGroup(new GroupName("M1101"));
        var groupWithStudent2 = isuService.AddGroup(new GroupName("M2202"));
        var student1 = isuService.AddStudent(groupWithStudent1, "Vlad");
        var student2 = isuService.AddStudent(groupWithStudent2, "Artem");

        isuService.ChangeStudentGroup(student1, groupWithStudent2);

        Assert.Equal(2, groupWithStudent2.ListStudents.Count);
        Assert.Equal("Artem", groupWithStudent2.ListStudents[0].NameOfStudent);
        Assert.Equal("Vlad", groupWithStudent2.ListStudents[1].NameOfStudent);
        Assert.True(groupWithStudent1.ListStudents.Count == 0);
    }
}