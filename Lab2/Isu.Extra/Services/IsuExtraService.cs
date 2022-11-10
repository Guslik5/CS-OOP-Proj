using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Moduls;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuService
{
    private readonly List<Ognp> _listOgnp = new List<Ognp>();
    private readonly List<GroupExtra> _listGroupExtra = new List<GroupExtra>();
    private readonly List<StudentExtra> _listAllStudentExtra = new List<StudentExtra>();
    private readonly IsuService _isuService = new IsuService();

    public Ognp AddOgnp(char flow, UniversitySubject ognpSubject)
    {
        var ognp = new Ognp(flow, ognpSubject);
        if (_listOgnp.Where(o => o.Equals(ognp)).Any())
        {
            throw new OgnpAddedException("Ognp has been Added");
        }

        _listOgnp.Add(ognp);
        return ognp;
    }

    public StudentExtra RegistrationToOgnp(Student student, UniversitySubject ognpSubject)
    {
        var studentExtra = _listAllStudentExtra.Where(s => s.ID.Equals(student.ID)).FirstOrDefault();
        var groupExtraStudent = _listGroupExtra.Where(g => g.ListStudentExtra.Contains(studentExtra)).FirstOrDefault();
        if (CheckingIntersectionTimeTableAndOgnp(groupExtraStudent, ognpSubject))
        {
            throw new IntersectionException("Intersection Ognp And TimeTable");
        }

        var findOgnp = FindOgnp(ognpSubject);
        if (student.Group.NameOfGroup.NameGroup[0] == findOgnp.Flow)
        {
            throw new RegistrationOgnpOfStudentFacultyException(
                "The student signed up for the start-up of his faculty");
        }

        studentExtra.Ognp1 = findOgnp;
        findOgnp.ListStudent.Add(studentExtra);
        return studentExtra;
    }

    public StudentExtra RegistrationToOgnp(StudentExtra studentExtra, UniversitySubject ognpSubject)
    {
        var findOgnp = FindOgnp(ognpSubject);
        var groupExtraStudent = _listGroupExtra.Where(g => g.ListStudentExtra.Contains(studentExtra)).FirstOrDefault();
        if (CheckingIntersectionTimeTableAndOgnp(groupExtraStudent, ognpSubject))
        {
            throw new IntersectionException("Intersection Ognp And TimeTable");
        }

        if (studentExtra.Group.NameOfGroup.NameGroup[0] == findOgnp.Flow)
        {
            throw new RegistrationOgnpOfStudentFacultyException(
                "The student signed up for the start-up of his faculty");
        }

        if (Equals(findOgnp, studentExtra.Ognp1) || Equals(findOgnp, studentExtra.Ognp2))
        {
            throw new TryRegistrationInOneOgnpTwoTimesException("Фttempt to sign up for the same ognp 2 times");
        }

        if (studentExtra.Ognp1 is null)
        {
            studentExtra.Ognp1 = findOgnp;
            findOgnp.ListStudent.Add(studentExtra);
            return studentExtra;
        }

        if (studentExtra.Ognp2 is null)
        {
            studentExtra.Ognp2 = findOgnp;
            findOgnp.ListStudent.Add(studentExtra);
            return studentExtra;
        }

        throw new MaxOgnpSubjectException("Student has benn Added two ognp");
    }

    public void RemovingTheOgnp(StudentExtra student, UniversitySubject ognpSubject)
    {
        var findOgnp = FindOgnp(ognpSubject);
        if (student.Ognp1.OgnpSubject.Equals(ognpSubject))
        {
            student.Ognp1 = null;
            findOgnp.ListStudent.Remove(student);
            return;
        }

        if (student.Ognp2.OgnpSubject.Equals(ognpSubject))
        {
            student.Ognp2 = null;
            findOgnp.ListStudent.Remove(student);
            return;
        }

        throw new NotHaveSubjectException("This student do not has this ognp");
    }

    public List<StudentExtra> ListStudentInTheOgnp(Ognp ognp)
    {
        return ognp.ListStudent;
    }

    public List<StudentExtra> ListStudentDoNotHaveOgnp(GroupExtra group)
    {
        var students = group.ListStudentExtra.Where(s => s.Ognp1 is null && s.Ognp2 is null).ToList();
        return students;
    }

    public Group AddGroup(GroupName name)
    {
        var group = _isuService.AddGroup(name);
        GroupExtra groupExtra = new GroupExtra(group, new TimeTable());
        _listGroupExtra.Add(groupExtra);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        var student = _isuService.AddStudent(group, name);
        var studentExtra = new StudentExtra(student);
        GroupExtra groupExtraForStudentExtra = _listGroupExtra
            .Where(g => g.NameOfGroup.NameGroup.Equals(group.NameOfGroup.NameGroup)).FirstOrDefault();
        _listAllStudentExtra.Add(studentExtra);
        if (groupExtraForStudentExtra is null)
        {
            throw new GroupExtraException("GroupExtra is not found");
        }

        groupExtraForStudentExtra.Add(studentExtra);
        return student;
    }

    public Student GetStudent(int id)
    {
       return _isuService.GetStudent(id);
    }

    public Student FindStudent(int id)
    {
        return _isuService.FindStudent(id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _isuService.FindStudents(groupName);
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _isuService.FindStudents(courseNumber);
    }

    public Group FindGroup(GroupName groupName)
    {
        return _isuService.FindGroup(groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _isuService.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        _isuService.ChangeStudentGroup(student, newGroup);
        var studentExtra = _listAllStudentExtra.Where(s => s.ID.Equals(student.ID)).FirstOrDefault();
        var newGroupExtra = _listGroupExtra
            .Where(g => g.NameOfGroup.NameGroup.Equals(newGroup.NameOfGroup.NameGroup)).FirstOrDefault();
        var oldGroupExtra = _listGroupExtra.Where(g => g.ListStudentExtra.Contains(studentExtra)).FirstOrDefault();
        var copyStudent = new StudentExtra(student);
        copyStudent.Ognp1 = studentExtra.Ognp1;
        copyStudent.Ognp2 = studentExtra.Ognp2;
        newGroupExtra.Add(copyStudent);
        oldGroupExtra.Remove(studentExtra);
    }

    public GroupExtra AddTimeTable(Group group, TimeTable timeTable)
    {
        var groupExtra = _listGroupExtra.Where(g => g.NameOfGroup.Equals(group.NameOfGroup)).FirstOrDefault();
        groupExtra.TTable = timeTable;
        return groupExtra;
    }

    private Ognp FindOgnp(UniversitySubject ognpSubject)
    {
        return _listOgnp.Where(s => s.OgnpSubject.Equals(ognpSubject)).FirstOrDefault() ??
               throw new NotFindOgnpException("Ognp was not find");
    }

    private bool CheckingIntersectionTimeTableAndOgnp(GroupExtra groupExtra, UniversitySubject ognpSubject)
    {
        if (groupExtra.TTable.ListSubject.Where(s => ognpSubject.TDate >= s.TDate && ognpSubject.TDate <= s.EndingTime).Any())
        {
            return true;
        }

        return false;
    }
}