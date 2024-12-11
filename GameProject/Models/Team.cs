namespace GameProject.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Relationship with characters
        public List<Character> Characters { get; set; } = new();
    }

}
