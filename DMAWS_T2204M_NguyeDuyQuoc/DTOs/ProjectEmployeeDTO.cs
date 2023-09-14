using DMAWS_T2204M_NguyeDuyQuoc.Entities;

namespace DMAWS_T2204M_NguyeDuyQuoc.DTOs
{
    public class ProjectEmployeeDTO
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public string Tasks { get; set; }
        public virtual Employee? Employee { get; set; }

        public virtual Project? Project { get; set; }
    }
}
