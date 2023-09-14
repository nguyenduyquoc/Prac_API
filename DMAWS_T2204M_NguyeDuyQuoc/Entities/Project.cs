using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_NguyeDuyQuoc.Entities
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ProjectName { get; set; } = null!;

        [Required]
        public DateTime ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
       
    }
}
