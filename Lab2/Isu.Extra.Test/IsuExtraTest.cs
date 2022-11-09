using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Moduls;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    [Fact]
    public void Mytest1()
    {
        TimeTable timeTable = new TimeTable();
        timeTable.Add(new UniversitySubject("Papi", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));
        timeTable.Add(new UniversitySubject("Mami", new DateTime(2022, 1, 1, 16, 00, 0), "Dmitriy", "322"));

        IsuExtraService isu = new IsuExtraService();
        var group = isu.AddGroup(new GroupName("M3108"));
        var group1 = isu.AddGroup(new GroupName("P3108"));
        var student = isu.AddStudent(group, "Andrey");
        var ognp1 = isu.AddOgnp('K', new UniversitySubject("Math", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));
        var ognp2 = isu.AddOgnp('C', new UniversitySubject("Programming", new DateTime(2022, 1, 1, 13, 3, 0), "Dmitriy", "322"));
        var ognp3 = isu.AddOgnp('S', new UniversitySubject("OS", new DateTime(2022, 1, 1, 13, 3, 0), "Dmitriy", "322"));

        var groupExtra = isu.AddTimeTable(group1, timeTable);
        var studentExtra = isu.RegistrationToOgnp(student, new UniversitySubject("Math", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));
        isu.RegistrationToOgnp(studentExtra, new UniversitySubject("Programming", new DateTime(2022, 1, 1, 13, 3, 0), "Dmitriy", "322"));
        isu.RemovingTheOgnp(studentExtra, new UniversitySubject("Math", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));
        isu.RegistrationToOgnp(studentExtra, new UniversitySubject("OS", new DateTime(2022, 1, 1, 13, 3, 0), "Dmitriy", "322"));

        isu.ChangeStudentGroup(student, group1);
        Assert.Equal("Andrey", isu.FindStudent(1).NameOfStudent);
        Assert.Empty(groupExtra.ListStudents);
        Assert.Empty(isu.ListStudentInTheOgnp(ognp1));
        Assert.Equal("OS", studentExtra.Ognp1.OgnpSubject.Name);
    }

    [Fact]
    public void ChangeStudentExtra()
    {
        TimeTable timeTable = new TimeTable();
        timeTable.Add(new UniversitySubject("Papi", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));
        timeTable.Add(new UniversitySubject("Mami", new DateTime(2022, 1, 1, 16, 00, 0), "Dmitriy", "322"));

        IsuExtraService isu = new IsuExtraService();
        var group1 = isu.AddGroup(new GroupName("M3108"));
        var group2 = isu.AddGroup(new GroupName("P3108"));
        var student = isu.AddStudent(group1, "Andrey");
        var ognp1 = isu.AddOgnp('K', new UniversitySubject("Math", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));

        var groupExtra1 = isu.AddTimeTable(group1, timeTable);
        var groupExtra2 = isu.AddTimeTable(group2, timeTable);
        var studentExtra = isu.RegistrationToOgnp(student, ognp1.OgnpSubject);

        isu.ChangeStudentGroup(student, group2);
        Assert.Equal("Andrey", groupExtra2.ListStudentExtra[0].NameOfStudent);
    }

    [Fact]
    public void StudentsDoNotHaveOgnp()
    {
        TimeTable timeTable = new TimeTable();
        timeTable.Add(new UniversitySubject("Papi", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));
        timeTable.Add(new UniversitySubject("Mami", new DateTime(2022, 1, 1, 16, 00, 0), "Dmitriy", "322"));

        IsuExtraService isu = new IsuExtraService();
        var group = isu.AddGroup(new GroupName("M3108"));
        var ognp1 = isu.AddOgnp('K', new UniversitySubject("Math", new DateTime(2022, 1, 1, 13, 30, 0), "Dmitriy", "322"));
        var groupExtra = isu.AddTimeTable(group, timeTable);

        var student1 = isu.AddStudent(group, "Andrey");
        var student2 = isu.AddStudent(group, "Artem");
        var student3 = isu.AddStudent(group, "Sveta");
        var student4 = isu.AddStudent(group, "Vlad");
        var studentExtra1 = isu.RegistrationToOgnp(student1, ognp1.OgnpSubject);
        var studentExtra3 = isu.RegistrationToOgnp(student3, ognp1.OgnpSubject);
        isu.RemovingTheOgnp(studentExtra1, ognp1.OgnpSubject);
        Assert.Equal(3, isu.ListStudentDoNotHaveOgnp(groupExtra).Count());
        Assert.Equal("Sveta", isu.ListStudentInTheOgnp(ognp1)[0].NameOfStudent);
    }
}