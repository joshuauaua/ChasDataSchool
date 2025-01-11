using System;
using System.Collections.Generic;
using ChasData.Models;
using Microsoft.EntityFrameworkCore;

namespace ChasData.Data;

public partial class ChasDataSchoolDbContext : DbContext
{
    public ChasDataSchoolDbContext()
    {
    }

    public ChasDataSchoolDbContext(DbContextOptions<ChasDataSchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }
    
    //Added StudentClassViews for method
    public virtual DbSet<StudentClassView> StudentClassViews { get; set; }
    
    //Added GradesByStudentView for method GetGradesByStudent
    public virtual DbSet<GradesByStudentView> GradesByStudentView { get; set; }
    
    //Added Average Salary Result for method Average Salary Result
    public virtual DbSet<AverageSalaryResult> AverageSalaryResult { get; set; }
    
    //Added Average Salary Result for method Average Salary Result
    public virtual DbSet<TotalSalaryResult> TotalSalaryResult { get; set; }



    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,2022;Database=ChasDataSchool;User Id=sa;Password=Fr00t_L00pth;TrustServerCertificate=True;Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mark StudentClassView as keyless
        modelBuilder.Entity<StudentClassView>().HasNoKey();
        
        //Mark GradesByStudentView as Keyless
        modelBuilder.Entity<GradesByStudentView>().HasNoKey();
        
        //Mark AverageSalaryResult as Keyless
        modelBuilder.Entity<AverageSalaryResult>().HasNoKey();
        
        //Mark TotalSalaryResult as Keyless
        modelBuilder.Entity<TotalSalaryResult>().HasNoKey();
        
        


        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927C0B74F662F");

            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(50);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D71A73C0AEF64");

            entity.ToTable("Course");

            entity.Property(e => e.CourseName).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED7B201DF6");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grade__54F87A579933188C");

            entity.ToTable("Grade");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Grade_Course");

            entity.HasOne(d => d.Staff).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Grade_Staff");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Grade_Student");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AB17C6B90498");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Staff_Department");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B9984233696");

            entity.ToTable("Student");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Student_Class");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

public class StudentClassView
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ClassName { get; set; }
}


public class GradesByStudentView
{
    public int GradeValue { get; set; }
    public DateTime DateAssigned { get; set; }
    public string CourseName { get; set; }
    public string TeacherFirstName { get; set; }
    public string TeacherLastName { get; set; }
}

public class AverageSalaryResult
{
    public decimal AverageSalary { get; set; }
}


public class TotalSalaryResult
{
    public decimal TotalSalary { get; set; }
}

