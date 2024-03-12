using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace FBox.Entities;

public partial class TrenazerpilkarskiContext : DbContext
{
    public TrenazerpilkarskiContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    private readonly string _connectionString;
    public TrenazerpilkarskiContext(DbContextOptions<TrenazerpilkarskiContext> options): base(options) { }
    public virtual DbSet<Club> Clubs { get; set; }
    public virtual DbSet<Config> Configs { get; set; }
    public virtual DbSet<Exercisereport> Exercisereports { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Playerposition> Playerpositions { get; set; }
    public virtual DbSet<Playerteam> Playerteams { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<Sensorsconfig> Sensorsconfigs { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    public virtual DbSet<GameConfig> GameConfigs { get; set; }
    public virtual DbSet<GameRecord> GameRecords { get; set; }
    public virtual DbSet<GameRecordPlayer> Scores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseMySQL(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("club");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("config");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.ExerciseId).HasColumnName("exerciseId");
            entity.Property(e => e.GeneralConfig)
                .HasColumnType("mediumtext")
                .HasColumnName("generalConfig");
            entity.Property(e => e.IsAutosave).HasColumnName("isAutosave");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.ScenarioConfig)
                .HasColumnType("mediumtext")
                .HasColumnName("scenarioConfig");
        });

        modelBuilder.Entity<Exercisereport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("exercisereport");

            entity.HasIndex(e => e.PlayerId, "fk1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.ExerciseId).HasColumnName("exerciseId");
            entity.Property(e => e.PlayerId).HasColumnName("playerId");
            entity.Property(e => e.ReportBinary)
                .HasColumnType("blob")
                .HasColumnName("reportBinary");

            entity.HasOne(d => d.Player).WithMany(p => p.Exercisereports)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk1");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("player");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Playerposition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("playerposition");

            entity.HasIndex(e => e.PlayerId, "player3_idx");

            entity.HasIndex(e => e.PositionId, "position3_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlayerId).HasColumnName("playerId");
            entity.Property(e => e.PositionId).HasColumnName("positionId");

            entity.HasOne(d => d.Player).WithMany(p => p.Playerpositions)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("player3");

            entity.HasOne(d => d.Position).WithMany(p => p.Playerpositions)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("position3");
        });

        modelBuilder.Entity<Playerteam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("playerteam");

            entity.HasIndex(e => e.PlayerId, "player_idx");

            entity.HasIndex(e => e.TeamId, "team_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlayerId).HasColumnName("playerId");
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.Player).WithMany(p => p.Playerteams)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("player");

            entity.HasOne(d => d.Team).WithMany(p => p.Playerteams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("team");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("position");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sensorsconfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sensorsconfig");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasColumnType("mediumtext")
                .HasColumnName("value");
            entity.Property(e => e.WallNr).HasColumnName("wallNr");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("team");

            entity.HasIndex(e => e.ClubId, "club_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClubId).HasColumnName("clubId");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");

            entity.HasOne(d => d.Club).WithMany(p => p.Teams)
                .HasForeignKey(d => d.ClubId)
                .HasConstraintName("club");
        });
        modelBuilder.Entity<GameConfig>(entity =>
        {
            entity.HasIndex(e => e.Name);
        });
        modelBuilder.Entity<GameRecord>(entity =>
        {
            entity.HasOne(e => e.GameConfig).WithMany(g => g.GameRecords).HasForeignKey(d => d.GameConfigId).OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.GameConfigId);

        });
        modelBuilder.Entity<GameRecordPlayer>(entity =>
        {
            entity.HasOne(e=>e.GameRecord).WithMany(g=>g.Scores).HasForeignKey(d => d.GameRecordId).OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Player).WithMany(p => p.Scores).HasForeignKey(e => e.PlayerId).OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.PlayerId);
            entity.HasIndex(e => e.GameRecordId);

        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
