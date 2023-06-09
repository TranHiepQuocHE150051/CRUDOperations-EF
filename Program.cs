
using CRUDOperationsEFCore.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

using (var context= new StudentDBContext())
{
    while (true)
    {
        Console.WriteLine("1.Get Student by Id (using stored procedure)");
        Console.WriteLine("2.Create a new student");
        Console.WriteLine("3.Update a student");
        Console.WriteLine("4.Delete a student");
        Console.WriteLine("5.List students");
        Console.WriteLine("6.Exit");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                {
                    int studentId;
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Enter Id: ");
                            studentId = Convert.ToInt32(Console.ReadLine());
                            break;
                        }catch(Exception )
                        {
                            Console.WriteLine("Input must be a number");
                            continue;
                        }
                        
                    }
                    
                    var studentsInSp = context.Students.FromSql(FormattableStringFactory.Create($"GetStudents {studentId}")).ToList();
                    if (studentsInSp.Any()) {
                        foreach (var student in studentsInSp)
                        {
                            Console.WriteLine("Student Name: " + student.FullName + " Age:" + student.Age + " Address: " + student.Address);

                        }
                    }
                    
                    Console.WriteLine("---------------------");
                    break;
                }
            case "2":
                {
                    Console.WriteLine("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter age: ");
                    int age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter address: ");
                    string address = Console.ReadLine();
                    Student student = new Student
                    {
                        FullName = name,
                        Age = age,
                        Address = address
                    };
                    context.Students.Add(student);

                    if (context.SaveChanges() > 0)
                    {
                        Console.WriteLine("Add new student success");
                    }
                    Console.WriteLine("---------------------");
                    break;
                }
            case "3":
                {
                    int studentId;
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Enter Id: ");
                            studentId = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Input must be a number");
                            continue;
                        }

                    }
                    var student = context.Students.SingleOrDefault(s=>s.Id== studentId);
                    if(student == null)
                    {
                        Console.WriteLine("Cannot find student");
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine("Enter Name: ");
                            string? name = Console.ReadLine();
                            Console.WriteLine("Enter age: ");
                            string? age = Console.ReadLine();
                            Console.WriteLine("Enter address: ");
                            string? address = Console.ReadLine();
                            if(name!=null || name.Trim() != "")
                            {
                                student.FullName = name;
                            }
                            if(address!=null|| address.Trim()!="")
                            {
                                student.Address = address;
                            }
                            if(age!=null|| age.Trim() != "")
                            {
                                student.Age = int.Parse(age);
                            }
                            context.Students.Update(student);
                            if (context.SaveChanges() > 0)
                            {
                                Console.WriteLine("Update success");
                            }
                        }
                        catch (Exception e )
                        {
                            Console.WriteLine(e.Message);
                        }
                        
                    }
                    break;
                }
            case "4":
                {
                    int studentId;
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Enter Id: ");
                            studentId = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Input must be a number");
                            continue;
                        }

                    }
                    var student = context.Students.SingleOrDefault(s => s.Id == studentId);
                    if (student == null)
                    {
                        Console.WriteLine("Cannot find student");
                    }
                    else
                    {
                        context.Students.Remove(student);
                        if (context.SaveChanges() > 0)
                        {
                            Console.WriteLine("Delete success");
                        }
                    }
                    break;
                }
            case "5":
                {
                    Console.WriteLine("List all students");
                    var students = context.Students.ToList();
                    foreach (var s in students)
                    {
                        Console.WriteLine("Student Name: " + s.FullName + " Age:" + s.Age + " Address: " + s.Address);

                    }
                    Console.WriteLine("---------------------");
                    break;
                }
            case "6":
                {
                    return;
                }
            default:
                {
                    Console.WriteLine("Please enter between 1 and 6");
                    continue;
                }
        }
    }
}