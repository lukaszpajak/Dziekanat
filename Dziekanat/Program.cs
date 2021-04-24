﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                        Console.WriteLine($"Insert student town ss");
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
                            var query = ctx.Student.Select(s => new { s.StudentId, s.Name, s.SecondName, s.Pesel, s.Town }).ToList();
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
                            var query = ctx.Student.Select(s => new { s.StudentId, s.Name, s.SecondName, s.Pesel, s.Town, s.DateOfBirth, s.FieldOfStudy}).ToList();
                            foreach (var names in query)
                            {
                                Console.WriteLine(names);
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

