using DMAWS_T2204M_NguyeDuyQuoc.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_NguyeDuyQuoc.DTOs
{
    public class EmployeeDTO
    {
        
        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public DateTime EmployeeDOB { get; set; }

        public string EmployeeDepartment { get; set; } = null!;

        public List<ProjectEmployeeDTO>? ProjectEmployees { get; set; } = null!;
    }
}
