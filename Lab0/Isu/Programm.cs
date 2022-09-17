/*using System;
using Isu;
using Isu.Entities;
using Isu.Models;
using Isu.Services;

namespace test
{
    public class program
    {
        public static void Main(string[] args)
        {
            var group_nameDima = new GroupName("M1101");
            var group_nameVlad = new GroupName("M2202");
            var group_name3 = new GroupName("M3303");
            var group_name4 = new GroupName("M4404");
            var group_name5 = new GroupName("M5505");
            var ISU = new IsuService();
            var groupDima = ISU.AddGroup(group_nameDima);
            var groupVlad = ISU.AddGroup(group_nameVlad);
            var group3 = ISU.AddGroup(group_name3);
            var group4 = ISU.AddGroup(group_name4);
            ISU.AddStudent(groupDima, "Dima");
            ISU.AddStudent(groupVlad, "Vlad");
            if (ISU.FindGroup(group_nameVlad) == null)
            {
                Console.WriteLine("da");
            }

            var listDima = ISU.FindStudents(group_nameDima);
            Console.WriteLine(listDima[0].NameOfStudent);
            //Console.WriteLine(list[1].NameOfStudent);
            Console.WriteLine();
            ISU.ChangeStudentGroup(listDima[0], groupVlad);
            var listVlad = ISU.FindStudents(group_nameVlad);
            Console.WriteLine(listVlad[0].NameOfStudent);
            Console.WriteLine(listVlad[1].NameOfStudent);
            Console.WriteLine();
            //Console.WriteLine(listDima[0].NameOfStudent);

            //Console.WriteLine(ISU.FindStudents(group_name)[0].NameOfStudent);
        }
    }
}*/