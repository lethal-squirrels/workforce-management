using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BanagazonWorkforceManager.Models
{
    public class TrainingProgram
    {
        [Key]
        public int TrainingProgramID { get; set; }
        
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public int MaxAttendees { get; set; }

        public virtual ICollection<EmployeeTraining> EmployeeTrainingPrograms { get; set; }
    }
}