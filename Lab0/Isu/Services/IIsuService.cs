using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public interface IIsuService
{
    Group AddGroup(GroupName name);
    Student AddStudent(Group group, string name);

    Student GetStudent(int id);
    Student? FindStudent(int id);
    IEnumerable<List<Student>> FindStudents(GroupName groupName);
    List<Student> FindStudents(CourseNumber courseNumber);

    IEnumerable<Group>? FindGroup(GroupName groupName);
    IEnumerable<Group> FindGroups(CourseNumber courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}