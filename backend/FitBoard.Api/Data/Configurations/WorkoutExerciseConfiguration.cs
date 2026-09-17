using FitBoard.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitBoard.Api.Data.Configurations;

public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.ToTable("WorkoutExercises", t => t.HasComment("單次訓練中的動作項目（Workout 與 Exercise 的中介）"));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasComment("主鍵");

        builder.Property(x => x.WorkoutId)
            .HasComment("所屬訓練 FK → Workouts.Id");

        builder.Property(x => x.ExerciseId)
            .HasComment("動作主檔 FK → Exercises.Id");

        builder.Property(x => x.SupersetId)
            .HasComment("超組編號，對應 CSV superset_id；同組動作共用同一值，可為空");

        builder.Property(x => x.Notes)
            .HasMaxLength(2000)
            .HasComment("此動作在該次訓練的備註，對應 CSV exercise_notes，可為空");

        builder.Property(x => x.SortOrder)
            .HasComment("此動作在該次訓練中的順序（由匯入順序或手動指定）");

        builder.HasIndex(x => new { x.WorkoutId, x.SortOrder });

        builder.HasOne(x => x.Exercise)
            .WithMany(x => x.WorkoutExercises)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Sets)
            .WithOne(x => x.WorkoutExercise)
            .HasForeignKey(x => x.WorkoutExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
