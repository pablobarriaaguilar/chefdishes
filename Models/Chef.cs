using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chefsdishes.Models;
public class Chef{
    [Key]
    public int ChefId{ get; set;}
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [PastDate]
    public DateTime DateOfBirth {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Dish> AllDishes { get; set; } = new List<Dish>();
    [NotMapped]
    public int Age
        {
        get
    {
        DateTime dateOfBirth = DateOfBirth;
        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        int age = now.Year - dateOfBirth.Year;

        // Ajusta la edad si aún no ha tenido su cumpleaños este año
        if (now.DayOfYear < dateOfBirth.DayOfYear)
        {
            age--;
        }

        return age;
    }
        }


        public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
        
            
            if (value is DateTime) {
                DateTime v = (DateTime) value;
                if (v <= DateTime.Now) {
                    return ValidationResult.Success;
                }
                else {
                    return new ValidationResult("Date must be in the past");
                }
            } 
            else {
                return new ValidationResult("Date must be a date object");
            }
            
        }

        }

}