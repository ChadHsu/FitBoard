using FitBoard.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitBoard.Api.Data.Configurations;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.ToTable("Workouts", t => t.HasComment("訓練紀錄（一次完整訓練 session）"));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasComment("主鍵");

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("訓練標題，對應 CSV title，例如 Upper、Arm/Shoulder");

        builder.Property(x => x.StartTime)
            .HasComment("訓練開始時間，對應 CSV start_time");

        builder.Property(x => x.EndTime)
            .HasComment("訓練結束時間，對應 CSV end_time");

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .HasComment("訓練備註，對應 CSV description，可為空");

        builder.HasIndex(x => x.StartTime);

        builder.HasMany(x => x.Exercises)
            .WithOne(x => x.Workout)
            .HasForeignKey(x => x.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
