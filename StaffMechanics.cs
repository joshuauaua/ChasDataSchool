namespace ChasData;
using ChasData.Data;
using Microsoft.EntityFrameworkCore;

public class StaffMechanics
{
    //Declare a new DbContextOptionsBuilder
    var options = new DbContextOptionsBuilder<ChasDataSchoolDbContext>()
        .UseSqlServer("Server=localhost,2022;Database=Northwind;User Id=sa;Password=Fr00t_L00pth;")
        .Options;
    
        using var context = new ChasDataSchoolDbContext(options);

    
    //Execute GetAllStaff with RawSQL
    void GetAllStaff()
    {
        Console.Clear();
        var staff = context.Staff
            .FromSqlRaw("EXEC GetAllStaff")
            .ToList();

        Console.WriteLine("Staff ID | First Name | Last Name | Position         |  Salary  | Hire Date   ");
        Console.WriteLine(new string ('-', 80));
            
        foreach (var s in staff)
        {
            Console.WriteLine($"{s.StaffId, -10} {s.FirstName, -12} {s.LastName, -10} {s.Position, -18} {s.Salary, -10} {s.HireDate, -10}");
        }
            
        Console.WriteLine("");
        Console.WriteLine("Press Enter to Return");
        Console.ReadLine();
    }

    //To perform EXEC SaveNewStaff
    void SaveNewStaff()
    {
        Console.Clear();
        Console.WriteLine("Save a new staff");
        Console.WriteLine(new string ('-', 30));

        Console.WriteLine("Enter First Name:");
        string firstName = Console.ReadLine();
            
        Console.WriteLine("Enter Last Name:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Enter Position:");
        string position;

        DateTime hireDate = DateTime.Now;

        Console.WriteLine("Enter salary (in decimal format):");
        decimal salary = Console.ReadLine();
            
        int departmentId;
}