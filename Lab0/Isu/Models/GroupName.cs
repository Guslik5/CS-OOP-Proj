using Isu.exceptions;
namespace Isu.Models;

public class GroupName
{
    private const int CoursePossitionInNameGroup = 2;
    private const int MinGroupLenght = 5;
    private const int MaxGroupLenght = 6;
    private const int MaxCourseNumber = 6;
    public GroupName(string nameofgroup)
    {
        if ((nameofgroup.Length < MinGroupLenght) ^ (nameofgroup.Length > MaxGroupLenght))
        {
            throw new GroupNameExceptions("Error\n Incorrect group name: ", nameofgroup);
        }

        if (Convert.ToInt32(new string(nameofgroup[CoursePossitionInNameGroup], 1)) > MaxCourseNumber)
        {
            throw new GroupNameExceptions("Error\n Incorrect group name\n Incorrect course: ", nameofgroup);
        }

        NameGroup = nameofgroup;
    }

    public string NameGroup { get; }
}