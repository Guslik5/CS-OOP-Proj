using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Moduls;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraServise : IsuService
{
    private const int MaxOgnpSubjects = 2;
    private readonly List<Ognp> _listOgnp = new List<Ognp>();

    public Ognp AddOgnp(string flow, List<OgnpSubject> list)
    {
        var ognp = new Ognp(flow, list);
        return ognp;
    }

    public void RegistrationToOgnp(StudentExtra student, OgnpSubject ognpSubject)
    {
        var findOgnp = FindOgnp(ognpSubject);

        if (student.GroupExtra.TTable.ListSubject
            .Any(s => s.TDate.Equals(ognpSubject.TDate)))
        {
            throw new IntersectionException("Intersection with the main schedule");
        }

        if (student.Ognp.ListOgnpSubjects.Count() > MaxOgnpSubjects)
        {
            throw new MaxOgnpSubjectException("Student has 2 Ognp subject");
        }

        student.Ognp = findOgnp;
    }

    public void RemovingTheOgnp(StudentExtra student, OgnpSubject ognpSubject)
    {
        var findOgnp = FindOgnp(ognpSubject);

        if (!student.Ognp.ListOgnpSubjects.Contains(ognpSubject))
        {
            throw new StudentNotHaveOgnp("The student is not registered on this Ognp");
        }

        student.Ognp.ListOgnpSubjects.Remove(ognpSubject);
    }

    public List<StudentExtra> ListStudentInTheOgnp(Ognp ognp)
    {
        return ognp.ListStudent;
    }

    /*public List<StudentExtra> ListStudentDoNotHaveOgnp()
    {
        var listAllStudent = this.Groups.SelectMany(s => s.ListStudents).ToList();
    }*/

    private Ognp FindOgnp(OgnpSubject ognpSubject)
    {
        return _listOgnp.Where(s => s.ListOgnpSubjects.Contains(ognpSubject)).FirstOrDefault() ??
               throw new NotFindOgnpException("Ognp was not find");
    }
}