using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CodeFisrt_MultiTable.Models
{
    public class DataContext : DbContext       
    {
        public DataContext()
            : base("constr")
        {
        }

        public  DbSet<Employee> Employees { get; set; }
        public  DbSet<Department> Departments { get; set; }
        public  DbSet<Course> Courses { get; set; }
        public  DbSet<Company> Companies { get; set; }
        public DbSet<EmployeeCourses> EmployeeCourses { get; set; }

    }
}