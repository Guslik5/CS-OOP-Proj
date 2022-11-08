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
    public void Mytest()
    {
        var isu = new IsuExtraServise();
        var ognpSubjects = new List<OgnpSubject>()
        {
            new OgnpSubject("Math", DateTime.Now, "Dima", "324"),
            new OgnpSubject("Programming", DateTime.Now, "Anna", "342"),
        };

        var universitySubjects = new List<UniversitySubject>()
        {
            new UniversitySubject("Math", DateTime.Now.AddHours(2), "Dima", "324"),
            new UniversitySubject("Programming", DateTime.Now.AddHours(3), "Anna", "342"),
        };
        TimeTable timeTable = new TimeTable(universitySubjects);

        var ognp = isu.AddOgnp("M", ognpSubjects);
        var group = isu.AddGroup(new GroupName("M3108"));
        GroupExtra groupExtra = new GroupExtra(group.NameOfGroup, timeTable);
        StudentExtra studentExtra = new StudentExtra(isu.AddStudent(group, "Anton"), groupExtra, ognp);
        Assert.Equal(groupExtra, studentExtra.GroupExtra);
    }
}