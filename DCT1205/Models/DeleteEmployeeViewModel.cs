using DCT1205.Entity;
using System.ComponentModel.DataAnnotations;

namespace DCT1205.Models
{
    public class DeleteEmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Employee Number")]
        public string EmployeeNo { get; set; }
    }
}
