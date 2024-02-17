using System.ComponentModel.DataAnnotations;
namespace FoodManagementSystem.Models
{
    public class FoodModel
    {
        [Key]
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Id")]
        public int FId { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Item Name")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Price")]
        public float FPrice { get; set; }
    }
}