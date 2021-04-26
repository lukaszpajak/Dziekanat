using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Dziekanat
{
    class Program
    {
       

        static void Main(string[] args)
        {
            var input = 0;

            while (true)
            {
                Console.WriteLine($"1.Add new student");
                Console.WriteLine($"2.Add grade");
                Console.WriteLine($"3.Display all students");
                Console.WriteLine($"4.Display students with grade less than or equal to 4");
                Console.WriteLine($"5.Save students to XML File");
                Console.WriteLine($"6.Read XML File and display");
                Console.WriteLine($"Insert 0 to exit ");
                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 0:
                        break;
                    case 1:

                        Console.WriteLine($"Insert student pesel");
                        var pesel = Console.ReadLine();
                        Console.WriteLine($"Insert student name");
                        var name = Console.ReadLine();
                        Console.WriteLine($"Insert student second name");
                        var secondName = Console.ReadLine();
                        Console.WriteLine($"Insert student birth date in format: dd/mm/yyy");
                        var birthDate = Console.ReadLine();
                        Console.WriteLine($"Insert student town");
                        var town = Console.ReadLine();
                        Console.WriteLine($"Insert student field of study");
                        var fieldOfStudy = Console.ReadLine();

                        using (var ctx = new DbModel())
                        {
                            var student = new Student
                            {
                                Pesel = pesel,
                                Name = name,
                                SecondName = secondName,
                                DateOfBirth = birthDate,
                                Town = town,
                                FieldOfStudy = fieldOfStudy
                            };

                            ctx.Add(student);
                            ctx.SaveChanges();
                        }
                        break;
                    case 2:

                        

                        Console.WriteLine($"Insert grade");
                        var inpute = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Insert verbal grade");
                        var verbalGrade = Console.ReadLine();
                        Console.WriteLine($"Insert relase date");
                        var relaseDate = Console.ReadLine();
                        Console.WriteLine($"Insert StudentId for which you want to enter the grade");
                        var currentStudent = int.Parse(Console.ReadLine());


                        using (var ctx = new DbModel())
                        {
                            
                           
                            var newGrades = new[] { new Grade { YourGrade = 4 }, new Grade { YourGrade = 5 } };
                            var newStudent = new Student { Name = "Nikos", SecondName = "Tak", Pesel = "meow", DateOfBirth = "12/12/12", Town = "Pcim", FieldOfStudy = "Taks", Grades = newGradesw };

                            ctx.Add(newStudent);
                            ctx.SaveChanges();

                        }
                        break;
                    case 3:

                        using (var ctx = new DbModel())
                        {
                            var studentsGrade =
                                ctx
                                    .Grade
                                    .Include(a => a.Student)
                                    .Select(a => new 
                                    { 
                                        a.Student.StudentId, 
                                        a.Student.Name, 
                                        a.Student.SecondName, 
                                        a.Student.Pesel, 
                                        a.Student.Town, 
                                        a.Student.DateOfBirth, 
                                        a.Student.FieldOfStudy, 
                                        a.YourGrade 
                                    })
                                    .ToList();

                            foreach (var students in studentsGrade)
                            {
                                Console.WriteLine(students);
                                Console.WriteLine(" ");
                            }
                        }
                        break;
                    case 4:
                        using (var ctx = new DbModel())
                        {
                            var studentsGrade =
                                ctx
                                    .Grade
                                    .Include(a => a.Student)
                                    .Where(a => a.YourGrade <= 4)
                                    .Select(a => new { a.YourGrade, a.Student.Name })
                                    .ToList();

                            foreach (var students in studentsGrade)
                            {
                                Console.WriteLine(students);
                                Console.WriteLine(" ");
                            }
           
                        }

                        break;

                    case 5:
                        using (var ctx = new DbModel())
                        {
                            var student =
                                new XElement("Students",
                                ctx.
                                Student.
                                AsEnumerable().
                                Select(c => new XElement("Student", new XAttribute
                                ("Id", c.StudentId), new[] { 
                                                            new XElement("Name", c.Name),
                                                            new XElement("SecondName", c.SecondName),
                                                            new XElement("Pesel", c.Pesel),
                                                            new XElement("Town", c.Town),
                                                            new XElement("DateOfBirth", c.DateOfBirth),
                                                            new XElement("FieldOfStudy", c.FieldOfStudy)})));
                            Console.WriteLine(student);
                            student.Save(@"C:\Repos\Dziekanat\Dziekanat\student.xml");

                        }

                        break;
                    case 6:
                        XElement root = XElement.Load("student.xml");
                        IEnumerable<XElement> xmlStudents =
                            from el in root.Elements("Student")
                            
                            select el;
                        foreach (XElement el in xmlStudents)
                            Console.WriteLine(el);
                        break;
                    default:
                        Console.WriteLine("Wrong value!");
                        break;
                        
                }
                if (input == 0)
                {
                    break;
                }

            }
    
       }
    }    

 }

