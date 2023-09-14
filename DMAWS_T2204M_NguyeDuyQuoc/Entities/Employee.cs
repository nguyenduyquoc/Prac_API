using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_NguyeDuyQuoc.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string EmployeeName { get; set; } = null!;

        [Required]
        public DateTime EmployeeDOB { get; set; }

        [Required]
        [StringLength(255)]
        public string EmployeeDepartment { get; set; } = null!;

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = null!;
    }
}
