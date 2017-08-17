using System.ComponentModel.DataAnnotations;

namespace BanagazonWorkforceManager.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int EmployeeTrainingID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public int TrainingID { get; set; }

        public Employee Employee { get; set; }

        public TrainingProgram TrainingProgram { get; set; }


    }
}