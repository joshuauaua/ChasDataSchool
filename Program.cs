namespace ChasData;
using Microsoft.EntityFrameworkCore;
using ChasData.Data;


class Program
{
    static void Main(string[] args)
    {
        //Declare a new DbContextOptionsBuilder
        var options = new DbContextOptionsBuilder<ChasDataSchoolDbContext>()
            .UseSqlServer("Server=localhost,2022;Database=ChasDataSchool;User Id=sa;Password=Fr00t_L00pth;")
            .Options;

        //Declare context
        using var context = new ChasDataSchoolDbContext(options);
        
        //Try-Catch to make sure that DB is connected succesfully
        try
        {
            context.Database.CanConnect();
            Console.WriteLine("Database connection successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection failed: {ex.Message}");
        }

       

        //Instantiate Main menu to run program
        MainMenu();

        //Main Menu Switch Menu
        void MainMenu()
        {
            ConsoleHeader();
            bool isRunning = true;

            while (isRunning)
            {
                {
                    ConsoleHeader();

                    Console.WriteLine("Choose an option:");
                    Console.WriteLine("1. Staff");
                    Console.WriteLine("2. Students");
                    Console.WriteLine("3. Grades");
                    Console.WriteLine("4. Department");
                    Console.WriteLine("5. Exit");

                    string userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            StaffMenu();
                            break;

                        case "2":
                            StudentMenu();
                            break;

                        case "3":
                            GradesMenu();
                            break;

                        case "4":
                            DepartmentMenu();
                            break;

                        case "5":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid input. Please try again.");
                            break;
                    }
                }
            }

            //Student Menu as a Switch Menu
            void StudentMenu()
            {
                bool isRunning = true;

                while (isRunning)
                {
                    ConsoleHeader();
                    Console.WriteLine("Choose an Option");
                    Console.WriteLine("1. Save New Student");
                    Console.WriteLine("2. View Students By Class");
                    Console.WriteLine("3. View All Students");
                    Console.WriteLine("4. View Student By ID");
                    Console.WriteLine("5. Exit");

                    string userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            isRunning = false;
                            SaveNewStudent();
                            break;

                        case "2":
                            isRunning = false;
                            ViewStudentByClass();
                            break;

                        case "3":
                            isRunning = false;
                            ViewAllStudents();
                            break;

                        case "4":
                            isRunning = false;
                            ViewStudentById();
                            break;

                        case "5":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid Input. Please Try Again");
                            break;
                    }
                }
            }


            //Staff Menu as Switch Menu
            void StaffMenu()
            {
                bool isRunning = true;

                while (isRunning)
                {
                    ConsoleHeader();

                    Console.WriteLine("Choose an Option:");
                    Console.WriteLine("1. Save New Staff");
                    Console.WriteLine("2. View All Staff");
                    Console.WriteLine("3. Exit");

                    string userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            isRunning = false;
                            SaveNewStaff();
                            break;

                        case "2":
                            isRunning = false;
                            GetAllStaff();
                            break;

                        case "3":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid Input. Please Try Again");
                            break;
                    }

                }
            }


            //Grades Menu as Switch Menu

            void GradesMenu()
            {
                bool isRunning = true;

                while (isRunning)
                {
                    ConsoleHeader();

                    Console.WriteLine("Choose An Option");
                    Console.WriteLine("1. Grade A Student By Transaction");
                    Console.WriteLine("2. Save Grades For a Student");
                    Console.WriteLine("3. View Grades By Student");
                    Console.WriteLine("4. Exit");

                    string userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            isRunning = false;
                            SaveGradeAsTransaction();
                            break;

                        case "2":
                            isRunning = false;
                            SaveGrade();
                            break;

                        case "3":
                            isRunning = false;
                            ViewGradesByStudent();
                            break;

                        case "4":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid Input. Please Try Again");
                            break;
                    }
                }
            }


            //Department Menu as Switch Menu

            void DepartmentMenu()
            {
                bool isRunning = true;

                while (isRunning)
                {
                    ConsoleHeader();

                    Console.WriteLine("Choose an Option:");
                    Console.WriteLine("1. View Teachers By Department");
                    Console.WriteLine("2. Show All Active Courses");
                    Console.WriteLine("3. Salary Total By Department");
                    Console.WriteLine("4. Salary Average By Department");
                    Console.WriteLine("5. Exit");

                    string userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            isRunning = false;
                            ViewTeachersByDepartment();
                            break;

                        case "2":
                            isRunning = false;
                            ShowActiveCourses();
                            break;

                        case "3":
                            isRunning = false;
                            ViewTotalSalaryByDepartment();
                            break;

                        case "4":
                            isRunning = false;
                            ViewAverageSalaryByDepartment();
                            break;

                        case "5":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid Input. Please Try Again");
                            break;
                    }
                }
            }

        }
        
        //STAFFMENU FUNCTIONS BELOW HERE
        //Execute GetAllStaff with RawSQL
        void GetAllStaff()
        {

            var staff = context.Staff
                .FromSqlRaw("EXEC GetAllStaff")
                .ToList();
            ConsoleHeader();
            Console.WriteLine("Staff ID | First Name | Last Name | Position         |  Salary  | Hire Date   ");
            Console.WriteLine(new string('-', 80));

            foreach (var s in staff)
            {
                Console.WriteLine(
                    $"{s.StaffId,-10} {s.FirstName,-12} {s.LastName,-10} {s.Position,-18} {s.Salary,-10} {s.HireDate,-10}");
            }

            Console.WriteLine("");
            Console.WriteLine("Press Enter to Return");
            Console.ReadLine();
        }

        //To perform EXEC SaveNewStaff
        void SaveNewStaff()
        {
            ConsoleHeader();
            Console.WriteLine("Save a new staff");
            Console.WriteLine(new string('-', 30));

            ConsoleHeader();
            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();

            ConsoleHeader();
            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();


            ConsoleHeader();
            Console.WriteLine("Enter Position:");
            Console.WriteLine("1. Principal");
            Console.WriteLine("2. Office Manager");
            Console.WriteLine("3. Teacher");
            Console.WriteLine("4. Janitor");
            Console.WriteLine("5. Office Technician");
            Console.WriteLine("\n \n Enter a choice number");

            string positionChoice = Console.ReadLine();
            string position = null;
            bool isRunning = true;

            while (isRunning)
            {
                switch (positionChoice)
                {
                    case "1":
                        position = "Principal";
                        isRunning = false;
                        break;

                    case "2":
                        position = "Office Manager";
                        isRunning = false;
                        break;

                    case "3":
                        position = "Teacher";
                        isRunning = false;
                        break;

                    case "4":
                        position = "Janitor";
                        isRunning = false;
                        break;

                    case "5":
                        position = "Office Technician";
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Incorrect Input. Please Try Again.");
                        break;
                }
            }

            //Hired date automatically set to today
            DateTime hireDate = DateTime.Now;

            //Get the salary in decimal
            ConsoleHeader();
            Console.WriteLine("Enter salary (in decimal format):");
            string salaryString = Console.ReadLine();

            decimal salary = decimal.Parse(salaryString);

            //Get the departmentId
            ConsoleHeader();
            Console.WriteLine("Which Department? Enter only the number and press enter");
            Console.WriteLine("1. Administration");
            Console.WriteLine("2. Maintenance");
            Console.WriteLine("3. Social Sciences");
            Console.WriteLine("4. IT");
            Console.WriteLine("5. Science & Mathematics");
            Console.WriteLine("6. Physical Education");
            Console.WriteLine("7. Languages");

            string departmentIdString = Console.ReadLine();
            int departmentId;


            while (true)
            {
                if (int.TryParse(departmentIdString, out departmentId))
                {
                    if (departmentId >= 1 && departmentId <= 7)
                    {
                        Console.WriteLine($"You entered a correct number {departmentId}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number between 1 - 7");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please enter an integer between 1 - 7");
                    return;
                }
            }

            {
                context.Database.ExecuteSqlRaw(
                    "EXEC SaveNewStaff @FirstName = {0}, @LastName = {1}, @Position = {2}, @HireDate = {3}, @Salary = {4}, @DepartmentId = {5}",
                    firstName, lastName, position, hireDate, salary, departmentId);
            }

            ConsoleHeader();
            Console.WriteLine("New Staff Member succesfully added. Press Enter to Return");
            Console.ReadKey();

        }

        //STUDENT MENU FUNCTIONALITY
        //SAVE A NEW STUDENT
        void SaveNewStudent()
        {
            ConsoleHeader();
            Console.WriteLine("Save a new student");
            Console.WriteLine(new string('-', 30));

            ConsoleHeader();
            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();

            ConsoleHeader();
            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();

            ConsoleHeader();
            Console.WriteLine("Enter Class Id as number between 1 - 18:");
            string classIdString = Console.ReadLine();
            int classId = 0;
            bool isRunning = true;

            while (isRunning)
            {
                if (int.TryParse(classIdString, out classId))
                {
                    if (classId >= 1 && classId <= 18)
                    {
                        Console.WriteLine($"You entered a correct number {classId}");
                        isRunning = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number between 1 - 18");
                        Console.WriteLine("Press any key to return");
                        Console.ReadLine();
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect Input. Please enter an integer between 1 - 18");
                    Console.WriteLine("Press any key to return");
                    Console.ReadLine();
                    break;
                }
            }
            
            {
                context.Database.ExecuteSqlRaw(
                    "EXEC SaveNewStudent @FirstName = {0}, @LastName = {1}, @ClassId = {2}",
                    firstName, lastName, classId);
            }

            ConsoleHeader();
            Console.WriteLine("New Student succesfully added. Press Enter to Return");
            Console.ReadKey();

        }

        //View Students By Class
        void ViewStudentByClass()
        {
            ConsoleHeader();
            Console.WriteLine("View Students By Class");
            string classIdString = Console.ReadLine();
            int classId;

            while (true)
            {
                ConsoleHeader();
                Console.WriteLine("Enter Class Id (number between 1 - 18");
                if (int.TryParse(classIdString, out classId))
                {
                    // Check if the number is within the specified range
                    if (classId >= 1 && classId <= 18)
                    {
                        Console.WriteLine($"You entered a valid number: {classId}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: The number is not between 1 and 18. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
                }
            }
            
            //EF 
            {
                var student = context.StudentClassViews
                    .FromSqlInterpolated($"EXEC ViewStudentByClass @ClassId = {classId}")
                    .ToList();

                // Format and display the results
                ConsoleHeader();
                Console.WriteLine($"Students in Class ID: {classId}");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Class Name",-10}");
                Console.WriteLine(new string('-', 50));

                foreach (var st in student)
                {
                    Console.WriteLine($"{st.StudentId,-5} {st.FirstName,-15} {st.LastName,-15} {st.ClassName,-10}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to Return");
            Console.ReadKey();
        }

        //View by Student ID SQL i SSMS
        void ViewStudentById()
        {
            ConsoleHeader();
            Console.WriteLine("View Students By Student Id");
            Console.WriteLine("Enter Student Id (5 numbers long starting with 200)");

            string studentIdString = Console.ReadLine();
            int studentId = int.Parse(studentIdString);

            //EF 
            {
                var student = context.StudentClassViews
                    .FromSqlInterpolated($"EXEC ViewStudentById @StudentId = {studentId}")
                    .ToList();

                // Format and display the results
                ConsoleHeader();
                Console.WriteLine($"Students with Student ID: {studentId}");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Class Name",-10}");
                Console.WriteLine(new string('-', 50));

                foreach (var st in student)
                {
                    Console.WriteLine($"{st.StudentId,-5} {st.FirstName,-15} {st.LastName,-15} {st.ClassName,-10}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to Return");
            Console.ReadKey();
        }


        //Get all students in EF in VS 

        void ViewAllStudents()
        {
            var students = context.Students.ToList();

            ConsoleHeader();
            Console.WriteLine("All Students at Chas Data School");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Student ID | Name");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId,-12} {student.FirstName} {student.LastName}");
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to Return");
            Console.ReadKey();
        }

        //GRADES MENU FUNCTIONALITY

        //1. Grade A Student By Transaction SQL i SSMS
        //Grade a student by transaction
        void SaveGradeAsTransaction()
        {
            ConsoleHeader();
            Console.WriteLine("Save New Grade For a Student using Transaction");
            Console.WriteLine(new string('-', 30));

            ConsoleHeader();
            Console.WriteLine("Enter Grade Value (0-100):");
            string gradeValueString = Console.ReadLine();
            int gradeValue = int.Parse(gradeValueString);

            DateTime dateAssigned = DateTime.Now;

            ConsoleHeader();
            Console.WriteLine("Enter Student Id (5 digit number starting with 200)");
            string studentIdString = Console.ReadLine();
            int studentId = int.Parse(studentIdString);

            ConsoleHeader();
            Console.WriteLine("Enter Course Id (Number between 100 - 111)");

            string courseIdString = Console.ReadLine();
            int courseId = int.Parse(courseIdString);

            ConsoleHeader();
            Console.WriteLine("Enter Staff Id (5 digit number starting with 300)");

            string staffIdString = Console.ReadLine();
            int staffId = int.Parse(staffIdString);

            {
                context.Database.ExecuteSqlRaw(
                    "EXEC SaveGradeAsTransaction @GradeValue = {0}, @DateAssigned = {1}, @StudentId = {2}, @CourseId = {3}, @StaffId = {4}",
                    gradeValue, dateAssigned, studentId, courseId, staffId);
            }

            ConsoleHeader();
            Console.WriteLine("New Grade succesfully added. Press Enter to Return");
            Console.ReadKey();
        }

        //2. Save Grades For a Student SQL i SSMS
        void SaveGrade()
        {
            ConsoleHeader();
            Console.WriteLine("Save New Grade For a Student");
            Console.WriteLine(new string('-', 30));

            ConsoleHeader();
            Console.WriteLine("Enter Grade Value (0-100):");
            string gradeValueString = Console.ReadLine();
            int gradeValue = int.Parse(gradeValueString);

            DateTime dateAssigned = DateTime.Now;

            ConsoleHeader();
            Console.WriteLine("Enter Student Id (5 digit number starting with 200)");
            string studentIdString = Console.ReadLine();
            int studentId = int.Parse(studentIdString);

            ConsoleHeader();
            Console.WriteLine("Enter Course Id (Number between 100 - 111)");

            string courseIdString = Console.ReadLine();
            int courseId = int.Parse(courseIdString);

            ConsoleHeader();
            Console.WriteLine("Enter Staff Id (5 digit number starting with 300)");

            string staffIdString = Console.ReadLine();
            int staffId = int.Parse(staffIdString);

            {
                context.Database.ExecuteSqlRaw(
                    "EXEC SaveGrade @GradeValue = {0}, @DateAssigned = {1}, @StudentId = {2}, @CourseId = {3}, @StaffId = {4}",
                    gradeValue, dateAssigned, studentId, courseId, staffId);
            }

            ConsoleHeader();
            Console.WriteLine("New Grade succesfully added. Press Enter to Return");
            Console.ReadKey();
        }




        //3. View Grades By Student Id SQL i SSMS
        void ViewGradesByStudent()
        {
            ConsoleHeader();
            Console.WriteLine("View Grades By Student Id");
            Console.WriteLine("Enter Student Id (5 numbers long starting with 200)");

            string studentIdString = Console.ReadLine();
            int studentId = int.Parse(studentIdString);

            //EF 
            {
                var grades = context.GradesByStudentView
                    .FromSqlInterpolated($"EXEC ViewGradeByStudent @StudentId = {studentId}")
                    .ToList();

                // Format and display the results
                ConsoleHeader();
                Console.WriteLine($"Grades By Student with Student Id {studentId}");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"Grade",-5} {"Date Assigned",-15} {"Course Name",-10} {"Teacher Name",-10}");
                Console.WriteLine(new string('-', 50));

                foreach (var g in grades)
                {
                    Console.WriteLine(
                        $"{g.GradeValue,-5} {g.DateAssigned,-15} {g.CourseName,-15}{g.TeacherFirstName,-10} {g.TeacherLastName} ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter to Return");
            Console.ReadKey();
        }

        

       

        //DEPARTMENT MENU

        //View Teachers By Department
        
        void ViewTeachersByDepartment()
        {
            ConsoleHeader();
            Console.WriteLine("View Teachers By Department");

            int departmentId;
            
            while (true)
            {
                ConsoleHeader();
                Console.WriteLine("Enter a Department Id (Number between 1 - 7):");
                string input = Console.ReadLine();

                ConsoleHeader();
                // Try to parse the input to an integer
                if (int.TryParse(input, out departmentId))
                {
                    // Check if the number is within the specified range
                    if (departmentId >= 1 && departmentId <= 7)
                    {
                        Console.WriteLine($"You entered a valid number: {departmentId}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: The number is not between 1 and 7. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
                }
            }
            
            //EF 
            {
                var teacher = context.Staff
                    .FromSqlInterpolated($"SELECT * FROM Staff WHERE DepartmentId = {departmentId} AND Position = 'Teacher'")
                    .ToList();
                
                ConsoleHeader();
                Console.WriteLine($"Staff who work for Department Id {departmentId}");
                Console.WriteLine("Staff ID | First Name | Last Name | Position         |  Salary  | Hire Date   ");
                Console.WriteLine(new string('-', 80));

                foreach (var t in teacher)
                {
                    Console.WriteLine(
                        $"{t.StaffId,-10} {t.FirstName,-12} {t.LastName,-10} {t.Position,-18} {t.Salary,-10} {t.HireDate,-10}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter to Return");
            Console.ReadKey();
        }


        //Show All Active Courses

        void ShowActiveCourses()
        {
            {
                var courses = context.Courses
                    .FromSqlRaw("SELECT * FROM Course WHERE IsActive = 1")
                    .ToList();
                
                ConsoleHeader();
                Console.WriteLine("Current Active Courses Are");
                Console.WriteLine("Course Id | Course Name ");
                Console.WriteLine(new string('-', 30));

                foreach (var course in courses)
                {
                    Console.WriteLine(
                        $"{course.CourseId,-10} {course.CourseName, -10}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter to Return");
            Console.ReadKey();
        }
        


        //How much does each department pay out in total salaries
        void ViewTotalSalaryByDepartment()
        {
            Console.WriteLine("View the Total Salary By Department");

            int departmentId;

            while (true)
            {
                ConsoleHeader();
                Console.WriteLine("Enter a Department Id (Number between 1 - 7):");
                string input = Console.ReadLine();

                ConsoleHeader();
                // Try to parse the input to an integer
                if (int.TryParse(input, out departmentId))
                {
                    // Check if the number is within the specified range
                    if (departmentId >= 0 && departmentId <= 7)
                    {
                        Console.WriteLine($"You entered a valid number: {departmentId}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: The number is not between 1 and 7. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
                }
            }

            ConsoleHeader();
            {
                var total = context.Set<TotalSalaryResult>()
                    .FromSqlInterpolated($"EXEC ViewTotalSalaryByDepartment @DepartmentId = {departmentId}")
                    .ToList();

                if (total.Any())
                {
                    foreach (var t in total)
                    {
                        Console.WriteLine($"Total Salary: {t.TotalSalary}");
                        Console.WriteLine("Press Enter to Return");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("No results found.");
                    Console.WriteLine("Press Enter to Return");
                    Console.ReadLine();
                }
            }
        }

        //How much does each department pay out on average in salaries
            void ViewAverageSalaryByDepartment()
            {
                Console.WriteLine("View the Average Salary By Department");

                int departmentId;
                while (true)
                {
                    ConsoleHeader();
                    Console.WriteLine("Enter a Department Id (Number between 1 - 7):");
                    string input = Console.ReadLine();

                    ConsoleHeader();
                    // Try to parse the input to an integer
                    if (int.TryParse(input, out departmentId))
                    {
                        // Check if the number is within the specified range
                        if (departmentId >= 0 && departmentId <= 7)
                        {
                            Console.WriteLine($"You entered a valid number: {departmentId}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: The number is not between 1 and 7. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
                    }
                }

                ConsoleHeader();
                {
                    var average = context.Set<AverageSalaryResult>()
                        .FromSqlInterpolated($"EXEC ViewAverageSalaryByDepartment @DepartmentId = {departmentId}")
                        .ToList();

                    if (average.Any())
                    {
                        foreach (var a in average)
                        {
                            Console.WriteLine($"Average Salary: {a.AverageSalary}");
                            Console.WriteLine("Press Enter to Return");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No results found.");
                        Console.WriteLine("Press Enter to Return");
                        Console.ReadLine();
                    }
                }

            }
            
            
            
        //Header Styling
        void ConsoleHeader()
        {
            Console.Clear();
            Console.WriteLine("Chas Data School");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine();
        }
    }

}