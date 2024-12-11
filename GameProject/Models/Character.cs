namespace GameProject.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }

        // Foreign key for Team
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }

}
