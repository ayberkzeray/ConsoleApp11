using ConsoleApp11.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
         
        }
        
    }
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
    }
    public class Product 
    {
        public int ID { get; set; }
        public bool IsOnSale { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        public List<Teacher> Teachers { get; set; }
    }

    public class Teacher
    {
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }

        public List<Student> Students { get; set; }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public EmployeeContactDetails EmployeeContactDetail { get; set; }
    }

    public class EmployeeContactDetails
    {
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Employee Employee { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeContactDetails> EmployeeContactDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-Q3NBEGU;Database=MappingDB;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //conditional mapping
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.IsOnSale == true);

            //many-to-many
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Students)
                .WithMany(e => e.Teachers);

            // table splitting
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<EmployeeContactDetails>().HasKey(ed => ed.EmployeeID);
            modelBuilder.Entity<EmployeeContactDetails>().ToTable("Employees");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeContactDetail)
                .WithOne(d => d.Employee)
                .HasForeignKey<EmployeeContactDetails>(d => d.EmployeeID);

            base.OnModelCreating(modelBuilder);
        }
    }
}