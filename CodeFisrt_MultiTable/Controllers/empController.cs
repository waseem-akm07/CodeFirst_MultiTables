using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Routing;
using System.Web.Http;
using CodeFisrt_MultiTable.Models;


namespace CodeFisrt_MultiTable.Controllers
{
    public class empController : ApiController
    {
        DataContext db = new DataContext();

        // GET: api/emp
        [HttpGet]
        [Route("api/emp/getemployees")]
        public IHttpActionResult  GetEmployee()
        {
            List<Employee> employees = db.Employees.ToList();

            var data = (from emp in employees
                        join dep in db.Departments on emp.EmployeeId equals dep.DepartmentsId
                        join com in db.Companies on emp.EmployeeId equals com.CompanyId
                       join map in db.EmployeeCourses on emp.EmployeeId equals map.CourseId
                        join crs in db.Courses on map.CourseId equals crs.CourseId
                        select new HelperModel
                        {
                            EmployeeName = emp.EmployeeName,
                            EmployeeAddress = emp.EmployeeAddress,
                            EmployeeSalary = emp.EmployeeSalary,
                            DepartmentName = dep.DepartmentName,
                            CompanyName = com.CompanyName,
                            CourseName = crs.CourseName
                            //userCourses = new List<UserCourses>
                            //{
                            //    new UserCourses
                            //    {
                            //        CourseName=string.Join(",", from crs in db.Courses
                            //                                    join e in employees on crs.EmployeeId equals e.EmployeeId 
                            //                                    where e.EmployeeId == crs.EmployeeId
                            //                                    select crs.CourseName)
                            //    }
                            //}
                        }).ToList();


            return Ok(data);
        }

        // GET: api/emp/5
        [HttpGet]
        [Route("api/emp/getemployee/{id}")]
        public IHttpActionResult GetEmplyee(int id)
        {
            var data = (from e in db.Employees
                        join c in db.Companies on e.EmployeeId equals c.CompanyId
                        join d in db.Departments on e.EmployeeId equals d.DepartmentsId
                        join m in db.EmployeeCourses on e.EmployeeId equals m.EmployeeId
                        join crs in db.Courses on m.CourseId equals crs.CourseId
                        where e.EmployeeId == id
                        select new HelperModel
                        {
                            EmployeeName = e.EmployeeName,
                            EmployeeAddress = e.EmployeeAddress,
                            EmployeeSalary = e.EmployeeSalary,
                            DepartmentName = d.DepartmentName,
                            CompanyName = c.CompanyName,
                            CourseName = crs.CourseName
                        }).ToList();

            return Ok(data);
        }

        // POST: api/emp
        [HttpPost]
        [Route("api/emp/postemployee")]
        public IHttpActionResult PostEmployee(HelperModel model)
        {
            Employee employee = new Employee();
            Department department = new Department();
            Course course = new Course();
            Company company = new Company();
            EmployeeCourses employeeCourses = new EmployeeCourses();

            department.DepartmentName = model.DepartmentName;
            db.Departments.Add(department);
            db.SaveChanges();
            var depId = department.DepartmentsId;

            company.CompanyName = model.CompanyName;
            db.Companies.Add(company);
            db.SaveChanges();
            var comId = company.CompanyId;

            course.CourseName = model.CourseName;
            db.Courses.Add(course);
            db.SaveChanges();
            var crsId = course.CourseId;

            employee.EmployeeName = model.EmployeeName;
            employee.EmployeeAddress = model.EmployeeAddress;
            employee.EmployeeSalary = model.EmployeeSalary;
            employee.CompanyId = comId;
            employee.DepartmentId = depId;
            db.Employees.Add(employee);
            db.SaveChanges();
            var empId = employee.EmployeeId;

            employeeCourses.CourseId = crsId;
            employeeCourses.EmployeeId = empId;
            db.EmployeeCourses.Add(employeeCourses);

            db.SaveChanges();

            return Ok("Add successfully");
        }

        // PUT: api/emp/5
        [HttpPut]
        [Route("api/emp/putemployee/{id}")]
        public IHttpActionResult PutEmployee(int id, HelperModel model)
        {
            

            var emp = db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            var dep = db.Departments.Where(x => x.DepartmentsId == emp.EmployeeId).FirstOrDefault();
            var com = db.Companies.Where(x => x.CompanyId == emp.EmployeeId).FirstOrDefault();
            var cr = db.Courses.Where(x => x.CourseId == emp.EmployeeId).FirstOrDefault();

            emp.EmployeeName = model.EmployeeName;
            emp.EmployeeAddress = model.EmployeeAddress;
            emp.EmployeeSalary = model.EmployeeSalary;
            db.Entry(emp).State = System.Data.Entity.EntityState.Modified;

            dep.DepartmentName = model.DepartmentName;
            db.Entry(dep).State = System.Data.Entity.EntityState.Modified;

            com.CompanyName = model.CompanyName;
            db.Entry(com).State = System.Data.Entity.EntityState.Modified;

            cr.CourseName = model.CourseName;
            db.Entry(cr).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();



            return Ok("Update successfully");
        }

        // DELETE: api/emp/5
        [HttpDelete]
        [Route("api/emp/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var data = db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Ok("Delete successfully");
        }
    }
}
