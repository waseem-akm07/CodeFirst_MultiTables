using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFisrt_MultiTable.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentsId { get; set; }
        public string DepartmentName { get; set; }

      //  public virtual Employee Employee { get; set; }
      //  public int? EmployeeId { get; set; }
    }
}