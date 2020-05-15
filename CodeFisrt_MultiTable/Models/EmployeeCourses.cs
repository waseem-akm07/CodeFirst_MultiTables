using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFisrt_MultiTable.Models
{
    public class EmployeeCourses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MapId { get; set; }
        public virtual Course Course { get; set; }
        public int? CourseId { get; set; }
        public virtual Employee Employee { get; set; }
        public int? EmployeeId { get; set; }

    }
}