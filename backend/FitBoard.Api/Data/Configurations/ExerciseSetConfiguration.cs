using FitBoard.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitBoard.Api.Data.Configurations;

public class ExerciseSetConfiguration : IEntityTypeConfiguration<ExerciseSet>
{
    public void Configure(EntityTypeBuilder<ExerciseSet> builder)
    {
        builder.ToTable("ExerciseSets", t => t.HasComment("組數明細（單一 WorkoutExercise 下的每一組）"));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasComment("主鍵");

        builder.Property(x => x.WorkoutExerciseId)
            .HasComment("所屬訓練動作 FK → WorkoutExercises.Id");

        builder.Property(x => x.SetIndex)
            .HasComment("組序，對應 CSV set_index，同動作內從 0 起算");

        builder.Property(x => x.SetType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired()
            .HasComment("組類型，對應 CSV set_type：Warmup / Normal");

        builder.Property(x => x.WeightKg)
            .HasPrecision(8, 2)
            .HasComment("重量（公斤），對應 CSV weight_kg，可為空");

        builder.Property(x => x.Reps)
            .HasComment("次數，對應 CSV reps，可為空");

        builder.Property(x => x.DistanceKm)
            .HasPrecision(8, 3)
            .HasComment("距離（公里），對應 CSV distance_km，有氧等用途，可為空");

        builder.Property(x => x.DurationSeconds)
            .HasComment("持續秒數，對應 CSV duration_seconds，可為空");

        builder.Property(x => x.Rpe)
            .HasPrecision(4, 1)
            .HasComment("自覺強度 RPE（約 1–10），對應 CSV rpe，可為空");

        builder.HasIndex(x => new { x.WorkoutExerciseId, x.SetIndex })
            .IsUnique();
    }
}
