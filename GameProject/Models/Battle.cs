namespace GameProject.Models
{
    public class Battle
    {
        public int Id { get; set; }
        public int Team1Id { get; set; }
        public Team Team1 { get; set; }
        public int Team2Id { get; set; }
        public Team Team2 { get; set; }
        public int? WinnerTeamId { get; set; } // Null until the battle is decided
        public Team WinnerTeam { get; set; }
        public DateTime BattleDate { get; set; } = DateTime.Now;

    }
}
