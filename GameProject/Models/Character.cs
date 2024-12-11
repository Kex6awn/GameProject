using System.ComponentModel.DataAnnotations;

namespace GameProject.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Health must be between 1 and 100.")]
        public int Health { get; set; }

        [Range(1, 100, ErrorMessage = "Strength must be between 1 and 100.")]
        public int Strength { get; set; }

        // Foreign key for Team
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }

}
