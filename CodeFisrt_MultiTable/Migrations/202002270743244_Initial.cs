namespace CodeFisrt_MultiTable.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentsId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentsId);
            
            CreateTable(
                "dbo.EmployeeCourses",
                c => new
                    {
                        MapId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.MapId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.CourseId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        EmployeeAddress = c.String(),
                        EmployeeSalary = c.String(),
                        CompanyId = c.Int(),
                        DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.CompanyId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeCourses", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.EmployeeCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropIndex("dbo.EmployeeCourses", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeCourses", new[] { "CourseId" });
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeCourses");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.Companies");
        }
    }
}
