using FitBoard.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitBoard.Api.Data;

public class FitBoardDbContext(DbContextOptions<FitBoardDbContext> options) : DbContext(options)
{
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
    public DbSet<ExerciseSet> ExerciseSets => Set<ExerciseSet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitBoardDbContext).Assembly);
    }
}
