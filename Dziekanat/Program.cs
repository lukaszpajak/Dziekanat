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
                Console.WriteLine($"5.Delete student");
                Console.WriteLine($"6.XML Editor");
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

                        using (var ctx = new DbModel())
                        {
                            var query = ctx.Student.Select(s => new 
                            { 
                                s.StudentId, 
                                s.Name, 
                                s.SecondName, 
                                s.Pesel, 
                                s.Town 
                            })
                                .ToList();
                            foreach (var names in query)
                            {
                                Console.WriteLine(names);
                                Console.WriteLine(" ");

                            }
                        }


                        Console.WriteLine($"Insert grade");
                        var yourGrade = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Insert verbal grade");
                        var verbalGrade = Console.ReadLine();
                        Console.WriteLine($"Insert relase date");
                        var relaseDate = Console.ReadLine();
                        Console.WriteLine($"Insert StudentId for which you want to enter the grade");
                        var currentStudent = int.Parse(Console.ReadLine());


                        using (var ctx = new DbModel())
                        {

                            var grade = new Grade
                            {
                                YourGrade = yourGrade,
                                VerbalGrade = verbalGrade,
                                RelaseDate = relaseDate,
                                StudentId = currentStudent
                            };

                            ctx.Add(grade);
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
                            Console.WriteLine($"Insert StudentID to remove");
                            var inputRemove = int.Parse(Console.ReadLine());

                            var studentsToRemove =
                                ctx
                                    .Student
                                    .Where(a => a.StudentId == inputRemove).First();

                            ctx.Remove(studentsToRemove);
                            Console.WriteLine($"Student removed succesfully");
                            ctx.SaveChanges();
                            
                        }
                        break;
                    case 6:
                        Console.Clear();
                        var inputInsideCase = 0;
                        while (true)
                        {
                            


                            Console.WriteLine($"1.Display all students");
                            Console.WriteLine($"2.Add student to database from XML file");
                            Console.WriteLine($"3.Save  database to XML file and display in console");
                            Console.WriteLine($"Insert 0 to exit ");
                            inputInsideCase = int.Parse(Console.ReadLine());

                            switch (inputInsideCase)
                            {
                                case 0:
                                    break;
                                
                                case 1:
                                    using (var ctx = new DbModel())
                                    {
                                        var displayStudents =
                                            ctx
                                                .Student
                                                .Select(a => new
                                                {
                                                    a.StudentId,
                                                    a.Name,
                                                    a.SecondName,
                                                    a.Pesel,
                                                    a.Town,
                                                    a.DateOfBirth,
                                                    a.FieldOfStudy
                                                    
                                                })
                                                .ToList();

                                        foreach (var students in displayStudents)
                                        {
                                            Console.WriteLine(students);
                                            Console.WriteLine(" ");
                                        }
                                    }

                                    break;
                                case 2:
                                    XDocument xDoc = XDocument.Load(@"C:\Repos\Dziekanat\Dziekanat\bin\Debug\net5.0\student.xml");
                                    List<Student> studentList = xDoc.Descendants("Student").Select
                                        (student =>
                                        new Student
                                        {
                                            Name = student.Element("Name").Value,
                                            SecondName = student.Element("SecondName").Value,
                                            Pesel = student.Element("Pesel").Value,
                                            DateOfBirth = student.Element("DateOfBirth").Value,
                                            Town = student.Element("Town").Value,
                                            FieldOfStudy = student.Element("FieldOfStudy").Value

                                        }).ToList();

                                    using (var ctx = new DbModel())
                                    {
                                        foreach (var i in studentList)
                                        {
                                        var student = new Student
                                        {
                                            Pesel = i.Pesel,
                                            Name = i.Name,
                                            SecondName = i.SecondName,
                                            DateOfBirth = i.DateOfBirth,
                                            Town = i.Town,
                                            FieldOfStudy = i.FieldOfStudy
                                        };
                                            ctx.Add(student);
                                            ctx.SaveChanges();
                                        }
                                        

                                        
                                    }

                                    break;
                                case 3:
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
                                default:
                                    break;


                            }

                            if (inputInsideCase == 0)
                            {
                                break;
                            }
                        }  
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

