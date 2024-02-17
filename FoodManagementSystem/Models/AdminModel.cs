using System.ComponentModel.DataAnnotations;
namespace EmployeeManagementSystem.Models
{
    public class AdminModel
    {
        [Key]
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Enter Id")]
        public int AdminId { get; set; }

        [Key]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        [Display(Name = "Enter Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Enter Full Name")]
        public string AdminName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Enter Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string CfmPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Enter New Password")]
        public string NewPassword { get; set; }
    }
}