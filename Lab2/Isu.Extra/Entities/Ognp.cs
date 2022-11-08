using Isu.Extra.Exceptions;
using Isu.Extra.Moduls;

namespace Isu.Extra.Entities;

public class Ognp
{
    private readonly List<OgnpSubject> _listOgnpSubjects = new List<OgnpSubject>();
    private readonly List<StudentExtra> _listStudent = new List<StudentExtra>();

    public Ognp(string flow, List<OgnpSubject> listOgnpSubjects)
    {
        if (!listOgnpSubjects.Any())
        {
            throw new OgnpSubjectException("List subject for ognp is empty");
        }

        Flow = flow;
        _listOgnpSubjects = _listOgnpSubjects.Concat(listOgnpSubjects).ToList();
    }

    public List<StudentExtra> ListStudent => _listStudent;

    public string Flow { get; }

    public List<OgnpSubject> ListOgnpSubjects => _listOgnpSubjects;
}