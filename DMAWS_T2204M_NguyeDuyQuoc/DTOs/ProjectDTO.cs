using DMAWS_T2204M_NguyeDuyQuoc.Entities;
using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_NguyeDuyQuoc.DTOs
{
    public class ProjectDTO
    {

        public int? ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;

        public DateTime ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public List<ProjectEmployeeDTO> ProjectEmployees { get; set; } = null!;
    }
}
