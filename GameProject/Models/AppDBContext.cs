using GameProject.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Existing DbSets
    public DbSet<Team> Teams { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Battle> Battles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurations for relationships
        modelBuilder.Entity<Team>()
            .HasMany(t => t.Characters)
            .WithOne(c => c.Team)
            .HasForeignKey(c => c.TeamId);

        modelBuilder.Entity<Battle>()
            .HasOne(b => b.Team1)
            .WithMany()
            .HasForeignKey(b => b.Team1Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Battle>()
            .HasOne(b => b.Team2)
            .WithMany()
            .HasForeignKey(b => b.Team2Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Battle>()
            .HasOne(b => b.WinnerTeam)
            .WithMany()
            .HasForeignKey(b => b.WinnerTeamId)
            .OnDelete(DeleteBehavior.Restrict);


        base.OnModelCreating(modelBuilder);

        // Seeding Teams
        modelBuilder.Entity<Team>().HasData(
            new Team { Id = 1, Name = "Team Alpha", Description = "Elite fighters" },
            new Team { Id = 2, Name = "Team Beta", Description = "Strategic masterminds" }
        );

        // Seeding Characters
        modelBuilder.Entity<Character>().HasData(
            new Character { Id = 1, Name = "Fighter A1", Health = 80, Strength = 90, TeamId = 1, Wins = 0 },
            new Character { Id = 2, Name = "Fighter A2", Health = 70, Strength = 85, TeamId = 1, Wins = 0 },
            new Character { Id = 3, Name = "Fighter B1", Health = 75, Strength = 88, TeamId = 2, Wins = 0 },
            new Character { Id = 4, Name = "Fighter B2", Health = 85, Strength = 80, TeamId = 2, Wins = 0 }
        );

        // Seeding Battles
        modelBuilder.Entity<Battle>().HasData(
            new Battle { Id = 1, Team1Id = 1, Team2Id = 2, WinnerTeamId = 1, BattleDate = DateTime.Now.AddDays(-1) },
            new Battle { Id = 2, Team1Id = 2, Team2Id = 1, WinnerTeamId = 2, BattleDate = DateTime.Now.AddDays(-2) }
        );
    }
}